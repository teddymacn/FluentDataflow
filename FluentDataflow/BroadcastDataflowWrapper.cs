using System;
using System.Linq;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace FluentDataflow
{
    internal class BroadcastDataflowWrapper<T> : ITargetBlock<T>
    {
        private readonly ITargetBlock<T> _broadcastBlock;
        private readonly ITargetBlock<T>[] _targetBlocks;

        public BroadcastDataflowWrapper(BroadcastBlock<T> broadcastBlock, ITargetBlock<T>[] targetBlocks)
        {
            _broadcastBlock = broadcastBlock;
            _targetBlocks = targetBlocks;
        }

        public Task Completion
        {
            get
            {
                if (_targetBlocks == null || _targetBlocks.Length == 0) return _broadcastBlock.Completion;

                return _broadcastBlock.Completion.ContinueWith(task =>
                {
                    if (task.IsFaulted)
                    {
                        foreach (var targetBlock in _targetBlocks)
                        {
                            targetBlock.Fault(task.Exception);
                        }
                    }
                    else
                    {
                        foreach (var targetBlock in _targetBlocks)
                        {
                            targetBlock.Complete();
                        }
                    }

                    return Task.WhenAll(_targetBlocks.Select(b => b.Completion));
                });
            }
        }

        public void Complete()
        {
            _broadcastBlock.Complete();
        }

        public void Fault(Exception exception)
        {
            _broadcastBlock.Fault(exception);
        }

        public DataflowMessageStatus OfferMessage(DataflowMessageHeader messageHeader, T messageValue, ISourceBlock<T> source, bool consumeToAccept)
        {
            return _broadcastBlock.OfferMessage(messageHeader, messageValue, source, consumeToAccept);
        }
    }
}
