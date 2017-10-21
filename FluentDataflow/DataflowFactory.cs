using System;
using System.Threading.Tasks.Dataflow;
using System.Collections.Generic;
using System.Linq;

namespace FluentDataflow
{
    /// <summary>
    /// The implementation of <see cref="IDataflowFactory"/>.
    /// </summary>
    public class DataflowFactory : IDataflowFactory
    {
        /// <summary>
        /// Creates a dataflow builder from multiple source blocks, the output of all the sources are merged into the pipeline.
        /// </summary>
        /// <typeparam name="TOutput"></typeparam>
        /// <param name="sourceBlocks"></param>
        /// <returns></returns>
        public IMultipleSourceDataflowBuilder<TOutput> FromMultipleSources<TOutput>(params ISourceBlock<TOutput>[] sourceBlocks)
        {
            if (sourceBlocks == null || sourceBlocks.Length == 0) throw new ArgumentNullException("sourceBlocks");

            return new MultipleSourceDataflowBuilder<TOutput>(sourceBlocks);
        }

        /// <summary>
        /// Creates a dataflow block from a propagator block.
        /// </summary>
        /// <typeparam name="TInput"></typeparam>
        /// <typeparam name="TOutput"></typeparam>
        /// <param name="propagatorBlock"></param>
        /// <returns></returns>
        public IPropagatorDataflowBuilder<TInput, TOutput> FromPropagator<TInput, TOutput>(IPropagatorBlock<TInput, TOutput> propagatorBlock)
        {
            if (propagatorBlock == null) throw new ArgumentNullException("propagatorBlock");

            return new PropagatorDataflowBuilder<TInput, TOutput>(propagatorBlock);
        }

        /// <summary>
        /// Creates a dataflow builder from a source block.
        /// </summary>
        /// <typeparam name="TOutput"></typeparam>
        /// <param name="sourceBlock"></param>
        /// <returns></returns>
        public ISourceDataflowBuilder<TOutput> FromSource<TOutput>(ISourceBlock<TOutput> sourceBlock)
        {
            if (sourceBlock == null) throw new ArgumentNullException("sourceBlock");

            return new SourceDataflowBuilder<TOutput>(sourceBlock);
        }

        /// <summary>
        /// Creates a dataflow builder from a target block.
        /// </summary>
        /// <typeparam name="TInput"></typeparam>
        /// <param name="targetBlock"></param>
        /// <returns></returns>
        public ITargetDataflowBuilder<TInput> FromTarget<TInput>(ITargetBlock<TInput> targetBlock)
        {
            if (targetBlock == null) throw new ArgumentNullException("targetBlock");

            return new TargetDataflowBuilder<TInput>(targetBlock);
        }

        /// <summary>
        /// Creates a dataflow builder from a join of multiple source blocks.
        /// </summary>
        /// <typeparam name="TOutput1"></typeparam>
        /// <typeparam name="TOutput2"></typeparam>
        /// <param name="sourceBlock1"></param>
        /// <param name="sourceBlock2"></param>
        /// <param name="joinOptions"></param>
        /// <returns></returns>
        public ISourceDataflowBuilder<Tuple<TOutput1, TOutput2>> Join<TOutput1, TOutput2>(ISourceBlock<TOutput1> sourceBlock1, ISourceBlock<TOutput2> sourceBlock2, DataflowJoinOptions joinOptions = default(DataflowJoinOptions))
        {
            if (sourceBlock1 == null) throw new ArgumentNullException("sourceBlock1");
            if (sourceBlock2 == null) throw new ArgumentNullException("sourceBlock2");

            var joinBlock = new JoinBlock<TOutput1, TOutput2>(joinOptions.JoinBlockOptions);
            LinkHelper.Link(sourceBlock1, joinBlock.Target1, joinOptions.Target1LinkOptions);
            LinkHelper.Link(sourceBlock2, joinBlock.Target2, joinOptions.Target2LinkOptions);

            var multipleSourcesWrapper = new MultipleSourceDataflowWrapper(new IDataflowBlock[] { sourceBlock1, sourceBlock2 });
            var sourceWrapper = new SourceDataflowWrapper<Tuple<TOutput1, TOutput2>>(multipleSourcesWrapper, joinBlock, joinBlock);

            return FromSource(sourceWrapper);
        }

        /// <summary>
        /// Creates a dataflow builder from a join of multiple source blocks.
        /// </summary>
        /// <typeparam name="TOutput1"></typeparam>
        /// <typeparam name="TOutput2"></typeparam>
        /// <typeparam name="TOutput3"></typeparam>
        /// <param name="sourceBlock1"></param>
        /// <param name="sourceBlock2"></param>
        /// <param name="sourceBlock3"></param>
        /// <param name="joinOptions"></param>
        /// <returns></returns>
        public ISourceDataflowBuilder<Tuple<TOutput1, TOutput2, TOutput3>> Join<TOutput1, TOutput2, TOutput3>(ISourceBlock<TOutput1> sourceBlock1, ISourceBlock<TOutput2> sourceBlock2, ISourceBlock<TOutput3> sourceBlock3, DataflowJoinOptions joinOptions = default(DataflowJoinOptions))
        {
            if (sourceBlock1 == null) throw new ArgumentNullException("sourceBlock1");
            if (sourceBlock2 == null) throw new ArgumentNullException("sourceBlock2");
            if (sourceBlock3 == null) throw new ArgumentNullException("sourceBlock3");

            var joinBlock = new JoinBlock<TOutput1, TOutput2, TOutput3>(joinOptions.JoinBlockOptions);
            LinkHelper.Link(sourceBlock1, joinBlock.Target1, joinOptions.Target1LinkOptions);
            LinkHelper.Link(sourceBlock2, joinBlock.Target2, joinOptions.Target2LinkOptions);
            LinkHelper.Link(sourceBlock3, joinBlock.Target3, joinOptions.Target3LinkOptions);

            var multipleSourcesWrapper = new MultipleSourceDataflowWrapper(new IDataflowBlock[] { sourceBlock1, sourceBlock2, sourceBlock3 });
            var sourceWrapper = new SourceDataflowWrapper<Tuple<TOutput1, TOutput2, TOutput3>>(multipleSourcesWrapper, joinBlock, joinBlock);

            return FromSource(sourceWrapper);
        }

        /// <summary>
        /// Creates a dataflow builder from a batched join of multiple source blocks.
        /// </summary>
        /// <typeparam name="TOutput1"></typeparam>
        /// <typeparam name="TOutput2"></typeparam>
        /// <param name="sourceBlock1"></param>
        /// <param name="sourceBlock2"></param>
        /// <param name="batchSize"></param>
        /// <param name="joinOptions"></param>
        /// <returns></returns>
        public ISourceDataflowBuilder<Tuple<IList<TOutput1>, IList<TOutput2>>> BatchedJoin<TOutput1, TOutput2>(ISourceBlock<TOutput1> sourceBlock1, ISourceBlock<TOutput2> sourceBlock2, int batchSize, DataflowJoinOptions joinOptions = default(DataflowJoinOptions))
        {
            if (sourceBlock1 == null) throw new ArgumentNullException("sourceBlock1");
            if (sourceBlock2 == null) throw new ArgumentNullException("sourceBlock2");

            var batchedJoinBlock = new BatchedJoinBlock<TOutput1, TOutput2>(batchSize, joinOptions.JoinBlockOptions);
            LinkHelper.Link(sourceBlock1, batchedJoinBlock.Target1, joinOptions.Target1LinkOptions);
            LinkHelper.Link(sourceBlock2, batchedJoinBlock.Target2, joinOptions.Target2LinkOptions);

            var multipleSourcesWrapper = new MultipleSourceDataflowWrapper(new IDataflowBlock[] { sourceBlock1, sourceBlock2 });
            var sourceWrapper = new SourceDataflowWrapper<Tuple<IList<TOutput1>, IList<TOutput2>>>(multipleSourcesWrapper, batchedJoinBlock, batchedJoinBlock);

            return FromSource(sourceWrapper);
        }

        /// <summary>
        /// Creates a dataflow builder from a batched join of multiple source blocks.
        /// </summary>
        /// <typeparam name="TOutput1"></typeparam>
        /// <typeparam name="TOutput2"></typeparam>
        /// <typeparam name="TOutput3"></typeparam>
        /// <param name="sourceBlock1"></param>
        /// <param name="sourceBlock2"></param>
        /// <param name="sourceBlock3"></param>
        /// <param name="batchSize"></param>
        /// <param name="joinOptions"></param>
        /// <returns></returns>
        public ISourceDataflowBuilder<Tuple<IList<TOutput1>, IList<TOutput2>, IList<TOutput3>>> BatchedJoin<TOutput1, TOutput2, TOutput3>(ISourceBlock<TOutput1> sourceBlock1, ISourceBlock<TOutput2> sourceBlock2, ISourceBlock<TOutput3> sourceBlock3, int batchSize, DataflowJoinOptions joinOptions = default(DataflowJoinOptions))
        {
            if (sourceBlock1 == null) throw new ArgumentNullException("sourceBlock1");
            if (sourceBlock2 == null) throw new ArgumentNullException("sourceBlock2");
            if (sourceBlock3 == null) throw new ArgumentNullException("sourceBlock3");

            var batchedJoinBlock = new BatchedJoinBlock<TOutput1, TOutput2, TOutput3>(batchSize, joinOptions.JoinBlockOptions);
            LinkHelper.Link(sourceBlock1, batchedJoinBlock.Target1, joinOptions.Target1LinkOptions);
            LinkHelper.Link(sourceBlock2, batchedJoinBlock.Target2, joinOptions.Target2LinkOptions);
            LinkHelper.Link(sourceBlock3, batchedJoinBlock.Target3, joinOptions.Target3LinkOptions);

            var multipleSourcesWrapper = new MultipleSourceDataflowWrapper(new IDataflowBlock[] { sourceBlock1, sourceBlock2, sourceBlock3 });
            var sourceWrapper = new SourceDataflowWrapper<Tuple<IList<TOutput1>, IList<TOutput2>, IList<TOutput3>>>(multipleSourcesWrapper, batchedJoinBlock, batchedJoinBlock);

            return FromSource(sourceWrapper);
        }

        /// <summary>
        /// Createsa dataflow builder from a broadcast block.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="broadcastBlock"></param>
        /// <returns></returns>
        public IBroadcastDataflowBuilder<T> FromBroadcast<T>(BroadcastBlock<T> broadcastBlock)
        {
            if (broadcastBlock == null) throw new ArgumentNullException("broadcastBlock");

            return new BroadcastDataflowBuilder<T>(broadcastBlock);
        }

        /// <summary>
        /// Encapsulates custom dataflow block head and tail as one dataflow block.
        /// </summary>
        /// <param name="head"></param>
        /// <param name="tail"></param>
        /// <returns></returns>
        public IDataflowBlock EncapsulateDataflow(IDataflowBlock head, IDataflowBlock tail)
        {
            if (head == null) throw new ArgumentNullException("head");
            if (tail == null) throw new ArgumentNullException("tail");

            return new DataflowWrapper(head, head, tail);
        }

        /// <summary>
        /// Encapsulates custom dataflow block multiple heads and a tail as one dataflow block.
        /// </summary>
        /// <param name="multipleHeads"></param>
        /// <param name="tail"></param>
        /// <returns></returns>
        public IDataflowBlock EncapsulateDataflow(IEnumerable<IDataflowBlock> multipleHeads, IDataflowBlock tail)
        {
            if (multipleHeads == null || !multipleHeads.Any()) throw new ArgumentNullException("multipleHeads");
            if (tail == null) throw new ArgumentNullException("tail");

            return EncapsulateDataflow(new MultipleSourceDataflowWrapper(multipleHeads.ToArray()), tail);
        }

        /// <summary>
        /// Encapsulates custom dataflow block head and tail as one dataflow block.
        /// </summary>
        /// <typeparam name="TOutput"></typeparam>
        /// <param name="head"></param>
        /// <param name="tail"></param>
        /// <returns></returns>
        public ISourceBlock<TOutput> EncapsulateSourceDataflow<TOutput>(IDataflowBlock head, ISourceBlock<TOutput> tail)
        {
            if (head == null) throw new ArgumentNullException("head");
            if (tail == null) throw new ArgumentNullException("tail");

            return new SourceDataflowWrapper<TOutput>(head, head, tail);
        }

        /// <summary>
        /// Encapsulates custom dataflow block multiple heads and a tail as one dataflow block.
        /// </summary>
        /// <typeparam name="TOutput"></typeparam>
        /// <param name="multipleHeads"></param>
        /// <param name="tail"></param>
        /// <returns></returns>
        public ISourceBlock<TOutput> EncapsulateSourceDataflow<TOutput>(IEnumerable<IDataflowBlock> multipleHeads, ISourceBlock<TOutput> tail)
        {
            if (multipleHeads == null || !multipleHeads.Any()) throw new ArgumentNullException("multipleHeads");
            if (tail == null) throw new ArgumentNullException("tail");

            return EncapsulateSourceDataflow(new MultipleSourceDataflowWrapper(multipleHeads.ToArray()), tail);
        }

        /// <summary>
        /// Encapsulates custom dataflow block head and tail as one dataflow block.
        /// </summary>
        /// <typeparam name="TInput"></typeparam>
        /// <param name="head"></param>
        /// <param name="tail"></param>
        /// <returns></returns>
        public ITargetBlock<TInput> EncapsulateTargetDataflow<TInput>(ITargetBlock<TInput> head, IDataflowBlock tail)
        {
            if (head == null) throw new ArgumentNullException("head");
            if (tail == null) throw new ArgumentNullException("tail");

            return new TargetDataflowWrapper<TInput>(head, tail);
        }

        /// <summary>
        /// Encapsulates custom dataflow block head and tail as one dataflow block.
        /// </summary>
        /// <typeparam name="TInput"></typeparam>
        /// <typeparam name="TOutput"></typeparam>
        /// <param name="head"></param>
        /// <param name="tail"></param>
        /// <returns></returns>
        public IPropagatorBlock<TInput, TOutput> EncapsulatePropagatorDataflow<TInput, TOutput>(ITargetBlock<TInput> head, ISourceBlock<TOutput> tail)
        {
            if (head == null) throw new ArgumentNullException("head");
            if (tail == null) throw new ArgumentNullException("tail");

            return DataflowBlock.Encapsulate(head, tail);
        }
    }
}
