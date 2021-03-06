FluentDataflow
==============

FluentDataflow is a fluent style wrapper and extension of TPL Dataflow.

License terms
-------------
FluentDataflow is released under the [MIT license](https://mit-license.org/).

Nuget Package
-------------

- [FluentDataflow](https://www.nuget.org/packages/FluentDataflow/)

Examples
--------
You can git clone this repo and run [the test app](https://github.com/teddymacn/FluentDataflow/tree/master/FluentDataflow.Tests.Console) for real examples.

- Simple aggregation example:

```
var splitter = new TransformBlock<string, KeyValuePair<string, int>>(input =>
{
    string[] splitted = input.Split('=');
    return new KeyValuePair<string, int>(splitted[0], int.Parse(splitted[1]));
});

var dict = new Dictionary<string, int>();

var aggregater = new ActionBlock<KeyValuePair<string, int>>(pair =>
{
    int oldValue;
    dict[pair.Key] = dict.TryGetValue(pair.Key, out oldValue) ? oldValue + pair.Value : pair.Value;
});

// the created the dataflow instance is also
// a strong-typed standard dataflow block
var dataflow = new DataflowFactory().FromPropagator(splitter)
    .LinkToTarget(aggregater)
    .Create();

dataflow.Post("a=1");
dataflow.Post("b=2");
dataflow.Post("a=5");
dataflow.Complete();

Task.WaitAll(dataflow.Completion);

System.Console.WriteLine("sum(a) = {0}", dict["a"]); //prints sum(a) = 6
```

- A dataflow as part of another dataflow example

```
var factory = new DataflowFactory();

var aggregater = ... // Build an aggregator dataflow
var splitter = new TransformManyBlock<string, string>(line => line.Split(' '));

var dataflow = factory.FromPropagator<string, string>(splitter)
    .LinkToTarget(aggregator)
    .Create();

dataflow.Post("a=1 b=2 a=5");
dataflow.Post("c=6 b=8");
dataflow.Complete();

Task.WaitAll(dataflow.Completion);

System.Console.WriteLine("sum(b) = {0}", result["b"]); //prints sum(b) = 10
```

- Broadcast example

```
var printer1 = new ActionBlock<string>(s => System.Console.WriteLine("Printer1: {0}", s));
var printer2 = new ActionBlock<string>(s => System.Console.WriteLine("Printer2: {0}", s));
var printer3 = new ActionBlock<string>(s => System.Console.WriteLine("Printer3: {0}", s));

var dataflow = new DataflowFactory().FromBroadcast<string>()
    .LinkTo(printer1)
    .LinkTo(printer2)
    .LinkTo(printer3)
    .Create();

dataflow.Post("first message");
dataflow.Post("second message");
dataflow.Post("third message");
dataflow.Complete();

Task.WaitAll(dataflow.Completion);
```

- Multiple sources example

```
// by default, with native TPL, if multiple sources link to the same target,
// if set PropagateCompletion=true,
// as long as one of the source complets, the target complets.
// the target will miss some of the messages from the other sources.

// BUT, with our dataflow factory here, when set PropagateCompletion=true,
// dataflow.Complete() internally waits for all the sources to complete,
// so the target is guaranteed to receive all the messages from all the sources

var source1 = new BufferBlock<string>();
var source2 = new BufferBlock<string>();
var source3 = new BufferBlock<string>();
var printer = new ActionBlock<string>(s => System.Console.WriteLine(s));

var dataflow = new DataflowFactory().FromMultipleSources(source1, source2, source3)
    .LinkToTarget(printer)
    .Create();

for (var i = 0; i < 3; ++i)
{
    var s = i.ToString();
    source1.Post(s);
    source2.Post(s);
    source3.Post(s);
}

dataflow.Complete();

Task.WaitAll(dataflow.Completion);
```

- Linking with filter example

```
// the filter only accepts even numbers,
// so odd numbers goes to declined printer
var filter = new Predicate<int>(i =>
{
    return i % 2 == 0;
});
var printer = new ActionBlock<int>(s => System.Console.WriteLine("printer: " + s.ToString()));
var declinedPrinter = new ActionBlock<int>(s => System.Console.WriteLine("declined: " + s.ToString()));
var inputBlock = new BufferBlock<int>();

var dataflow = new DataflowFactory().FromPropagator(inputBlock)
    .LinkToTarget(printer
        , filter
        // when linking with filter, you have to specify a declined block
        // otherwise, because there will be messages declined still in the queue,
        // the current block will not be able to COMPLETE (waits on its Completion will never return)
        , declinedPrinter)
    .Create();

for (int i = 0; i < 10; ++i)
{
    dataflow.Post(i);
}

dataflow.Complete();

Task.WaitAll(dataflow.Completion);
```
- Join example

```
var source1 = new BufferBlock<string>();
var source2 = new BufferBlock<string>();
var printer = new ActionBlock<Tuple<string, string>>(s => System.Console.WriteLine("printer: {0},{1}", s.Item1, s.Item2));

var dataflow = new DataflowFactory().Join(source1, source2)
    .LinkToTarget(printer)
    .Create();

for (var i = 0; i < 3; ++i)
{
    var s = i.ToString();
    source1.Post(s);
    source2.Post(s);
}

dataflow.Complete();

Task.WaitAll(dataflow.Completion);
```

- Batch example

```
var source = new BufferBlock<string>();
var printer = new ActionBlock<IEnumerable<string>>(s => System.Console.WriteLine("printer: " +string.Join("|", s)));

var dataflow = new DataflowFactory().FromSource(source)
    .Batch(2)
    .LinkToTarget(printer)
    .Create();

for (var i = 0; i < 6; ++i)
{
    var s = i.ToString();
    source.Post(s);
}

dataflow.Complete();

Task.WaitAll(dataflow.Completion);
```

- If-else branching and merging example

```
var factory = new DataflowFactory();
// the ifFilter only accepts even numbers,
// so odd numbers goes to elseBlock
var ifFilter = new Predicate<int>(i =>
{
    return i % 2 == 0;
});
var printer = new ActionBlock<int>(s => System.Console.WriteLine("printer: " + s.ToString()));
var inputBlock = new BufferBlock<int>();
// if meet ifFilter, convert to: i -> i * 10
var ifBlock = new TransformBlock<int, int>(i => i * 10);
// else, convert to: i -> i * 100
var elseBlock = new TransformBlock<int, int>(i => i * 100);

var branchingDataflow = factory.FromPropagator(inputBlock)
    .LinkToPropagator(ifBlock, ifFilter, elseBlock)
    .Create();

var mergeingDataflow = factory.FromMultipleSources(ifBlock, elseBlock)
    .LinkToTarget(printer)
    .Create();

//encapsulate branchingDataflow and mergeingDataflow
var dataflow = factory.EncapsulateTargetDataflow(branchingDataflow, mergeingDataflow);

for (int i = 0; i < 10; ++i)
{
    dataflow.Post(i);
}

dataflow.Complete();

Task.WaitAll(dataflow.Completion);
```

- Fluent dataflow options creation

```
var joinOptions = new DataflowJoinOptions(
    join => join.BoundedCapacity(11).EnsureOrdered().MaxNumberOfGroups(1)
    , target2: _ => _.Append().MaxMessages(1).PropagateCompletion());

var batchOptions = new DataflowBatchOptions(
    batch => batch.BoundedCapacity(2).Greedy().MaxNumberOfGroups(3)
    , link => link.MaxMessages(1).PropagateCompletion(false)
    );

var linkOptions = new DataflowLinkOptions().PropagateCompletion(false).MaxMessages(2);
```
