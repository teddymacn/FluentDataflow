using System;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace FluentDataflow
{
    internal class TargetDataflowWrapper<TInput> : ITargetBlock<TInput>
    {
        private readonly ITargetBlock<TInput> _originalTargetBlock;
        private readonly IDataflowBlock _finalTargetBlock;

        public TargetDataflowWrapper(ITargetBlock<TInput> originalTargetBlock, IDataflowBlock finalTargetBlock)
        {
            _originalTargetBlock = originalTargetBlock;
            _finalTargetBlock = finalTargetBlock;
        }

        public Task Completion => _finalTargetBlock.Completion;

        public void Complete()
        {
            _originalTargetBlock.Complete();
        }

        public void Fault(Exception exception)
        {
            _originalTargetBlock.Fault(exception);
        }

        public DataflowMessageStatus OfferMessage(DataflowMessageHeader messageHeader, TInput messageValue, ISourceBlock<TInput> source, bool consumeToAccept)
        {
            return _originalTargetBlock.OfferMessage(messageHeader, messageValue, source, consumeToAccept);
        }
    }
}
