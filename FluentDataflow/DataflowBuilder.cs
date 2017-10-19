using FluentDataflow;
using System.Threading.Tasks.Dataflow;

namespace FluentDataflow
{
    internal class DataflowBuilder : IDataflowBuilder
    {
        private readonly IDataflowBlock _originalSourceBlock;
        private readonly IDataflowBlock _currentSourceBlock;
        private readonly IDataflowBlock _targetBlock;
        private readonly bool? _propagateCompletion;

        public IDataflowBlock OriginalSourceBlock => _originalSourceBlock;
        public IDataflowBlock CurrentSourceBlock => _currentSourceBlock;
        public IDataflowBlock TargetBlock => _targetBlock;
        public bool? PropagateCompletion => _propagateCompletion;

        public DataflowBuilder(IDataflowBlock sourceBlock, IDataflowBlock targetBlock, bool? propagateCompletion = null)
            : this(sourceBlock, sourceBlock, targetBlock, propagateCompletion)
        {

        }

        public DataflowBuilder(IDataflowBlock orginalSourceBlock, IDataflowBlock currentSourceBlock, IDataflowBlock targetBlock, bool? propagateCompletion = null)
        {
            _originalSourceBlock = orginalSourceBlock;
            _currentSourceBlock = currentSourceBlock;
            _targetBlock = targetBlock;
            _propagateCompletion = propagateCompletion;
        }

        public IDataflowBlock Create()
        {
            return new DataflowWrapper(_originalSourceBlock, _currentSourceBlock, _targetBlock, _propagateCompletion);
        }
    }
}
