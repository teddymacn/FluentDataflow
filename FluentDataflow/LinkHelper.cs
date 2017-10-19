using System;
using System.Threading.Tasks.Dataflow;

namespace FluentDataflow
{
    internal static class LinkHelper
    {
        public static void Link<T>(ISourceBlock<T> sourceBlock, ITargetBlock<T> targetBlock, DataflowLinkOptions linkOptions, Predicate<T> predicate = null)
        {
            // by default, propagate completion of source to target
            var options = linkOptions ?? DataflowDefaultOptions.DefaultLinkOptions;

            if (predicate == null)
                sourceBlock.LinkTo(targetBlock, options);
            else
                sourceBlock.LinkTo(targetBlock, options, predicate);
        }
    }
}
