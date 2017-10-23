using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace FluentDataflow
{
    /// <summary>
    /// Extension methods for dataflow builders.
    /// </summary>
    public static class DataflowBuilderExtenions
    {
        #region IPropagatorDataflowBuilder link to ITargetBlock

        /// <summary>
        /// Links to target block.
        /// </summary>
        /// <typeparam name="TInput"></typeparam>
        /// <typeparam name="TOutput"></typeparam>
        /// <param name="builder"></param>
        /// <param name="targetBlock"></param>
        /// <param name="linkOptions"></param>
        /// <param name="predicate"></param>
        /// <param name="declinedTargetBlock"></param>
        /// <param name="declinedLinkOptions"></param>
        /// <returns></returns>
        public static ITargetDataflowBuilder<TInput> LinkToTarget<TInput, TOutput>(
            this IPropagatorDataflowBuilder<TInput, TOutput> builder
            , ITargetBlock<TOutput> targetBlock
            , DataflowLinkOptions linkOptions
            , Predicate<TOutput> predicate
            , ITargetBlock<TOutput> declinedTargetBlock
            , DataflowLinkOptions declinedLinkOptions)
        {
            if (builder == null) return null;
            if (targetBlock == null) throw new ArgumentNullException("targetBlock");
            if (predicate == null) throw new ArgumentNullException("predicate");

            var nextBuilder = builder.LinkToTarget(targetBlock, linkOptions, predicate);

            if (declinedTargetBlock != null)
            {
                // LinkTo declined target
                LinkHelper.Link((builder as PropagatorDataflowBuilder<TInput, TOutput>).FinalSourceBlock, declinedTargetBlock, declinedLinkOptions ?? DataflowDefaultOptions.DefaultLinkOptions);
            }

            return nextBuilder;
        }

        /// <summary>
        /// Links to target block.
        /// </summary>
        /// <typeparam name="TInput"></typeparam>
        /// <typeparam name="TOutput"></typeparam>
        /// <param name="builder"></param>
        /// <param name="targetBlock"></param>
        /// <param name="predicate"></param>
        /// <param name="declinedTargetBlock"></param>
        /// <returns></returns>
        public static ITargetDataflowBuilder<TInput> LinkToTarget<TInput, TOutput>(
            this IPropagatorDataflowBuilder<TInput, TOutput> builder
            , ITargetBlock<TOutput> targetBlock
            , Predicate<TOutput> predicate
            , ITargetBlock<TOutput> declinedTargetBlock = null)
        {
            return builder.LinkToTarget(targetBlock, DataflowDefaultOptions.DefaultLinkOptions, predicate, declinedTargetBlock, DataflowDefaultOptions.DefaultLinkOptions);
        }

        #endregion

        #region IPropagatorDataflowBuilder link to IPropagatorBlock

        /// <summary>
        /// Links to a propagator block.
        /// </summary>
        /// <typeparam name="TInput"></typeparam>
        /// <typeparam name="TOutput"></typeparam>
        /// <typeparam name="TOutput2"></typeparam>
        /// <param name="builder"></param>
        /// <param name="propagatorBlock"></param>
        /// <param name="linkOptions"></param>
        /// <param name="predicate"></param>
        /// <param name="declinedTargetBlock"></param>
        /// <param name="declinedLinkOptions"></param>
        /// <returns></returns>
        public static IPropagatorDataflowBuilder<TInput, TOutput2> LinkToPropagator<TInput, TOutput, TOutput2>(
            this IPropagatorDataflowBuilder<TInput, TOutput> builder
            , IPropagatorBlock<TOutput, TOutput2> propagatorBlock
            , DataflowLinkOptions linkOptions
            , Predicate<TOutput> predicate
            , ITargetBlock<TOutput> declinedTargetBlock
            , DataflowLinkOptions declinedLinkOptions)
        {
            if (builder == null) return null;
            if (propagatorBlock == null) throw new ArgumentNullException("propagatorBlock");
            if (predicate == null) throw new ArgumentNullException("predicate");

            var nextBuilder = builder.LinkToPropagator(propagatorBlock, linkOptions, predicate);

            if (declinedTargetBlock != null)
            {
                // LinkTo declined target
                LinkHelper.Link((builder as PropagatorDataflowBuilder<TInput, TOutput>).FinalSourceBlock, declinedTargetBlock, declinedLinkOptions ?? DataflowDefaultOptions.DefaultLinkOptions);
            }

            return nextBuilder;
        }

        /// <summary>
        /// Links to a propagator block.
        /// </summary>
        /// <typeparam name="TInput"></typeparam>
        /// <typeparam name="TOutput"></typeparam>
        /// <typeparam name="TOutput2"></typeparam>
        /// <param name="builder"></param>
        /// <param name="propagatorBlock"></param>
        /// <param name="predicate"></param>
        /// <param name="declinedTargetBlock"></param>
        /// <returns></returns>
        public static IPropagatorDataflowBuilder<TInput, TOutput2> LinkToPropagator<TInput, TOutput, TOutput2>(
            this IPropagatorDataflowBuilder<TInput, TOutput> builder
            , IPropagatorBlock<TOutput, TOutput2> propagatorBlock
            , Predicate<TOutput> predicate
            , ITargetBlock<TOutput> declinedTargetBlock = null)
        {
            return builder.LinkToPropagator(propagatorBlock, DataflowDefaultOptions.DefaultLinkOptions, predicate, declinedTargetBlock, DataflowDefaultOptions.DefaultLinkOptions);
        }

        #endregion

        #region ISourceDataflowBuilder link to ITargetBlock

        /// <summary>
        /// Links to a target block.
        /// </summary>
        /// <typeparam name="TOutput"></typeparam>
        /// <param name="builder"></param>
        /// <param name="targetBlock"></param>
        /// <param name="linkOptions"></param>
        /// <param name="predicate"></param>
        /// <param name="declinedTargetBlock"></param>
        /// <param name="declinedLinkOptions"></param>
        /// <returns></returns>
        public static IDataflowBuilder LinkToTarget<TOutput>(
            this ISourceDataflowBuilder<TOutput> builder
            , ITargetBlock<TOutput> targetBlock
            , DataflowLinkOptions linkOptions
            , Predicate<TOutput> predicate
            , ITargetBlock<TOutput> declinedTargetBlock
            , DataflowLinkOptions declinedLinkOptions)
        {
            if (builder == null) return null;
            if (targetBlock == null) throw new ArgumentNullException("targetBlock");
            if (predicate == null) throw new ArgumentNullException("predicate");

            var nextBuilder = builder.LinkToTarget(targetBlock, linkOptions, predicate);

            if (declinedTargetBlock != null)
            {
                // LinkTo declined target
                LinkHelper.Link((builder as SourceDataflowBuilder<TOutput>).FinalSourceBlock, declinedTargetBlock, declinedLinkOptions ?? DataflowDefaultOptions.DefaultLinkOptions);
            }

            return nextBuilder;
        }

        /// <summary>
        /// Links to a target block.
        /// </summary>
        /// <typeparam name="TOutput"></typeparam>
        /// <param name="builder"></param>
        /// <param name="targetBlock"></param>
        /// <param name="predicate"></param>
        /// <param name="declinedTargetBlock"></param>
        /// <returns></returns>
        public static IDataflowBuilder LinkToTarget<TOutput>(
            this ISourceDataflowBuilder<TOutput> builder
            , ITargetBlock<TOutput> targetBlock
            , Predicate<TOutput> predicate
            , ITargetBlock<TOutput> declinedTargetBlock = null)
        {
            return builder.LinkToTarget(targetBlock, DataflowDefaultOptions.DefaultLinkOptions, predicate, declinedTargetBlock, DataflowDefaultOptions.DefaultLinkOptions);
        }

        #endregion

        #region ISourceDataflowBuilder link to IPropagatorBlock

        /// <summary>
        /// Links to a propagator block.
        /// </summary>
        /// <typeparam name="TOutput"></typeparam>
        /// <typeparam name="TOutput2"></typeparam>
        /// <param name="builder"></param>
        /// <param name="propagatorBlock"></param>
        /// <param name="linkOptions"></param>
        /// <param name="predicate"></param>
        /// <param name="declinedTargetBlock"></param>
        /// <param name="declinedLinkOptions"></param>
        /// <returns></returns>
        public static ISourceDataflowBuilder<TOutput2> LinkToPropagator<TOutput, TOutput2>(
            this ISourceDataflowBuilder<TOutput> builder
            , IPropagatorBlock<TOutput, TOutput2> propagatorBlock
            , DataflowLinkOptions linkOptions
            , Predicate<TOutput> predicate
            , ITargetBlock<TOutput> declinedTargetBlock
            , DataflowLinkOptions declinedLinkOptions)
        {
            if (builder == null) return null;
            if (propagatorBlock == null) throw new ArgumentNullException("propagatorBlock");
            if (predicate == null) throw new ArgumentNullException("predicate");

            var nextBuilder = builder.LinkToPropagator(propagatorBlock, linkOptions, predicate);

            if (declinedTargetBlock != null)
            {
                // LinkTo declined target
                LinkHelper.Link((builder as SourceDataflowBuilder<TOutput>).FinalSourceBlock, declinedTargetBlock, declinedLinkOptions ?? DataflowDefaultOptions.DefaultLinkOptions);
            }

            return nextBuilder;
        }

        /// <summary>
        /// Links to a propagator block.
        /// </summary>
        /// <typeparam name="TOutput"></typeparam>
        /// <typeparam name="TOutput2"></typeparam>
        /// <param name="builder"></param>
        /// <param name="propagatorBlock"></param>
        /// <param name="predicate"></param>
        /// <param name="declinedTargetBlock"></param>
        /// <returns></returns>
        public static ISourceDataflowBuilder<TOutput2> LinkToPropagator<TOutput, TOutput2>(
            this ISourceDataflowBuilder<TOutput> builder
            , IPropagatorBlock<TOutput, TOutput2> propagatorBlock
            , Predicate<TOutput> predicate
            , ITargetBlock<TOutput> declinedTargetBlock = null)
        {
            return builder.LinkToPropagator(propagatorBlock, DataflowDefaultOptions.DefaultLinkOptions, predicate, declinedTargetBlock, DataflowDefaultOptions.DefaultLinkOptions);
        }

        #endregion

        #region IMultipleSourceDataflowBuilder link to TargetBlock

        /// <summary>
        /// Links to a target block.
        /// </summary>
        /// <typeparam name="TOutput"></typeparam>
        /// <param name="builder"></param>
        /// <param name="targetBlock"></param>
        /// <param name="linkOptions"></param>
        /// <param name="predicate"></param>
        /// <param name="declinedTargetBlock"></param>
        /// <param name="declinedLinkOptions"></param>
        /// <returns></returns>
        public static IDataflowBuilder LinkToTarget<TOutput>(
            this IMultipleSourceDataflowBuilder<TOutput> builder
            , ITargetBlock<TOutput> targetBlock
            , DataflowLinkOptions linkOptions
            , Predicate<TOutput> predicate
            , ITargetBlock<TOutput> declinedTargetBlock
            , DataflowLinkOptions declinedLinkOptions)
        {
            if (builder == null) return null;
            if (targetBlock == null) throw new ArgumentNullException("targetBlock");
            if (predicate == null) throw new ArgumentNullException("predicate");

            var nextBuilder = builder.LinkToTarget(targetBlock, linkOptions, predicate);

            if (declinedTargetBlock != null)
            {
                // LinkTo declined target
                (builder as MultipleSourceDataflowBuilder<TOutput>).Link(declinedTargetBlock, declinedLinkOptions ?? DataflowDefaultOptions.DefaultLinkOptions, null);
            }

            return nextBuilder;
        }

        /// <summary>
        /// Links to a target block.
        /// </summary>
        /// <typeparam name="TOutput"></typeparam>
        /// <param name="builder"></param>
        /// <param name="targetBlock"></param>
        /// <param name="predicate"></param>
        /// <param name="declinedTargetBlock"></param>
        /// <returns></returns>
        public static IDataflowBuilder LinkToTarget<TOutput>(
            this IMultipleSourceDataflowBuilder<TOutput> builder
            , ITargetBlock<TOutput> targetBlock
            , Predicate<TOutput> predicate
            , ITargetBlock<TOutput> declinedTargetBlock = null)
        {
            return builder.LinkToTarget(targetBlock, DataflowDefaultOptions.DefaultLinkOptions, predicate, declinedTargetBlock, DataflowDefaultOptions.DefaultLinkOptions);
        }

        #endregion

        #region IBroadcastDataflowBuilder link to ITargetBlock

        /// <summary>
        /// Links to a target block.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="builder"></param>
        /// <param name="targetBlock"></param>
        /// <param name="linkOptions"></param>
        /// <param name="predicate"></param>
        /// <param name="declinedTargetBlock"></param>
        /// <param name="declinedLinkOptions"></param>
        /// <returns></returns>
        public static IBroadcastDataflowBuilder<T> LinkTo<T>(
            this IBroadcastDataflowBuilder<T> builder
            , ITargetBlock<T> targetBlock
            , DataflowLinkOptions linkOptions
            , Predicate<T> predicate
            , ITargetBlock<T> declinedTargetBlock
            , DataflowLinkOptions declinedLinkOptions)
        {
            if (builder == null) return null;
            if (targetBlock == null) throw new ArgumentNullException("targetBlock");
            if (predicate == null) throw new ArgumentNullException("predicate");

            var nextBuilder = builder.LinkTo(targetBlock, linkOptions, predicate);

            if (declinedTargetBlock != null)
            {
                // LinkTo declined target
                LinkHelper.Link((builder as BroadcastDataflowBuilder<T>).BroadcastBock, declinedTargetBlock, declinedLinkOptions ?? DataflowDefaultOptions.DefaultLinkOptions);
            }

            return nextBuilder;
        }

        /// <summary>
        /// Links to a target block.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="builder"></param>
        /// <param name="targetBlock"></param>
        /// <param name="predicate"></param>
        /// <param name="declinedTargetBlock"></param>
        /// <returns></returns>
        public static IBroadcastDataflowBuilder<T> LinkTo<T>(
            this IBroadcastDataflowBuilder<T> builder
            , ITargetBlock<T> targetBlock
            , Predicate<T> predicate
            , ITargetBlock<T> declinedTargetBlock = null)
        {
            return builder.LinkTo(targetBlock, DataflowDefaultOptions.DefaultLinkOptions, predicate, declinedTargetBlock, DataflowDefaultOptions.DefaultLinkOptions);
        }

        #endregion
    }
}
