using System.Threading.Tasks.Dataflow;
using System;

namespace FluentDataflow
{
    internal class SourceDataflowBuilder<TOutput> : ISourceDataflowBuilder<TOutput>
    {
        private readonly IDataflowBlock _originalSourceBlock;
        private readonly IDataflowBlock _currentSourceBlock;
        private readonly ISourceBlock<TOutput> _finalSourceBlock;
        private readonly bool? _propagateCompletion;

        public IDataflowBlock OriginalSourceBlock => _originalSourceBlock;
        public IDataflowBlock CurrentSourceBlock => _currentSourceBlock;
        public ISourceBlock<TOutput> FinalSourceBlock => _finalSourceBlock;
        public bool? PropagateCompletion => _propagateCompletion;

        public SourceDataflowBuilder(ISourceBlock<TOutput> sourceBlock)
            : this(sourceBlock, sourceBlock, sourceBlock)
        {
        }

        public SourceDataflowBuilder(
            IDataflowBlock originalSourceBlock
            , IDataflowBlock currentSourceBlock
            , ISourceBlock<TOutput> finalSourceBlock
            , bool? propagateCompletion = null)
        {
            _originalSourceBlock = originalSourceBlock;
            _currentSourceBlock = currentSourceBlock;
            _finalSourceBlock = finalSourceBlock;
            _propagateCompletion = propagateCompletion;
        }

        public ISourceBlock<TOutput> Create()
        {
            if (ReferenceEquals(_originalSourceBlock, _finalSourceBlock)) return _finalSourceBlock;
            return new SourceDataflowWrapper<TOutput>(_originalSourceBlock, _currentSourceBlock, _finalSourceBlock, _propagateCompletion);
        }

        public IDataflowBuilder LinkToTarget(ITargetBlock<TOutput> targetBlock, DataflowLinkOptions linkOptions, Predicate<TOutput> predicate)
        {
            if (targetBlock == null) throw new ArgumentNullException("targetBlock");

            LinkHelper.Link(_finalSourceBlock, targetBlock, linkOptions, predicate);
            return new DataflowBuilder(_originalSourceBlock, _finalSourceBlock, targetBlock, _propagateCompletion);
        }

        public ISourceDataflowBuilder<TOutput2> LinkToPropagator<TOutput2>(IPropagatorBlock<TOutput, TOutput2> propagatorBlock, DataflowLinkOptions linkOptions, Predicate<TOutput> predicate)
        {
            if (propagatorBlock == null) throw new ArgumentNullException("propagatorBlock");

            LinkHelper.Link(_finalSourceBlock, propagatorBlock, linkOptions, predicate);
            return new SourceDataflowBuilder<TOutput2>(_originalSourceBlock, _finalSourceBlock, propagatorBlock, _propagateCompletion);
        }

        public ISourceDataflowBuilder<TOutput[]> Batch(int batchSize, DataflowBatchOptions batchOptions)
        {
            var batchBlock = new BatchBlock<TOutput>(batchSize, batchOptions.BatchBlockOptions);
            LinkHelper.Link(_finalSourceBlock, batchBlock, batchOptions.LinkOptions);
            return new SourceDataflowBuilder<TOutput[]>(_originalSourceBlock, _finalSourceBlock, batchBlock, _propagateCompletion);
        }

        public ISourceDataflowBuilder<TOutput> WriteOnce(Func<TOutput, TOutput> cloningFunction, DataflowWriteOnceOptions writeOnceOptions)
        {
            if (cloningFunction == null) throw new ArgumentNullException("cloningFunction");

            var writeOnceBlock = new WriteOnceBlock<TOutput>(cloningFunction, writeOnceOptions.WriteOnceBlockOptions);
            LinkHelper.Link(_finalSourceBlock, writeOnceBlock, writeOnceOptions.LinkOptions);
            return new SourceDataflowBuilder<TOutput>(_originalSourceBlock, _finalSourceBlock, writeOnceBlock, _propagateCompletion);
        }
    }
}
