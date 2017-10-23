using System;
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
        /// Initializes a DataflowBatchOptions.
        /// </summary>
        /// <param name="batch"></param>
        /// <param name="link"></param>
        public DataflowBatchOptions(Action<GroupingDataflowBlockOptions> batch = null
            , Action<DataflowLinkOptions> link = null)
        {
            _batchOptions = DataflowDefaultOptions.DefaultGroupingBlockOptions;
            _linkOptions = DataflowDefaultOptions.DefaultLinkOptions;

            if (batch != null) batch(_batchOptions);
            if (link != null) link(_linkOptions);
        }

        /// <summary>
        /// Block options for creating batch block.
        /// </summary>
        public GroupingDataflowBlockOptions BatchBlockOptions
        {
            get
            {
                return _batchOptions ?? (_batchOptions = DataflowDefaultOptions.DefaultGroupingBlockOptions);
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
                return _linkOptions ?? (_linkOptions = DataflowDefaultOptions.DefaultLinkOptions);
            }
            set
            {
                _linkOptions = value;
            }
        }
    }
}
