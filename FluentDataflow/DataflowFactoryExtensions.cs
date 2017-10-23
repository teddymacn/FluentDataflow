using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace FluentDataflow
{
    /// <summary>
    /// Extension methods for <see cref="IDataflowFactory"/>.
    /// </summary>
    public static class DataflowFactoryExtensions
    {
        /// <summary>
        /// Creates a dataflow builder from a cloning function.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="factory"></param>
        /// <param name="cloningFunction"></param>
        /// <param name="blockOptions"></param>
        /// <returns></returns>
        public static IBroadcastDataflowBuilder<T> FromBroadcast<T>(
            this IDataflowFactory factory
            , Func<T, T> cloningFunction = null
            , DataflowBlockOptions blockOptions = null)
        {
            var broadcastBlock = new BroadcastBlock<T>(cloningFunction ?? new Func<T, T>(t => t), blockOptions ?? DataflowDefaultOptions.DefaultBlockOptions);
            return factory.FromBroadcast(broadcastBlock);
        }
    }
}
