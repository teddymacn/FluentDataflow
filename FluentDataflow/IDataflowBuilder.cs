using FluentDataflow;
using System;
using System.Threading.Tasks.Dataflow;

namespace FluentDataflow
{
    /// <summary>
    /// Represents a dataflow builder to create a dataflow with no input and no output.
    /// </summary>
    public interface IDataflowBuilder
    {
        /// <summary>
        /// Creates a dataflow from builder.
        /// </summary>
        /// <returns></returns>
        IDataflowBlock Create();
    }

    /// <summary>
    /// Represents a dataflow builder to create a dataflow with no input but with output.
    /// </summary>
    /// <typeparam name="TOutput"></typeparam>
    public interface ISourceDataflowBuilder<TOutput>
    {
        /// <summary>
        /// Links to a target block.
        /// </summary>
        /// <param name="targetBlock"></param>
        /// <param name="linkOptions"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        IDataflowBuilder LinkToTarget(ITargetBlock<TOutput> targetBlock, DataflowLinkOptions linkOptions = null, Predicate<TOutput> predicate = null);

        /// <summary>
        /// Links to a propagator block.
        /// </summary>
        /// <typeparam name="TOutput2"></typeparam>
        /// <param name="propagatorBlock"></param>
        /// <param name="linkOptions"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        ISourceDataflowBuilder<TOutput2> LinkToPropagator<TOutput2>(IPropagatorBlock<TOutput, TOutput2> propagatorBlock, DataflowLinkOptions linkOptions = null, Predicate<TOutput> predicate = null);

        /// <summary>
        /// Creates a dataflow from builder.
        /// </summary>
        /// <returns></returns>
        ISourceBlock<TOutput> Create();

        /// <summary>
        /// Batches the current pipeline.
        /// </summary>
        /// <param name="batchSize"></param>
        /// <param name="batchOptions"></param>
        /// <returns></returns>
        ISourceDataflowBuilder<TOutput[]> Batch(int batchSize, DataflowBatchOptions batchOptions = default(DataflowBatchOptions));

        /// <summary>
        /// Ensures write once on the current pipeline.
        /// </summary>
        /// <param name="cloningFunction"></param>
        /// <param name="writeOnceOptions"></param>
        /// <returns></returns>
        ISourceDataflowBuilder<TOutput> WriteOnce(Func<TOutput, TOutput> cloningFunction, DataflowWriteOnceOptions writeOnceOptions = default(DataflowWriteOnceOptions));
    }

    /// <summary>
    /// Represents a dataflow builder to create a dataflow with input but with no output.
    /// </summary>
    /// <typeparam name="TInput"></typeparam>
    public interface ITargetDataflowBuilder<TInput>
    {
        /// <summary>
        /// Creates a dataflow from builder.
        /// </summary>
        /// <returns></returns>
        ITargetBlock<TInput> Create();
    }

    /// <summary>
    /// Represents a dataflow builder to create a dataflow with both input and output.
    /// </summary>
    /// <typeparam name="TInput"></typeparam>
    /// <typeparam name="TOutput"></typeparam>
    public interface IPropagatorDataflowBuilder<TInput, TOutput>
    {
        /// <summary>
        /// Links to a target block.
        /// </summary>
        /// <param name="targetBlock"></param>
        /// <param name="linkOptions"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        ITargetDataflowBuilder<TInput> LinkToTarget(ITargetBlock<TOutput> targetBlock, DataflowLinkOptions linkOptions = null, Predicate<TOutput> predicate = null);

        /// <summary>
        /// Links to a propagator block.
        /// </summary>
        /// <typeparam name="TOutput2"></typeparam>
        /// <param name="propagatorBlock"></param>
        /// <param name="linkOptions"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        IPropagatorDataflowBuilder<TInput, TOutput2> LinkToPropagator<TOutput2>(IPropagatorBlock<TOutput, TOutput2> propagatorBlock, DataflowLinkOptions linkOptions = null, Predicate<TOutput> predicate = null);

        /// <summary>
        /// Creates a data flow from current builder.
        /// </summary>
        /// <returns></returns>
        IPropagatorBlock<TInput, TOutput> Create();

        /// <summary>
        /// Batches the current pipeline.
        /// </summary>
        /// <param name="batchSize"></param>
        /// <param name="batchOptions"></param>
        /// <returns></returns>
        IPropagatorDataflowBuilder<TInput, TOutput[]> Batch(int batchSize, DataflowBatchOptions batchOptions = default(DataflowBatchOptions));

        /// <summary>
        /// Ensures write once on the current pipeline.
        /// </summary>
        /// <param name="cloningFunction"></param>
        /// <param name="writeOnceOptions"></param>
        /// <returns></returns>
        IPropagatorDataflowBuilder<TInput, TOutput> WriteOnce(Func<TOutput, TOutput> cloningFunction, DataflowWriteOnceOptions writeOnceOptions = default(DataflowWriteOnceOptions));
    }

    /// <summary>
    /// Represents a dataflow builder to create a dataflow with multiple outputs and no input.
    /// </summary>
    /// <typeparam name="TOutput"></typeparam>
    public interface IMultipleSourceDataflowBuilder<TOutput>
    {
        /// <summary>
        /// Links to a target block.
        /// </summary>
        /// <param name="targetBlock"></param>
        /// <param name="linkOptions"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        IDataflowBuilder LinkToTarget(ITargetBlock<TOutput> targetBlock, DataflowLinkOptions linkOptions = null, Predicate<TOutput> predicate = null);

        /// <summary>
        /// Links to a propagator block.
        /// </summary>
        /// <typeparam name="TOutput2"></typeparam>
        /// <param name="propagatorBlock"></param>
        /// <param name="linkOptions"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        ISourceDataflowBuilder<TOutput2> LinkToPropagator<TOutput2>(IPropagatorBlock<TOutput, TOutput2> propagatorBlock, DataflowLinkOptions linkOptions = null, Predicate<TOutput> predicate = null);

        /// <summary>
        /// Batches the current pipeline.
        /// </summary>
        /// <param name="batchSize"></param>
        /// <param name="batchOptions"></param>
        /// <returns></returns>
        ISourceDataflowBuilder<TOutput[]> Batch(int batchSize, DataflowBatchOptions batchOptions = default(DataflowBatchOptions));

        /// <summary>
        /// Ensures write once on the current pipeline.
        /// </summary>
        /// <param name="cloningFunction"></param>
        /// <param name="writeOnceOptions"></param>
        /// <returns></returns>
        ISourceDataflowBuilder<TOutput> WriteOnce(Func<TOutput, TOutput> cloningFunction, DataflowWriteOnceOptions writeOnceOptions = default(DataflowWriteOnceOptions));
    }

    /// <summary>
    /// Represents a dataflow builder to create a dataflow with a broadcast input and multiple outputs.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IBroadcastDataflowBuilder<T>
    {
        /// <summary>
        /// Links to a target block.
        /// </summary>
        /// <param name="targetBlock"></param>
        /// <param name="linkOptions"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        IBroadcastDataflowBuilder<T> LinkTo(ITargetBlock<T> targetBlock, DataflowLinkOptions linkOptions = null, Predicate<T> predicate = null);

        /// <summary>
        /// Creates a dataflow from builder.
        /// </summary>
        /// <returns></returns>
        ITargetBlock<T> Create();
    }
}
