using System;
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
        /// Initializes a DataflowWriteOnceOptions.
        /// </summary>
        /// <param name="writeOnce"></param>
        /// <param name="link"></param>
        public DataflowWriteOnceOptions(Action<GroupingDataflowBlockOptions> writeOnce = null
            , Action<DataflowLinkOptions> link = null)
        {
            _writeOnceOptions = DataflowDefaultOptions.DefaultGroupingBlockOptions;
            _linkOptions = DataflowDefaultOptions.DefaultLinkOptions;

            if (writeOnce != null) writeOnce(_writeOnceOptions);
            if (link != null) link(_linkOptions);
        }

        /// <summary>
        /// Block options for creating write once block.
        /// </summary>
        public GroupingDataflowBlockOptions WriteOnceBlockOptions
        {
            get
            {
                return _writeOnceOptions ?? (_writeOnceOptions = DataflowDefaultOptions.DefaultGroupingBlockOptions);
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
                return _linkOptions ?? (_linkOptions = DataflowDefaultOptions.DefaultLinkOptions);
            }
            set
            {
                _linkOptions = value;
            }
        }
    }
}
