using System;
using System.Collections.Generic;
using System.Threading.Tasks.Dataflow;

namespace FluentDataflow
{
    /// <summary>
    /// Represents a dataflow factory.
    /// </summary>
    public interface IDataflowFactory
    {
        /// <summary>
        /// Creates a dataflow builder from a source block.
        /// </summary>
        /// <typeparam name="TOutput"></typeparam>
        /// <param name="sourceBlock"></param>
        /// <returns></returns>
        ISourceDataflowBuilder<TOutput> FromSource<TOutput>(ISourceBlock<TOutput> sourceBlock);

        /// <summary>
        /// Creates a dataflow builder from a target block.
        /// </summary>
        /// <typeparam name="TInput"></typeparam>
        /// <param name="targetBlock"></param>
        /// <returns></returns>
        ITargetDataflowBuilder<TInput> FromTarget<TInput>(ITargetBlock<TInput> targetBlock);

        /// <summary>
        /// Creates a dataflow block from a propagator block.
        /// </summary>
        /// <typeparam name="TInput"></typeparam>
        /// <typeparam name="TOutput"></typeparam>
        /// <param name="propagatorBlock"></param>
        /// <returns></returns>
        IPropagatorDataflowBuilder<TInput, TOutput> FromPropagator<TInput, TOutput>(IPropagatorBlock<TInput, TOutput> propagatorBlock);

        /// <summary>
        /// Creates a dataflow builder from multiple source blocks, the output of all the sources are merged into the pipeline.
        /// </summary>
        /// <typeparam name="TOutput"></typeparam>
        /// <param name="sourceBlocks"></param>
        /// <returns></returns>
        IMultipleSourceDataflowBuilder<TOutput> FromMultipleSources<TOutput>(params ISourceBlock<TOutput>[] sourceBlocks);

        /// <summary>
        /// Creates a dataflow builder from a join of multiple source blocks.
        /// </summary>
        /// <typeparam name="TOutput1"></typeparam>
        /// <typeparam name="TOutput2"></typeparam>
        /// <param name="sourceBlock1"></param>
        /// <param name="sourceBlock2"></param>
        /// <param name="joinOptions"></param>
        /// <returns></returns>
        ISourceDataflowBuilder<Tuple<TOutput1, TOutput2>> Join<TOutput1, TOutput2>(ISourceBlock<TOutput1> sourceBlock1, ISourceBlock<TOutput2> sourceBlock2, DataflowJoinOptions joinOptions = default(DataflowJoinOptions));

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
        ISourceDataflowBuilder<Tuple<TOutput1, TOutput2, TOutput3>> Join<TOutput1, TOutput2, TOutput3>(ISourceBlock<TOutput1> sourceBlock1, ISourceBlock<TOutput2> sourceBlock2, ISourceBlock<TOutput3> sourceBlock3, DataflowJoinOptions joinOptions = default(DataflowJoinOptions));

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
        ISourceDataflowBuilder<Tuple<IList<TOutput1>, IList<TOutput2>>> BatchedJoin<TOutput1, TOutput2>(ISourceBlock<TOutput1> sourceBlock1, ISourceBlock<TOutput2> sourceBlock2, int batchSize, DataflowJoinOptions joinOptions = default(DataflowJoinOptions));

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
        ISourceDataflowBuilder<Tuple<IList<TOutput1>, IList<TOutput2>, IList<TOutput3>>> BatchedJoin<TOutput1, TOutput2, TOutput3>(ISourceBlock<TOutput1> sourceBlock1, ISourceBlock<TOutput2> sourceBlock2, ISourceBlock<TOutput3> sourceBlock3, int batchSize, DataflowJoinOptions joinOptions = default(DataflowJoinOptions));

        /// <summary>
        /// Createsa dataflow builder from a broadcast block.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="broadcastBlock"></param>
        /// <returns></returns>
        IBroadcastDataflowBuilder<T> FromBroadcast<T>(BroadcastBlock<T> broadcastBlock);

        /// <summary>
        /// Encapsulates custom dataflow block head and tail as one dataflow block.
        /// </summary>
        /// <param name="head"></param>
        /// <param name="tail"></param>
        /// <returns></returns>
        IDataflowBlock EncapsulateDataflow(IDataflowBlock head, IDataflowBlock tail);

        /// <summary>
        /// Encapsulates custom dataflow block multiple heads and a tail as one dataflow block.
        /// </summary>
        /// <param name="multipleHeads"></param>
        /// <param name="tail"></param>
        /// <returns></returns>
        IDataflowBlock EncapsulateDataflow(IEnumerable<IDataflowBlock> multipleHeads, IDataflowBlock tail);

        /// <summary>
        /// Encapsulates custom dataflow block head and tail as one dataflow block.
        /// </summary>
        /// <typeparam name="TOutput"></typeparam>
        /// <param name="head"></param>
        /// <param name="tail"></param>
        /// <returns></returns>
        ISourceBlock<TOutput> EncapsulateSourceDataflow<TOutput>(IDataflowBlock head, ISourceBlock<TOutput> tail);

        /// <summary>
        /// Encapsulates custom dataflow block multiple heads and a tail as one dataflow block.
        /// </summary>
        /// <typeparam name="TOutput"></typeparam>
        /// <param name="multipleHeads"></param>
        /// <param name="tail"></param>
        /// <returns></returns>
        ISourceBlock<TOutput> EncapsulateSourceDataflow<TOutput>(IEnumerable<IDataflowBlock> multipleHeads, ISourceBlock<TOutput> tail);

        /// <summary>
        /// Encapsulates custom dataflow block head and tail as one dataflow block.
        /// </summary>
        /// <typeparam name="TInput"></typeparam>
        /// <param name="head"></param>
        /// <param name="tail"></param>
        /// <returns></returns>
        ITargetBlock<TInput> EncapsulateTargetDataflow<TInput>(ITargetBlock<TInput> head, IDataflowBlock tail);

        /// <summary>
        /// Encapsulates custom dataflow block head and tail as one dataflow block.
        /// </summary>
        /// <typeparam name="TInput"></typeparam>
        /// <typeparam name="TOutput"></typeparam>
        /// <param name="head"></param>
        /// <param name="tail"></param>
        /// <returns></returns>
        IPropagatorBlock<TInput, TOutput> EncapsulatePropagatorDataflow<TInput, TOutput>(ITargetBlock<TInput> head, ISourceBlock<TOutput> tail);
    }
}
