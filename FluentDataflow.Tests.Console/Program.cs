﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace FluentDataflow.Tests.Console
{
    class Program
    {
        private static readonly IDataflowFactory _factory = new DataflowFactory();

        static void Main(string[] args)
        {
            TestIfElseBranchingAndMerging();
            System.Console.ReadLine();
        }

        private static ITargetBlock<string> GetAggregatorFlow(out Dictionary<string, int> result)
        {
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

            var dataflow = _factory.FromPropagator(splitter)
                .LinkToTarget(aggregater)
                .Create();

            result = dict;
            return dataflow;
        }

        private static void TestAggregatorFlow()
        {
            var dataflow = GetAggregatorFlow(out Dictionary<string, int> result);
            dataflow.Post("a=1");
            dataflow.Post("b=2");
            dataflow.Post("a=5");
            dataflow.Complete();

            Task.WaitAll(dataflow.Completion);

            System.Console.WriteLine("sum(a) = {0}", result["a"]); //prints sum(a) = 6
        }

        private static ITargetBlock<string> GetLineAggregatorFlow(out Dictionary<string, int> result)
        {
            var aggregator = GetAggregatorFlow(out result);
            var splitter = new TransformManyBlock<string, string>(line => line.Split(' '));
            var dataflow = _factory.FromPropagator(splitter)
                .LinkToTarget(aggregator)
                .Create();

            return dataflow;
        }

        private static void TestLineAggregatorFlow()
        {
            var dataflow = GetLineAggregatorFlow(out Dictionary<string, int> result);
            dataflow.Post("a=1 b=2 a=5");
            dataflow.Post("c=6 b=8");
            dataflow.Complete();

            Task.WaitAll(dataflow.Completion);

            System.Console.WriteLine("sum(b) = {0}", result["b"]); //prints sum(b) = 10
        }

        private static void TestAggregatorFlowOnError()
        {
            var dataflow = GetAggregatorFlow(out Dictionary<string, int> result);
            dataflow.Post("a=1");
            dataflow.Post("b=2");
            dataflow.Post("a=badstring");
            dataflow.Complete();

            Task.WaitAll(dataflow.Completion);
        }

        private static ITargetBlock<string> GetBroadcastFlow()
        {
            var printer1 = new ActionBlock<string>(s => System.Console.WriteLine("Printer1: {0}", s));
            var printer2 = new ActionBlock<string>(s => System.Console.WriteLine("Printer2: {0}", s));
            var printer3 = new ActionBlock<string>(s => System.Console.WriteLine("Printer3: {0}", s));

            var dataflow = _factory.FromBroadcast<string>()
                .LinkTo(printer1)
                .LinkTo(printer2)
                .LinkTo(printer3)
                .Create();

            return dataflow;
        }

        private static void TestBroadcastFlow()
        {
            var dataflow = GetBroadcastFlow();
            dataflow.Post("first message");
            dataflow.Post("second message");
            dataflow.Post("third message");
            dataflow.Complete();

            Task.WaitAll(dataflow.Completion);
        }

        private static void TestMultipleSourcesFlow()
        {
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

            var dataflow = _factory.FromMultipleSources(source1, source2, source3)
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
        }

        private static void TestDataflowLinkWithFilter()
        {
            // the filter only accepts even numbers,
            // so odd numbers goes to declined printer
            var filter = new Predicate<int>(i =>
            {
                return i % 2 == 0;
            });
            var printer = new ActionBlock<int>(s => System.Console.WriteLine("printer: " + s.ToString()));
            var declinedPrinter = new ActionBlock<int>(s => System.Console.WriteLine("declined: " + s.ToString()));
            var inputBlock = new BufferBlock<int>();

            var dataflow = _factory.FromPropagator(inputBlock)
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
        }

        private static void TestBatch()
        {
            var source = new BufferBlock<string>();
            var printer = new ActionBlock<IEnumerable<string>>(s => System.Console.WriteLine("printer: " +string.Join("|", s)));

            var dataflow = _factory.FromSource(source)
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
        }

        private static void TestJoin()
        {
            var source1 = new BufferBlock<string>();
            var source2 = new BufferBlock<string>();
            var printer = new ActionBlock<Tuple<string, string>>(s => System.Console.WriteLine("printer: {0},{1}", s.Item1, s.Item2));

            var dataflow = _factory.Join(source1, source2)
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
        }

        private static void TestBatchedJoin()
        {
            var source1 = new BufferBlock<string>();
            var source2 = new BufferBlock<string>();
            var printer = new ActionBlock<Tuple<IList<string>, IList<string>>>(s => System.Console.WriteLine("printer: {0},{1}", string.Join("|", s.Item1), string.Join("|", s.Item2)));

            var dataflow = _factory.BatchedJoin(source1, source2, 2)
                .LinkToTarget(printer)
                .Create();

            for (var i = 0; i < 4; ++i)
            {
                var s = i.ToString();
                source1.Post(s);
                source2.Post(s);
            }

            dataflow.Complete();

            Task.WaitAll(dataflow.Completion);
        }

        private static void TestIfElseBranchingAndMerging()
        {
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

            var branchingDataflow = _factory.FromPropagator(inputBlock)
                .LinkToPropagator(ifBlock, ifFilter, elseBlock)
                .Create();

            var mergeingDataflow = _factory.FromMultipleSources(ifBlock, elseBlock)
                .LinkToTarget(printer)
                .Create();

            //encapsulate branchingDataflow and mergeingDataflow
            var dataflow = _factory.EncapsulateTargetDataflow(branchingDataflow, mergeingDataflow);

            for (int i = 0; i < 10; ++i)
            {
                dataflow.Post(i);
            }

            dataflow.Complete();

            Task.WaitAll(dataflow.Completion);
        }

        private static void TestFluentOptions()
        {
            var joinOptions = new DataflowJoinOptions(
                join => join.BoundedCapacity(11).EnsureOrdered().MaxNumberOfGroups(1)
                , target2: _ => _.Append().MaxMessages(1).PropagateCompletion());

            var batchOptions = new DataflowBatchOptions(
                batch => batch.BoundedCapacity(2).Greedy().MaxNumberOfGroups(3)
                , link => link.MaxMessages(1).PropagateCompletion(false)
                );

            var linkOptions = new DataflowLinkOptions().PropagateCompletion(false).MaxMessages(2);
        }
    }
}
