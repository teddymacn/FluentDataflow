using System;
using System.Linq;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace FluentDataflow
{
    internal class MultipleSourceDataflowWrapper : IDataflowBlock
    {
        private readonly IDataflowBlock[] _sourceBlocks;

        public MultipleSourceDataflowWrapper(IDataflowBlock[] sourceBlocks)
        {
            _sourceBlocks = sourceBlocks;
        }

        public Task Completion => Task.WhenAll(_sourceBlocks.Select(s => s.Completion));

        public void Complete()
        {
            foreach (var sourceBlock in _sourceBlocks)
            {
                sourceBlock.Complete();
            }
        }

        public void Fault(Exception exception)
        {
            foreach (var sourceBlock in _sourceBlocks)
            {
                sourceBlock.Fault(exception);
            }
        }
    }
}
