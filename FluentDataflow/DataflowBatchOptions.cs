using System.Threading.Tasks.Dataflow;

namespace FluentDataflow
{
    /// <summary>
    /// Options for applying batching.
    /// </summary>
    public struct DataflowBatchOptions
    {
        private GroupingDataflowBlockOptions _batchOptions;
        private DataflowLinkOptions _linkOptions;

        /// <summary>
        /// Block options for creating batch block.
        /// </summary>
        public GroupingDataflowBlockOptions BatchBlockOptions
        {
            get
            {
                return _batchOptions ?? DataflowDefaultOptions.DefaultGroupingBlockOptions;
            }
            set
            {
                _batchOptions = value;
            }
        }

        /// <summary>
        /// Link options for linking to the batch block.
        /// </summary>
        public DataflowLinkOptions LinkOptions
        {
            get
            {
                return _linkOptions ?? DataflowDefaultOptions.DefaultLinkOptions;
            }
            set
            {
                _linkOptions = value;
            }
        }
    }
}
