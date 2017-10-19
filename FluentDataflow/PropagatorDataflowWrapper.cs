using System;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace FluentDataflow
{
    internal class PropagatorDataflowWrapper<TInput, TOutput> : IPropagatorBlock<TInput, TOutput>
    {
        private readonly ITargetBlock<TInput> _originalTargetBlock;
        private readonly IDataflowBlock _currentSourceBlock;
        private readonly ISourceBlock<TOutput> _finalSourceBlock;
        private readonly bool? _propagateCompletion;

        public PropagatorDataflowWrapper(ITargetBlock<TInput> originalTargetBlock
            , IDataflowBlock currentSourceBlock
            , ISourceBlock<TOutput> finalSourceBlock
            , bool? propagateCompletion = null)
        {
            _originalTargetBlock = originalTargetBlock;
            _currentSourceBlock = currentSourceBlock;
            _finalSourceBlock = finalSourceBlock;
            _propagateCompletion = propagateCompletion;
        }

        public Task Completion
        {
            get
            {
                if (_propagateCompletion.GetValueOrDefault())
                {
                    return _currentSourceBlock.Completion.ContinueWith(task =>
                    {
                        if (task.IsFaulted)
                            _finalSourceBlock.Fault(task.Exception);
                        else
                            _finalSourceBlock.Complete();

                        return _finalSourceBlock.Completion;
                    });
                }

                return _finalSourceBlock.Completion;
            }
        }

        public void Complete()
        {
            _originalTargetBlock.Complete();
        }

        public TOutput ConsumeMessage(DataflowMessageHeader messageHeader, ITargetBlock<TOutput> target, out bool messageConsumed)
        {
            return _finalSourceBlock.ConsumeMessage(messageHeader, target, out messageConsumed);
        }

        public void Fault(Exception exception)
        {
            _originalTargetBlock.Fault(exception);
        }

        public IDisposable LinkTo(ITargetBlock<TOutput> target, DataflowLinkOptions linkOptions)
        {
            return _finalSourceBlock.LinkTo(target, linkOptions);
        }

        public DataflowMessageStatus OfferMessage(DataflowMessageHeader messageHeader, TInput messageValue, ISourceBlock<TInput> source, bool consumeToAccept)
        {
            return _originalTargetBlock.OfferMessage(messageHeader, messageValue, source, consumeToAccept);
        }

        public void ReleaseReservation(DataflowMessageHeader messageHeader, ITargetBlock<TOutput> target)
        {
            _finalSourceBlock.ReleaseReservation(messageHeader, target);
        }

        public bool ReserveMessage(DataflowMessageHeader messageHeader, ITargetBlock<TOutput> target)
        {
            return _finalSourceBlock.ReserveMessage(messageHeader, target);
        }
    }
}
