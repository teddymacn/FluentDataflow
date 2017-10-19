using System;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace FluentDataflow
{
    internal class SourceDataflowWrapper<TOutput> : ISourceBlock<TOutput>
    {
        private readonly IDataflowBlock _originalSourceBlock;
        private readonly IDataflowBlock _currentSourceBlock;
        private readonly ISourceBlock<TOutput> _finalSourceBlock;
        private readonly bool? _propagateCompletion;

        public SourceDataflowWrapper(
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
            _originalSourceBlock.Complete();
        }

        public TOutput ConsumeMessage(DataflowMessageHeader messageHeader, ITargetBlock<TOutput> target, out bool messageConsumed)
        {
            return _finalSourceBlock.ConsumeMessage(messageHeader, target, out messageConsumed);
        }

        public void Fault(Exception exception)
        {
            _originalSourceBlock.Fault(exception);
        }

        public IDisposable LinkTo(ITargetBlock<TOutput> target, DataflowLinkOptions linkOptions)
        {
            return _finalSourceBlock.LinkTo(target, linkOptions);
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
