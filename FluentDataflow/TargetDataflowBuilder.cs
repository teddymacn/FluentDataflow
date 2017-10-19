using System.Threading.Tasks.Dataflow;

namespace FluentDataflow
{
    internal class TargetDataflowBuilder<TInput> : ITargetDataflowBuilder<TInput>
    {
        private readonly ITargetBlock<TInput> _originalTargetBlock;
        private readonly IDataflowBlock _finalTargetBlock;

        public ITargetBlock<TInput> OriginalTargetBlock => _originalTargetBlock;
        public IDataflowBlock FinalTargetBlock => _finalTargetBlock;

        public TargetDataflowBuilder(ITargetBlock<TInput> targetBlock)
            : this(targetBlock, targetBlock)
        {
        }

        public TargetDataflowBuilder(ITargetBlock<TInput> originalTargetBlock, IDataflowBlock finalTargetBlock)
        {
            _originalTargetBlock = originalTargetBlock;
            _finalTargetBlock = finalTargetBlock;
        }

        public ITargetBlock<TInput> Create()
        {
            if (ReferenceEquals(_originalTargetBlock, _finalTargetBlock)) return _originalTargetBlock;

            return new TargetDataflowWrapper<TInput>(_originalTargetBlock, _finalTargetBlock);
        }
    }
}
