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
        /// Creates a dataflow builder from an action.
        /// </summary>
        /// <typeparam name="TInput"></typeparam>
        /// <param name="factory"></param>
        /// <param name="action"></param>
        /// <param name="blockOptions"></param>
        /// <returns></returns>
        public static ITargetDataflowBuilder<TInput> FromTarget<TInput>(
            this IDataflowFactory factory
            , Action<TInput> action
            , ExecutionDataflowBlockOptions blockOptions = null)
        {
            if (factory == null) throw new ArgumentNullException("factory");
            if (action == null) throw new ArgumentNullException("action");

            return factory.FromTarget(new ActionBlock<TInput>(action, blockOptions ?? DataflowDefaultOptions.DefaultBlockOptions));
        }

        /// <summary>
        /// Creates a dataflow builder from an action.
        /// </summary>
        /// <typeparam name="TInput"></typeparam>
        /// <param name="factory"></param>
        /// <param name="action"></param>
        /// <param name="blockOptions"></param>
        /// <returns></returns>
        public static ITargetDataflowBuilder<TInput> FromTarget<TInput>(
            this IDataflowFactory factory
            , Func<TInput, Task> action
            , ExecutionDataflowBlockOptions blockOptions = null)
        {
            if (factory == null) throw new ArgumentNullException("factory");
            if (action == null) throw new ArgumentNullException("action");

            return factory.FromTarget(new ActionBlock<TInput>(action, blockOptions ?? DataflowDefaultOptions.DefaultBlockOptions));
        }

        /// <summary>
        /// Creates a dataflow builder from a transform function.
        /// </summary>
        /// <typeparam name="TInput"></typeparam>
        /// <typeparam name="TOutput"></typeparam>
        /// <param name="factory"></param>
        /// <param name="transform"></param>
        /// <param name="blockOptions"></param>
        /// <returns></returns>
        public static IPropagatorDataflowBuilder<TInput, TOutput> FromPropagator<TInput, TOutput>(
            this IDataflowFactory factory
            , Func<TInput, TOutput> transform
            , ExecutionDataflowBlockOptions blockOptions = null)
        {
            if (factory == null) throw new ArgumentNullException("factory");
            if (transform == null) throw new ArgumentNullException("transform");

            return factory.FromPropagator(new TransformBlock<TInput, TOutput>(transform, blockOptions ?? DataflowDefaultOptions.DefaultBlockOptions));
        }

        /// <summary>
        /// Creates a dataflow builder from a transform function.
        /// </summary>
        /// <typeparam name="TInput"></typeparam>
        /// <typeparam name="TOutput"></typeparam>
        /// <param name="factory"></param>
        /// <param name="transform"></param>
        /// <param name="blockOptions"></param>
        /// <returns></returns>
        public static IPropagatorDataflowBuilder<TInput, TOutput> FromPropagator<TInput, TOutput>(
            this IDataflowFactory factory
            , Func<TInput, Task<TOutput>> transform
            , ExecutionDataflowBlockOptions blockOptions = null)
        {
            if (factory == null) throw new ArgumentNullException("factory");
            if (transform == null) throw new ArgumentNullException("transform");

            return factory.FromPropagator(new TransformBlock<TInput, TOutput>(transform, blockOptions ?? DataflowDefaultOptions.DefaultBlockOptions));
        }

        /// <summary>
        /// Creates a dataflow builder from a transformMany function.
        /// </summary>
        /// <typeparam name="TInput"></typeparam>
        /// <typeparam name="TOutput"></typeparam>
        /// <param name="factory"></param>
        /// <param name="transformMany"></param>
        /// <param name="blockOptions"></param>
        /// <returns></returns>
        public static IPropagatorDataflowBuilder<TInput, TOutput> FromPropagator<TInput, TOutput>(
            this IDataflowFactory factory
            , Func<TInput, IEnumerable<TOutput>> transformMany
            , ExecutionDataflowBlockOptions blockOptions = null)
        {
            if (factory == null) throw new ArgumentNullException("factory");
            if (transformMany == null) throw new ArgumentNullException("transform");

            return factory.FromPropagator(new TransformManyBlock<TInput, TOutput>(transformMany, blockOptions ?? DataflowDefaultOptions.DefaultBlockOptions));
        }

        /// <summary>
        /// Creates a dataflow builder from a transformMany function.
        /// </summary>
        /// <typeparam name="TInput"></typeparam>
        /// <typeparam name="TOutput"></typeparam>
        /// <param name="factory"></param>
        /// <param name="transformMany"></param>
        /// <param name="blockOptions"></param>
        /// <returns></returns>
        public static IPropagatorDataflowBuilder<TInput, TOutput> FromPropagator<TInput, TOutput>(
            this IDataflowFactory factory
            , Func<TInput, Task<IEnumerable<TOutput>>> transformMany
            , ExecutionDataflowBlockOptions blockOptions = null)
        {
            if (factory == null) throw new ArgumentNullException("factory");
            if (transformMany == null) throw new ArgumentNullException("transform");

            return factory.FromPropagator(new TransformManyBlock<TInput, TOutput>(transformMany, blockOptions ?? DataflowDefaultOptions.DefaultBlockOptions));
        }

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
