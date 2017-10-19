using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks.Dataflow;

namespace FluentDataflow
{
    internal class BroadcastDataflowBuilder<T> : IBroadcastDataflowBuilder<T>
    {
        private readonly BroadcastBlock<T> _broadcastBlock;
        private readonly IList<ITargetBlock<T>> _targetBlocks = new List<ITargetBlock<T>>();

        public BroadcastBlock<T> BroadcastBock => _broadcastBlock;

        public BroadcastDataflowBuilder(BroadcastBlock<T> broadcastBlock, ITargetBlock<T> targetBlock = null)
        {
            _broadcastBlock = broadcastBlock;
            if (targetBlock != null && !_targetBlocks.Contains(targetBlock))
            {
                _targetBlocks.Add(targetBlock);
            }
        }

        public ITargetBlock<T> Create()
        {
            return new BroadcastDataflowWrapper<T>(_broadcastBlock, _targetBlocks.ToArray());
        }

        public IBroadcastDataflowBuilder<T> LinkTo(ITargetBlock<T> targetBlock, DataflowLinkOptions linkOptions = null, Predicate<T> predicate = null)
        {
            if (targetBlock == null) throw new ArgumentNullException("targetBlock");

            LinkHelper.Link(_broadcastBlock, targetBlock, linkOptions, predicate);
            return new BroadcastDataflowBuilder<T>(_broadcastBlock, targetBlock);
        }
    }
}
