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

// each step of a dataflow could be either a dataflow block
// or an action/function delegate
var splitter = new Func<string, KeyValuePair<string, int>>(input =>
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

var splitter = ...
var aggregatorDataflow = .FromPropagator(splitter)
    .LinkToTarget(aggregater)
    .Create();

var dataflow = factory.FromPropagator<string, string>(line => line.Split(' '))
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

var dataflow = new DataflowFactory().FromBroadcast<string>()
    .LinkTo(printer1)
    .LinkTo(printer2)
    .LinkTo<string>(s => System.Console.WriteLine("Printer3: {0}", s))
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

var dataflow = new DataflowFactory().FromMultipleSources(source1, source2, source3)
    .LinkToTarget(s => System.Console.WriteLine(s))
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
// so even numbers go to printer
// and odd numbers go to declined printer

var filter = new Predicate<int>(i =>
{
    return i % 2 == 0;
});
var printer = new Action<int>(s => System.Console.WriteLine("printer: " + s.ToString()));
var declinedPrinter = new Action<int>(s => System.Console.WriteLine("declined: " + s.ToString()));
var inputBlock = new BufferBlock<int>();

var dataflow = new DataflowFactory().FromPropagator(inputBlock)
    .LinkToTarget(printer, DataflowDefaultOptions.DefaultBlockOptions, new DataflowLinkOptions { MaxMessages = -1, PropagateCompletion = true }, filter, declinedPrinter)
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
var printer = new Action<Tuple<string, string>>(s => System.Console.WriteLine("printer: {0},{1}", s.Item1, s.Item2));

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
var printer = new Action<IEnumerable<string>>(s => System.Console.WriteLine("printer: " +string.Join("|", s)));

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

