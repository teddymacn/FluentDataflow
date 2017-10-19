using System.Threading.Tasks.Dataflow;
using System;

namespace FluentDataflow
{
    internal class MultipleSourceDataflowBuilder<TOutput> : IMultipleSourceDataflowBuilder<TOutput>
    {
        private readonly ISourceBlock<TOutput>[] _sourceBlocks;

        public ISourceBlock<TOutput>[] SourceBlocks => _sourceBlocks;

        public MultipleSourceDataflowBuilder(ISourceBlock<TOutput>[] sourceBlocks)
        {
            _sourceBlocks = sourceBlocks;
        }

        public ISourceDataflowBuilder<TOutput[]> Batch(int batchSize, DataflowBatchOptions batchOptions = default(DataflowBatchOptions))
        {
            var batchBlock = new BatchBlock<TOutput>(batchSize, batchOptions.BatchBlockOptions);
            var propagateCompletion = Link(batchBlock, batchOptions.LinkOptions, null);
            var sourceWrapper = new MultipleSourceDataflowWrapper(_sourceBlocks);
            return new SourceDataflowBuilder<TOutput[]>(sourceWrapper, sourceWrapper, batchBlock, propagateCompletion);
        }

        public ISourceDataflowBuilder<TOutput2> LinkToPropagator<TOutput2>(IPropagatorBlock<TOutput, TOutput2> propagatorBlock, DataflowLinkOptions linkOptions, Predicate<TOutput> predicate)
        {
            if (propagatorBlock == null) throw new ArgumentNullException("propagatorBlock");

            var propagateCompletion = Link(propagatorBlock, linkOptions, predicate);
            var sourceWrapper = new MultipleSourceDataflowWrapper(_sourceBlocks);
            return new SourceDataflowBuilder<TOutput2>(sourceWrapper, sourceWrapper, propagatorBlock, propagateCompletion);
        }

        public IDataflowBuilder LinkToTarget(ITargetBlock<TOutput> targetBlock, DataflowLinkOptions linkOptions, Predicate<TOutput> predicate)
        {
            if (targetBlock == null) throw new ArgumentNullException("targetBlock");

            var propagateCompletion = Link(targetBlock, linkOptions, predicate);
            var sourceWrapper = new MultipleSourceDataflowWrapper(_sourceBlocks);
            return new DataflowBuilder(sourceWrapper, sourceWrapper, targetBlock, propagateCompletion);
        }

        public ISourceDataflowBuilder<TOutput> WriteOnce(Func<TOutput, TOutput> cloningFunction, DataflowWriteOnceOptions writeOnceOptions = default(DataflowWriteOnceOptions))
        {
            if (cloningFunction == null) throw new ArgumentNullException("cloningFunction");

            var writeOnceBlock = new WriteOnceBlock<TOutput>(cloningFunction, writeOnceOptions.WriteOnceBlockOptions);
            var propagateCompletion = Link(writeOnceBlock, writeOnceOptions.LinkOptions, null);
            var sourceWrapper = new MultipleSourceDataflowWrapper(_sourceBlocks);
            return new SourceDataflowBuilder<TOutput>(sourceWrapper, sourceWrapper, writeOnceBlock, propagateCompletion);
        }

        internal bool Link(ITargetBlock<TOutput> targetBlock, DataflowLinkOptions linkOptions, Predicate<TOutput> predicate)
        {
            var options = linkOptions ?? DataflowDefaultOptions.DefaultLinkOptions;
            var propagateCompletion = options.PropagateCompletion;

            // when multiple source link to one target, 
            // to make the PropagateCompletion behavior be consistent with when only one source
            // we need to disable it in LinkTo and hack it in MultipleDataflowWrapper.
            // so here we simply return the original PropagateCompletion value
            options.PropagateCompletion = false;
            foreach (var sourceBlock in _sourceBlocks)
            {
                if (predicate == null)
                    sourceBlock.LinkTo(targetBlock, options);
                else
                    sourceBlock.LinkTo(targetBlock, options, predicate);
            }

            return propagateCompletion;
        }
    }
}
