using System;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace FluentDataflow
{
    internal class DataflowWrapper : IDataflowBlock
    {
        private readonly IDataflowBlock _originalSourceBlock;
        private readonly IDataflowBlock _currentSourceBlock;
        private readonly IDataflowBlock _targetBlock;
        private readonly bool? _propagateCompletion;

        public DataflowWrapper(IDataflowBlock originalSourceBlock, IDataflowBlock currentSourceBlock, IDataflowBlock targetBlock, bool? propagateCompletion = null)
        {
            _originalSourceBlock = originalSourceBlock;
            _currentSourceBlock = currentSourceBlock;
            _targetBlock = targetBlock;
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
                            _targetBlock.Fault(task.Exception);
                        else
                            _targetBlock.Complete();

                        return _targetBlock.Completion;
                    });
                }

                return _targetBlock.Completion;
            }
        }

        public void Complete()
        {
            _originalSourceBlock.Complete();
        }

        public void Fault(Exception exception)
        {
            _originalSourceBlock.Fault(exception);
        }
    }
}
