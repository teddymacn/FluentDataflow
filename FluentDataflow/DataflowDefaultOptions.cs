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
        public static readonly ExecutionDataflowBlockOptions DefaultBlockOptions = new ExecutionDataflowBlockOptions();

        /// <summary>
        /// Default link options.
        /// </summary>
        public static readonly DataflowLinkOptions DefaultLinkOptions = new DataflowLinkOptions { PropagateCompletion = true };
    }
}
