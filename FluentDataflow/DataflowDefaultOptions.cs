using System.Threading.Tasks.Dataflow;

namespace FluentDataflow
{
    /// <summary>
    /// Default dataflow options.
    /// </summary>
    public static class DataflowDefaultOptions
    {
        /// <summary>
        /// Default block options.
        /// </summary>
        public static ExecutionDataflowBlockOptions DefaultBlockOptions => new ExecutionDataflowBlockOptions();

        /// <summary>
        /// Default link options.
        /// </summary>
        public static DataflowLinkOptions DefaultLinkOptions => new DataflowLinkOptions { PropagateCompletion = true };

        /// <summary>
        /// Default grouping block options.
        /// </summary>
        public static GroupingDataflowBlockOptions DefaultGroupingBlockOptions => new GroupingDataflowBlockOptions();
    }
}
