using System;
using System.Collections.Generic;
using System.Threading.Tasks.Dataflow;

namespace FluentDataflow
{
    internal class ReceivableSourceDataflowWrapper<TOutput> : SourceDataflowWrapper<TOutput>, IReceivableSourceBlock<TOutput>
    {
        private readonly IReceivableSourceBlock<TOutput> _receivableSourceBlock;

        public ReceivableSourceDataflowWrapper(
            IDataflowBlock originalSourceBlock
            , IDataflowBlock currentSourceBlock
            , ISourceBlock<TOutput> finalSourceBlock
            , bool? propagateCompletion = null)
            : base(originalSourceBlock, currentSourceBlock, finalSourceBlock, propagateCompletion)
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
