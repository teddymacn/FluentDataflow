using System;
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
        /// Initializes a DataflowJoinOptions.
        /// </summary>
        /// <param name="join"></param>
        /// <param name="target1"></param>
        /// <param name="target2"></param>
        /// <param name="target3"></param>
        public DataflowJoinOptions(
            Action<GroupingDataflowBlockOptions> join = null
            , Action<DataflowLinkOptions> target1 = null
            , Action<DataflowLinkOptions> target2 = null
            , Action<DataflowLinkOptions> target3 = null)
        {
            _joinOptions = DataflowDefaultOptions.DefaultGroupingBlockOptions;
            _target1LinkOptions = DataflowDefaultOptions.DefaultLinkOptions;
            _target2LinkOptions = DataflowDefaultOptions.DefaultLinkOptions;
            _target3LinkOptions = DataflowDefaultOptions.DefaultLinkOptions;

            if (join != null) join(_joinOptions);
            if (target1 != null) target1(_target1LinkOptions);
            if (target2 != null) target2(_target2LinkOptions);
            if (target3 != null) target3(_target3LinkOptions);
        }

        /// <summary>
        /// Block options for creating join block.
        /// </summary>
        public GroupingDataflowBlockOptions JoinBlockOptions
        {
            get
            {
                return _joinOptions ?? (_joinOptions = DataflowDefaultOptions.DefaultGroupingBlockOptions);
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
                return _target1LinkOptions ?? (_target1LinkOptions = DataflowDefaultOptions.DefaultLinkOptions);
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
                return _target2LinkOptions ?? (_target2LinkOptions = DataflowDefaultOptions.DefaultLinkOptions);
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
                return _target3LinkOptions ?? (_target3LinkOptions = DataflowDefaultOptions.DefaultLinkOptions);
            }
            set
            {
                _target3LinkOptions = value;
            }
        }
    }
}
