using System.Threading.Tasks.Dataflow;

namespace FluentDataflow
{
    /// <summary>
    /// Options for applying write once.
    /// </summary>
    public struct DataflowWriteOnceOptions
    {
        private GroupingDataflowBlockOptions _writeOnceOptions;
        private DataflowLinkOptions _linkOptions;

        /// <summary>
        /// Block options for creating write once block.
        /// </summary>
        public GroupingDataflowBlockOptions WriteOnceBlockOptions
        {
            get
            {
                return _writeOnceOptions ?? DataflowDefaultOptions.DefaultGroupingBlockOptions;
            }
            set
            {
                _writeOnceOptions = value;
            }
        }

        /// <summary>
        /// Link options for linking to the write once block.
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
