using System.Threading.Tasks.Dataflow;
using System;

namespace FluentDataflow
{
    internal class PropagatorDataflowBuilder<TInput, TOutput> : IPropagatorDataflowBuilder<TInput, TOutput>
    {
        private readonly ITargetBlock<TInput> _originalTargetBlock;
        private readonly IDataflowBlock _currentSourceBlock;
        private readonly ISourceBlock<TOutput> _finalSourceBlock;
        private readonly bool? _propagateCompletion;

        public ITargetBlock<TInput> OriginalTargetBlock => _originalTargetBlock;
        public IDataflowBlock CurrentSourceBlock => _currentSourceBlock;
        public ISourceBlock<TOutput> FinalSourceBlock => _finalSourceBlock;
        public bool? PropagateCompletion => _propagateCompletion;

        public PropagatorDataflowBuilder(IPropagatorBlock<TInput, TOutput> propagatorBlock)
            : this(propagatorBlock, propagatorBlock, propagatorBlock)
        {
        }

        public PropagatorDataflowBuilder(
            ITargetBlock<TInput> originalTargetBlock
            , IDataflowBlock currentSourceBlock
            , ISourceBlock<TOutput> finalSourceBlock
            , bool? propagateCompletion = null)
        {
            _originalTargetBlock = originalTargetBlock;
            _currentSourceBlock = currentSourceBlock;
            _finalSourceBlock = finalSourceBlock;
            _propagateCompletion = propagateCompletion;
        }

        public IPropagatorBlock<TInput, TOutput> Create()
        {
            if (ReferenceEquals(_originalTargetBlock, _finalSourceBlock))
                return _originalTargetBlock as IPropagatorBlock<TInput, TOutput>;
            if (ReferenceEquals(_originalTargetBlock, _currentSourceBlock))
                return DataflowBlock.Encapsulate(_originalTargetBlock, _finalSourceBlock);

            if (_finalSourceBlock is IReceivableSourceBlock<TOutput>)
                return new ReceivablePropagatorDataflowWrapper<TInput, TOutput>(_originalTargetBlock, _currentSourceBlock, _finalSourceBlock, _propagateCompletion);
            else
                return new PropagatorDataflowWrapper<TInput, TOutput>(_originalTargetBlock, _currentSourceBlock, _finalSourceBlock, _propagateCompletion);
        }

        public ITargetDataflowBuilder<TInput> LinkToTarget(ITargetBlock<TOutput> targetBlock, DataflowLinkOptions linkOptions, Predicate<TOutput> predicate)
        {
            if (targetBlock == null) throw new ArgumentNullException("targetBlock");

            LinkHelper.Link(_finalSourceBlock, targetBlock, linkOptions, predicate);
            return new TargetDataflowBuilder<TInput>(_originalTargetBlock, targetBlock);
        }

        public IPropagatorDataflowBuilder<TInput, TOutput2> LinkToPropagator<TOutput2>(IPropagatorBlock<TOutput, TOutput2> propagatorBlock, DataflowLinkOptions linkOptions, Predicate<TOutput> predicate)
        {
            if (propagatorBlock == null) throw new ArgumentNullException("propagatorBlock");

            LinkHelper.Link(_finalSourceBlock, propagatorBlock, linkOptions, predicate);
            return new PropagatorDataflowBuilder<TInput, TOutput2>(_originalTargetBlock, _finalSourceBlock, propagatorBlock, _propagateCompletion);
        }

        public IPropagatorDataflowBuilder<TInput, TOutput[]> Batch(int batchSize, DataflowBatchOptions batchOptions = default(DataflowBatchOptions))
        {
            var batchBlock = new BatchBlock<TOutput>(batchSize, batchOptions.BatchBlockOptions);
            LinkHelper.Link(_finalSourceBlock, batchBlock, batchOptions.LinkOptions);
            return new PropagatorDataflowBuilder<TInput, TOutput[]>(_originalTargetBlock, _finalSourceBlock, batchBlock, _propagateCompletion);
        }

        public IPropagatorDataflowBuilder<TInput, TOutput> WriteOnce(Func<TOutput, TOutput> cloningFunction, DataflowWriteOnceOptions writeOnceOptions = default(DataflowWriteOnceOptions))
        {
            if (cloningFunction == null) throw new ArgumentNullException("cloningFunction");

            var writeOnceBlock = new WriteOnceBlock<TOutput>(cloningFunction, writeOnceOptions.WriteOnceBlockOptions);
            LinkHelper.Link(_finalSourceBlock, writeOnceBlock, writeOnceOptions.LinkOptions);
            return new PropagatorDataflowBuilder<TInput, TOutput>(_originalTargetBlock, _finalSourceBlock, writeOnceBlock, _propagateCompletion);
        }
    }
}
