using System.Threading.Tasks.Dataflow;

namespace FluentDataflow
{
    /// <summary>
    /// Options for applying join.
    /// </summary>
    public struct DataflowJoinOptions
    {
        private GroupingDataflowBlockOptions _joinOptions;
        private DataflowLinkOptions _target1LinkOptions;
        private DataflowLinkOptions _target2LinkOptions;
        private DataflowLinkOptions _target3LinkOptions;

        /// <summary>
        /// Block options for creating join block.
        /// </summary>
        public GroupingDataflowBlockOptions JoinBlockOptions
        {
            get
            {
                return _joinOptions ?? DataflowDefaultOptions.DefaultGroupingBlockOptions;
            }
            set
            {
                _joinOptions = value;
            }
        }

        /// <summary>
        /// Link options for linking to the join block.
        /// </summary>
        public DataflowLinkOptions Target1LinkOptions
        {
            get
            {
                return _target1LinkOptions ?? DataflowDefaultOptions.DefaultLinkOptions;
            }
            set
            {
                _target1LinkOptions = value;
            }
        }

        /// <summary>
        /// Link options for linking to the join block.
        /// </summary>
        public DataflowLinkOptions Target2LinkOptions
        {
            get
            {
                return _target2LinkOptions ?? DataflowDefaultOptions.DefaultLinkOptions;
            }
            set
            {
                _target2LinkOptions = value;
            }
        }

        /// <summary>
        /// Link options for linking to the join block.
        /// </summary>
        public DataflowLinkOptions Target3LinkOptions
        {
            get
            {
                return _target3LinkOptions ?? DataflowDefaultOptions.DefaultLinkOptions;
            }
            set
            {
                _target3LinkOptions = value;
            }
        }
    }
}
