using System;
using System.Collections.Generic;
using System.Threading.Tasks.Dataflow;

namespace FluentDataflow
{
    internal class ReceivablePropagatorDataflowWrapper<TInput, TOutput> : PropagatorDataflowWrapper<TInput, TOutput>, IReceivableSourceBlock<TOutput>
    {
        private readonly IReceivableSourceBlock<TOutput> _receivableSourceBlock;

        public ReceivablePropagatorDataflowWrapper(ITargetBlock<TInput> originalTargetBlock
            , IDataflowBlock currentSourceBlock
            , ISourceBlock<TOutput> finalSourceBlock
            , bool? propagateCompletion = null)
            : base(originalTargetBlock, currentSourceBlock, finalSourceBlock, propagateCompletion)
        {
            _receivableSourceBlock = finalSourceBlock as IReceivableSourceBlock<TOutput>;
        }

        public bool TryReceive(Predicate<TOutput> filter, out TOutput item)
        {
            return _receivableSourceBlock.TryReceive(filter, out item);
        }

        public bool TryReceiveAll(out IList<TOutput> items)
        {
            return _receivableSourceBlock.TryReceiveAll(out items);
        }
    }
}
