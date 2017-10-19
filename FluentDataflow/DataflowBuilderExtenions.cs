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
            , DataflowLinkOptions declinedLinkOptions = null)
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
            return builder.LinkToTarget(targetBlock, DataflowDefaultOptions.DefaultLinkOptions, predicate, declinedTargetBlock);
        }

        /// <summary>
        /// Links to target block.
        /// </summary>
        /// <typeparam name="TInput"></typeparam>
        /// <typeparam name="TOutput"></typeparam>
        /// <param name="builder"></param>
        /// <param name="targetBlock"></param>
        /// <param name="linkOptions"></param>
        /// <param name="predicate"></param>
        /// <param name="declinedAction"></param>
        /// <param name="declinedBlockOptions"></param>
        /// <param name="declinedLinkOptions"></param>
        /// <returns></returns>
        public static ITargetDataflowBuilder<TInput> LinkToTarget<TInput, TOutput>(
            this IPropagatorDataflowBuilder<TInput, TOutput> builder
            , ITargetBlock<TOutput> targetBlock
            , DataflowLinkOptions linkOptions
            , Predicate<TOutput> predicate
            , Action<TOutput> declinedAction
            , ExecutionDataflowBlockOptions declinedBlockOptions = null
            , DataflowLinkOptions declinedLinkOptions = null)
        {
            if (builder == null) return null;
            if (targetBlock == null) throw new ArgumentNullException("targetBlock");
            if (predicate == null) throw new ArgumentNullException("predicate");

            var nextBuilder = builder.LinkToTarget(targetBlock, linkOptions, predicate);

            if (declinedAction != null)
            {
                // LinkTo declined target
                var declinedActionBlock = new ActionBlock<TOutput>(declinedAction, declinedBlockOptions ?? DataflowDefaultOptions.DefaultBlockOptions);
                LinkHelper.Link((builder as PropagatorDataflowBuilder<TInput, TOutput>).FinalSourceBlock, declinedActionBlock, declinedLinkOptions ?? DataflowDefaultOptions.DefaultLinkOptions);
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
        /// <param name="declinedAction"></param>
        /// <param name="declinedBlockOptions"></param>
        /// <returns></returns>
        public static ITargetDataflowBuilder<TInput> LinkToTarget<TInput, TOutput>(
            this IPropagatorDataflowBuilder<TInput, TOutput> builder
            , ITargetBlock<TOutput> targetBlock
            , Predicate<TOutput> predicate
            , Action<TOutput> declinedAction
            , ExecutionDataflowBlockOptions declinedBlockOptions = null)
        {
            return builder.LinkToTarget(targetBlock, DataflowDefaultOptions.DefaultLinkOptions, predicate, declinedAction, declinedBlockOptions);
        }

        /// <summary>
        /// Links to target block.
        /// </summary>
        /// <typeparam name="TInput"></typeparam>
        /// <typeparam name="TOutput"></typeparam>
        /// <param name="builder"></param>
        /// <param name="targetBlock"></param>
        /// <param name="linkOptions"></param>
        /// <param name="predicate"></param>
        /// <param name="declinedAction"></param>
        /// <param name="declinedBlockOptions"></param>
        /// <param name="declinedLinkOptions"></param>
        /// <returns></returns>
        public static ITargetDataflowBuilder<TInput> LinkToTarget<TInput, TOutput>(
            this IPropagatorDataflowBuilder<TInput, TOutput> builder
            , ITargetBlock<TOutput> targetBlock
            , DataflowLinkOptions linkOptions
            , Predicate<TOutput> predicate
            , Func<TOutput, Task> declinedAction
            , ExecutionDataflowBlockOptions declinedBlockOptions = null
            , DataflowLinkOptions declinedLinkOptions = null)
        {
            if (builder == null) return null;
            if (targetBlock == null) throw new ArgumentNullException("targetBlock");
            if (predicate == null) throw new ArgumentNullException("predicate");

            var nextBuilder = builder.LinkToTarget(targetBlock, linkOptions, predicate);

            if (declinedAction != null)
            {
                // LinkTo declined target
                var declinedActionBlock = new ActionBlock<TOutput>(declinedAction, declinedBlockOptions ?? DataflowDefaultOptions.DefaultBlockOptions);
                LinkHelper.Link((builder as PropagatorDataflowBuilder<TInput, TOutput>).FinalSourceBlock, declinedActionBlock, declinedLinkOptions ?? DataflowDefaultOptions.DefaultLinkOptions);
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
        /// <param name="declinedAction"></param>
        /// <param name="declinedBlockOptions"></param>
        /// <returns></returns>
        public static ITargetDataflowBuilder<TInput> LinkToTarget<TInput, TOutput>(
            this IPropagatorDataflowBuilder<TInput, TOutput> builder
            , ITargetBlock<TOutput> targetBlock
            , Predicate<TOutput> predicate
            , Func<TOutput, Task> declinedAction
            , ExecutionDataflowBlockOptions declinedBlockOptions = null)
        {
            return builder.LinkToTarget(targetBlock, DataflowDefaultOptions.DefaultLinkOptions, predicate, declinedAction, declinedBlockOptions);
        }

        #endregion

        #region IPropagatorDataflowBuilder link to action

        /// <summary>
        /// Links to a target action.
        /// </summary>
        /// <typeparam name="TInput"></typeparam>
        /// <typeparam name="TOutput"></typeparam>
        /// <param name="builder"></param>
        /// <param name="action"></param>
        /// <param name="blockOptions"></param>
        /// <param name="linkOptions"></param>
        /// <param name="predicate"></param>
        /// <param name="declinedTargetBlock"></param>
        /// <param name="declinedLinkOptions"></param>
        /// <returns></returns>
        public static ITargetDataflowBuilder<TInput> LinkToTarget<TInput, TOutput>(
            this IPropagatorDataflowBuilder<TInput, TOutput> builder
            , Action<TOutput> action
            , ExecutionDataflowBlockOptions blockOptions = null
            , DataflowLinkOptions linkOptions = null
            , Predicate<TOutput> predicate = null
            , ITargetBlock<TOutput> declinedTargetBlock = null
            , DataflowLinkOptions declinedLinkOptions = null)
        {
            if (builder == null) return null;
            if (action == null) throw new ArgumentNullException("action");

            var actionBlock = new ActionBlock<TOutput>(action, blockOptions ?? DataflowDefaultOptions.DefaultBlockOptions);
            var nextBuilder = builder.LinkToTarget(actionBlock, linkOptions, predicate);

            if (declinedTargetBlock != null)
            {
                // LinkTo declined target
                LinkHelper.Link((builder as PropagatorDataflowBuilder<TInput, TOutput>).FinalSourceBlock, declinedTargetBlock, declinedLinkOptions ?? DataflowDefaultOptions.DefaultLinkOptions);
            }

            return nextBuilder;
        }

        /// <summary>
        /// Links to a target action.
        /// </summary>
        /// <typeparam name="TInput"></typeparam>
        /// <typeparam name="TOutput"></typeparam>
        /// <param name="builder"></param>
        /// <param name="action"></param>
        /// <param name="blockOptions"></param>
        /// <param name="predicate"></param>
        /// <param name="declinedTargetBlock"></param>
        /// <returns></returns>
        public static ITargetDataflowBuilder<TInput> LinkToTarget<TInput, TOutput>(
            this IPropagatorDataflowBuilder<TInput, TOutput> builder
            , Action<TOutput> action
            , ExecutionDataflowBlockOptions blockOptions
            , Predicate<TOutput> predicate
            , ITargetBlock<TOutput> declinedTargetBlock = null)
        {
            return builder.LinkToTarget(action, blockOptions, DataflowDefaultOptions.DefaultLinkOptions, predicate, declinedTargetBlock);
        }

        /// <summary>
        /// Links to a target action.
        /// </summary>
        /// <typeparam name="TInput"></typeparam>
        /// <typeparam name="TOutput"></typeparam>
        /// <param name="builder"></param>
        /// <param name="action"></param>
        /// <param name="blockOptions"></param>
        /// <param name="linkOptions"></param>
        /// <param name="predicate"></param>
        /// <param name="declinedAction"></param>
        /// <param name="declinedBlockOptions"></param>
        /// <param name="declinedLinkOptions"></param>
        /// <returns></returns>
        public static ITargetDataflowBuilder<TInput> LinkToTarget<TInput, TOutput>(
            this IPropagatorDataflowBuilder<TInput, TOutput> builder
            , Action<TOutput> action
            , ExecutionDataflowBlockOptions blockOptions
            , DataflowLinkOptions linkOptions
            , Predicate<TOutput> predicate
            , Action<TOutput> declinedAction
            , ExecutionDataflowBlockOptions declinedBlockOptions = null
            , DataflowLinkOptions declinedLinkOptions = null)
        {
            if (builder == null) return null;
            if (action == null) throw new ArgumentNullException("action");
            if (predicate == null) throw new ArgumentNullException("predicate");

            var actionBlock = new ActionBlock<TOutput>(action, blockOptions ?? DataflowDefaultOptions.DefaultBlockOptions);
            var nextBuilder = builder.LinkToTarget(actionBlock, linkOptions, predicate);

            if (declinedAction != null)
            {
                // LinkTo declined target
                var declinedActionBlock = new ActionBlock<TOutput>(declinedAction, declinedBlockOptions ?? DataflowDefaultOptions.DefaultBlockOptions);
                LinkHelper.Link((builder as PropagatorDataflowBuilder<TInput, TOutput>).FinalSourceBlock, declinedActionBlock, declinedLinkOptions ?? DataflowDefaultOptions.DefaultLinkOptions);
            }

            return nextBuilder;

        }

        /// <summary>
        /// Links to a target action.
        /// </summary>
        /// <typeparam name="TInput"></typeparam>
        /// <typeparam name="TOutput"></typeparam>
        /// <param name="builder"></param>
        /// <param name="action"></param>
        /// <param name="blockOptions"></param>
        /// <param name="predicate"></param>
        /// <param name="declinedAction"></param>
        /// <param name="declinedBlockOptions"></param>
        /// <returns></returns>
        public static ITargetDataflowBuilder<TInput> LinkToTarget<TInput, TOutput>(
            this IPropagatorDataflowBuilder<TInput, TOutput> builder
            , Action<TOutput> action
            , ExecutionDataflowBlockOptions blockOptions
            , Predicate<TOutput> predicate
            , Action<TOutput> declinedAction
            , ExecutionDataflowBlockOptions declinedBlockOptions = null)
        {
            return builder.LinkToTarget(action, DataflowDefaultOptions.DefaultBlockOptions, DataflowDefaultOptions.DefaultLinkOptions, predicate, declinedAction, declinedBlockOptions);
        }

        /// <summary>
        /// Links to a target action.
        /// </summary>
        /// <typeparam name="TInput"></typeparam>
        /// <typeparam name="TOutput"></typeparam>
        /// <param name="builder"></param>
        /// <param name="action"></param>
        /// <param name="blockOptions"></param>
        /// <param name="linkOptions"></param>
        /// <param name="predicate"></param>
        /// <param name="declinedAction"></param>
        /// <param name="declinedBlockOptions"></param>
        /// <param name="declinedLinkOptions"></param>
        /// <returns></returns>
        public static ITargetDataflowBuilder<TInput> LinkToTarget<TInput, TOutput>(
            this IPropagatorDataflowBuilder<TInput, TOutput> builder
            , Action<TOutput> action
            , ExecutionDataflowBlockOptions blockOptions
            , DataflowLinkOptions linkOptions
            , Predicate<TOutput> predicate
            , Func<TOutput, Task> declinedAction
            , ExecutionDataflowBlockOptions declinedBlockOptions = null
            , DataflowLinkOptions declinedLinkOptions = null)
        {
            if (builder == null) return null;
            if (action == null) throw new ArgumentNullException("action");
            if (predicate == null) throw new ArgumentNullException("predicate");

            var actionBlock = new ActionBlock<TOutput>(action, blockOptions ?? DataflowDefaultOptions.DefaultBlockOptions);
            var nextBuilder = builder.LinkToTarget(actionBlock, linkOptions, predicate);

            if (declinedAction != null)
            {
                // LinkTo declined target
                var declinedActionBlock = new ActionBlock<TOutput>(declinedAction, declinedBlockOptions ?? DataflowDefaultOptions.DefaultBlockOptions);
                LinkHelper.Link((builder as PropagatorDataflowBuilder<TInput, TOutput>).FinalSourceBlock, declinedActionBlock, declinedLinkOptions ?? DataflowDefaultOptions.DefaultLinkOptions);
            }

            return nextBuilder;

        }

        /// <summary>
        /// Links to a target action.
        /// </summary>
        /// <typeparam name="TInput"></typeparam>
        /// <typeparam name="TOutput"></typeparam>
        /// <param name="builder"></param>
        /// <param name="action"></param>
        /// <param name="blockOptions"></param>
        /// <param name="predicate"></param>
        /// <param name="declinedAction"></param>
        /// <param name="declinedBlockOptions"></param>
        /// <returns></returns>
        public static ITargetDataflowBuilder<TInput> LinkToTarget<TInput, TOutput>(
            this IPropagatorDataflowBuilder<TInput, TOutput> builder
            , Action<TOutput> action
            , ExecutionDataflowBlockOptions blockOptions
            , Predicate<TOutput> predicate
            , Func<TOutput, Task> declinedAction
            , ExecutionDataflowBlockOptions declinedBlockOptions = null)
        {
            return builder.LinkToTarget(action, DataflowDefaultOptions.DefaultBlockOptions, DataflowDefaultOptions.DefaultLinkOptions, predicate, declinedAction, declinedBlockOptions);
        }

        /// <summary>
        /// Links to a target action.
        /// </summary>
        /// <typeparam name="TInput"></typeparam>
        /// <typeparam name="TOutput"></typeparam>
        /// <param name="builder"></param>
        /// <param name="action"></param>
        /// <param name="blockOptions"></param>
        /// <param name="linkOptions"></param>
        /// <param name="predicate"></param>
        /// <param name="declinedTargetBlock"></param>
        /// <param name="declinedLinkOptions"></param>
        /// <returns></returns>
        public static ITargetDataflowBuilder<TInput> LinkToTarget<TInput, TOutput>(
            this IPropagatorDataflowBuilder<TInput, TOutput> builder
            , Func<TOutput, Task> action
            , ExecutionDataflowBlockOptions blockOptions = null
            , DataflowLinkOptions linkOptions = null
            , Predicate<TOutput> predicate = null
            , ITargetBlock<TOutput> declinedTargetBlock = null
            , DataflowLinkOptions declinedLinkOptions = null)
        {
            if (builder == null) return null;
            if (action == null) throw new ArgumentNullException("action");

            var actionBlock = new ActionBlock<TOutput>(action, blockOptions ?? DataflowDefaultOptions.DefaultBlockOptions);
            var nextBuilder = builder.LinkToTarget(actionBlock, linkOptions, predicate);

            if (declinedTargetBlock != null)
            {
                // LinkTo declined target
                LinkHelper.Link((builder as PropagatorDataflowBuilder<TInput, TOutput>).FinalSourceBlock, declinedTargetBlock, declinedLinkOptions ?? DataflowDefaultOptions.DefaultLinkOptions);
            }

            return nextBuilder;
        }

        /// <summary>
        /// Links to a target action.
        /// </summary>
        /// <typeparam name="TInput"></typeparam>
        /// <typeparam name="TOutput"></typeparam>
        /// <param name="builder"></param>
        /// <param name="action"></param>
        /// <param name="blockOptions"></param>
        /// <param name="predicate"></param>
        /// <param name="declinedTargetBlock"></param>
        /// <returns></returns>
        public static ITargetDataflowBuilder<TInput> LinkToTarget<TInput, TOutput>(
            this IPropagatorDataflowBuilder<TInput, TOutput> builder
            , Func<TOutput, Task> action
            , ExecutionDataflowBlockOptions blockOptions
            , Predicate<TOutput> predicate
            , ITargetBlock<TOutput> declinedTargetBlock = null)
        {
            return builder.LinkToTarget(action, blockOptions, DataflowDefaultOptions.DefaultLinkOptions, predicate, declinedTargetBlock);
        }

        /// <summary>
        /// Links to a target action.
        /// </summary>
        /// <typeparam name="TInput"></typeparam>
        /// <typeparam name="TOutput"></typeparam>
        /// <param name="builder"></param>
        /// <param name="action"></param>
        /// <param name="blockOptions"></param>
        /// <param name="linkOptions"></param>
        /// <param name="predicate"></param>
        /// <param name="declinedAction"></param>
        /// <param name="declinedBlockOptions"></param>
        /// <param name="declinedLinkOptions"></param>
        /// <returns></returns>
        public static ITargetDataflowBuilder<TInput> LinkToTarget<TInput, TOutput>(
            this IPropagatorDataflowBuilder<TInput, TOutput> builder
            , Func<TOutput, Task> action
            , ExecutionDataflowBlockOptions blockOptions
            , DataflowLinkOptions linkOptions
            , Predicate<TOutput> predicate
            , Action<TOutput> declinedAction
            , ExecutionDataflowBlockOptions declinedBlockOptions = null
            , DataflowLinkOptions declinedLinkOptions = null)
        {
            if (builder == null) return null;
            if (action == null) throw new ArgumentNullException("action");
            if (predicate == null) throw new ArgumentNullException("predicate");

            var actionBlock = new ActionBlock<TOutput>(action, blockOptions ?? DataflowDefaultOptions.DefaultBlockOptions);
            var nextBuilder = builder.LinkToTarget(actionBlock, linkOptions, predicate);

            if (declinedAction != null)
            {
                // LinkTo declined target
                var declinedActionBlock = new ActionBlock<TOutput>(declinedAction, declinedBlockOptions ?? DataflowDefaultOptions.DefaultBlockOptions);
                LinkHelper.Link((builder as PropagatorDataflowBuilder<TInput, TOutput>).FinalSourceBlock, declinedActionBlock, declinedLinkOptions ?? DataflowDefaultOptions.DefaultLinkOptions);
            }

            return nextBuilder;

        }

        /// <summary>
        /// Links to a target action.
        /// </summary>
        /// <typeparam name="TInput"></typeparam>
        /// <typeparam name="TOutput"></typeparam>
        /// <param name="builder"></param>
        /// <param name="action"></param>
        /// <param name="blockOptions"></param>
        /// <param name="predicate"></param>
        /// <param name="declinedAction"></param>
        /// <param name="declinedBlockOptions"></param>
        /// <returns></returns>
        public static ITargetDataflowBuilder<TInput> LinkToTarget<TInput, TOutput>(
            this IPropagatorDataflowBuilder<TInput, TOutput> builder
            , Func<TOutput, Task> action
            , ExecutionDataflowBlockOptions blockOptions
            , Predicate<TOutput> predicate
            , Action<TOutput> declinedAction
            , ExecutionDataflowBlockOptions declinedBlockOptions = null)
        {
            return builder.LinkToTarget(action, DataflowDefaultOptions.DefaultBlockOptions, DataflowDefaultOptions.DefaultLinkOptions, predicate, declinedAction, declinedBlockOptions);
        }

        /// <summary>
        /// Links to a target action.
        /// </summary>
        /// <typeparam name="TInput"></typeparam>
        /// <typeparam name="TOutput"></typeparam>
        /// <param name="builder"></param>
        /// <param name="action"></param>
        /// <param name="blockOptions"></param>
        /// <param name="linkOptions"></param>
        /// <param name="predicate"></param>
        /// <param name="declinedAction"></param>
        /// <param name="declinedBlockOptions"></param>
        /// <param name="declinedLinkOptions"></param>
        /// <returns></returns>
        public static ITargetDataflowBuilder<TInput> LinkToTarget<TInput, TOutput>(
            this IPropagatorDataflowBuilder<TInput, TOutput> builder
            , Func<TOutput, Task> action
            , ExecutionDataflowBlockOptions blockOptions
            , DataflowLinkOptions linkOptions
            , Predicate<TOutput> predicate
            , Func<TOutput, Task> declinedAction
            , ExecutionDataflowBlockOptions declinedBlockOptions = null
            , DataflowLinkOptions declinedLinkOptions = null)
        {
            if (builder == null) return null;
            if (action == null) throw new ArgumentNullException("action");
            if (predicate == null) throw new ArgumentNullException("predicate");

            var actionBlock = new ActionBlock<TOutput>(action, blockOptions ?? DataflowDefaultOptions.DefaultBlockOptions);
            var nextBuilder = builder.LinkToTarget(actionBlock, linkOptions, predicate);

            if (declinedAction != null)
            {
                // LinkTo declined target
                var declinedActionBlock = new ActionBlock<TOutput>(declinedAction, declinedBlockOptions ?? DataflowDefaultOptions.DefaultBlockOptions);
                LinkHelper.Link((builder as PropagatorDataflowBuilder<TInput, TOutput>).FinalSourceBlock, declinedActionBlock, declinedLinkOptions ?? DataflowDefaultOptions.DefaultLinkOptions);
            }

            return nextBuilder;

        }

        /// <summary>
        /// Links to a target action.
        /// </summary>
        /// <typeparam name="TInput"></typeparam>
        /// <typeparam name="TOutput"></typeparam>
        /// <param name="builder"></param>
        /// <param name="action"></param>
        /// <param name="blockOptions"></param>
        /// <param name="predicate"></param>
        /// <param name="declinedAction"></param>
        /// <param name="declinedBlockOptions"></param>
        /// <returns></returns>
        public static ITargetDataflowBuilder<TInput> LinkToTarget<TInput, TOutput>(
            this IPropagatorDataflowBuilder<TInput, TOutput> builder
            , Func<TOutput, Task> action
            , ExecutionDataflowBlockOptions blockOptions
            , Predicate<TOutput> predicate
            , Func<TOutput, Task> declinedAction
            , ExecutionDataflowBlockOptions declinedBlockOptions = null)
        {
            return builder.LinkToTarget(action, DataflowDefaultOptions.DefaultBlockOptions, DataflowDefaultOptions.DefaultLinkOptions, predicate, declinedAction, declinedBlockOptions);
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
            , DataflowLinkOptions declinedLinkOptions = null)
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
            return builder.LinkToPropagator(propagatorBlock, DataflowDefaultOptions.DefaultLinkOptions, predicate, declinedTargetBlock);
        }

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
        /// <param name="declinedAction"></param>
        /// <param name="declinedBlockOptions"></param>
        /// <param name="declinedLinkOptions"></param>
        /// <returns></returns>
        public static IPropagatorDataflowBuilder<TInput, TOutput2> LinkToPropagator<TInput, TOutput, TOutput2>(
            this IPropagatorDataflowBuilder<TInput, TOutput> builder
            , IPropagatorBlock<TOutput, TOutput2> propagatorBlock
            , DataflowLinkOptions linkOptions
            , Predicate<TOutput> predicate
            , Action<TOutput> declinedAction
            , ExecutionDataflowBlockOptions declinedBlockOptions = null
            , DataflowLinkOptions declinedLinkOptions = null)
        {
            if (builder == null) return null;
            if (propagatorBlock == null) throw new ArgumentNullException("propagatorBlock");
            if (predicate == null) throw new ArgumentNullException("predicate");

            var nextBuilder = builder.LinkToPropagator(propagatorBlock, linkOptions, predicate);

            if (declinedAction != null)
            {
                // LinkTo declined target
                var declinedActionBlock = new ActionBlock<TOutput>(declinedAction, declinedBlockOptions ?? DataflowDefaultOptions.DefaultBlockOptions);
                LinkHelper.Link((builder as PropagatorDataflowBuilder<TInput, TOutput>).FinalSourceBlock, declinedActionBlock, declinedLinkOptions ?? DataflowDefaultOptions.DefaultLinkOptions);
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
        /// <param name="declinedAction"></param>
        /// <param name="declinedBlockOptions"></param>
        /// <returns></returns>
        public static IPropagatorDataflowBuilder<TInput, TOutput2> LinkToPropagator<TInput, TOutput, TOutput2>(
            this IPropagatorDataflowBuilder<TInput, TOutput> builder
            , IPropagatorBlock<TOutput, TOutput2> propagatorBlock
            , Predicate<TOutput> predicate
            , Action<TOutput> declinedAction
            , ExecutionDataflowBlockOptions declinedBlockOptions = null)
        {
            return builder.LinkToPropagator(propagatorBlock, DataflowDefaultOptions.DefaultLinkOptions, predicate, declinedAction, declinedBlockOptions);
        }

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
        /// <param name="declinedAction"></param>
        /// <param name="declinedBlockOptions"></param>
        /// <param name="declinedLinkOptions"></param>
        /// <returns></returns>
        public static IPropagatorDataflowBuilder<TInput, TOutput2> LinkToPropagator<TInput, TOutput, TOutput2>(
            this IPropagatorDataflowBuilder<TInput, TOutput> builder
            , IPropagatorBlock<TOutput, TOutput2> propagatorBlock
            , DataflowLinkOptions linkOptions
            , Predicate<TOutput> predicate
            , Func<TOutput, Task> declinedAction
            , ExecutionDataflowBlockOptions declinedBlockOptions = null
            , DataflowLinkOptions declinedLinkOptions = null)
        {
            if (builder == null) return null;
            if (propagatorBlock == null) throw new ArgumentNullException("propagatorBlock");
            if (predicate == null) throw new ArgumentNullException("predicate");

            var nextBuilder = builder.LinkToPropagator(propagatorBlock, linkOptions, predicate);

            if (declinedAction != null)
            {
                // LinkTo declined target
                var declinedActionBlock = new ActionBlock<TOutput>(declinedAction, declinedBlockOptions ?? DataflowDefaultOptions.DefaultBlockOptions);
                LinkHelper.Link((builder as PropagatorDataflowBuilder<TInput, TOutput>).FinalSourceBlock, declinedActionBlock, declinedLinkOptions ?? DataflowDefaultOptions.DefaultLinkOptions);
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
        /// <param name="declinedAction"></param>
        /// <param name="declinedBlockOptions"></param>
        /// <returns></returns>
        public static IPropagatorDataflowBuilder<TInput, TOutput2> LinkToPropagator<TInput, TOutput, TOutput2>(
            this IPropagatorDataflowBuilder<TInput, TOutput> builder
            , IPropagatorBlock<TOutput, TOutput2> propagatorBlock
            , Predicate<TOutput> predicate
            , Func<TOutput, Task> declinedAction
            , ExecutionDataflowBlockOptions declinedBlockOptions = null)
        {
            return builder.LinkToPropagator(propagatorBlock, DataflowDefaultOptions.DefaultLinkOptions, predicate, declinedAction, declinedBlockOptions);
        }

        #endregion

        #region IPropagatorDataflowBuilder link to transform function

        /// <summary>
        /// Links to a transform function.
        /// </summary>
        /// <typeparam name="TInput"></typeparam>
        /// <typeparam name="TOutput"></typeparam>
        /// <typeparam name="TOutput2"></typeparam>
        /// <param name="builder"></param>
        /// <param name="transform"></param>
        /// <param name="blockOptions"></param>
        /// <param name="linkOptions"></param>
        /// <param name="predicate"></param>
        /// <param name="declinedTargetBlock"></param>
        /// <param name="declinedLinkOptions"></param>
        /// <returns></returns>
        public static IPropagatorDataflowBuilder<TInput, TOutput2> LinkToPropagator<TInput, TOutput, TOutput2>(
            this IPropagatorDataflowBuilder<TInput, TOutput> builder
            , Func<TOutput, TOutput2> transform
            , ExecutionDataflowBlockOptions blockOptions = null
            , DataflowLinkOptions linkOptions = null
            , Predicate<TOutput> predicate = null
            , ITargetBlock<TOutput> declinedTargetBlock = null
            , DataflowLinkOptions declinedLinkOptions = null)
        {
            if (builder == null) return null;
            if (transform == null) throw new ArgumentNullException("transform");

            var transformBlock = new TransformBlock<TOutput, TOutput2>(transform, blockOptions ?? DataflowDefaultOptions.DefaultBlockOptions);
            var nextBuilder = builder.LinkToPropagator(transformBlock, linkOptions, predicate);

            if (declinedTargetBlock != null)
            {
                // LinkTo declined target
                LinkHelper.Link((builder as PropagatorDataflowBuilder<TInput, TOutput>).FinalSourceBlock, declinedTargetBlock, declinedLinkOptions ?? DataflowDefaultOptions.DefaultLinkOptions);
            }

            return nextBuilder;
        }

        /// <summary>
        /// Links to a transform function.
        /// </summary>
        /// <typeparam name="TInput"></typeparam>
        /// <typeparam name="TOutput"></typeparam>
        /// <typeparam name="TOutput2"></typeparam>
        /// <param name="builder"></param>
        /// <param name="transform"></param>
        /// <param name="blockOptions"></param>
        /// <param name="predicate"></param>
        /// <param name="declinedTargetBlock"></param>
        /// <returns></returns>
        public static IPropagatorDataflowBuilder<TInput, TOutput2> LinkToPropagator<TInput, TOutput, TOutput2>(
            this IPropagatorDataflowBuilder<TInput, TOutput> builder
            , Func<TOutput, TOutput2> transform
            , ExecutionDataflowBlockOptions blockOptions
            , Predicate<TOutput> predicate
            , ITargetBlock<TOutput> declinedTargetBlock = null)
        {
            return builder.LinkToPropagator(transform, blockOptions, DataflowDefaultOptions.DefaultLinkOptions, predicate, declinedTargetBlock);
        }

        /// <summary>
        /// Links to a transform function.
        /// </summary>
        /// <typeparam name="TInput"></typeparam>
        /// <typeparam name="TOutput"></typeparam>
        /// <typeparam name="TOutput2"></typeparam>
        /// <param name="builder"></param>
        /// <param name="transform"></param>
        /// <param name="blockOptions"></param>
        /// <param name="linkOptions"></param>
        /// <param name="predicate"></param>
        /// <param name="declinedAction"></param>
        /// <param name="declinedBlockOptions"></param>
        /// <param name="declinedLinkOptions"></param>
        /// <returns></returns>
        public static IPropagatorDataflowBuilder<TInput, TOutput2> LinkToPropagator<TInput, TOutput, TOutput2>(
            this IPropagatorDataflowBuilder<TInput, TOutput> builder
            , Func<TOutput, TOutput2> transform
            , ExecutionDataflowBlockOptions blockOptions = null
            , DataflowLinkOptions linkOptions = null
            , Predicate<TOutput> predicate = null
            , Action<TOutput> declinedAction = null
            , ExecutionDataflowBlockOptions declinedBlockOptions = null
            , DataflowLinkOptions declinedLinkOptions = null)
        {
            if (builder == null) return null;
            if (transform == null) throw new ArgumentNullException("transform");

            var transformBlock = new TransformBlock<TOutput, TOutput2>(transform, blockOptions ?? DataflowDefaultOptions.DefaultBlockOptions);
            var nextBuilder = builder.LinkToPropagator(transformBlock, linkOptions, predicate);

            if (declinedAction != null)
            {
                // LinkTo declined target
                var declinedActionBlock = new ActionBlock<TOutput>(declinedAction, declinedBlockOptions ?? DataflowDefaultOptions.DefaultBlockOptions);
                LinkHelper.Link((builder as PropagatorDataflowBuilder<TInput, TOutput>).FinalSourceBlock, declinedActionBlock, declinedLinkOptions ?? DataflowDefaultOptions.DefaultLinkOptions);
            }

            return nextBuilder;
        }

        /// <summary>
        /// Links to a transform function.
        /// </summary>
        /// <typeparam name="TInput"></typeparam>
        /// <typeparam name="TOutput"></typeparam>
        /// <typeparam name="TOutput2"></typeparam>
        /// <param name="builder"></param>
        /// <param name="transform"></param>
        /// <param name="blockOptions"></param>
        /// <param name="predicate"></param>
        /// <param name="declinedAction"></param>
        /// <param name="declinedBlockOptions"></param>
        /// <returns></returns>
        public static IPropagatorDataflowBuilder<TInput, TOutput2> LinkToPropagator<TInput, TOutput, TOutput2>(
            this IPropagatorDataflowBuilder<TInput, TOutput> builder
            , Func<TOutput, TOutput2> transform
            , ExecutionDataflowBlockOptions blockOptions
            , Predicate<TOutput> predicate
            , Action<TOutput> declinedAction
            , ExecutionDataflowBlockOptions declinedBlockOptions = null)
        {
            return builder.LinkToPropagator(transform, blockOptions, DataflowDefaultOptions.DefaultLinkOptions, predicate, declinedAction, declinedBlockOptions);
        }

        /// <summary>
        /// Links to a transform function.
        /// </summary>
        /// <typeparam name="TInput"></typeparam>
        /// <typeparam name="TOutput"></typeparam>
        /// <typeparam name="TOutput2"></typeparam>
        /// <param name="builder"></param>
        /// <param name="transform"></param>
        /// <param name="blockOptions"></param>
        /// <param name="linkOptions"></param>
        /// <param name="predicate"></param>
        /// <param name="declinedAction"></param>
        /// <param name="declinedBlockOptions"></param>
        /// <param name="declinedLinkOptions"></param>
        /// <returns></returns>
        public static IPropagatorDataflowBuilder<TInput, TOutput2> LinkToPropagator<TInput, TOutput, TOutput2>(
            this IPropagatorDataflowBuilder<TInput, TOutput> builder
            , Func<TOutput, TOutput2> transform
            , ExecutionDataflowBlockOptions blockOptions = null
            , DataflowLinkOptions linkOptions = null
            , Predicate<TOutput> predicate = null
            , Func<TOutput, Task> declinedAction = null
            , ExecutionDataflowBlockOptions declinedBlockOptions = null
            , DataflowLinkOptions declinedLinkOptions = null)
        {
            if (builder == null) return null;
            if (transform == null) throw new ArgumentNullException("transform");

            var transformBlock = new TransformBlock<TOutput, TOutput2>(transform, blockOptions ?? DataflowDefaultOptions.DefaultBlockOptions);
            var nextBuilder = builder.LinkToPropagator(transformBlock, linkOptions, predicate);

            if (declinedAction != null)
            {
                // LinkTo declined target
                var declinedActionBlock = new ActionBlock<TOutput>(declinedAction, declinedBlockOptions ?? DataflowDefaultOptions.DefaultBlockOptions);
                LinkHelper.Link((builder as PropagatorDataflowBuilder<TInput, TOutput>).FinalSourceBlock, declinedActionBlock, declinedLinkOptions ?? DataflowDefaultOptions.DefaultLinkOptions);
            }

            return nextBuilder;
        }

        /// <summary>
        /// Links to a transform function.
        /// </summary>
        /// <typeparam name="TInput"></typeparam>
        /// <typeparam name="TOutput"></typeparam>
        /// <typeparam name="TOutput2"></typeparam>
        /// <param name="builder"></param>
        /// <param name="transform"></param>
        /// <param name="blockOptions"></param>
        /// <param name="predicate"></param>
        /// <param name="declinedAction"></param>
        /// <param name="declinedBlockOptions"></param>
        /// <returns></returns>
        public static IPropagatorDataflowBuilder<TInput, TOutput2> LinkToPropagator<TInput, TOutput, TOutput2>(
            this IPropagatorDataflowBuilder<TInput, TOutput> builder
            , Func<TOutput, TOutput2> transform
            , ExecutionDataflowBlockOptions blockOptions
            , Predicate<TOutput> predicate
            , Func<TOutput, Task> declinedAction
            , ExecutionDataflowBlockOptions declinedBlockOptions = null)
        {
            return builder.LinkToPropagator(transform, blockOptions, DataflowDefaultOptions.DefaultLinkOptions, predicate, declinedAction, declinedBlockOptions);
        }

        /// <summary>
        /// Links to a transform function.
        /// </summary>
        /// <typeparam name="TInput"></typeparam>
        /// <typeparam name="TOutput"></typeparam>
        /// <typeparam name="TOutput2"></typeparam>
        /// <param name="builder"></param>
        /// <param name="transform"></param>
        /// <param name="blockOptions"></param>
        /// <param name="linkOptions"></param>
        /// <param name="predicate"></param>
        /// <param name="declinedTargetBlock"></param>
        /// <param name="declinedLinkOptions"></param>
        /// <returns></returns>
        public static IPropagatorDataflowBuilder<TInput, TOutput2> LinkToPropagator<TInput, TOutput, TOutput2>(
            this IPropagatorDataflowBuilder<TInput, TOutput> builder
            , Func<TOutput, Task<TOutput2>> transform
            , ExecutionDataflowBlockOptions blockOptions = null
            , DataflowLinkOptions linkOptions = null
            , Predicate<TOutput> predicate = null
            , ITargetBlock<TOutput> declinedTargetBlock = null
            , DataflowLinkOptions declinedLinkOptions = null)
        {
            if (builder == null) return null;

            var transformBlock = new TransformBlock<TOutput, TOutput2>(transform, blockOptions ?? DataflowDefaultOptions.DefaultBlockOptions);
            var nextBuilder = builder.LinkToPropagator(transformBlock, linkOptions, predicate);

            if (declinedTargetBlock != null)
            {
                // LinkTo declined target
                LinkHelper.Link((builder as PropagatorDataflowBuilder<TInput, TOutput>).FinalSourceBlock, declinedTargetBlock, declinedLinkOptions ?? DataflowDefaultOptions.DefaultLinkOptions);
            }

            return nextBuilder;
        }

        /// <summary>
        /// Links to a transform function.
        /// </summary>
        /// <typeparam name="TInput"></typeparam>
        /// <typeparam name="TOutput"></typeparam>
        /// <typeparam name="TOutput2"></typeparam>
        /// <param name="builder"></param>
        /// <param name="transform"></param>
        /// <param name="blockOptions"></param>
        /// <param name="predicate"></param>
        /// <param name="declinedTargetBlock"></param>
        /// <returns></returns>
        public static IPropagatorDataflowBuilder<TInput, TOutput2> LinkToPropagator<TInput, TOutput, TOutput2>(
            this IPropagatorDataflowBuilder<TInput, TOutput> builder
            , Func<TOutput, Task<TOutput2>> transform
            , ExecutionDataflowBlockOptions blockOptions
            , Predicate<TOutput> predicate
            , ITargetBlock<TOutput> declinedTargetBlock = null)
        {
            return builder.LinkToPropagator(transform, blockOptions, DataflowDefaultOptions.DefaultLinkOptions, predicate, declinedTargetBlock);
        }

        /// <summary>
        /// Links to a transform function.
        /// </summary>
        /// <typeparam name="TInput"></typeparam>
        /// <typeparam name="TOutput"></typeparam>
        /// <typeparam name="TOutput2"></typeparam>
        /// <param name="builder"></param>
        /// <param name="transform"></param>
        /// <param name="blockOptions"></param>
        /// <param name="linkOptions"></param>
        /// <param name="predicate"></param>
        /// <param name="declinedAction"></param>
        /// <param name="declinedBlockOptions"></param>
        /// <param name="declinedLinkOptions"></param>
        /// <returns></returns>
        public static IPropagatorDataflowBuilder<TInput, TOutput2> LinkToPropagator<TInput, TOutput, TOutput2>(
            this IPropagatorDataflowBuilder<TInput, TOutput> builder
            , Func<TOutput, Task<TOutput2>> transform
            , ExecutionDataflowBlockOptions blockOptions = null
            , DataflowLinkOptions linkOptions = null
            , Predicate<TOutput> predicate = null
            , Action<TOutput> declinedAction = null
            , ExecutionDataflowBlockOptions declinedBlockOptions = null
            , DataflowLinkOptions declinedLinkOptions = null)
        {
            if (builder == null) return null;
            if (transform == null) throw new ArgumentNullException("transform");

            var transformBlock = new TransformBlock<TOutput, TOutput2>(transform, blockOptions ?? DataflowDefaultOptions.DefaultBlockOptions);
            var nextBuilder = builder.LinkToPropagator(transformBlock, linkOptions, predicate);

            if (declinedAction != null)
            {
                // LinkTo declined target
                var declinedActionBlock = new ActionBlock<TOutput>(declinedAction, declinedBlockOptions ?? DataflowDefaultOptions.DefaultBlockOptions);
                LinkHelper.Link((builder as PropagatorDataflowBuilder<TInput, TOutput>).FinalSourceBlock, declinedActionBlock, declinedLinkOptions ?? DataflowDefaultOptions.DefaultLinkOptions);
            }

            return nextBuilder;
        }

        /// <summary>
        /// Links to a transform function.
        /// </summary>
        /// <typeparam name="TInput"></typeparam>
        /// <typeparam name="TOutput"></typeparam>
        /// <typeparam name="TOutput2"></typeparam>
        /// <param name="builder"></param>
        /// <param name="transform"></param>
        /// <param name="blockOptions"></param>
        /// <param name="predicate"></param>
        /// <param name="declinedAction"></param>
        /// <param name="declinedBlockOptions"></param>
        /// <returns></returns>
        public static IPropagatorDataflowBuilder<TInput, TOutput2> LinkToPropagator<TInput, TOutput, TOutput2>(
            this IPropagatorDataflowBuilder<TInput, TOutput> builder
            , Func<TOutput, Task<TOutput2>> transform
            , ExecutionDataflowBlockOptions blockOptions
            , Predicate<TOutput> predicate
            , Action<TOutput> declinedAction
            , ExecutionDataflowBlockOptions declinedBlockOptions = null)
        {
            return builder.LinkToPropagator(transform, blockOptions, DataflowDefaultOptions.DefaultLinkOptions, predicate, declinedAction, declinedBlockOptions);
        }

        /// <summary>
        /// Links to a transform function.
        /// </summary>
        /// <typeparam name="TInput"></typeparam>
        /// <typeparam name="TOutput"></typeparam>
        /// <typeparam name="TOutput2"></typeparam>
        /// <param name="builder"></param>
        /// <param name="transform"></param>
        /// <param name="blockOptions"></param>
        /// <param name="linkOptions"></param>
        /// <param name="predicate"></param>
        /// <param name="declinedAction"></param>
        /// <param name="declinedBlockOptions"></param>
        /// <param name="declinedLinkOptions"></param>
        /// <returns></returns>
        public static IPropagatorDataflowBuilder<TInput, TOutput2> LinkToPropagator<TInput, TOutput, TOutput2>(
            this IPropagatorDataflowBuilder<TInput, TOutput> builder
            , Func<TOutput, Task<TOutput2>> transform
            , ExecutionDataflowBlockOptions blockOptions = null
            , DataflowLinkOptions linkOptions = null
            , Predicate<TOutput> predicate = null
            , Func<TOutput, Task> declinedAction = null
            , ExecutionDataflowBlockOptions declinedBlockOptions = null
            , DataflowLinkOptions declinedLinkOptions = null)
        {
            if (builder == null) return null;
            if (transform == null) throw new ArgumentNullException("transform");

            var transformBlock = new TransformBlock<TOutput, TOutput2>(transform, blockOptions ?? DataflowDefaultOptions.DefaultBlockOptions);
            var nextBuilder = builder.LinkToPropagator(transformBlock, linkOptions, predicate);

            if (declinedAction != null)
            {
                // LinkTo declined target
                var declinedActionBlock = new ActionBlock<TOutput>(declinedAction, declinedBlockOptions ?? DataflowDefaultOptions.DefaultBlockOptions);
                LinkHelper.Link((builder as PropagatorDataflowBuilder<TInput, TOutput>).FinalSourceBlock, declinedActionBlock, declinedLinkOptions ?? DataflowDefaultOptions.DefaultLinkOptions);
            }

            return nextBuilder;
        }

        /// <summary>
        /// Links to a transform function.
        /// </summary>
        /// <typeparam name="TInput"></typeparam>
        /// <typeparam name="TOutput"></typeparam>
        /// <typeparam name="TOutput2"></typeparam>
        /// <param name="builder"></param>
        /// <param name="transform"></param>
        /// <param name="blockOptions"></param>
        /// <param name="predicate"></param>
        /// <param name="declinedAction"></param>
        /// <param name="declinedBlockOptions"></param>
        /// <returns></returns>
        public static IPropagatorDataflowBuilder<TInput, TOutput2> LinkToPropagator<TInput, TOutput, TOutput2>(
            this IPropagatorDataflowBuilder<TInput, TOutput> builder
            , Func<TOutput, Task<TOutput2>> transform
            , ExecutionDataflowBlockOptions blockOptions
            , Predicate<TOutput> predicate
            , Func<TOutput, Task> declinedAction
            , ExecutionDataflowBlockOptions declinedBlockOptions = null)
        {
            return builder.LinkToPropagator(transform, blockOptions, DataflowDefaultOptions.DefaultLinkOptions, predicate, declinedAction, declinedBlockOptions);
        }

        #endregion

        #region IPropagatorDataflowBuilder link to transformMany function

        /// <summary>
        /// Links to a transformMany function.
        /// </summary>
        /// <typeparam name="TInput"></typeparam>
        /// <typeparam name="TOutput"></typeparam>
        /// <typeparam name="TOutput2"></typeparam>
        /// <param name="builder"></param>
        /// <param name="transformMany"></param>
        /// <param name="blockOptions"></param>
        /// <param name="linkOptions"></param>
        /// <param name="predicate"></param>
        /// <param name="declinedTargetBlock"></param>
        /// <param name="declinedLinkOptions"></param>
        /// <returns></returns>
        public static IPropagatorDataflowBuilder<TInput, TOutput2> LinkToPropagator<TInput, TOutput, TOutput2>(
            this IPropagatorDataflowBuilder<TInput, TOutput> builder
            , Func<TOutput, IEnumerable<TOutput2>> transformMany
            , ExecutionDataflowBlockOptions blockOptions = null
            , DataflowLinkOptions linkOptions = null
            , Predicate<TOutput> predicate = null
            , ITargetBlock<TOutput> declinedTargetBlock = null
            , DataflowLinkOptions declinedLinkOptions = null)
        {
            if (builder == null) return null;

            var transformManyBlock = new TransformManyBlock<TOutput, TOutput2>(transformMany, blockOptions ?? DataflowDefaultOptions.DefaultBlockOptions);
            var nextBuilder = builder.LinkToPropagator(transformManyBlock, linkOptions, predicate);

            if (declinedTargetBlock != null)
            {
                // LinkTo declined target
                LinkHelper.Link((builder as PropagatorDataflowBuilder<TInput, TOutput>).FinalSourceBlock, declinedTargetBlock, declinedLinkOptions ?? DataflowDefaultOptions.DefaultLinkOptions);
            }

            return nextBuilder;
        }

        /// <summary>
        /// Links to a transformMany function.
        /// </summary>
        /// <typeparam name="TInput"></typeparam>
        /// <typeparam name="TOutput"></typeparam>
        /// <typeparam name="TOutput2"></typeparam>
        /// <param name="builder"></param>
        /// <param name="transformMany"></param>
        /// <param name="blockOptions"></param>
        /// <param name="predicate"></param>
        /// <param name="declinedTargetBlock"></param>
        /// <returns></returns>
        public static IPropagatorDataflowBuilder<TInput, TOutput2> LinkToPropagator<TInput, TOutput, TOutput2>(
            this IPropagatorDataflowBuilder<TInput, TOutput> builder
            , Func<TOutput, IEnumerable<TOutput2>> transformMany
            , ExecutionDataflowBlockOptions blockOptions
            , Predicate<TOutput> predicate
            , ITargetBlock<TOutput> declinedTargetBlock = null)
        {
            return builder.LinkToPropagator(transformMany, blockOptions, DataflowDefaultOptions.DefaultLinkOptions, predicate, declinedTargetBlock);
        }

        /// <summary>
        /// Links to a transformMany function.
        /// </summary>
        /// <typeparam name="TInput"></typeparam>
        /// <typeparam name="TOutput"></typeparam>
        /// <typeparam name="TOutput2"></typeparam>
        /// <param name="builder"></param>
        /// <param name="transformMany"></param>
        /// <param name="blockOptions"></param>
        /// <param name="linkOptions"></param>
        /// <param name="predicate"></param>
        /// <param name="declinedAction"></param>
        /// <param name="declinedBlockOptions"></param>
        /// <param name="declinedLinkOptions"></param>
        /// <returns></returns>
        public static IPropagatorDataflowBuilder<TInput, TOutput2> LinkToPropagator<TInput, TOutput, TOutput2>(
            this IPropagatorDataflowBuilder<TInput, TOutput> builder
            , Func<TOutput, IEnumerable<TOutput2>> transformMany
            , ExecutionDataflowBlockOptions blockOptions = null
            , DataflowLinkOptions linkOptions = null
            , Predicate<TOutput> predicate = null
            , Action<TOutput> declinedAction = null
            , ExecutionDataflowBlockOptions declinedBlockOptions = null
            , DataflowLinkOptions declinedLinkOptions = null)
        {
            if (builder == null) return null;

            var transformManyBlock = new TransformManyBlock<TOutput, TOutput2>(transformMany, blockOptions ?? DataflowDefaultOptions.DefaultBlockOptions);
            var nextBuilder = builder.LinkToPropagator(transformManyBlock, linkOptions, predicate);

            if (declinedAction != null)
            {
                // LinkTo declined target
                var declinedActionBlock = new ActionBlock<TOutput>(declinedAction, declinedBlockOptions ?? DataflowDefaultOptions.DefaultBlockOptions);
                LinkHelper.Link((builder as PropagatorDataflowBuilder<TInput, TOutput>).FinalSourceBlock, declinedActionBlock, declinedLinkOptions ?? DataflowDefaultOptions.DefaultLinkOptions);
            }

            return nextBuilder;
        }

        /// <summary>
        /// Links to a transformMany function.
        /// </summary>
        /// <typeparam name="TInput"></typeparam>
        /// <typeparam name="TOutput"></typeparam>
        /// <typeparam name="TOutput2"></typeparam>
        /// <param name="builder"></param>
        /// <param name="transformMany"></param>
        /// <param name="blockOptions"></param>
        /// <param name="predicate"></param>
        /// <param name="declinedAction"></param>
        /// <param name="declinedBlockOptions"></param>
        /// <returns></returns>
        public static IPropagatorDataflowBuilder<TInput, TOutput2> LinkToPropagator<TInput, TOutput, TOutput2>(
            this IPropagatorDataflowBuilder<TInput, TOutput> builder
            , Func<TOutput, IEnumerable<TOutput2>> transformMany
            , ExecutionDataflowBlockOptions blockOptions
            , Predicate<TOutput> predicate
            , Action<TOutput> declinedAction
            , ExecutionDataflowBlockOptions declinedBlockOptions = null)
        {
            return builder.LinkToPropagator(transformMany, blockOptions, DataflowDefaultOptions.DefaultLinkOptions, predicate, declinedAction, declinedBlockOptions);
        }

        /// <summary>
        /// Links to a transformMany function.
        /// </summary>
        /// <typeparam name="TInput"></typeparam>
        /// <typeparam name="TOutput"></typeparam>
        /// <typeparam name="TOutput2"></typeparam>
        /// <param name="builder"></param>
        /// <param name="transformMany"></param>
        /// <param name="blockOptions"></param>
        /// <param name="linkOptions"></param>
        /// <param name="predicate"></param>
        /// <param name="declinedAction"></param>
        /// <param name="declinedBlockOptions"></param>
        /// <param name="declinedLinkOptions"></param>
        /// <returns></returns>
        public static IPropagatorDataflowBuilder<TInput, TOutput2> LinkToPropagator<TInput, TOutput, TOutput2>(
            this IPropagatorDataflowBuilder<TInput, TOutput> builder
            , Func<TOutput, IEnumerable<TOutput2>> transformMany
            , ExecutionDataflowBlockOptions blockOptions = null
            , DataflowLinkOptions linkOptions = null
            , Predicate<TOutput> predicate = null
            , Func<TOutput, Task> declinedAction = null
            , ExecutionDataflowBlockOptions declinedBlockOptions = null
            , DataflowLinkOptions declinedLinkOptions = null)
        {
            if (builder == null) return null;

            var transformManyBlock = new TransformManyBlock<TOutput, TOutput2>(transformMany, blockOptions ?? DataflowDefaultOptions.DefaultBlockOptions);
            var nextBuilder = builder.LinkToPropagator(transformManyBlock, linkOptions, predicate);

            if (declinedAction != null)
            {
                // LinkTo declined target
                var declinedActionBlock = new ActionBlock<TOutput>(declinedAction, declinedBlockOptions ?? DataflowDefaultOptions.DefaultBlockOptions);
                LinkHelper.Link((builder as PropagatorDataflowBuilder<TInput, TOutput>).FinalSourceBlock, declinedActionBlock, declinedLinkOptions ?? DataflowDefaultOptions.DefaultLinkOptions);
            }

            return nextBuilder;
        }

        /// <summary>
        /// Links to a transformMany function.
        /// </summary>
        /// <typeparam name="TInput"></typeparam>
        /// <typeparam name="TOutput"></typeparam>
        /// <typeparam name="TOutput2"></typeparam>
        /// <param name="builder"></param>
        /// <param name="transformMany"></param>
        /// <param name="blockOptions"></param>
        /// <param name="predicate"></param>
        /// <param name="declinedAction"></param>
        /// <param name="declinedBlockOptions"></param>
        /// <returns></returns>
        public static IPropagatorDataflowBuilder<TInput, TOutput2> LinkToPropagator<TInput, TOutput, TOutput2>(
            this IPropagatorDataflowBuilder<TInput, TOutput> builder
            , Func<TOutput, IEnumerable<TOutput2>> transformMany
            , ExecutionDataflowBlockOptions blockOptions
            , Predicate<TOutput> predicate
            , Func<TOutput, Task> declinedAction
            , ExecutionDataflowBlockOptions declinedBlockOptions = null)
        {
            return builder.LinkToPropagator(transformMany, blockOptions, DataflowDefaultOptions.DefaultLinkOptions, predicate, declinedAction, declinedBlockOptions);
        }

        /// <summary>
        /// Links to a transformMany function.
        /// </summary>
        /// <typeparam name="TInput"></typeparam>
        /// <typeparam name="TOutput"></typeparam>
        /// <typeparam name="TOutput2"></typeparam>
        /// <param name="builder"></param>
        /// <param name="transformMany"></param>
        /// <param name="blockOptions"></param>
        /// <param name="linkOptions"></param>
        /// <param name="predicate"></param>
        /// <param name="declinedTargetBlock"></param>
        /// <param name="declinedLinkOptions"></param>
        /// <returns></returns>
        public static IPropagatorDataflowBuilder<TInput, TOutput2> LinkToPropagator<TInput, TOutput, TOutput2>(
            this IPropagatorDataflowBuilder<TInput, TOutput> builder
            , Func<TOutput, Task<IEnumerable<TOutput2>>> transformMany
            , ExecutionDataflowBlockOptions blockOptions = null
            , DataflowLinkOptions linkOptions = null
            , Predicate<TOutput> predicate = null
            , ITargetBlock<TOutput> declinedTargetBlock = null
            , DataflowLinkOptions declinedLinkOptions = null)
        {
            if (builder == null) return null;

            var transformManyBlock = new TransformManyBlock<TOutput, TOutput2>(transformMany, blockOptions ?? DataflowDefaultOptions.DefaultBlockOptions);
            var nextBuilder = builder.LinkToPropagator(transformManyBlock, linkOptions, predicate);

            if (declinedTargetBlock != null)
            {
                // LinkTo declined target
                LinkHelper.Link((builder as PropagatorDataflowBuilder<TInput, TOutput>).FinalSourceBlock, declinedTargetBlock, declinedLinkOptions ?? DataflowDefaultOptions.DefaultLinkOptions);
            }

            return nextBuilder;
        }

        /// <summary>
        /// Links to a transformMany function.
        /// </summary>
        /// <typeparam name="TInput"></typeparam>
        /// <typeparam name="TOutput"></typeparam>
        /// <typeparam name="TOutput2"></typeparam>
        /// <param name="builder"></param>
        /// <param name="transformMany"></param>
        /// <param name="blockOptions"></param>
        /// <param name="predicate"></param>
        /// <param name="declinedTargetBlock"></param>
        /// <returns></returns>
        public static IPropagatorDataflowBuilder<TInput, TOutput2> LinkToPropagator<TInput, TOutput, TOutput2>(
            this IPropagatorDataflowBuilder<TInput, TOutput> builder
            , Func<TOutput, Task<IEnumerable<TOutput2>>> transformMany
            , ExecutionDataflowBlockOptions blockOptions
            , Predicate<TOutput> predicate
            , ITargetBlock<TOutput> declinedTargetBlock = null)
        {
            return builder.LinkToPropagator(transformMany, blockOptions, DataflowDefaultOptions.DefaultLinkOptions, predicate, declinedTargetBlock);
        }

        /// <summary>
        /// Links to a transformMany function.
        /// </summary>
        /// <typeparam name="TInput"></typeparam>
        /// <typeparam name="TOutput"></typeparam>
        /// <typeparam name="TOutput2"></typeparam>
        /// <param name="builder"></param>
        /// <param name="transformMany"></param>
        /// <param name="blockOptions"></param>
        /// <param name="linkOptions"></param>
        /// <param name="predicate"></param>
        /// <param name="declinedAction"></param>
        /// <param name="declinedBlockOptions"></param>
        /// <param name="declinedLinkOptions"></param>
        /// <returns></returns>
        public static IPropagatorDataflowBuilder<TInput, TOutput2> LinkToPropagator<TInput, TOutput, TOutput2>(
            this IPropagatorDataflowBuilder<TInput, TOutput> builder
            , Func<TOutput, Task<IEnumerable<TOutput2>>> transformMany
            , ExecutionDataflowBlockOptions blockOptions = null
            , DataflowLinkOptions linkOptions = null
            , Predicate<TOutput> predicate = null
            , Action<TOutput> declinedAction = null
            , ExecutionDataflowBlockOptions declinedBlockOptions = null
            , DataflowLinkOptions declinedLinkOptions = null)
        {
            if (builder == null) return null;

            var transformManyBlock = new TransformManyBlock<TOutput, TOutput2>(transformMany, blockOptions ?? DataflowDefaultOptions.DefaultBlockOptions);
            var nextBuilder = builder.LinkToPropagator(transformManyBlock, linkOptions, predicate);

            if (declinedAction != null)
            {
                // LinkTo declined target
                var declinedActionBlock = new ActionBlock<TOutput>(declinedAction, declinedBlockOptions ?? DataflowDefaultOptions.DefaultBlockOptions);
                LinkHelper.Link((builder as PropagatorDataflowBuilder<TInput, TOutput>).FinalSourceBlock, declinedActionBlock, declinedLinkOptions ?? DataflowDefaultOptions.DefaultLinkOptions);
            }

            return nextBuilder;
        }

        /// <summary>
        /// Links to a transformMany function.
        /// </summary>
        /// <typeparam name="TInput"></typeparam>
        /// <typeparam name="TOutput"></typeparam>
        /// <typeparam name="TOutput2"></typeparam>
        /// <param name="builder"></param>
        /// <param name="transformMany"></param>
        /// <param name="blockOptions"></param>
        /// <param name="predicate"></param>
        /// <param name="declinedAction"></param>
        /// <param name="declinedBlockOptions"></param>
        /// <returns></returns>
        public static IPropagatorDataflowBuilder<TInput, TOutput2> LinkToPropagator<TInput, TOutput, TOutput2>(
            this IPropagatorDataflowBuilder<TInput, TOutput> builder
            , Func<TOutput, Task<IEnumerable<TOutput2>>> transformMany
            , ExecutionDataflowBlockOptions blockOptions
            , Predicate<TOutput> predicate
            , Action<TOutput> declinedAction
            , ExecutionDataflowBlockOptions declinedBlockOptions = null)
        {
            return builder.LinkToPropagator(transformMany, blockOptions, DataflowDefaultOptions.DefaultLinkOptions, predicate, declinedAction, declinedBlockOptions);
        }

        /// <summary>
        /// Links to a transformMany function.
        /// </summary>
        /// <typeparam name="TInput"></typeparam>
        /// <typeparam name="TOutput"></typeparam>
        /// <typeparam name="TOutput2"></typeparam>
        /// <param name="builder"></param>
        /// <param name="transformMany"></param>
        /// <param name="blockOptions"></param>
        /// <param name="linkOptions"></param>
        /// <param name="predicate"></param>
        /// <param name="declinedAction"></param>
        /// <param name="declinedBlockOptions"></param>
        /// <param name="declinedLinkOptions"></param>
        /// <returns></returns>
        public static IPropagatorDataflowBuilder<TInput, TOutput2> LinkToPropagator<TInput, TOutput, TOutput2>(
            this IPropagatorDataflowBuilder<TInput, TOutput> builder
            , Func<TOutput, Task<IEnumerable<TOutput2>>> transformMany
            , ExecutionDataflowBlockOptions blockOptions = null
            , DataflowLinkOptions linkOptions = null
            , Predicate<TOutput> predicate = null
            , Func<TOutput, Task> declinedAction = null
            , ExecutionDataflowBlockOptions declinedBlockOptions = null
            , DataflowLinkOptions declinedLinkOptions = null)
        {
            if (builder == null) return null;

            var transformManyBlock = new TransformManyBlock<TOutput, TOutput2>(transformMany, blockOptions ?? DataflowDefaultOptions.DefaultBlockOptions);
            var nextBuilder = builder.LinkToPropagator(transformManyBlock, linkOptions, predicate);

            if (declinedAction != null)
            {
                // LinkTo declined target
                var declinedActionBlock = new ActionBlock<TOutput>(declinedAction, declinedBlockOptions ?? DataflowDefaultOptions.DefaultBlockOptions);
                LinkHelper.Link((builder as PropagatorDataflowBuilder<TInput, TOutput>).FinalSourceBlock, declinedActionBlock, declinedLinkOptions ?? DataflowDefaultOptions.DefaultLinkOptions);
            }

            return nextBuilder;
        }

        /// <summary>
        /// Links to a transformMany function.
        /// </summary>
        /// <typeparam name="TInput"></typeparam>
        /// <typeparam name="TOutput"></typeparam>
        /// <typeparam name="TOutput2"></typeparam>
        /// <param name="builder"></param>
        /// <param name="transformMany"></param>
        /// <param name="blockOptions"></param>
        /// <param name="predicate"></param>
        /// <param name="declinedAction"></param>
        /// <param name="declinedBlockOptions"></param>
        /// <returns></returns>
        public static IPropagatorDataflowBuilder<TInput, TOutput2> LinkToPropagator<TInput, TOutput, TOutput2>(
            this IPropagatorDataflowBuilder<TInput, TOutput> builder
            , Func<TOutput, Task<IEnumerable<TOutput2>>> transformMany
            , ExecutionDataflowBlockOptions blockOptions
            , Predicate<TOutput> predicate
            , Func<TOutput, Task> declinedAction
            , ExecutionDataflowBlockOptions declinedBlockOptions = null)
        {
            return builder.LinkToPropagator(transformMany, blockOptions, DataflowDefaultOptions.DefaultLinkOptions, predicate, declinedAction, declinedBlockOptions);
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
            , DataflowLinkOptions declinedLinkOptions = null)
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
            return builder.LinkToTarget(targetBlock, DataflowDefaultOptions.DefaultLinkOptions, predicate, declinedTargetBlock);
        }

        /// <summary>
        /// Links to a target block.
        /// </summary>
        /// <typeparam name="TOutput"></typeparam>
        /// <param name="builder"></param>
        /// <param name="targetBlock"></param>
        /// <param name="linkOptions"></param>
        /// <param name="predicate"></param>
        /// <param name="declinedAction"></param>
        /// <param name="declinedBlockOptions"></param>
        /// <param name="declinedLinkOptions"></param>
        /// <returns></returns>
        public static IDataflowBuilder LinkToTarget<TOutput>(
            this ISourceDataflowBuilder<TOutput> builder
            , ITargetBlock<TOutput> targetBlock
            , DataflowLinkOptions linkOptions
            , Predicate<TOutput> predicate
            , Action<TOutput> declinedAction
            , ExecutionDataflowBlockOptions declinedBlockOptions = null
            , DataflowLinkOptions declinedLinkOptions = null)
        {
            if (builder == null) return null;
            if (targetBlock == null) throw new ArgumentNullException("targetBlock");
            if (predicate == null) throw new ArgumentNullException("predicate");

            var nextBuilder = builder.LinkToTarget(targetBlock, linkOptions, predicate);

            if (declinedAction != null)
            {
                // LinkTo declined target
                var declinedActionBlock = new ActionBlock<TOutput>(declinedAction, declinedBlockOptions ?? DataflowDefaultOptions.DefaultBlockOptions);
                LinkHelper.Link((builder as SourceDataflowBuilder<TOutput>).FinalSourceBlock, declinedActionBlock, declinedLinkOptions ?? DataflowDefaultOptions.DefaultLinkOptions);
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
        /// <param name="declinedAction"></param>
        /// <param name="declinedBlockOptions"></param>
        /// <returns></returns>
        public static IDataflowBuilder LinkToTarget<TOutput>(
            this ISourceDataflowBuilder<TOutput> builder
            , ITargetBlock<TOutput> targetBlock
            , Predicate<TOutput> predicate
            , Action<TOutput> declinedAction
            , ExecutionDataflowBlockOptions declinedBlockOptions = null)
        {
            return builder.LinkToTarget(targetBlock, DataflowDefaultOptions.DefaultLinkOptions, predicate, declinedAction, declinedBlockOptions);
        }

        /// <summary>
        /// Links to a target block.
        /// </summary>
        /// <typeparam name="TOutput"></typeparam>
        /// <param name="builder"></param>
        /// <param name="targetBlock"></param>
        /// <param name="linkOptions"></param>
        /// <param name="predicate"></param>
        /// <param name="declinedAction"></param>
        /// <param name="declinedBlockOptions"></param>
        /// <param name="declinedLinkOptions"></param>
        /// <returns></returns>
        public static IDataflowBuilder LinkToTarget<TOutput>(
            this ISourceDataflowBuilder<TOutput> builder
            , ITargetBlock<TOutput> targetBlock
            , DataflowLinkOptions linkOptions
            , Predicate<TOutput> predicate
            , Func<TOutput, Task> declinedAction
            , ExecutionDataflowBlockOptions declinedBlockOptions = null
            , DataflowLinkOptions declinedLinkOptions = null)
        {
            if (builder == null) return null;
            if (targetBlock == null) throw new ArgumentNullException("targetBlock");
            if (predicate == null) throw new ArgumentNullException("predicate");

            var nextBuilder = builder.LinkToTarget(targetBlock, linkOptions, predicate);

            if (declinedAction != null)
            {
                // LinkTo declined target
                var declinedActionBlock = new ActionBlock<TOutput>(declinedAction, declinedBlockOptions ?? DataflowDefaultOptions.DefaultBlockOptions);
                LinkHelper.Link((builder as SourceDataflowBuilder<TOutput>).FinalSourceBlock, declinedActionBlock, declinedLinkOptions ?? DataflowDefaultOptions.DefaultLinkOptions);
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
        /// <param name="declinedAction"></param>
        /// <param name="declinedBlockOptions"></param>
        /// <returns></returns>
        public static IDataflowBuilder LinkToTarget<TOutput>(
            this ISourceDataflowBuilder<TOutput> builder
            , ITargetBlock<TOutput> targetBlock
            , Predicate<TOutput> predicate
            , Func<TOutput, Task> declinedAction
            , ExecutionDataflowBlockOptions declinedBlockOptions = null)
        {
            return builder.LinkToTarget(targetBlock, DataflowDefaultOptions.DefaultLinkOptions, predicate, declinedAction, declinedBlockOptions);
        }

        #endregion

        #region ISourceDataflowBuilder link to action

        /// <summary>
        /// Links to a target action.
        /// </summary>
        /// <typeparam name="TOutput"></typeparam>
        /// <param name="builder"></param>
        /// <param name="action"></param>
        /// <param name="blockOptions"></param>
        /// <param name="linkOptions"></param>
        /// <param name="predicate"></param>
        /// <param name="declinedTargetBlock"></param>
        /// <param name="declinedLinkOptions"></param>
        /// <returns></returns>
        public static IDataflowBuilder LinkToTarget<TOutput>(
            this ISourceDataflowBuilder<TOutput> builder
            , Action<TOutput> action
            , ExecutionDataflowBlockOptions blockOptions = null
            , DataflowLinkOptions linkOptions = null
            , Predicate<TOutput> predicate = null
            , ITargetBlock<TOutput> declinedTargetBlock = null
            , DataflowLinkOptions declinedLinkOptions = null)
        {
            if (builder == null) return null;

            var actionBlock = new ActionBlock<TOutput>(action, blockOptions ?? DataflowDefaultOptions.DefaultBlockOptions);
            var nextBuilder = builder.LinkToTarget(actionBlock, linkOptions, predicate);

            if (declinedTargetBlock != null)
            {
                // LinkTo declined target
                LinkHelper.Link((builder as SourceDataflowBuilder<TOutput>).FinalSourceBlock, declinedTargetBlock, declinedLinkOptions ?? DataflowDefaultOptions.DefaultLinkOptions);
            }

            return nextBuilder;
        }

        /// <summary>
        /// Links to a target action.
        /// </summary>
        /// <typeparam name="TOutput"></typeparam>
        /// <param name="builder"></param>
        /// <param name="action"></param>
        /// <param name="blockOptions"></param>
        /// <param name="predicate"></param>
        /// <param name="declinedTargetBlock"></param>
        /// <returns></returns>
        public static IDataflowBuilder LinkToTarget<TOutput>(
            this ISourceDataflowBuilder<TOutput> builder
            , Action<TOutput> action
            , ExecutionDataflowBlockOptions blockOptions
            , Predicate<TOutput> predicate
            , ITargetBlock<TOutput> declinedTargetBlock = null)
        {
            return builder.LinkToTarget(action, blockOptions, DataflowDefaultOptions.DefaultLinkOptions, predicate, declinedTargetBlock);
        }

        /// <summary>
        /// Links to a target action.
        /// </summary>
        /// <typeparam name="TOutput"></typeparam>
        /// <param name="builder"></param>
        /// <param name="action"></param>
        /// <param name="blockOptions"></param>
        /// <param name="linkOptions"></param>
        /// <param name="predicate"></param>
        /// <param name="declinedAction"></param>
        /// <param name="declinedBlockOptions"></param>
        /// <param name="declinedLinkOptions"></param>
        /// <returns></returns>
        public static IDataflowBuilder LinkToTarget<TOutput>(
            this ISourceDataflowBuilder<TOutput> builder
            , Action<TOutput> action
            , ExecutionDataflowBlockOptions blockOptions
            , DataflowLinkOptions linkOptions
            , Predicate<TOutput> predicate
            , Action<TOutput> declinedAction
            , ExecutionDataflowBlockOptions declinedBlockOptions = null
            , DataflowLinkOptions declinedLinkOptions = null)
        {
            if (builder == null) return null;
            if (action == null) throw new ArgumentNullException("action");
            if (predicate == null) throw new ArgumentNullException("predicate");

            var actionBlock = new ActionBlock<TOutput>(action, blockOptions ?? DataflowDefaultOptions.DefaultBlockOptions);
            var nextBuilder = builder.LinkToTarget(actionBlock, linkOptions, predicate);

            if (declinedAction != null)
            {
                // LinkTo declined target
                var declinedActionBlock = new ActionBlock<TOutput>(declinedAction, declinedBlockOptions ?? DataflowDefaultOptions.DefaultBlockOptions);
                LinkHelper.Link((builder as SourceDataflowBuilder<TOutput>).FinalSourceBlock, declinedActionBlock, declinedLinkOptions ?? DataflowDefaultOptions.DefaultLinkOptions);
            }

            return nextBuilder;

        }

        /// <summary>
        /// Links to a target action.
        /// </summary>
        /// <typeparam name="TOutput"></typeparam>
        /// <param name="builder"></param>
        /// <param name="action"></param>
        /// <param name="blockOptions"></param>
        /// <param name="predicate"></param>
        /// <param name="declinedAction"></param>
        /// <param name="declinedBlockOptions"></param>
        /// <returns></returns>
        public static IDataflowBuilder LinkToTarget<TOutput>(
            this ISourceDataflowBuilder<TOutput> builder
            , Action<TOutput> action
            , ExecutionDataflowBlockOptions blockOptions
            , Predicate<TOutput> predicate
            , Action<TOutput> declinedAction
            , ExecutionDataflowBlockOptions declinedBlockOptions = null)
        {
            return builder.LinkToTarget(action, DataflowDefaultOptions.DefaultBlockOptions, DataflowDefaultOptions.DefaultLinkOptions, predicate, declinedAction, declinedBlockOptions);
        }

        /// <summary>
        /// Links to a target action.
        /// </summary>
        /// <typeparam name="TOutput"></typeparam>
        /// <param name="builder"></param>
        /// <param name="action"></param>
        /// <param name="blockOptions"></param>
        /// <param name="linkOptions"></param>
        /// <param name="predicate"></param>
        /// <param name="declinedAction"></param>
        /// <param name="declinedBlockOptions"></param>
        /// <param name="declinedLinkOptions"></param>
        /// <returns></returns>
        public static IDataflowBuilder LinkToTarget<TOutput>(
            this ISourceDataflowBuilder<TOutput> builder
            , Action<TOutput> action
            , ExecutionDataflowBlockOptions blockOptions
            , DataflowLinkOptions linkOptions
            , Predicate<TOutput> predicate
            , Func<TOutput, Task> declinedAction
            , ExecutionDataflowBlockOptions declinedBlockOptions = null
            , DataflowLinkOptions declinedLinkOptions = null)
        {
            if (builder == null) return null;
            if (action == null) throw new ArgumentNullException("action");
            if (predicate == null) throw new ArgumentNullException("predicate");

            var actionBlock = new ActionBlock<TOutput>(action, blockOptions ?? DataflowDefaultOptions.DefaultBlockOptions);
            var nextBuilder = builder.LinkToTarget(actionBlock, linkOptions, predicate);

            if (declinedAction != null)
            {
                // LinkTo declined target
                var declinedActionBlock = new ActionBlock<TOutput>(declinedAction, declinedBlockOptions ?? DataflowDefaultOptions.DefaultBlockOptions);
                LinkHelper.Link((builder as SourceDataflowBuilder<TOutput>).FinalSourceBlock, declinedActionBlock, declinedLinkOptions ?? DataflowDefaultOptions.DefaultLinkOptions);
            }

            return nextBuilder;

        }

        /// <summary>
        /// Links to a target action.
        /// </summary>
        /// <typeparam name="TOutput"></typeparam>
        /// <param name="builder"></param>
        /// <param name="action"></param>
        /// <param name="blockOptions"></param>
        /// <param name="predicate"></param>
        /// <param name="declinedAction"></param>
        /// <param name="declinedBlockOptions"></param>
        /// <returns></returns>
        public static IDataflowBuilder LinkToTarget<TOutput>(
            this ISourceDataflowBuilder<TOutput> builder
            , Action<TOutput> action
            , ExecutionDataflowBlockOptions blockOptions
            , Predicate<TOutput> predicate
            , Func<TOutput, Task> declinedAction
            , ExecutionDataflowBlockOptions declinedBlockOptions = null)
        {
            return builder.LinkToTarget(action, DataflowDefaultOptions.DefaultBlockOptions, DataflowDefaultOptions.DefaultLinkOptions, predicate, declinedAction, declinedBlockOptions);
        }

        /// <summary>
        /// Links to a target action.
        /// </summary>
        /// <typeparam name="TOutput"></typeparam>
        /// <param name="builder"></param>
        /// <param name="action"></param>
        /// <param name="blockOptions"></param>
        /// <param name="linkOptions"></param>
        /// <param name="predicate"></param>
        /// <param name="declinedTargetBlock"></param>
        /// <param name="declinedLinkOptions"></param>
        /// <returns></returns>
        public static IDataflowBuilder LinkToTarget<TOutput>(
            this ISourceDataflowBuilder<TOutput> builder
            , Func<TOutput, Task> action
            , ExecutionDataflowBlockOptions blockOptions = null
            , DataflowLinkOptions linkOptions = null
            , Predicate<TOutput> predicate = null
            , ITargetBlock<TOutput> declinedTargetBlock = null
            , DataflowLinkOptions declinedLinkOptions = null)
        {
            if (builder == null) return null;
            if (action == null) throw new ArgumentNullException("action");

            var actionBlock = new ActionBlock<TOutput>(action, blockOptions ?? DataflowDefaultOptions.DefaultBlockOptions);
            var nextBuilder = builder.LinkToTarget(actionBlock, linkOptions, predicate);

            if (declinedTargetBlock != null)
            {
                // LinkTo declined target
                LinkHelper.Link((builder as SourceDataflowBuilder<TOutput>).FinalSourceBlock, declinedTargetBlock, declinedLinkOptions ?? DataflowDefaultOptions.DefaultLinkOptions);
            }

            return nextBuilder;
        }

        /// <summary>
        /// Links to a target action.
        /// </summary>
        /// <typeparam name="TOutput"></typeparam>
        /// <param name="builder"></param>
        /// <param name="action"></param>
        /// <param name="blockOptions"></param>
        /// <param name="predicate"></param>
        /// <param name="declinedTargetBlock"></param>
        /// <returns></returns>
        public static IDataflowBuilder LinkToTarget<TOutput>(
            this ISourceDataflowBuilder<TOutput> builder
            , Func<TOutput, Task> action
            , ExecutionDataflowBlockOptions blockOptions
            , Predicate<TOutput> predicate
            , ITargetBlock<TOutput> declinedTargetBlock = null)
        {
            return builder.LinkToTarget(action, blockOptions, DataflowDefaultOptions.DefaultLinkOptions, predicate, declinedTargetBlock);
        }

        /// <summary>
        /// Links to a target action.
        /// </summary>
        /// <typeparam name="TOutput"></typeparam>
        /// <param name="builder"></param>
        /// <param name="action"></param>
        /// <param name="blockOptions"></param>
        /// <param name="linkOptions"></param>
        /// <param name="predicate"></param>
        /// <param name="declinedAction"></param>
        /// <param name="declinedBlockOptions"></param>
        /// <param name="declinedLinkOptions"></param>
        /// <returns></returns>
        public static IDataflowBuilder LinkToTarget<TOutput>(
            this ISourceDataflowBuilder<TOutput> builder
            , Func<TOutput, Task> action
            , ExecutionDataflowBlockOptions blockOptions
            , DataflowLinkOptions linkOptions
            , Predicate<TOutput> predicate
            , Action<TOutput> declinedAction
            , ExecutionDataflowBlockOptions declinedBlockOptions = null
            , DataflowLinkOptions declinedLinkOptions = null)
        {
            if (builder == null) return null;
            if (action == null) throw new ArgumentNullException("action");
            if (predicate == null) throw new ArgumentNullException("predicate");

            var actionBlock = new ActionBlock<TOutput>(action, blockOptions ?? DataflowDefaultOptions.DefaultBlockOptions);
            var nextBuilder = builder.LinkToTarget(actionBlock, linkOptions, predicate);

            if (declinedAction != null)
            {
                // LinkTo declined target
                var declinedActionBlock = new ActionBlock<TOutput>(declinedAction, declinedBlockOptions ?? DataflowDefaultOptions.DefaultBlockOptions);
                LinkHelper.Link((builder as SourceDataflowBuilder<TOutput>).FinalSourceBlock, declinedActionBlock, declinedLinkOptions ?? DataflowDefaultOptions.DefaultLinkOptions);
            }

            return nextBuilder;

        }

        /// <summary>
        /// Links to a target action.
        /// </summary>
        /// <typeparam name="TOutput"></typeparam>
        /// <param name="builder"></param>
        /// <param name="action"></param>
        /// <param name="blockOptions"></param>
        /// <param name="predicate"></param>
        /// <param name="declinedAction"></param>
        /// <param name="declinedBlockOptions"></param>
        /// <returns></returns>
        public static IDataflowBuilder LinkToTarget<TOutput>(
            this ISourceDataflowBuilder<TOutput> builder
            , Func<TOutput, Task> action
            , ExecutionDataflowBlockOptions blockOptions
            , Predicate<TOutput> predicate
            , Action<TOutput> declinedAction
            , ExecutionDataflowBlockOptions declinedBlockOptions = null)
        {
            return builder.LinkToTarget(action, DataflowDefaultOptions.DefaultBlockOptions, DataflowDefaultOptions.DefaultLinkOptions, predicate, declinedAction, declinedBlockOptions);
        }

        /// <summary>
        /// Links to a target action.
        /// </summary>
        /// <typeparam name="TOutput"></typeparam>
        /// <param name="builder"></param>
        /// <param name="action"></param>
        /// <param name="blockOptions"></param>
        /// <param name="linkOptions"></param>
        /// <param name="predicate"></param>
        /// <param name="declinedAction"></param>
        /// <param name="declinedBlockOptions"></param>
        /// <param name="declinedLinkOptions"></param>
        /// <returns></returns>
        public static IDataflowBuilder LinkToTarget<TOutput>(
            this ISourceDataflowBuilder<TOutput> builder
            , Func<TOutput, Task> action
            , ExecutionDataflowBlockOptions blockOptions
            , DataflowLinkOptions linkOptions
            , Predicate<TOutput> predicate
            , Func<TOutput, Task> declinedAction
            , ExecutionDataflowBlockOptions declinedBlockOptions = null
            , DataflowLinkOptions declinedLinkOptions = null)
        {
            if (builder == null) return null;
            if (action == null) throw new ArgumentNullException("action");
            if (predicate == null) throw new ArgumentNullException("predicate");

            var actionBlock = new ActionBlock<TOutput>(action, blockOptions ?? DataflowDefaultOptions.DefaultBlockOptions);
            var nextBuilder = builder.LinkToTarget(actionBlock, linkOptions, predicate);

            if (declinedAction != null)
            {
                // LinkTo declined target
                var declinedActionBlock = new ActionBlock<TOutput>(declinedAction, declinedBlockOptions ?? DataflowDefaultOptions.DefaultBlockOptions);
                LinkHelper.Link((builder as SourceDataflowBuilder<TOutput>).FinalSourceBlock, declinedActionBlock, declinedLinkOptions ?? DataflowDefaultOptions.DefaultLinkOptions);
            }

            return nextBuilder;

        }

        /// <summary>
        /// Links to a target action.
        /// </summary>
        /// <typeparam name="TOutput"></typeparam>
        /// <param name="builder"></param>
        /// <param name="action"></param>
        /// <param name="blockOptions"></param>
        /// <param name="predicate"></param>
        /// <param name="declinedAction"></param>
        /// <param name="declinedBlockOptions"></param>
        /// <returns></returns>
        public static IDataflowBuilder LinkToTarget<TOutput>(
            this ISourceDataflowBuilder<TOutput> builder
            , Func<TOutput, Task> action
            , ExecutionDataflowBlockOptions blockOptions
            , Predicate<TOutput> predicate
            , Func<TOutput, Task> declinedAction
            , ExecutionDataflowBlockOptions declinedBlockOptions = null)
        {
            return builder.LinkToTarget(action, DataflowDefaultOptions.DefaultBlockOptions, DataflowDefaultOptions.DefaultLinkOptions, predicate, declinedAction, declinedBlockOptions);
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
            , DataflowLinkOptions declinedLinkOptions = null)
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
            return builder.LinkToPropagator(propagatorBlock, DataflowDefaultOptions.DefaultLinkOptions, predicate, declinedTargetBlock);
        }

        /// <summary>
        /// Links to a propagator block.
        /// </summary>
        /// <typeparam name="TOutput"></typeparam>
        /// <typeparam name="TOutput2"></typeparam>
        /// <param name="builder"></param>
        /// <param name="propagatorBlock"></param>
        /// <param name="linkOptions"></param>
        /// <param name="predicate"></param>
        /// <param name="declinedAction"></param>
        /// <param name="declinedBlockOptions"></param>
        /// <param name="declinedLinkOptions"></param>
        /// <returns></returns>
        public static ISourceDataflowBuilder<TOutput2> LinkToPropagator<TOutput, TOutput2>(
            this ISourceDataflowBuilder<TOutput> builder
            , IPropagatorBlock<TOutput, TOutput2> propagatorBlock
            , DataflowLinkOptions linkOptions
            , Predicate<TOutput> predicate
            , Action<TOutput> declinedAction
            , ExecutionDataflowBlockOptions declinedBlockOptions = null
            , DataflowLinkOptions declinedLinkOptions = null)
        {
            if (builder == null) return null;
            if (propagatorBlock == null) throw new ArgumentNullException("propagatorBlock");
            if (predicate == null) throw new ArgumentNullException("predicate");

            var nextBuilder = builder.LinkToPropagator(propagatorBlock, linkOptions, predicate);

            if (declinedAction != null)
            {
                // LinkTo declined target
                var declinedActionBlock = new ActionBlock<TOutput>(declinedAction, declinedBlockOptions ?? DataflowDefaultOptions.DefaultBlockOptions);
                LinkHelper.Link((builder as SourceDataflowBuilder<TOutput>).FinalSourceBlock, declinedActionBlock, declinedLinkOptions ?? DataflowDefaultOptions.DefaultLinkOptions);
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
        /// <param name="declinedAction"></param>
        /// <param name="declinedBlockOptions"></param>
        /// <returns></returns>
        public static ISourceDataflowBuilder<TOutput2> LinkToPropagator<TOutput, TOutput2>(
            this ISourceDataflowBuilder<TOutput> builder
            , IPropagatorBlock<TOutput, TOutput2> propagatorBlock
            , Predicate<TOutput> predicate
            , Action<TOutput> declinedAction
            , ExecutionDataflowBlockOptions declinedBlockOptions = null)
        {
            return builder.LinkToPropagator(propagatorBlock, DataflowDefaultOptions.DefaultLinkOptions, predicate, declinedAction, declinedBlockOptions);
        }

        /// <summary>
        /// Links to a propagator block.
        /// </summary>
        /// <typeparam name="TOutput"></typeparam>
        /// <typeparam name="TOutput2"></typeparam>
        /// <param name="builder"></param>
        /// <param name="propagatorBlock"></param>
        /// <param name="linkOptions"></param>
        /// <param name="predicate"></param>
        /// <param name="declinedAction"></param>
        /// <param name="declinedBlockOptions"></param>
        /// <param name="declinedLinkOptions"></param>
        /// <returns></returns>
        public static ISourceDataflowBuilder<TOutput2> LinkToPropagator<TOutput, TOutput2>(
            this ISourceDataflowBuilder<TOutput> builder
            , IPropagatorBlock<TOutput, TOutput2> propagatorBlock
            , DataflowLinkOptions linkOptions
            , Predicate<TOutput> predicate
            , Func<TOutput, Task> declinedAction
            , ExecutionDataflowBlockOptions declinedBlockOptions = null
            , DataflowLinkOptions declinedLinkOptions = null)
        {
            if (builder == null) return null;
            if (propagatorBlock == null) throw new ArgumentNullException("propagatorBlock");
            if (predicate == null) throw new ArgumentNullException("predicate");

            var nextBuilder = builder.LinkToPropagator(propagatorBlock, linkOptions, predicate);

            if (declinedAction != null)
            {
                // LinkTo declined target
                var declinedActionBlock = new ActionBlock<TOutput>(declinedAction, declinedBlockOptions ?? DataflowDefaultOptions.DefaultBlockOptions);
                LinkHelper.Link((builder as SourceDataflowBuilder<TOutput>).FinalSourceBlock, declinedActionBlock, declinedLinkOptions ?? DataflowDefaultOptions.DefaultLinkOptions);
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
        /// <param name="declinedAction"></param>
        /// <param name="declinedBlockOptions"></param>
        /// <returns></returns>
        public static ISourceDataflowBuilder<TOutput2> LinkToPropagator<TOutput, TOutput2>(
            this ISourceDataflowBuilder<TOutput> builder
            , IPropagatorBlock<TOutput, TOutput2> propagatorBlock
            , Predicate<TOutput> predicate
            , Func<TOutput, Task> declinedAction
            , ExecutionDataflowBlockOptions declinedBlockOptions = null)
        {
            return builder.LinkToPropagator(propagatorBlock, DataflowDefaultOptions.DefaultLinkOptions, predicate, declinedAction, declinedBlockOptions);
        }

        #endregion

        #region ISourceDataflowBuilder link to transform function

        /// <summary>
        /// Links to a transform function.
        /// </summary>
        /// <typeparam name="TOutput"></typeparam>
        /// <typeparam name="TOutput2"></typeparam>
        /// <param name="builder"></param>
        /// <param name="transform"></param>
        /// <param name="blockOptions"></param>
        /// <param name="linkOptions"></param>
        /// <param name="predicate"></param>
        /// <param name="declinedTargetBlock"></param>
        /// <param name="declinedLinkOptions"></param>
        /// <returns></returns>
        public static ISourceDataflowBuilder<TOutput2> LinkToPropagator<TOutput, TOutput2>(
            this ISourceDataflowBuilder<TOutput> builder
            , Func<TOutput, TOutput2> transform
            , ExecutionDataflowBlockOptions blockOptions = null
            , DataflowLinkOptions linkOptions = null
            , Predicate<TOutput> predicate = null
            , ITargetBlock<TOutput> declinedTargetBlock = null
            , DataflowLinkOptions declinedLinkOptions = null)
        {
            if (builder == null) return null;
            if (transform == null) throw new ArgumentNullException("transform");

            var transformBlock = new TransformBlock<TOutput, TOutput2>(transform, blockOptions ?? DataflowDefaultOptions.DefaultBlockOptions);
            var nextBuilder = builder.LinkToPropagator(transformBlock, linkOptions, predicate);

            if (declinedTargetBlock != null)
            {
                // LinkTo declined target
                LinkHelper.Link((builder as SourceDataflowBuilder<TOutput>).FinalSourceBlock, declinedTargetBlock, declinedLinkOptions ?? DataflowDefaultOptions.DefaultLinkOptions);
            }

            return nextBuilder;
        }

        /// <summary>
        /// Links to a transform function.
        /// </summary>
        /// <typeparam name="TOutput"></typeparam>
        /// <typeparam name="TOutput2"></typeparam>
        /// <param name="builder"></param>
        /// <param name="transform"></param>
        /// <param name="blockOptions"></param>
        /// <param name="predicate"></param>
        /// <param name="declinedTargetBlock"></param>
        /// <returns></returns>
        public static ISourceDataflowBuilder<TOutput2> LinkToPropagator<TOutput, TOutput2>(
            this ISourceDataflowBuilder<TOutput> builder
            , Func<TOutput, TOutput2> transform
            , ExecutionDataflowBlockOptions blockOptions
            , Predicate<TOutput> predicate
            , ITargetBlock<TOutput> declinedTargetBlock = null)
        {
            return builder.LinkToPropagator(transform, blockOptions, DataflowDefaultOptions.DefaultLinkOptions, predicate, declinedTargetBlock);
        }

        /// <summary>
        /// Links to a transform function.
        /// </summary>
        /// <typeparam name="TOutput"></typeparam>
        /// <typeparam name="TOutput2"></typeparam>
        /// <param name="builder"></param>
        /// <param name="transform"></param>
        /// <param name="blockOptions"></param>
        /// <param name="linkOptions"></param>
        /// <param name="predicate"></param>
        /// <param name="declinedAction"></param>
        /// <param name="declinedBlockOptions"></param>
        /// <param name="declinedLinkOptions"></param>
        /// <returns></returns>
        public static ISourceDataflowBuilder<TOutput2> LinkToPropagator<TOutput, TOutput2>(
            this ISourceDataflowBuilder<TOutput> builder
            , Func<TOutput, TOutput2> transform
            , ExecutionDataflowBlockOptions blockOptions = null
            , DataflowLinkOptions linkOptions = null
            , Predicate<TOutput> predicate = null
            , Action<TOutput> declinedAction = null
            , ExecutionDataflowBlockOptions declinedBlockOptions = null
            , DataflowLinkOptions declinedLinkOptions = null)
        {
            if (builder == null) return null;
            if (transform == null) throw new ArgumentNullException("transform");

            var transformBlock = new TransformBlock<TOutput, TOutput2>(transform, blockOptions ?? DataflowDefaultOptions.DefaultBlockOptions);
            var nextBuilder = builder.LinkToPropagator(transformBlock, linkOptions, predicate);

            if (declinedAction != null)
            {
                // LinkTo declined target
                var declinedActionBlock = new ActionBlock<TOutput>(declinedAction, declinedBlockOptions ?? DataflowDefaultOptions.DefaultBlockOptions);
                LinkHelper.Link((builder as SourceDataflowBuilder<TOutput>).FinalSourceBlock, declinedActionBlock, declinedLinkOptions ?? DataflowDefaultOptions.DefaultLinkOptions);
            }

            return nextBuilder;
        }

        /// <summary>
        /// Links to a transform function.
        /// </summary>
        /// <typeparam name="TOutput"></typeparam>
        /// <typeparam name="TOutput2"></typeparam>
        /// <param name="builder"></param>
        /// <param name="transform"></param>
        /// <param name="blockOptions"></param>
        /// <param name="predicate"></param>
        /// <param name="declinedAction"></param>
        /// <param name="declinedBlockOptions"></param>
        /// <returns></returns>
        public static ISourceDataflowBuilder<TOutput2> LinkToPropagator<TOutput, TOutput2>(
            this ISourceDataflowBuilder<TOutput> builder
            , Func<TOutput, TOutput2> transform
            , ExecutionDataflowBlockOptions blockOptions
            , Predicate<TOutput> predicate
            , Action<TOutput> declinedAction
            , ExecutionDataflowBlockOptions declinedBlockOptions = null)
        {
            return builder.LinkToPropagator(transform, blockOptions, DataflowDefaultOptions.DefaultLinkOptions, predicate, declinedAction, declinedBlockOptions);
        }

        /// <summary>
        /// Links to a transform function.
        /// </summary>
        /// <typeparam name="TOutput"></typeparam>
        /// <typeparam name="TOutput2"></typeparam>
        /// <param name="builder"></param>
        /// <param name="transform"></param>
        /// <param name="blockOptions"></param>
        /// <param name="linkOptions"></param>
        /// <param name="predicate"></param>
        /// <param name="declinedAction"></param>
        /// <param name="declinedBlockOptions"></param>
        /// <param name="declinedLinkOptions"></param>
        /// <returns></returns>
        public static ISourceDataflowBuilder<TOutput2> LinkToPropagator<TOutput, TOutput2>(
            this ISourceDataflowBuilder<TOutput> builder
            , Func<TOutput, TOutput2> transform
            , ExecutionDataflowBlockOptions blockOptions = null
            , DataflowLinkOptions linkOptions = null
            , Predicate<TOutput> predicate = null
            , Func<TOutput, Task> declinedAction = null
            , ExecutionDataflowBlockOptions declinedBlockOptions = null
            , DataflowLinkOptions declinedLinkOptions = null)
        {
            if (builder == null) return null;
            if (transform == null) throw new ArgumentNullException("transform");

            var transformBlock = new TransformBlock<TOutput, TOutput2>(transform, blockOptions ?? DataflowDefaultOptions.DefaultBlockOptions);
            var nextBuilder = builder.LinkToPropagator(transformBlock, linkOptions, predicate);

            if (declinedAction != null)
            {
                // LinkTo declined target
                var declinedActionBlock = new ActionBlock<TOutput>(declinedAction, declinedBlockOptions ?? DataflowDefaultOptions.DefaultBlockOptions);
                LinkHelper.Link((builder as SourceDataflowBuilder<TOutput>).FinalSourceBlock, declinedActionBlock, declinedLinkOptions ?? DataflowDefaultOptions.DefaultLinkOptions);
            }

            return nextBuilder;
        }

        /// <summary>
        /// Links to a transform function.
        /// </summary>
        /// <typeparam name="TOutput"></typeparam>
        /// <typeparam name="TOutput2"></typeparam>
        /// <param name="builder"></param>
        /// <param name="transform"></param>
        /// <param name="blockOptions"></param>
        /// <param name="predicate"></param>
        /// <param name="declinedAction"></param>
        /// <param name="declinedBlockOptions"></param>
        /// <returns></returns>
        public static ISourceDataflowBuilder<TOutput2> LinkToPropagator<TOutput, TOutput2>(
            this ISourceDataflowBuilder<TOutput> builder
            , Func<TOutput, TOutput2> transform
            , ExecutionDataflowBlockOptions blockOptions
            , Predicate<TOutput> predicate
            , Func<TOutput, Task> declinedAction
            , ExecutionDataflowBlockOptions declinedBlockOptions = null)
        {
            return builder.LinkToPropagator(transform, blockOptions, DataflowDefaultOptions.DefaultLinkOptions, predicate, declinedAction, declinedBlockOptions);
        }

        /// <summary>
        /// Links to a transform function.
        /// </summary>
        /// <typeparam name="TOutput"></typeparam>
        /// <typeparam name="TOutput2"></typeparam>
        /// <param name="builder"></param>
        /// <param name="transform"></param>
        /// <param name="blockOptions"></param>
        /// <param name="linkOptions"></param>
        /// <param name="predicate"></param>
        /// <param name="declinedTargetBlock"></param>
        /// <param name="declinedLinkOptions"></param>
        /// <returns></returns>
        public static ISourceDataflowBuilder<TOutput2> LinkToPropagator<TOutput, TOutput2>(
            this ISourceDataflowBuilder<TOutput> builder
            , Func<TOutput, Task<TOutput2>> transform
            , ExecutionDataflowBlockOptions blockOptions = null
            , DataflowLinkOptions linkOptions = null
            , Predicate<TOutput> predicate = null
            , ITargetBlock<TOutput> declinedTargetBlock = null
            , DataflowLinkOptions declinedLinkOptions = null)
        {
            if (builder == null) return null;

            var transformBlock = new TransformBlock<TOutput, TOutput2>(transform, blockOptions ?? DataflowDefaultOptions.DefaultBlockOptions);
            var nextBuilder = builder.LinkToPropagator(transformBlock, linkOptions, predicate);

            if (declinedTargetBlock != null)
            {
                // LinkTo declined target
                LinkHelper.Link((builder as SourceDataflowBuilder<TOutput>).FinalSourceBlock, declinedTargetBlock, declinedLinkOptions ?? DataflowDefaultOptions.DefaultLinkOptions);
            }

            return nextBuilder;
        }

        /// <summary>
        /// Links to a transform function.
        /// </summary>
        /// <typeparam name="TOutput"></typeparam>
        /// <typeparam name="TOutput2"></typeparam>
        /// <param name="builder"></param>
        /// <param name="transform"></param>
        /// <param name="blockOptions"></param>
        /// <param name="predicate"></param>
        /// <param name="declinedTargetBlock"></param>
        /// <returns></returns>
        public static ISourceDataflowBuilder<TOutput2> LinkToPropagator<TOutput, TOutput2>(
            this ISourceDataflowBuilder<TOutput> builder
            , Func<TOutput, Task<TOutput2>> transform
            , ExecutionDataflowBlockOptions blockOptions
            , Predicate<TOutput> predicate
            , ITargetBlock<TOutput> declinedTargetBlock = null)
        {
            return builder.LinkToPropagator(transform, blockOptions, DataflowDefaultOptions.DefaultLinkOptions, predicate, declinedTargetBlock);
        }

        /// <summary>
        /// Links to a transform function.
        /// </summary>
        /// <typeparam name="TOutput"></typeparam>
        /// <typeparam name="TOutput2"></typeparam>
        /// <param name="builder"></param>
        /// <param name="transform"></param>
        /// <param name="blockOptions"></param>
        /// <param name="linkOptions"></param>
        /// <param name="predicate"></param>
        /// <param name="declinedAction"></param>
        /// <param name="declinedBlockOptions"></param>
        /// <param name="declinedLinkOptions"></param>
        /// <returns></returns>
        public static ISourceDataflowBuilder<TOutput2> LinkToPropagator<TOutput, TOutput2>(
            this ISourceDataflowBuilder<TOutput> builder
            , Func<TOutput, Task<TOutput2>> transform
            , ExecutionDataflowBlockOptions blockOptions = null
            , DataflowLinkOptions linkOptions = null
            , Predicate<TOutput> predicate = null
            , Action<TOutput> declinedAction = null
            , ExecutionDataflowBlockOptions declinedBlockOptions = null
            , DataflowLinkOptions declinedLinkOptions = null)
        {
            if (builder == null) return null;
            if (transform == null) throw new ArgumentNullException("transform");

            var transformBlock = new TransformBlock<TOutput, TOutput2>(transform, blockOptions ?? DataflowDefaultOptions.DefaultBlockOptions);
            var nextBuilder = builder.LinkToPropagator(transformBlock, linkOptions, predicate);

            if (declinedAction != null)
            {
                // LinkTo declined target
                var declinedActionBlock = new ActionBlock<TOutput>(declinedAction, declinedBlockOptions ?? DataflowDefaultOptions.DefaultBlockOptions);
                LinkHelper.Link((builder as SourceDataflowBuilder<TOutput>).FinalSourceBlock, declinedActionBlock, declinedLinkOptions ?? DataflowDefaultOptions.DefaultLinkOptions);
            }

            return nextBuilder;
        }

        /// <summary>
        /// Links to a transform function.
        /// </summary>
        /// <typeparam name="TOutput"></typeparam>
        /// <typeparam name="TOutput2"></typeparam>
        /// <param name="builder"></param>
        /// <param name="transform"></param>
        /// <param name="blockOptions"></param>
        /// <param name="predicate"></param>
        /// <param name="declinedAction"></param>
        /// <param name="declinedBlockOptions"></param>
        /// <returns></returns>
        public static ISourceDataflowBuilder<TOutput2> LinkToPropagator<TOutput, TOutput2>(
            this ISourceDataflowBuilder<TOutput> builder
            , Func<TOutput, Task<TOutput2>> transform
            , ExecutionDataflowBlockOptions blockOptions
            , Predicate<TOutput> predicate
            , Action<TOutput> declinedAction
            , ExecutionDataflowBlockOptions declinedBlockOptions = null)
        {
            return builder.LinkToPropagator(transform, blockOptions, DataflowDefaultOptions.DefaultLinkOptions, predicate, declinedAction, declinedBlockOptions);
        }

        /// <summary>
        /// Links to a transform function.
        /// </summary>
        /// <typeparam name="TOutput"></typeparam>
        /// <typeparam name="TOutput2"></typeparam>
        /// <param name="builder"></param>
        /// <param name="transform"></param>
        /// <param name="blockOptions"></param>
        /// <param name="linkOptions"></param>
        /// <param name="predicate"></param>
        /// <param name="declinedAction"></param>
        /// <param name="declinedBlockOptions"></param>
        /// <param name="declinedLinkOptions"></param>
        /// <returns></returns>
        public static ISourceDataflowBuilder<TOutput2> LinkToPropagator<TOutput, TOutput2>(
            this ISourceDataflowBuilder<TOutput> builder
            , Func<TOutput, Task<TOutput2>> transform
            , ExecutionDataflowBlockOptions blockOptions = null
            , DataflowLinkOptions linkOptions = null
            , Predicate<TOutput> predicate = null
            , Func<TOutput, Task> declinedAction = null
            , ExecutionDataflowBlockOptions declinedBlockOptions = null
            , DataflowLinkOptions declinedLinkOptions = null)
        {
            if (builder == null) return null;
            if (transform == null) throw new ArgumentNullException("transform");

            var transformBlock = new TransformBlock<TOutput, TOutput2>(transform, blockOptions ?? DataflowDefaultOptions.DefaultBlockOptions);
            var nextBuilder = builder.LinkToPropagator(transformBlock, linkOptions, predicate);

            if (declinedAction != null)
            {
                // LinkTo declined target
                var declinedActionBlock = new ActionBlock<TOutput>(declinedAction, declinedBlockOptions ?? DataflowDefaultOptions.DefaultBlockOptions);
                LinkHelper.Link((builder as SourceDataflowBuilder<TOutput>).FinalSourceBlock, declinedActionBlock, declinedLinkOptions ?? DataflowDefaultOptions.DefaultLinkOptions);
            }

            return nextBuilder;
        }

        /// <summary>
        /// Links to a transform function.
        /// </summary>
        /// <typeparam name="TOutput"></typeparam>
        /// <typeparam name="TOutput2"></typeparam>
        /// <param name="builder"></param>
        /// <param name="transform"></param>
        /// <param name="blockOptions"></param>
        /// <param name="predicate"></param>
        /// <param name="declinedAction"></param>
        /// <param name="declinedBlockOptions"></param>
        /// <returns></returns>
        public static ISourceDataflowBuilder<TOutput2> LinkToPropagator<TOutput, TOutput2>(
            this ISourceDataflowBuilder<TOutput> builder
            , Func<TOutput, Task<TOutput2>> transform
            , ExecutionDataflowBlockOptions blockOptions
            , Predicate<TOutput> predicate
            , Func<TOutput, Task> declinedAction
            , ExecutionDataflowBlockOptions declinedBlockOptions = null)
        {
            return builder.LinkToPropagator(transform, blockOptions, DataflowDefaultOptions.DefaultLinkOptions, predicate, declinedAction, declinedBlockOptions);
        }

        #endregion

        #region ISourceDataflowBuilder link to transformMany function

        /// <summary>
        /// Links to a transformMany function.
        /// </summary>
        /// <typeparam name="TOutput"></typeparam>
        /// <typeparam name="TOutput2"></typeparam>
        /// <param name="builder"></param>
        /// <param name="transformMany"></param>
        /// <param name="blockOptions"></param>
        /// <param name="linkOptions"></param>
        /// <param name="predicate"></param>
        /// <param name="declinedTargetBlock"></param>
        /// <param name="declinedLinkOptions"></param>
        /// <returns></returns>
        public static ISourceDataflowBuilder<TOutput2> LinkToPropagator<TOutput, TOutput2>(
            this ISourceDataflowBuilder<TOutput> builder
            , Func<TOutput, IEnumerable<TOutput2>> transformMany
            , ExecutionDataflowBlockOptions blockOptions = null
            , DataflowLinkOptions linkOptions = null
            , Predicate<TOutput> predicate = null
            , ITargetBlock<TOutput> declinedTargetBlock = null
            , DataflowLinkOptions declinedLinkOptions = null)
        {
            if (builder == null) return null;
            if (transformMany == null) throw new ArgumentNullException("transformMany");

            var transformManyBlock = new TransformManyBlock<TOutput, TOutput2>(transformMany, blockOptions ?? DataflowDefaultOptions.DefaultBlockOptions);
            var nextBuilder =  builder.LinkToPropagator(transformManyBlock, linkOptions, predicate);

            if (declinedTargetBlock != null)
            {
                // LinkTo declined target
                LinkHelper.Link((builder as SourceDataflowBuilder<TOutput>).FinalSourceBlock, declinedTargetBlock, declinedLinkOptions ?? DataflowDefaultOptions.DefaultLinkOptions);
            }

            return nextBuilder;
        }

        /// <summary>
        /// Links to a transformMany function.
        /// </summary>
        /// <typeparam name="TOutput"></typeparam>
        /// <typeparam name="TOutput2"></typeparam>
        /// <param name="builder"></param>
        /// <param name="transformMany"></param>
        /// <param name="blockOptions"></param>
        /// <param name="predicate"></param>
        /// <param name="declinedTargetBlock"></param>
        /// <returns></returns>
        public static ISourceDataflowBuilder<TOutput2> LinkToPropagator<TOutput, TOutput2>(
            this ISourceDataflowBuilder<TOutput> builder
            , Func<TOutput, IEnumerable<TOutput2>> transformMany
            , ExecutionDataflowBlockOptions blockOptions
            , Predicate<TOutput> predicate
            , ITargetBlock<TOutput> declinedTargetBlock = null)
        {
            return builder.LinkToPropagator(transformMany, blockOptions, DataflowDefaultOptions.DefaultLinkOptions, predicate, declinedTargetBlock);
        }

        /// <summary>
        /// Links to a transformMany function.
        /// </summary>
        /// <typeparam name="TOutput"></typeparam>
        /// <typeparam name="TOutput2"></typeparam>
        /// <param name="builder"></param>
        /// <param name="transformMany"></param>
        /// <param name="blockOptions"></param>
        /// <param name="linkOptions"></param>
        /// <param name="predicate"></param>
        /// <param name="declinedAction"></param>
        /// <param name="declinedBlockOptions"></param>
        /// <param name="declinedLinkOptions"></param>
        /// <returns></returns>
        public static ISourceDataflowBuilder<TOutput2> LinkToPropagator<TOutput, TOutput2>(
            this ISourceDataflowBuilder<TOutput> builder
            , Func<TOutput, IEnumerable<TOutput2>> transformMany
            , ExecutionDataflowBlockOptions blockOptions = null
            , DataflowLinkOptions linkOptions = null
            , Predicate<TOutput> predicate = null
            , Action<TOutput> declinedAction = null
            , ExecutionDataflowBlockOptions declinedBlockOptions = null
            , DataflowLinkOptions declinedLinkOptions = null)
        {
            if (builder == null) return null;

            var transformManyBlock = new TransformManyBlock<TOutput, TOutput2>(transformMany, blockOptions ?? DataflowDefaultOptions.DefaultBlockOptions);
            var nextBuilder = builder.LinkToPropagator(transformManyBlock, linkOptions, predicate);

            if (declinedAction != null)
            {
                // LinkTo declined target
                var declinedActionBlock = new ActionBlock<TOutput>(declinedAction, declinedBlockOptions ?? DataflowDefaultOptions.DefaultBlockOptions);
                LinkHelper.Link((builder as SourceDataflowBuilder<TOutput>).FinalSourceBlock, declinedActionBlock, declinedLinkOptions ?? DataflowDefaultOptions.DefaultLinkOptions);
            }

            return nextBuilder;
        }

        /// <summary>
        /// Links to a transformMany function.
        /// </summary>
        /// <typeparam name="TOutput"></typeparam>
        /// <typeparam name="TOutput2"></typeparam>
        /// <param name="builder"></param>
        /// <param name="transformMany"></param>
        /// <param name="blockOptions"></param>
        /// <param name="predicate"></param>
        /// <param name="declinedAction"></param>
        /// <param name="declinedBlockOptions"></param>
        /// <returns></returns>
        public static ISourceDataflowBuilder<TOutput2> LinkToPropagator<TOutput, TOutput2>(
            this ISourceDataflowBuilder<TOutput> builder
            , Func<TOutput, IEnumerable<TOutput2>> transformMany
            , ExecutionDataflowBlockOptions blockOptions
            , Predicate<TOutput> predicate
            , Action<TOutput> declinedAction
            , ExecutionDataflowBlockOptions declinedBlockOptions = null)
        {
            return builder.LinkToPropagator(transformMany, blockOptions, DataflowDefaultOptions.DefaultLinkOptions, predicate, declinedAction, declinedBlockOptions);
        }

        /// <summary>
        /// Links to a transformMany function.
        /// </summary>
        /// <typeparam name="TOutput"></typeparam>
        /// <typeparam name="TOutput2"></typeparam>
        /// <param name="builder"></param>
        /// <param name="transformMany"></param>
        /// <param name="blockOptions"></param>
        /// <param name="linkOptions"></param>
        /// <param name="predicate"></param>
        /// <param name="declinedAction"></param>
        /// <param name="declinedBlockOptions"></param>
        /// <param name="declinedLinkOptions"></param>
        /// <returns></returns>
        public static ISourceDataflowBuilder<TOutput2> LinkToPropagator<TOutput, TOutput2>(
            this ISourceDataflowBuilder<TOutput> builder
            , Func<TOutput, IEnumerable<TOutput2>> transformMany
            , ExecutionDataflowBlockOptions blockOptions = null
            , DataflowLinkOptions linkOptions = null
            , Predicate<TOutput> predicate = null
            , Func<TOutput, Task> declinedAction = null
            , ExecutionDataflowBlockOptions declinedBlockOptions = null
            , DataflowLinkOptions declinedLinkOptions = null)
        {
            if (builder == null) return null;

            var transformManyBlock = new TransformManyBlock<TOutput, TOutput2>(transformMany, blockOptions ?? DataflowDefaultOptions.DefaultBlockOptions);
            var nextBuilder = builder.LinkToPropagator(transformManyBlock, linkOptions, predicate);

            if (declinedAction != null)
            {
                // LinkTo declined target
                var declinedActionBlock = new ActionBlock<TOutput>(declinedAction, declinedBlockOptions ?? DataflowDefaultOptions.DefaultBlockOptions);
                LinkHelper.Link((builder as SourceDataflowBuilder<TOutput>).FinalSourceBlock, declinedActionBlock, declinedLinkOptions ?? DataflowDefaultOptions.DefaultLinkOptions);
            }

            return nextBuilder;
        }

        /// <summary>
        /// Links to a transformMany function.
        /// </summary>
        /// <typeparam name="TOutput"></typeparam>
        /// <typeparam name="TOutput2"></typeparam>
        /// <param name="builder"></param>
        /// <param name="transformMany"></param>
        /// <param name="blockOptions"></param>
        /// <param name="predicate"></param>
        /// <param name="declinedAction"></param>
        /// <param name="declinedBlockOptions"></param>
        /// <returns></returns>
        public static ISourceDataflowBuilder<TOutput2> LinkToPropagator<TOutput, TOutput2>(
            this ISourceDataflowBuilder<TOutput> builder
            , Func<TOutput, IEnumerable<TOutput2>> transformMany
            , ExecutionDataflowBlockOptions blockOptions
            , Predicate<TOutput> predicate
            , Func<TOutput, Task> declinedAction
            , ExecutionDataflowBlockOptions declinedBlockOptions = null)
        {
            return builder.LinkToPropagator(transformMany, blockOptions, DataflowDefaultOptions.DefaultLinkOptions, predicate, declinedAction, declinedBlockOptions);
        }

        /// <summary>
        /// Links to a transformMany function.
        /// </summary>
        /// <typeparam name="TOutput"></typeparam>
        /// <typeparam name="TOutput2"></typeparam>
        /// <param name="builder"></param>
        /// <param name="transformMany"></param>
        /// <param name="blockOptions"></param>
        /// <param name="linkOptions"></param>
        /// <param name="predicate"></param>
        /// <param name="declinedTargetBlock"></param>
        /// <param name="declinedLinkOptions"></param>
        /// <returns></returns>
        public static ISourceDataflowBuilder<TOutput2> LinkToPropagator<TOutput, TOutput2>(
            this ISourceDataflowBuilder<TOutput> builder
            , Func<TOutput, Task<IEnumerable<TOutput2>>> transformMany
            , ExecutionDataflowBlockOptions blockOptions = null
            , DataflowLinkOptions linkOptions = null
            , Predicate<TOutput> predicate = null
            , ITargetBlock<TOutput> declinedTargetBlock = null
            , DataflowLinkOptions declinedLinkOptions = null)
        {
            if (builder == null) return null;

            var transformManyBlock = new TransformManyBlock<TOutput, TOutput2>(transformMany, blockOptions ?? DataflowDefaultOptions.DefaultBlockOptions);
            var nextBuilder = builder.LinkToPropagator(transformManyBlock, linkOptions, predicate);

            if (declinedTargetBlock != null)
            {
                // LinkTo declined target
                LinkHelper.Link((builder as SourceDataflowBuilder<TOutput>).FinalSourceBlock, declinedTargetBlock, declinedLinkOptions ?? DataflowDefaultOptions.DefaultLinkOptions);
            }

            return nextBuilder;
        }

        /// <summary>
        /// Links to a transformMany function.
        /// </summary>
        /// <typeparam name="TOutput"></typeparam>
        /// <typeparam name="TOutput2"></typeparam>
        /// <param name="builder"></param>
        /// <param name="transformMany"></param>
        /// <param name="blockOptions"></param>
        /// <param name="predicate"></param>
        /// <param name="declinedTargetBlock"></param>
        /// <returns></returns>
        public static ISourceDataflowBuilder<TOutput2> LinkToPropagator<TOutput, TOutput2>(
            this ISourceDataflowBuilder<TOutput> builder
            , Func<TOutput, Task<IEnumerable<TOutput2>>> transformMany
            , ExecutionDataflowBlockOptions blockOptions
            , Predicate<TOutput> predicate
            , ITargetBlock<TOutput> declinedTargetBlock = null)
        {
            return builder.LinkToPropagator(transformMany, blockOptions, DataflowDefaultOptions.DefaultLinkOptions, predicate, declinedTargetBlock);
        }

        /// <summary>
        /// Links to a transformMany function.
        /// </summary>
        /// <typeparam name="TOutput"></typeparam>
        /// <typeparam name="TOutput2"></typeparam>
        /// <param name="builder"></param>
        /// <param name="transformMany"></param>
        /// <param name="blockOptions"></param>
        /// <param name="linkOptions"></param>
        /// <param name="predicate"></param>
        /// <param name="declinedAction"></param>
        /// <param name="declinedBlockOptions"></param>
        /// <param name="declinedLinkOptions"></param>
        /// <returns></returns>
        public static ISourceDataflowBuilder<TOutput2> LinkToPropagator<TOutput, TOutput2>(
            this ISourceDataflowBuilder<TOutput> builder
            , Func<TOutput, Task<IEnumerable<TOutput2>>> transformMany
            , ExecutionDataflowBlockOptions blockOptions = null
            , DataflowLinkOptions linkOptions = null
            , Predicate<TOutput> predicate = null
            , Action<TOutput> declinedAction = null
            , ExecutionDataflowBlockOptions declinedBlockOptions = null
            , DataflowLinkOptions declinedLinkOptions = null)
        {
            if (builder == null) return null;

            var transformManyBlock = new TransformManyBlock<TOutput, TOutput2>(transformMany, blockOptions ?? DataflowDefaultOptions.DefaultBlockOptions);
            var nextBuilder = builder.LinkToPropagator(transformManyBlock, linkOptions, predicate);

            if (declinedAction != null)
            {
                // LinkTo declined target
                var declinedActionBlock = new ActionBlock<TOutput>(declinedAction, declinedBlockOptions ?? DataflowDefaultOptions.DefaultBlockOptions);
                LinkHelper.Link((builder as SourceDataflowBuilder<TOutput>).FinalSourceBlock, declinedActionBlock, declinedLinkOptions ?? DataflowDefaultOptions.DefaultLinkOptions);
            }

            return nextBuilder;
        }

        /// <summary>
        /// Links to a transformMany function.
        /// </summary>
        /// <typeparam name="TOutput"></typeparam>
        /// <typeparam name="TOutput2"></typeparam>
        /// <param name="builder"></param>
        /// <param name="transformMany"></param>
        /// <param name="blockOptions"></param>
        /// <param name="predicate"></param>
        /// <param name="declinedAction"></param>
        /// <param name="declinedBlockOptions"></param>
        /// <returns></returns>
        public static ISourceDataflowBuilder<TOutput2> LinkToPropagator<TOutput, TOutput2>(
            this ISourceDataflowBuilder<TOutput> builder
            , Func<TOutput, Task<IEnumerable<TOutput2>>> transformMany
            , ExecutionDataflowBlockOptions blockOptions
            , Predicate<TOutput> predicate
            , Action<TOutput> declinedAction
            , ExecutionDataflowBlockOptions declinedBlockOptions = null)
        {
            return builder.LinkToPropagator(transformMany, blockOptions, DataflowDefaultOptions.DefaultLinkOptions, predicate, declinedAction, declinedBlockOptions);
        }

        /// <summary>
        /// Links to a transformMany function.
        /// </summary>
        /// <typeparam name="TOutput"></typeparam>
        /// <typeparam name="TOutput2"></typeparam>
        /// <param name="builder"></param>
        /// <param name="transformMany"></param>
        /// <param name="blockOptions"></param>
        /// <param name="linkOptions"></param>
        /// <param name="predicate"></param>
        /// <param name="declinedAction"></param>
        /// <param name="declinedBlockOptions"></param>
        /// <param name="declinedLinkOptions"></param>
        /// <returns></returns>
        public static ISourceDataflowBuilder<TOutput2> LinkToPropagator<TOutput, TOutput2>(
            this ISourceDataflowBuilder<TOutput> builder
            , Func<TOutput, Task<IEnumerable<TOutput2>>> transformMany
            , ExecutionDataflowBlockOptions blockOptions = null
            , DataflowLinkOptions linkOptions = null
            , Predicate<TOutput> predicate = null
            , Func<TOutput, Task> declinedAction = null
            , ExecutionDataflowBlockOptions declinedBlockOptions = null
            , DataflowLinkOptions declinedLinkOptions = null)
        {
            if (builder == null) return null;

            var transformManyBlock = new TransformManyBlock<TOutput, TOutput2>(transformMany, blockOptions ?? DataflowDefaultOptions.DefaultBlockOptions);
            var nextBuilder = builder.LinkToPropagator(transformManyBlock, linkOptions, predicate);

            if (declinedAction != null)
            {
                // LinkTo declined target
                var declinedActionBlock = new ActionBlock<TOutput>(declinedAction, declinedBlockOptions ?? DataflowDefaultOptions.DefaultBlockOptions);
                LinkHelper.Link((builder as SourceDataflowBuilder<TOutput>).FinalSourceBlock, declinedActionBlock, declinedLinkOptions ?? DataflowDefaultOptions.DefaultLinkOptions);
            }

            return nextBuilder;
        }

        /// <summary>
        /// Links to a transformMany function.
        /// </summary>
        /// <typeparam name="TOutput"></typeparam>
        /// <typeparam name="TOutput2"></typeparam>
        /// <param name="builder"></param>
        /// <param name="transformMany"></param>
        /// <param name="blockOptions"></param>
        /// <param name="predicate"></param>
        /// <param name="declinedAction"></param>
        /// <param name="declinedBlockOptions"></param>
        /// <returns></returns>
        public static ISourceDataflowBuilder<TOutput2> LinkToPropagator<TOutput, TOutput2>(
            this ISourceDataflowBuilder<TOutput> builder
            , Func<TOutput, Task<IEnumerable<TOutput2>>> transformMany
            , ExecutionDataflowBlockOptions blockOptions
            , Predicate<TOutput> predicate
            , Func<TOutput, Task> declinedAction
            , ExecutionDataflowBlockOptions declinedBlockOptions = null)
        {
            return builder.LinkToPropagator(transformMany, blockOptions, DataflowDefaultOptions.DefaultLinkOptions, predicate, declinedAction, declinedBlockOptions);
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
            , DataflowLinkOptions declinedLinkOptions = null)
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
            return builder.LinkToTarget(targetBlock, DataflowDefaultOptions.DefaultLinkOptions, predicate, declinedTargetBlock);
        }

        /// <summary>
        /// Links to a target block.
        /// </summary>
        /// <typeparam name="TOutput"></typeparam>
        /// <param name="builder"></param>
        /// <param name="targetBlock"></param>
        /// <param name="linkOptions"></param>
        /// <param name="predicate"></param>
        /// <param name="declinedAction"></param>
        /// <param name="declinedBlockOptions"></param>
        /// <param name="declinedLinkOptions"></param>
        /// <returns></returns>
        public static IDataflowBuilder LinkToTarget<TOutput>(
            this IMultipleSourceDataflowBuilder<TOutput> builder
            , ITargetBlock<TOutput> targetBlock
            , DataflowLinkOptions linkOptions
            , Predicate<TOutput> predicate
            , Action<TOutput> declinedAction
            , ExecutionDataflowBlockOptions declinedBlockOptions = null
            , DataflowLinkOptions declinedLinkOptions = null)
        {
            if (builder == null) return null;
            if (targetBlock == null) throw new ArgumentNullException("targetBlock");
            if (predicate == null) throw new ArgumentNullException("predicate");

            var nextBuilder = builder.LinkToTarget(targetBlock, linkOptions, predicate);

            if (declinedAction != null)
            {
                // LinkTo declined target
                var declinedActionBlock = new ActionBlock<TOutput>(declinedAction, declinedBlockOptions ?? DataflowDefaultOptions.DefaultBlockOptions);
                (builder as MultipleSourceDataflowBuilder<TOutput>).Link(declinedActionBlock, declinedLinkOptions ?? DataflowDefaultOptions.DefaultLinkOptions, null);
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
        /// <param name="declinedAction"></param>
        /// <param name="declinedBlockOptions"></param>
        /// <returns></returns>
        public static IDataflowBuilder LinkToTarget<TOutput>(
            this IMultipleSourceDataflowBuilder<TOutput> builder
            , ITargetBlock<TOutput> targetBlock
            , Predicate<TOutput> predicate
            , Action<TOutput> declinedAction
            , ExecutionDataflowBlockOptions declinedBlockOptions = null)
        {
            return builder.LinkToTarget(targetBlock, DataflowDefaultOptions.DefaultLinkOptions, predicate, declinedAction, declinedBlockOptions);
        }

        /// <summary>
        /// Links to a target block.
        /// </summary>
        /// <typeparam name="TOutput"></typeparam>
        /// <param name="builder"></param>
        /// <param name="targetBlock"></param>
        /// <param name="linkOptions"></param>
        /// <param name="predicate"></param>
        /// <param name="declinedAction"></param>
        /// <param name="declinedBlockOptions"></param>
        /// <param name="declinedLinkOptions"></param>
        /// <returns></returns>
        public static IDataflowBuilder LinkToTarget<TOutput>(
            this IMultipleSourceDataflowBuilder<TOutput> builder
            , ITargetBlock<TOutput> targetBlock
            , DataflowLinkOptions linkOptions
            , Predicate<TOutput> predicate
            , Func<TOutput, Task> declinedAction
            , ExecutionDataflowBlockOptions declinedBlockOptions = null
            , DataflowLinkOptions declinedLinkOptions = null)
        {
            if (builder == null) return null;
            if (targetBlock == null) throw new ArgumentNullException("targetBlock");
            if (predicate == null) throw new ArgumentNullException("predicate");

            var nextBuilder = builder.LinkToTarget(targetBlock, linkOptions, predicate);

            if (declinedAction != null)
            {
                // LinkTo declined target
                var declinedActionBlock = new ActionBlock<TOutput>(declinedAction, declinedBlockOptions ?? DataflowDefaultOptions.DefaultBlockOptions);
                (builder as MultipleSourceDataflowBuilder<TOutput>).Link(declinedActionBlock, declinedLinkOptions ?? DataflowDefaultOptions.DefaultLinkOptions, null);
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
        /// <param name="declinedAction"></param>
        /// <param name="declinedBlockOptions"></param>
        /// <returns></returns>
        public static IDataflowBuilder LinkToTarget<TOutput>(
            this IMultipleSourceDataflowBuilder<TOutput> builder
            , ITargetBlock<TOutput> targetBlock
            , Predicate<TOutput> predicate
            , Func<TOutput, Task> declinedAction
            , ExecutionDataflowBlockOptions declinedBlockOptions = null)
        {
            return builder.LinkToTarget(targetBlock, DataflowDefaultOptions.DefaultLinkOptions, predicate, declinedAction, declinedBlockOptions);
        }

        #endregion

        #region IMultipleSourceDataflowBuilder link to action

        /// <summary>
        /// Links to a target action.
        /// </summary>
        /// <typeparam name="TOutput"></typeparam>
        /// <param name="builder"></param>
        /// <param name="action"></param>
        /// <param name="blockOptions"></param>
        /// <param name="linkOptions"></param>
        /// <param name="predicate"></param>
        /// <param name="declinedTargetBlock"></param>
        /// <param name="declinedLinkOptions"></param>
        /// <returns></returns>
        public static IDataflowBuilder LinkToTarget<TOutput>(
            this IMultipleSourceDataflowBuilder<TOutput> builder
            , Action<TOutput> action
            , ExecutionDataflowBlockOptions blockOptions = null
            , DataflowLinkOptions linkOptions = null
            , Predicate<TOutput> predicate = null
            , ITargetBlock<TOutput> declinedTargetBlock = null
            , DataflowLinkOptions declinedLinkOptions = null)
        {
            if (builder == null) return null;

            var actionBlock = new ActionBlock<TOutput>(action, blockOptions ?? DataflowDefaultOptions.DefaultBlockOptions);
            var nextBuilder = builder.LinkToTarget(actionBlock, linkOptions, predicate);

            if (declinedTargetBlock != null)
            {
                // LinkTo declined target
                (builder as MultipleSourceDataflowBuilder<TOutput>).Link(declinedTargetBlock, declinedLinkOptions ?? DataflowDefaultOptions.DefaultLinkOptions, null);
            }

            return nextBuilder;
        }

        /// <summary>
        /// Links to a target action.
        /// </summary>
        /// <typeparam name="TOutput"></typeparam>
        /// <param name="builder"></param>
        /// <param name="action"></param>
        /// <param name="blockOptions"></param>
        /// <param name="predicate"></param>
        /// <param name="declinedTargetBlock"></param>
        /// <returns></returns>
        public static IDataflowBuilder LinkToTarget<TOutput>(
            this IMultipleSourceDataflowBuilder<TOutput> builder
            , Action<TOutput> action
            , ExecutionDataflowBlockOptions blockOptions
            , Predicate<TOutput> predicate
            , ITargetBlock<TOutput> declinedTargetBlock = null)
        {
            return builder.LinkToTarget(action, blockOptions, DataflowDefaultOptions.DefaultLinkOptions, predicate, declinedTargetBlock);
        }

        /// <summary>
        /// Links to a target action.
        /// </summary>
        /// <typeparam name="TOutput"></typeparam>
        /// <param name="builder"></param>
        /// <param name="action"></param>
        /// <param name="blockOptions"></param>
        /// <param name="linkOptions"></param>
        /// <param name="predicate"></param>
        /// <param name="declinedAction"></param>
        /// <param name="declinedBlockOptions"></param>
        /// <param name="declinedLinkOptions"></param>
        /// <returns></returns>
        public static IDataflowBuilder LinkToTarget<TOutput>(
            this IMultipleSourceDataflowBuilder<TOutput> builder
            , Action<TOutput> action
            , ExecutionDataflowBlockOptions blockOptions
            , DataflowLinkOptions linkOptions
            , Predicate<TOutput> predicate
            , Action<TOutput> declinedAction
            , ExecutionDataflowBlockOptions declinedBlockOptions = null
            , DataflowLinkOptions declinedLinkOptions = null)
        {
            if (builder == null) return null;
            if (action == null) throw new ArgumentNullException("action");
            if (predicate == null) throw new ArgumentNullException("predicate");

            var actionBlock = new ActionBlock<TOutput>(action, blockOptions ?? DataflowDefaultOptions.DefaultBlockOptions);
            var nextBuilder = builder.LinkToTarget(actionBlock, linkOptions, predicate);

            if (declinedAction != null)
            {
                // LinkTo declined target
                var declinedActionBlock = new ActionBlock<TOutput>(declinedAction, declinedBlockOptions ?? DataflowDefaultOptions.DefaultBlockOptions);
                (builder as MultipleSourceDataflowBuilder<TOutput>).Link(declinedActionBlock, declinedLinkOptions ?? DataflowDefaultOptions.DefaultLinkOptions, null);
            }

            return nextBuilder;

        }

        /// <summary>
        /// Links to a target action.
        /// </summary>
        /// <typeparam name="TOutput"></typeparam>
        /// <param name="builder"></param>
        /// <param name="action"></param>
        /// <param name="blockOptions"></param>
        /// <param name="predicate"></param>
        /// <param name="declinedAction"></param>
        /// <param name="declinedBlockOptions"></param>
        /// <returns></returns>
        public static IDataflowBuilder LinkToTarget<TOutput>(
            this IMultipleSourceDataflowBuilder<TOutput> builder
            , Action<TOutput> action
            , ExecutionDataflowBlockOptions blockOptions
            , Predicate<TOutput> predicate
            , Action<TOutput> declinedAction
            , ExecutionDataflowBlockOptions declinedBlockOptions = null)
        {
            return builder.LinkToTarget(action, DataflowDefaultOptions.DefaultBlockOptions, DataflowDefaultOptions.DefaultLinkOptions, predicate, declinedAction, declinedBlockOptions);
        }

        /// <summary>
        /// Links to a target action.
        /// </summary>
        /// <typeparam name="TOutput"></typeparam>
        /// <param name="builder"></param>
        /// <param name="action"></param>
        /// <param name="blockOptions"></param>
        /// <param name="linkOptions"></param>
        /// <param name="predicate"></param>
        /// <param name="declinedAction"></param>
        /// <param name="declinedBlockOptions"></param>
        /// <param name="declinedLinkOptions"></param>
        /// <returns></returns>
        public static IDataflowBuilder LinkToTarget<TOutput>(
            this IMultipleSourceDataflowBuilder<TOutput> builder
            , Action<TOutput> action
            , ExecutionDataflowBlockOptions blockOptions
            , DataflowLinkOptions linkOptions
            , Predicate<TOutput> predicate
            , Func<TOutput, Task> declinedAction
            , ExecutionDataflowBlockOptions declinedBlockOptions = null
            , DataflowLinkOptions declinedLinkOptions = null)
        {
            if (builder == null) return null;
            if (action == null) throw new ArgumentNullException("action");
            if (predicate == null) throw new ArgumentNullException("predicate");

            var actionBlock = new ActionBlock<TOutput>(action, blockOptions ?? DataflowDefaultOptions.DefaultBlockOptions);
            var nextBuilder = builder.LinkToTarget(actionBlock, linkOptions, predicate);

            if (declinedAction != null)
            {
                // LinkTo declined target
                var declinedActionBlock = new ActionBlock<TOutput>(declinedAction, declinedBlockOptions ?? DataflowDefaultOptions.DefaultBlockOptions);
                (builder as MultipleSourceDataflowBuilder<TOutput>).Link(declinedActionBlock, declinedLinkOptions ?? DataflowDefaultOptions.DefaultLinkOptions, null);
            }

            return nextBuilder;

        }

        /// <summary>
        /// Links to a target action.
        /// </summary>
        /// <typeparam name="TOutput"></typeparam>
        /// <param name="builder"></param>
        /// <param name="action"></param>
        /// <param name="blockOptions"></param>
        /// <param name="predicate"></param>
        /// <param name="declinedAction"></param>
        /// <param name="declinedBlockOptions"></param>
        /// <returns></returns>
        public static IDataflowBuilder LinkToTarget<TOutput>(
            this IMultipleSourceDataflowBuilder<TOutput> builder
            , Action<TOutput> action
            , ExecutionDataflowBlockOptions blockOptions
            , Predicate<TOutput> predicate
            , Func<TOutput, Task> declinedAction
            , ExecutionDataflowBlockOptions declinedBlockOptions = null)
        {
            return builder.LinkToTarget(action, DataflowDefaultOptions.DefaultBlockOptions, DataflowDefaultOptions.DefaultLinkOptions, predicate, declinedAction, declinedBlockOptions);
        }

        /// <summary>
        /// Links to a target action.
        /// </summary>
        /// <typeparam name="TOutput"></typeparam>
        /// <param name="builder"></param>
        /// <param name="action"></param>
        /// <param name="blockOptions"></param>
        /// <param name="linkOptions"></param>
        /// <param name="predicate"></param>
        /// <param name="declinedTargetBlock"></param>
        /// <param name="declinedLinkOptions"></param>
        /// <returns></returns>
        public static IDataflowBuilder LinkToTarget<TOutput>(
            this IMultipleSourceDataflowBuilder<TOutput> builder
            , Func<TOutput, Task> action
            , ExecutionDataflowBlockOptions blockOptions = null
            , DataflowLinkOptions linkOptions = null
            , Predicate<TOutput> predicate = null
            , ITargetBlock<TOutput> declinedTargetBlock = null
            , DataflowLinkOptions declinedLinkOptions = null)
        {
            if (builder == null) return null;
            if (action == null) throw new ArgumentNullException("action");

            var actionBlock = new ActionBlock<TOutput>(action, blockOptions ?? DataflowDefaultOptions.DefaultBlockOptions);
            var nextBuilder = builder.LinkToTarget(actionBlock, linkOptions, predicate);

            if (declinedTargetBlock != null)
            {
                // LinkTo declined target
                (builder as MultipleSourceDataflowBuilder<TOutput>).Link(declinedTargetBlock, declinedLinkOptions ?? DataflowDefaultOptions.DefaultLinkOptions, null);
            }

            return nextBuilder;
        }

        /// <summary>
        /// Links to a target action.
        /// </summary>
        /// <typeparam name="TOutput"></typeparam>
        /// <param name="builder"></param>
        /// <param name="action"></param>
        /// <param name="blockOptions"></param>
        /// <param name="predicate"></param>
        /// <param name="declinedTargetBlock"></param>
        /// <returns></returns>
        public static IDataflowBuilder LinkToTarget<TOutput>(
            this IMultipleSourceDataflowBuilder<TOutput> builder
            , Func<TOutput, Task> action
            , ExecutionDataflowBlockOptions blockOptions
            , Predicate<TOutput> predicate
            , ITargetBlock<TOutput> declinedTargetBlock = null)
        {
            return builder.LinkToTarget(action, blockOptions, DataflowDefaultOptions.DefaultLinkOptions, predicate, declinedTargetBlock);
        }

        /// <summary>
        /// Links to a target action.
        /// </summary>
        /// <typeparam name="TOutput"></typeparam>
        /// <param name="builder"></param>
        /// <param name="action"></param>
        /// <param name="blockOptions"></param>
        /// <param name="linkOptions"></param>
        /// <param name="predicate"></param>
        /// <param name="declinedAction"></param>
        /// <param name="declinedBlockOptions"></param>
        /// <param name="declinedLinkOptions"></param>
        /// <returns></returns>
        public static IDataflowBuilder LinkToTarget<TOutput>(
            this IMultipleSourceDataflowBuilder<TOutput> builder
            , Func<TOutput, Task> action
            , ExecutionDataflowBlockOptions blockOptions
            , DataflowLinkOptions linkOptions
            , Predicate<TOutput> predicate
            , Action<TOutput> declinedAction
            , ExecutionDataflowBlockOptions declinedBlockOptions = null
            , DataflowLinkOptions declinedLinkOptions = null)
        {
            if (builder == null) return null;
            if (action == null) throw new ArgumentNullException("action");
            if (predicate == null) throw new ArgumentNullException("predicate");

            var actionBlock = new ActionBlock<TOutput>(action, blockOptions ?? DataflowDefaultOptions.DefaultBlockOptions);
            var nextBuilder = builder.LinkToTarget(actionBlock, linkOptions, predicate);

            if (declinedAction != null)
            {
                // LinkTo declined target
                var declinedActionBlock = new ActionBlock<TOutput>(declinedAction, declinedBlockOptions ?? DataflowDefaultOptions.DefaultBlockOptions);
                (builder as MultipleSourceDataflowBuilder<TOutput>).Link(declinedActionBlock, declinedLinkOptions ?? DataflowDefaultOptions.DefaultLinkOptions, null);
            }

            return nextBuilder;

        }

        /// <summary>
        /// Links to a target action.
        /// </summary>
        /// <typeparam name="TOutput"></typeparam>
        /// <param name="builder"></param>
        /// <param name="action"></param>
        /// <param name="blockOptions"></param>
        /// <param name="predicate"></param>
        /// <param name="declinedAction"></param>
        /// <param name="declinedBlockOptions"></param>
        /// <returns></returns>
        public static IDataflowBuilder LinkToTarget<TOutput>(
            this IMultipleSourceDataflowBuilder<TOutput> builder
            , Func<TOutput, Task> action
            , ExecutionDataflowBlockOptions blockOptions
            , Predicate<TOutput> predicate
            , Action<TOutput> declinedAction
            , ExecutionDataflowBlockOptions declinedBlockOptions = null)
        {
            return builder.LinkToTarget(action, DataflowDefaultOptions.DefaultBlockOptions, DataflowDefaultOptions.DefaultLinkOptions, predicate, declinedAction, declinedBlockOptions);
        }

        /// <summary>
        /// Links to a target action.
        /// </summary>
        /// <typeparam name="TOutput"></typeparam>
        /// <param name="builder"></param>
        /// <param name="action"></param>
        /// <param name="blockOptions"></param>
        /// <param name="linkOptions"></param>
        /// <param name="predicate"></param>
        /// <param name="declinedAction"></param>
        /// <param name="declinedBlockOptions"></param>
        /// <param name="declinedLinkOptions"></param>
        /// <returns></returns>
        public static IDataflowBuilder LinkToTarget<TOutput>(
            this IMultipleSourceDataflowBuilder<TOutput> builder
            , Func<TOutput, Task> action
            , ExecutionDataflowBlockOptions blockOptions
            , DataflowLinkOptions linkOptions
            , Predicate<TOutput> predicate
            , Func<TOutput, Task> declinedAction
            , ExecutionDataflowBlockOptions declinedBlockOptions = null
            , DataflowLinkOptions declinedLinkOptions = null)
        {
            if (builder == null) return null;
            if (action == null) throw new ArgumentNullException("action");
            if (predicate == null) throw new ArgumentNullException("predicate");

            var actionBlock = new ActionBlock<TOutput>(action, blockOptions ?? DataflowDefaultOptions.DefaultBlockOptions);
            var nextBuilder = builder.LinkToTarget(actionBlock, linkOptions, predicate);

            if (declinedAction != null)
            {
                // LinkTo declined target
                var declinedActionBlock = new ActionBlock<TOutput>(declinedAction, declinedBlockOptions ?? DataflowDefaultOptions.DefaultBlockOptions);
                (builder as MultipleSourceDataflowBuilder<TOutput>).Link(declinedActionBlock, declinedLinkOptions ?? DataflowDefaultOptions.DefaultLinkOptions, null);
            }

            return nextBuilder;

        }

        /// <summary>
        /// Links to a target action.
        /// </summary>
        /// <typeparam name="TOutput"></typeparam>
        /// <param name="builder"></param>
        /// <param name="action"></param>
        /// <param name="blockOptions"></param>
        /// <param name="predicate"></param>
        /// <param name="declinedAction"></param>
        /// <param name="declinedBlockOptions"></param>
        /// <returns></returns>
        public static IDataflowBuilder LinkToTarget<TOutput>(
            this IMultipleSourceDataflowBuilder<TOutput> builder
            , Func<TOutput, Task> action
            , ExecutionDataflowBlockOptions blockOptions
            , Predicate<TOutput> predicate
            , Func<TOutput, Task> declinedAction
            , ExecutionDataflowBlockOptions declinedBlockOptions = null)
        {
            return builder.LinkToTarget(action, DataflowDefaultOptions.DefaultBlockOptions, DataflowDefaultOptions.DefaultLinkOptions, predicate, declinedAction, declinedBlockOptions);
        }

        #endregion

        #region IMultipleSourceDataflowBuilder link to transform function

        /// <summary>
        /// Links to a transform function.
        /// </summary>
        /// <typeparam name="TOutput"></typeparam>
        /// <typeparam name="TOutput2"></typeparam>
        /// <param name="builder"></param>
        /// <param name="transform"></param>
        /// <param name="blockOptions"></param>
        /// <param name="linkOptions"></param>
        /// <param name="predicate"></param>
        /// <param name="declinedTargetBlock"></param>
        /// <param name="declinedLinkOptions"></param>
        /// <returns></returns>
        public static ISourceDataflowBuilder<TOutput2> LinkToPropagator<TOutput, TOutput2>(
            this IMultipleSourceDataflowBuilder<TOutput> builder
            , Func<TOutput, TOutput2> transform
            , ExecutionDataflowBlockOptions blockOptions = null
            , DataflowLinkOptions linkOptions = null
            , Predicate<TOutput> predicate = null
            , ITargetBlock<TOutput> declinedTargetBlock = null
            , DataflowLinkOptions declinedLinkOptions = null)
        {
            if (builder == null) return null;

            var transformBlock = new TransformBlock<TOutput, TOutput2>(transform, blockOptions ?? DataflowDefaultOptions.DefaultBlockOptions);
            var nextBuilder =  builder.LinkToPropagator(transformBlock, linkOptions, predicate);

            if (declinedTargetBlock != null)
            {
                // LinkTo declined target
                (builder as MultipleSourceDataflowBuilder<TOutput>).Link(declinedTargetBlock, declinedLinkOptions ?? DataflowDefaultOptions.DefaultLinkOptions, null);
            }

            return nextBuilder;
        }

        /// <summary>
        /// Links to a transform function.
        /// </summary>
        /// <typeparam name="TOutput"></typeparam>
        /// <typeparam name="TOutput2"></typeparam>
        /// <param name="builder"></param>
        /// <param name="transform"></param>
        /// <param name="blockOptions"></param>
        /// <param name="predicate"></param>
        /// <param name="declinedTargetBlock"></param>
        /// <returns></returns>
        public static ISourceDataflowBuilder<TOutput2> LinkToPropagator<TOutput, TOutput2>(
            this IMultipleSourceDataflowBuilder<TOutput> builder
            , Func<TOutput, TOutput2> transform
            , ExecutionDataflowBlockOptions blockOptions
            , Predicate<TOutput> predicate
            , ITargetBlock<TOutput> declinedTargetBlock = null)
        {
            return builder.LinkToPropagator(transform, blockOptions, DataflowDefaultOptions.DefaultLinkOptions, predicate, declinedTargetBlock);
        }

        /// <summary>
        /// Links to a transform function.
        /// </summary>
        /// <typeparam name="TOutput"></typeparam>
        /// <typeparam name="TOutput2"></typeparam>
        /// <param name="builder"></param>
        /// <param name="transform"></param>
        /// <param name="blockOptions"></param>
        /// <param name="linkOptions"></param>
        /// <param name="predicate"></param>
        /// <param name="declinedAction"></param>
        /// <param name="declinedBlockOptions"></param>
        /// <param name="declinedLinkOptions"></param>
        /// <returns></returns>
        public static ISourceDataflowBuilder<TOutput2> LinkToPropagator<TOutput, TOutput2>(
            this IMultipleSourceDataflowBuilder<TOutput> builder
            , Func<TOutput, TOutput2> transform
            , ExecutionDataflowBlockOptions blockOptions = null
            , DataflowLinkOptions linkOptions = null
            , Predicate<TOutput> predicate = null
            , Action<TOutput> declinedAction = null
            , ExecutionDataflowBlockOptions declinedBlockOptions = null
            , DataflowLinkOptions declinedLinkOptions = null)
        {
            if (builder == null) return null;
            if (transform == null) throw new ArgumentNullException("transform");

            var transformBlock = new TransformBlock<TOutput, TOutput2>(transform, blockOptions ?? DataflowDefaultOptions.DefaultBlockOptions);
            var nextBuilder = builder.LinkToPropagator(transformBlock, linkOptions, predicate);

            if (declinedAction != null)
            {
                // LinkTo declined target
                var declinedActionBlock = new ActionBlock<TOutput>(declinedAction, declinedBlockOptions ?? DataflowDefaultOptions.DefaultBlockOptions);
                (builder as MultipleSourceDataflowBuilder<TOutput>).Link(declinedActionBlock, declinedLinkOptions ?? DataflowDefaultOptions.DefaultLinkOptions, null);
            }

            return nextBuilder;
        }

        /// <summary>
        /// Links to a transform function.
        /// </summary>
        /// <typeparam name="TOutput"></typeparam>
        /// <typeparam name="TOutput2"></typeparam>
        /// <param name="builder"></param>
        /// <param name="transform"></param>
        /// <param name="blockOptions"></param>
        /// <param name="predicate"></param>
        /// <param name="declinedAction"></param>
        /// <param name="declinedBlockOptions"></param>
        /// <returns></returns>
        public static ISourceDataflowBuilder<TOutput2> LinkToPropagator<TOutput, TOutput2>(
            this IMultipleSourceDataflowBuilder<TOutput> builder
            , Func<TOutput, TOutput2> transform
            , ExecutionDataflowBlockOptions blockOptions
            , Predicate<TOutput> predicate
            , Action<TOutput> declinedAction
            , ExecutionDataflowBlockOptions declinedBlockOptions = null)
        {
            return builder.LinkToPropagator(transform, blockOptions, DataflowDefaultOptions.DefaultLinkOptions, predicate, declinedAction, declinedBlockOptions);
        }

        /// <summary>
        /// Links to a transform function.
        /// </summary>
        /// <typeparam name="TOutput"></typeparam>
        /// <typeparam name="TOutput2"></typeparam>
        /// <param name="builder"></param>
        /// <param name="transform"></param>
        /// <param name="blockOptions"></param>
        /// <param name="linkOptions"></param>
        /// <param name="predicate"></param>
        /// <param name="declinedAction"></param>
        /// <param name="declinedBlockOptions"></param>
        /// <param name="declinedLinkOptions"></param>
        /// <returns></returns>
        public static ISourceDataflowBuilder<TOutput2> LinkToPropagator<TOutput, TOutput2>(
            this IMultipleSourceDataflowBuilder<TOutput> builder
            , Func<TOutput, TOutput2> transform
            , ExecutionDataflowBlockOptions blockOptions = null
            , DataflowLinkOptions linkOptions = null
            , Predicate<TOutput> predicate = null
            , Func<TOutput, Task> declinedAction = null
            , ExecutionDataflowBlockOptions declinedBlockOptions = null
            , DataflowLinkOptions declinedLinkOptions = null)
        {
            if (builder == null) return null;
            if (transform == null) throw new ArgumentNullException("transform");

            var transformBlock = new TransformBlock<TOutput, TOutput2>(transform, blockOptions ?? DataflowDefaultOptions.DefaultBlockOptions);
            var nextBuilder = builder.LinkToPropagator(transformBlock, linkOptions, predicate);

            if (declinedAction != null)
            {
                // LinkTo declined target
                var declinedActionBlock = new ActionBlock<TOutput>(declinedAction, declinedBlockOptions ?? DataflowDefaultOptions.DefaultBlockOptions);
                (builder as MultipleSourceDataflowBuilder<TOutput>).Link(declinedActionBlock, declinedLinkOptions ?? DataflowDefaultOptions.DefaultLinkOptions, null);
            }

            return nextBuilder;
        }

        /// <summary>
        /// Links to a transform function.
        /// </summary>
        /// <typeparam name="TOutput"></typeparam>
        /// <typeparam name="TOutput2"></typeparam>
        /// <param name="builder"></param>
        /// <param name="transform"></param>
        /// <param name="blockOptions"></param>
        /// <param name="predicate"></param>
        /// <param name="declinedAction"></param>
        /// <param name="declinedBlockOptions"></param>
        /// <returns></returns>
        public static ISourceDataflowBuilder<TOutput2> LinkToPropagator<TOutput, TOutput2>(
            this IMultipleSourceDataflowBuilder<TOutput> builder
            , Func<TOutput, TOutput2> transform
            , ExecutionDataflowBlockOptions blockOptions
            , Predicate<TOutput> predicate
            , Func<TOutput, Task> declinedAction
            , ExecutionDataflowBlockOptions declinedBlockOptions = null)
        {
            return builder.LinkToPropagator(transform, blockOptions, DataflowDefaultOptions.DefaultLinkOptions, predicate, declinedAction, declinedBlockOptions);
        }

        /// <summary>
        /// Links to a transform function.
        /// </summary>
        /// <typeparam name="TOutput"></typeparam>
        /// <typeparam name="TOutput2"></typeparam>
        /// <param name="builder"></param>
        /// <param name="transform"></param>
        /// <param name="blockOptions"></param>
        /// <param name="linkOptions"></param>
        /// <param name="predicate"></param>
        /// <param name="declinedTargetBlock"></param>
        /// <param name="declinedLinkOptions"></param>
        /// <returns></returns>
        public static ISourceDataflowBuilder<TOutput2> LinkToPropagator<TOutput, TOutput2>(
            this IMultipleSourceDataflowBuilder<TOutput> builder
            , Func<TOutput, Task<TOutput2>> transform
            , ExecutionDataflowBlockOptions blockOptions = null
            , DataflowLinkOptions linkOptions = null
            , Predicate<TOutput> predicate = null
            , ITargetBlock<TOutput> declinedTargetBlock = null
            , DataflowLinkOptions declinedLinkOptions = null)
        {
            if (builder == null) return null;

            var transformBlock = new TransformBlock<TOutput, TOutput2>(transform, blockOptions ?? DataflowDefaultOptions.DefaultBlockOptions);
            var nextBuilder = builder.LinkToPropagator(transformBlock, linkOptions, predicate);

            if (declinedTargetBlock != null)
            {
                // LinkTo declined target
                (builder as MultipleSourceDataflowBuilder<TOutput>).Link(declinedTargetBlock, declinedLinkOptions ?? DataflowDefaultOptions.DefaultLinkOptions, null);
            }

            return nextBuilder;
        }

        /// <summary>
        /// Links to a transform function.
        /// </summary>
        /// <typeparam name="TOutput"></typeparam>
        /// <typeparam name="TOutput2"></typeparam>
        /// <param name="builder"></param>
        /// <param name="transform"></param>
        /// <param name="blockOptions"></param>
        /// <param name="predicate"></param>
        /// <param name="declinedTargetBlock"></param>
        /// <returns></returns>
        public static ISourceDataflowBuilder<TOutput2> LinkToPropagator<TOutput, TOutput2>(
            this IMultipleSourceDataflowBuilder<TOutput> builder
            , Func<TOutput, Task<TOutput2>> transform
            , ExecutionDataflowBlockOptions blockOptions
            , Predicate<TOutput> predicate
            , ITargetBlock<TOutput> declinedTargetBlock = null)
        {
            return builder.LinkToPropagator(transform, blockOptions, DataflowDefaultOptions.DefaultLinkOptions, predicate, declinedTargetBlock);
        }

        /// <summary>
        /// Links to a transform function.
        /// </summary>
        /// <typeparam name="TOutput"></typeparam>
        /// <typeparam name="TOutput2"></typeparam>
        /// <param name="builder"></param>
        /// <param name="transform"></param>
        /// <param name="blockOptions"></param>
        /// <param name="linkOptions"></param>
        /// <param name="predicate"></param>
        /// <param name="declinedAction"></param>
        /// <param name="declinedBlockOptions"></param>
        /// <param name="declinedLinkOptions"></param>
        /// <returns></returns>
        public static ISourceDataflowBuilder<TOutput2> LinkToPropagator<TOutput, TOutput2>(
            this IMultipleSourceDataflowBuilder<TOutput> builder
            , Func<TOutput, Task<TOutput2>> transform
            , ExecutionDataflowBlockOptions blockOptions = null
            , DataflowLinkOptions linkOptions = null
            , Predicate<TOutput> predicate = null
            , Action<TOutput> declinedAction = null
            , ExecutionDataflowBlockOptions declinedBlockOptions = null
            , DataflowLinkOptions declinedLinkOptions = null)
        {
            if (builder == null) return null;
            if (transform == null) throw new ArgumentNullException("transform");

            var transformBlock = new TransformBlock<TOutput, TOutput2>(transform, blockOptions ?? DataflowDefaultOptions.DefaultBlockOptions);
            var nextBuilder = builder.LinkToPropagator(transformBlock, linkOptions, predicate);

            if (declinedAction != null)
            {
                // LinkTo declined target
                var declinedActionBlock = new ActionBlock<TOutput>(declinedAction, declinedBlockOptions ?? DataflowDefaultOptions.DefaultBlockOptions);
                (builder as MultipleSourceDataflowBuilder<TOutput>).Link(declinedActionBlock, declinedLinkOptions ?? DataflowDefaultOptions.DefaultLinkOptions, null);
            }

            return nextBuilder;
        }

        /// <summary>
        /// Links to a transform function.
        /// </summary>
        /// <typeparam name="TOutput"></typeparam>
        /// <typeparam name="TOutput2"></typeparam>
        /// <param name="builder"></param>
        /// <param name="transform"></param>
        /// <param name="blockOptions"></param>
        /// <param name="predicate"></param>
        /// <param name="declinedAction"></param>
        /// <param name="declinedBlockOptions"></param>
        /// <returns></returns>
        public static ISourceDataflowBuilder<TOutput2> LinkToPropagator<TOutput, TOutput2>(
            this IMultipleSourceDataflowBuilder<TOutput> builder
            , Func<TOutput, Task<TOutput2>> transform
            , ExecutionDataflowBlockOptions blockOptions
            , Predicate<TOutput> predicate
            , Action<TOutput> declinedAction
            , ExecutionDataflowBlockOptions declinedBlockOptions = null)
        {
            return builder.LinkToPropagator(transform, blockOptions, DataflowDefaultOptions.DefaultLinkOptions, predicate, declinedAction, declinedBlockOptions);
        }

        /// <summary>
        /// Links to a transform function.
        /// </summary>
        /// <typeparam name="TOutput"></typeparam>
        /// <typeparam name="TOutput2"></typeparam>
        /// <param name="builder"></param>
        /// <param name="transform"></param>
        /// <param name="blockOptions"></param>
        /// <param name="linkOptions"></param>
        /// <param name="predicate"></param>
        /// <param name="declinedAction"></param>
        /// <param name="declinedBlockOptions"></param>
        /// <param name="declinedLinkOptions"></param>
        /// <returns></returns>
        public static ISourceDataflowBuilder<TOutput2> LinkToPropagator<TOutput, TOutput2>(
            this IMultipleSourceDataflowBuilder<TOutput> builder
            , Func<TOutput, Task<TOutput2>> transform
            , ExecutionDataflowBlockOptions blockOptions = null
            , DataflowLinkOptions linkOptions = null
            , Predicate<TOutput> predicate = null
            , Func<TOutput, Task> declinedAction = null
            , ExecutionDataflowBlockOptions declinedBlockOptions = null
            , DataflowLinkOptions declinedLinkOptions = null)
        {
            if (builder == null) return null;
            if (transform == null) throw new ArgumentNullException("transform");

            var transformBlock = new TransformBlock<TOutput, TOutput2>(transform, blockOptions ?? DataflowDefaultOptions.DefaultBlockOptions);
            var nextBuilder = builder.LinkToPropagator(transformBlock, linkOptions, predicate);

            if (declinedAction != null)
            {
                // LinkTo declined target
                var declinedActionBlock = new ActionBlock<TOutput>(declinedAction, declinedBlockOptions ?? DataflowDefaultOptions.DefaultBlockOptions);
                (builder as MultipleSourceDataflowBuilder<TOutput>).Link(declinedActionBlock, declinedLinkOptions ?? DataflowDefaultOptions.DefaultLinkOptions, null);
            }

            return nextBuilder;
        }

        /// <summary>
        /// Links to a transform function.
        /// </summary>
        /// <typeparam name="TOutput"></typeparam>
        /// <typeparam name="TOutput2"></typeparam>
        /// <param name="builder"></param>
        /// <param name="transform"></param>
        /// <param name="blockOptions"></param>
        /// <param name="predicate"></param>
        /// <param name="declinedAction"></param>
        /// <param name="declinedBlockOptions"></param>
        /// <returns></returns>
        public static ISourceDataflowBuilder<TOutput2> LinkToPropagator<TOutput, TOutput2>(
            this IMultipleSourceDataflowBuilder<TOutput> builder
            , Func<TOutput, Task<TOutput2>> transform
            , ExecutionDataflowBlockOptions blockOptions
            , Predicate<TOutput> predicate
            , Func<TOutput, Task> declinedAction
            , ExecutionDataflowBlockOptions declinedBlockOptions = null)
        {
            return builder.LinkToPropagator(transform, blockOptions, DataflowDefaultOptions.DefaultLinkOptions, predicate, declinedAction, declinedBlockOptions);
        }

        #endregion

        #region IMultipleSourceDataflowBuilder link to transformMany function

        /// <summary>
        /// Links to a transformMany function.
        /// </summary>
        /// <typeparam name="TOutput"></typeparam>
        /// <typeparam name="TOutput2"></typeparam>
        /// <param name="builder"></param>
        /// <param name="transformMany"></param>
        /// <param name="blockOptions"></param>
        /// <param name="linkOptions"></param>
        /// <param name="predicate"></param>
        /// <param name="declinedTargetBlock"></param>
        /// <param name="declinedLinkOptions"></param>
        /// <returns></returns>
        public static ISourceDataflowBuilder<TOutput2> LinkToPropagator<TOutput, TOutput2>(
            this IMultipleSourceDataflowBuilder<TOutput> builder
            , Func<TOutput, IEnumerable<TOutput2>> transformMany
            , ExecutionDataflowBlockOptions blockOptions = null
            , DataflowLinkOptions linkOptions = null
            , Predicate<TOutput> predicate = null
            , ITargetBlock<TOutput> declinedTargetBlock = null
            , DataflowLinkOptions declinedLinkOptions = null)
        {
            if (builder == null) return null;

            var transformManyBlock = new TransformManyBlock<TOutput, TOutput2>(transformMany, blockOptions ?? DataflowDefaultOptions.DefaultBlockOptions);
            var nextBuilder = builder.LinkToPropagator(transformManyBlock, linkOptions, predicate);

            if (declinedTargetBlock != null)
            {
                // LinkTo declined target
                (builder as MultipleSourceDataflowBuilder<TOutput>).Link(declinedTargetBlock, declinedLinkOptions ?? DataflowDefaultOptions.DefaultLinkOptions, null);
            }

            return nextBuilder;
        }

        /// <summary>
        /// Links to a transformMany function.
        /// </summary>
        /// <typeparam name="TOutput"></typeparam>
        /// <typeparam name="TOutput2"></typeparam>
        /// <param name="builder"></param>
        /// <param name="transformMany"></param>
        /// <param name="blockOptions"></param>
        /// <param name="predicate"></param>
        /// <param name="declinedTargetBlock"></param>
        /// <returns></returns>
        public static ISourceDataflowBuilder<TOutput2> LinkToPropagator<TOutput, TOutput2>(
            this IMultipleSourceDataflowBuilder<TOutput> builder
            , Func<TOutput, IEnumerable<TOutput2>> transformMany
            , ExecutionDataflowBlockOptions blockOptions
            , Predicate<TOutput> predicate
            , ITargetBlock<TOutput> declinedTargetBlock = null)
        {
            return builder.LinkToPropagator(transformMany, blockOptions, DataflowDefaultOptions.DefaultLinkOptions, predicate, declinedTargetBlock);
        }

        /// <summary>
        /// Links to a transformMany function.
        /// </summary>
        /// <typeparam name="TOutput"></typeparam>
        /// <typeparam name="TOutput2"></typeparam>
        /// <param name="builder"></param>
        /// <param name="transformMany"></param>
        /// <param name="blockOptions"></param>
        /// <param name="linkOptions"></param>
        /// <param name="predicate"></param>
        /// <param name="declinedAction"></param>
        /// <param name="declinedBlockOptions"></param>
        /// <param name="declinedLinkOptions"></param>
        /// <returns></returns>
        public static ISourceDataflowBuilder<TOutput2> LinkToPropagator<TOutput, TOutput2>(
            this IMultipleSourceDataflowBuilder<TOutput> builder
            , Func<TOutput, IEnumerable<TOutput2>> transformMany
            , ExecutionDataflowBlockOptions blockOptions = null
            , DataflowLinkOptions linkOptions = null
            , Predicate<TOutput> predicate = null
            , Action<TOutput> declinedAction = null
            , ExecutionDataflowBlockOptions declinedBlockOptions = null
            , DataflowLinkOptions declinedLinkOptions = null)
        {
            if (builder == null) return null;

            var transformManyBlock = new TransformManyBlock<TOutput, TOutput2>(transformMany, blockOptions ?? DataflowDefaultOptions.DefaultBlockOptions);
            var nextBuilder = builder.LinkToPropagator(transformManyBlock, linkOptions, predicate);

            if (declinedAction != null)
            {
                // LinkTo declined target
                var declinedActionBlock = new ActionBlock<TOutput>(declinedAction, declinedBlockOptions ?? DataflowDefaultOptions.DefaultBlockOptions);
                (builder as MultipleSourceDataflowBuilder<TOutput>).Link(declinedActionBlock, declinedLinkOptions ?? DataflowDefaultOptions.DefaultLinkOptions, null);
            }

            return nextBuilder;
        }

        /// <summary>
        /// Links to a transformMany function.
        /// </summary>
        /// <typeparam name="TOutput"></typeparam>
        /// <typeparam name="TOutput2"></typeparam>
        /// <param name="builder"></param>
        /// <param name="transformMany"></param>
        /// <param name="blockOptions"></param>
        /// <param name="predicate"></param>
        /// <param name="declinedAction"></param>
        /// <param name="declinedBlockOptions"></param>
        /// <returns></returns>
        public static ISourceDataflowBuilder<TOutput2> LinkToPropagator<TOutput, TOutput2>(
            this IMultipleSourceDataflowBuilder<TOutput> builder
            , Func<TOutput, IEnumerable<TOutput2>> transformMany
            , ExecutionDataflowBlockOptions blockOptions
            , Predicate<TOutput> predicate
            , Action<TOutput> declinedAction
            , ExecutionDataflowBlockOptions declinedBlockOptions = null)
        {
            return builder.LinkToPropagator(transformMany, blockOptions, DataflowDefaultOptions.DefaultLinkOptions, predicate, declinedAction, declinedBlockOptions);
        }

        /// <summary>
        /// Links to a transformMany function.
        /// </summary>
        /// <typeparam name="TOutput"></typeparam>
        /// <typeparam name="TOutput2"></typeparam>
        /// <param name="builder"></param>
        /// <param name="transformMany"></param>
        /// <param name="blockOptions"></param>
        /// <param name="linkOptions"></param>
        /// <param name="predicate"></param>
        /// <param name="declinedAction"></param>
        /// <param name="declinedBlockOptions"></param>
        /// <param name="declinedLinkOptions"></param>
        /// <returns></returns>
        public static ISourceDataflowBuilder<TOutput2> LinkToPropagator<TOutput, TOutput2>(
            this IMultipleSourceDataflowBuilder<TOutput> builder
            , Func<TOutput, IEnumerable<TOutput2>> transformMany
            , ExecutionDataflowBlockOptions blockOptions = null
            , DataflowLinkOptions linkOptions = null
            , Predicate<TOutput> predicate = null
            , Func<TOutput, Task> declinedAction = null
            , ExecutionDataflowBlockOptions declinedBlockOptions = null
            , DataflowLinkOptions declinedLinkOptions = null)
        {
            if (builder == null) return null;

            var transformManyBlock = new TransformManyBlock<TOutput, TOutput2>(transformMany, blockOptions ?? DataflowDefaultOptions.DefaultBlockOptions);
            var nextBuilder = builder.LinkToPropagator(transformManyBlock, linkOptions, predicate);

            if (declinedAction != null)
            {
                // LinkTo declined target
                var declinedActionBlock = new ActionBlock<TOutput>(declinedAction, declinedBlockOptions ?? DataflowDefaultOptions.DefaultBlockOptions);
                (builder as MultipleSourceDataflowBuilder<TOutput>).Link(declinedActionBlock, declinedLinkOptions ?? DataflowDefaultOptions.DefaultLinkOptions, null);
            }

            return nextBuilder;
        }

        /// <summary>
        /// Links to a transformMany function.
        /// </summary>
        /// <typeparam name="TOutput"></typeparam>
        /// <typeparam name="TOutput2"></typeparam>
        /// <param name="builder"></param>
        /// <param name="transformMany"></param>
        /// <param name="blockOptions"></param>
        /// <param name="predicate"></param>
        /// <param name="declinedAction"></param>
        /// <param name="declinedBlockOptions"></param>
        /// <returns></returns>
        public static ISourceDataflowBuilder<TOutput2> LinkToPropagator<TOutput, TOutput2>(
            this IMultipleSourceDataflowBuilder<TOutput> builder
            , Func<TOutput, IEnumerable<TOutput2>> transformMany
            , ExecutionDataflowBlockOptions blockOptions
            , Predicate<TOutput> predicate
            , Func<TOutput, Task> declinedAction
            , ExecutionDataflowBlockOptions declinedBlockOptions = null)
        {
            return builder.LinkToPropagator(transformMany, blockOptions, DataflowDefaultOptions.DefaultLinkOptions, predicate, declinedAction, declinedBlockOptions);
        }

        /// <summary>
        /// Links to a transformMany function.
        /// </summary>
        /// <typeparam name="TOutput"></typeparam>
        /// <typeparam name="TOutput2"></typeparam>
        /// <param name="builder"></param>
        /// <param name="transformMany"></param>
        /// <param name="blockOptions"></param>
        /// <param name="linkOptions"></param>
        /// <param name="predicate"></param>
        /// <param name="declinedTargetBlock"></param>
        /// <param name="declinedLinkOptions"></param>
        /// <returns></returns>
        public static ISourceDataflowBuilder<TOutput2> LinkToPropagator<TOutput, TOutput2>(
            this IMultipleSourceDataflowBuilder<TOutput> builder
            , Func<TOutput, Task<IEnumerable<TOutput2>>> transformMany
            , ExecutionDataflowBlockOptions blockOptions = null
            , DataflowLinkOptions linkOptions = null
            , Predicate<TOutput> predicate = null
            , ITargetBlock<TOutput> declinedTargetBlock = null
            , DataflowLinkOptions declinedLinkOptions = null)
        {
            if (builder == null) return null;

            var transformManyBlock = new TransformManyBlock<TOutput, TOutput2>(transformMany, blockOptions ?? DataflowDefaultOptions.DefaultBlockOptions);
            var nextBuilder = builder.LinkToPropagator(transformManyBlock, linkOptions, predicate);

            if (declinedTargetBlock != null)
            {
                // LinkTo declined target
                (builder as MultipleSourceDataflowBuilder<TOutput>).Link(declinedTargetBlock, declinedLinkOptions ?? DataflowDefaultOptions.DefaultLinkOptions, null);
            }

            return nextBuilder;
        }

        /// <summary>
        /// Links to a transformMany function.
        /// </summary>
        /// <typeparam name="TOutput"></typeparam>
        /// <typeparam name="TOutput2"></typeparam>
        /// <param name="builder"></param>
        /// <param name="transformMany"></param>
        /// <param name="blockOptions"></param>
        /// <param name="predicate"></param>
        /// <param name="declinedTargetBlock"></param>
        /// <returns></returns>
        public static ISourceDataflowBuilder<TOutput2> LinkToPropagator<TOutput, TOutput2>(
            this IMultipleSourceDataflowBuilder<TOutput> builder
            , Func<TOutput, Task<IEnumerable<TOutput2>>> transformMany
            , ExecutionDataflowBlockOptions blockOptions
            , Predicate<TOutput> predicate
            , ITargetBlock<TOutput> declinedTargetBlock = null)
        {
            return builder.LinkToPropagator(transformMany, blockOptions, DataflowDefaultOptions.DefaultLinkOptions, predicate, declinedTargetBlock);
        }

        /// <summary>
        /// Links to a transformMany function.
        /// </summary>
        /// <typeparam name="TOutput"></typeparam>
        /// <typeparam name="TOutput2"></typeparam>
        /// <param name="builder"></param>
        /// <param name="transformMany"></param>
        /// <param name="blockOptions"></param>
        /// <param name="linkOptions"></param>
        /// <param name="predicate"></param>
        /// <param name="declinedAction"></param>
        /// <param name="declinedBlockOptions"></param>
        /// <param name="declinedLinkOptions"></param>
        /// <returns></returns>
        public static ISourceDataflowBuilder<TOutput2> LinkToPropagator<TOutput, TOutput2>(
            this IMultipleSourceDataflowBuilder<TOutput> builder
            , Func<TOutput, Task<IEnumerable<TOutput2>>> transformMany
            , ExecutionDataflowBlockOptions blockOptions = null
            , DataflowLinkOptions linkOptions = null
            , Predicate<TOutput> predicate = null
            , Action<TOutput> declinedAction = null
            , ExecutionDataflowBlockOptions declinedBlockOptions = null
            , DataflowLinkOptions declinedLinkOptions = null)
        {
            if (builder == null) return null;

            var transformManyBlock = new TransformManyBlock<TOutput, TOutput2>(transformMany, blockOptions ?? DataflowDefaultOptions.DefaultBlockOptions);
            var nextBuilder = builder.LinkToPropagator(transformManyBlock, linkOptions, predicate);

            if (declinedAction != null)
            {
                // LinkTo declined target
                var declinedActionBlock = new ActionBlock<TOutput>(declinedAction, declinedBlockOptions ?? DataflowDefaultOptions.DefaultBlockOptions);
                (builder as MultipleSourceDataflowBuilder<TOutput>).Link(declinedActionBlock, declinedLinkOptions ?? DataflowDefaultOptions.DefaultLinkOptions, null);
            }

            return nextBuilder;
        }

        /// <summary>
        /// Links to a transformMany function.
        /// </summary>
        /// <typeparam name="TOutput"></typeparam>
        /// <typeparam name="TOutput2"></typeparam>
        /// <param name="builder"></param>
        /// <param name="transformMany"></param>
        /// <param name="blockOptions"></param>
        /// <param name="predicate"></param>
        /// <param name="declinedAction"></param>
        /// <param name="declinedBlockOptions"></param>
        /// <returns></returns>
        public static ISourceDataflowBuilder<TOutput2> LinkToPropagator<TOutput, TOutput2>(
            this IMultipleSourceDataflowBuilder<TOutput> builder
            , Func<TOutput, Task<IEnumerable<TOutput2>>> transformMany
            , ExecutionDataflowBlockOptions blockOptions
            , Predicate<TOutput> predicate
            , Action<TOutput> declinedAction
            , ExecutionDataflowBlockOptions declinedBlockOptions = null)
        {
            return builder.LinkToPropagator(transformMany, blockOptions, DataflowDefaultOptions.DefaultLinkOptions, predicate, declinedAction, declinedBlockOptions);
        }

        /// <summary>
        /// Links to a transformMany function.
        /// </summary>
        /// <typeparam name="TOutput"></typeparam>
        /// <typeparam name="TOutput2"></typeparam>
        /// <param name="builder"></param>
        /// <param name="transformMany"></param>
        /// <param name="blockOptions"></param>
        /// <param name="linkOptions"></param>
        /// <param name="predicate"></param>
        /// <param name="declinedAction"></param>
        /// <param name="declinedBlockOptions"></param>
        /// <param name="declinedLinkOptions"></param>
        /// <returns></returns>
        public static ISourceDataflowBuilder<TOutput2> LinkToPropagator<TOutput, TOutput2>(
            this IMultipleSourceDataflowBuilder<TOutput> builder
            , Func<TOutput, Task<IEnumerable<TOutput2>>> transformMany
            , ExecutionDataflowBlockOptions blockOptions = null
            , DataflowLinkOptions linkOptions = null
            , Predicate<TOutput> predicate = null
            , Func<TOutput, Task> declinedAction = null
            , ExecutionDataflowBlockOptions declinedBlockOptions = null
            , DataflowLinkOptions declinedLinkOptions = null)
        {
            if (builder == null) return null;

            var transformManyBlock = new TransformManyBlock<TOutput, TOutput2>(transformMany, blockOptions ?? DataflowDefaultOptions.DefaultBlockOptions);
            var nextBuilder = builder.LinkToPropagator(transformManyBlock, linkOptions, predicate);

            if (declinedAction != null)
            {
                // LinkTo declined target
                var declinedActionBlock = new ActionBlock<TOutput>(declinedAction, declinedBlockOptions ?? DataflowDefaultOptions.DefaultBlockOptions);
                (builder as MultipleSourceDataflowBuilder<TOutput>).Link(declinedActionBlock, declinedLinkOptions ?? DataflowDefaultOptions.DefaultLinkOptions, null);
            }

            return nextBuilder;
        }

        /// <summary>
        /// Links to a transformMany function.
        /// </summary>
        /// <typeparam name="TOutput"></typeparam>
        /// <typeparam name="TOutput2"></typeparam>
        /// <param name="builder"></param>
        /// <param name="transformMany"></param>
        /// <param name="blockOptions"></param>
        /// <param name="predicate"></param>
        /// <param name="declinedAction"></param>
        /// <param name="declinedBlockOptions"></param>
        /// <returns></returns>
        public static ISourceDataflowBuilder<TOutput2> LinkToPropagator<TOutput, TOutput2>(
            this IMultipleSourceDataflowBuilder<TOutput> builder
            , Func<TOutput, Task<IEnumerable<TOutput2>>> transformMany
            , ExecutionDataflowBlockOptions blockOptions
            , Predicate<TOutput> predicate
            , Func<TOutput, Task> declinedAction
            , ExecutionDataflowBlockOptions declinedBlockOptions = null)
        {
            return builder.LinkToPropagator(transformMany, blockOptions, DataflowDefaultOptions.DefaultLinkOptions, predicate, declinedAction, declinedBlockOptions);
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
            , DataflowLinkOptions declinedLinkOptions = null)
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
            return builder.LinkTo(targetBlock, DataflowDefaultOptions.DefaultLinkOptions, predicate, declinedTargetBlock);
        }

        /// <summary>
        /// Links to a target block.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="builder"></param>
        /// <param name="targetBlock"></param>
        /// <param name="linkOptions"></param>
        /// <param name="predicate"></param>
        /// <param name="declinedAction"></param>
        /// <param name="declinedBlockOptions"></param>
        /// <param name="declinedLinkOptions"></param>
        /// <returns></returns>
        public static IBroadcastDataflowBuilder<T> LinkTo<T>(
            this IBroadcastDataflowBuilder<T> builder
            , ITargetBlock<T> targetBlock
            , DataflowLinkOptions linkOptions
            , Predicate<T> predicate
            , Action<T> declinedAction
            , ExecutionDataflowBlockOptions declinedBlockOptions = null
            , DataflowLinkOptions declinedLinkOptions = null)
        {
            if (builder == null) return null;
            if (targetBlock == null) throw new ArgumentNullException("targetBlock");
            if (predicate == null) throw new ArgumentNullException("predicate");

            var nextBuilder = builder.LinkTo(targetBlock, linkOptions, predicate);

            if (declinedAction != null)
            {
                // LinkTo declined target
                var declinedActionBlock = new ActionBlock<T>(declinedAction, declinedBlockOptions ?? DataflowDefaultOptions.DefaultBlockOptions);
                LinkHelper.Link((builder as BroadcastDataflowBuilder<T>).BroadcastBock, declinedActionBlock, declinedLinkOptions ?? DataflowDefaultOptions.DefaultLinkOptions);
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
        /// <param name="declinedAction"></param>
        /// <param name="declinedBlockOptions"></param>
        /// <returns></returns>
        public static IBroadcastDataflowBuilder<T> LinkTo<T>(
            this IBroadcastDataflowBuilder<T> builder
            , ITargetBlock<T> targetBlock
            , Predicate<T> predicate
            , Action<T> declinedAction
            , ExecutionDataflowBlockOptions declinedBlockOptions = null)
        {
            return builder.LinkTo(targetBlock, DataflowDefaultOptions.DefaultLinkOptions, predicate, declinedAction, declinedBlockOptions);
        }

        /// <summary>
        /// Links to a target block.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="builder"></param>
        /// <param name="targetBlock"></param>
        /// <param name="linkOptions"></param>
        /// <param name="predicate"></param>
        /// <param name="declinedAction"></param>
        /// <param name="declinedBlockOptions"></param>
        /// <param name="declinedLinkOptions"></param>
        /// <returns></returns>
        public static IBroadcastDataflowBuilder<T> LinkTo<T>(
            this IBroadcastDataflowBuilder<T> builder
            , ITargetBlock<T> targetBlock
            , DataflowLinkOptions linkOptions
            , Predicate<T> predicate
            , Func<T, Task> declinedAction
            , ExecutionDataflowBlockOptions declinedBlockOptions = null
            , DataflowLinkOptions declinedLinkOptions = null)
        {
            if (builder == null) return null;
            if (targetBlock == null) throw new ArgumentNullException("targetBlock");
            if (predicate == null) throw new ArgumentNullException("predicate");

            var nextBuilder = builder.LinkTo(targetBlock, linkOptions, predicate);

            if (declinedAction != null)
            {
                // LinkTo declined target
                var declinedActionBlock = new ActionBlock<T>(declinedAction, declinedBlockOptions ?? DataflowDefaultOptions.DefaultBlockOptions);
                LinkHelper.Link((builder as BroadcastDataflowBuilder<T>).BroadcastBock, declinedActionBlock, declinedLinkOptions ?? DataflowDefaultOptions.DefaultLinkOptions);
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
        /// <param name="declinedAction"></param>
        /// <param name="declinedBlockOptions"></param>
        /// <returns></returns>
        public static IBroadcastDataflowBuilder<T> LinkTo<T>(
            this IBroadcastDataflowBuilder<T> builder
            , ITargetBlock<T> targetBlock
            , Predicate<T> predicate
            , Func<T, Task> declinedAction
            , ExecutionDataflowBlockOptions declinedBlockOptions = null)
        {
            return builder.LinkTo(targetBlock, DataflowDefaultOptions.DefaultLinkOptions, predicate, declinedAction, declinedBlockOptions);
        }

        #endregion

        #region IBroadcastDataflowBuilder link to action

        /// <summary>
        /// Links to a target action.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="builder"></param>
        /// <param name="action"></param>
        /// <param name="blockOptions"></param>
        /// <param name="linkOptions"></param>
        /// <param name="predicate"></param>
        /// <param name="declinedTargetBlock"></param>
        /// <param name="declinedLinkOptions"></param>
        /// <returns></returns>
        public static IBroadcastDataflowBuilder<T> LinkTo<T>(
            this IBroadcastDataflowBuilder<T> builder
            , Action<T> action
            , ExecutionDataflowBlockOptions blockOptions = null
            , DataflowLinkOptions linkOptions = null
            , Predicate<T> predicate = null
            , ITargetBlock<T> declinedTargetBlock = null
            , DataflowLinkOptions declinedLinkOptions = null)
        {
            if (builder == null) return null;

            var actionBlock = new ActionBlock<T>(action, blockOptions ?? DataflowDefaultOptions.DefaultBlockOptions);
            var nextBuilder = builder.LinkTo(actionBlock, linkOptions, predicate);

            if (declinedTargetBlock != null)
            {
                // LinkTo declined target
                LinkHelper.Link((builder as BroadcastDataflowBuilder<T>).BroadcastBock, declinedTargetBlock, declinedLinkOptions ?? DataflowDefaultOptions.DefaultLinkOptions);
            }

            return nextBuilder;
        }

        /// <summary>
        /// Links to a target action.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="builder"></param>
        /// <param name="action"></param>
        /// <param name="blockOptions"></param>
        /// <param name="predicate"></param>
        /// <param name="declinedTargetBlock"></param>
        /// <returns></returns>
        public static IBroadcastDataflowBuilder<T> LinkTo<T>(
            this IBroadcastDataflowBuilder<T> builder
            , Action<T> action
            , ExecutionDataflowBlockOptions blockOptions
            , Predicate<T> predicate
            , ITargetBlock<T> declinedTargetBlock = null)
        {
            return builder.LinkTo(action, blockOptions, DataflowDefaultOptions.DefaultLinkOptions, predicate, declinedTargetBlock);
        }

        /// <summary>
        /// Links to a target action.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="builder"></param>
        /// <param name="action"></param>
        /// <param name="blockOptions"></param>
        /// <param name="linkOptions"></param>
        /// <param name="predicate"></param>
        /// <param name="declinedAction"></param>
        /// <param name="declinedBlockOptions"></param>
        /// <param name="declinedLinkOptions"></param>
        /// <returns></returns>
        public static IBroadcastDataflowBuilder<T> LinkTo<T>(
            this IBroadcastDataflowBuilder<T> builder
            , Action<T> action
            , ExecutionDataflowBlockOptions blockOptions
            , DataflowLinkOptions linkOptions
            , Predicate<T> predicate
            , Action<T> declinedAction
            , ExecutionDataflowBlockOptions declinedBlockOptions = null
            , DataflowLinkOptions declinedLinkOptions = null)
        {
            if (builder == null) return null;
            if (action == null) throw new ArgumentNullException("action");
            if (predicate == null) throw new ArgumentNullException("predicate");

            var actionBlock = new ActionBlock<T>(action, blockOptions ?? DataflowDefaultOptions.DefaultBlockOptions);
            var nextBuilder = builder.LinkTo(actionBlock, linkOptions, predicate);

            if (declinedAction != null)
            {
                // LinkTo declined target
                var declinedActionBlock = new ActionBlock<T>(declinedAction, declinedBlockOptions ?? DataflowDefaultOptions.DefaultBlockOptions);
                LinkHelper.Link((builder as BroadcastDataflowBuilder<T>).BroadcastBock, declinedActionBlock, declinedLinkOptions ?? DataflowDefaultOptions.DefaultLinkOptions);
            }

            return nextBuilder;

        }

        /// <summary>
        /// Links to a target action.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="builder"></param>
        /// <param name="action"></param>
        /// <param name="blockOptions"></param>
        /// <param name="predicate"></param>
        /// <param name="declinedAction"></param>
        /// <param name="declinedBlockOptions"></param>
        /// <returns></returns>
        public static IBroadcastDataflowBuilder<T> LinkTo<T>(
            this IBroadcastDataflowBuilder<T> builder
            , Action<T> action
            , ExecutionDataflowBlockOptions blockOptions
            , Predicate<T> predicate
            , Action<T> declinedAction
            , ExecutionDataflowBlockOptions declinedBlockOptions = null)
        {
            return builder.LinkTo(action, DataflowDefaultOptions.DefaultBlockOptions, DataflowDefaultOptions.DefaultLinkOptions, predicate, declinedAction, declinedBlockOptions);
        }

        /// <summary>
        /// Links to a target action.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="builder"></param>
        /// <param name="action"></param>
        /// <param name="blockOptions"></param>
        /// <param name="linkOptions"></param>
        /// <param name="predicate"></param>
        /// <param name="declinedAction"></param>
        /// <param name="declinedBlockOptions"></param>
        /// <param name="declinedLinkOptions"></param>
        /// <returns></returns>
        public static IBroadcastDataflowBuilder<T> LinkTo<T>(
            this IBroadcastDataflowBuilder<T> builder
            , Action<T> action
            , ExecutionDataflowBlockOptions blockOptions
            , DataflowLinkOptions linkOptions
            , Predicate<T> predicate
            , Func<T, Task> declinedAction
            , ExecutionDataflowBlockOptions declinedBlockOptions = null
            , DataflowLinkOptions declinedLinkOptions = null)
        {
            if (builder == null) return null;
            if (action == null) throw new ArgumentNullException("action");
            if (predicate == null) throw new ArgumentNullException("predicate");

            var actionBlock = new ActionBlock<T>(action, blockOptions ?? DataflowDefaultOptions.DefaultBlockOptions);
            var nextBuilder = builder.LinkTo(actionBlock, linkOptions, predicate);

            if (declinedAction != null)
            {
                // LinkTo declined target
                var declinedActionBlock = new ActionBlock<T>(declinedAction, declinedBlockOptions ?? DataflowDefaultOptions.DefaultBlockOptions);
                LinkHelper.Link((builder as BroadcastDataflowBuilder<T>).BroadcastBock, declinedActionBlock, declinedLinkOptions ?? DataflowDefaultOptions.DefaultLinkOptions);
            }

            return nextBuilder;

        }

        /// <summary>
        /// Links to a target action.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="builder"></param>
        /// <param name="action"></param>
        /// <param name="blockOptions"></param>
        /// <param name="predicate"></param>
        /// <param name="declinedAction"></param>
        /// <param name="declinedBlockOptions"></param>
        /// <returns></returns>
        public static IBroadcastDataflowBuilder<T> LinkTo<T>(
            this IBroadcastDataflowBuilder<T> builder
            , Action<T> action
            , ExecutionDataflowBlockOptions blockOptions
            , Predicate<T> predicate
            , Func<T, Task> declinedAction
            , ExecutionDataflowBlockOptions declinedBlockOptions = null)
        {
            return builder.LinkTo(action, DataflowDefaultOptions.DefaultBlockOptions, DataflowDefaultOptions.DefaultLinkOptions, predicate, declinedAction, declinedBlockOptions);
        }

        /// <summary>
        /// Links to a target action.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="builder"></param>
        /// <param name="action"></param>
        /// <param name="blockOptions"></param>
        /// <param name="linkOptions"></param>
        /// <param name="predicate"></param>
        /// <param name="declinedTargetBlock"></param>
        /// <param name="declinedLinkOptions"></param>
        /// <returns></returns>
        public static IBroadcastDataflowBuilder<T> LinkTo<T>(
            this IBroadcastDataflowBuilder<T> builder
            , Func<T, Task> action
            , ExecutionDataflowBlockOptions blockOptions = null
            , DataflowLinkOptions linkOptions = null
            , Predicate<T> predicate = null
            , ITargetBlock<T> declinedTargetBlock = null
            , DataflowLinkOptions declinedLinkOptions = null)
        {
            if (builder == null) return null;
            if (action == null) throw new ArgumentNullException("action");

            var actionBlock = new ActionBlock<T>(action, blockOptions ?? DataflowDefaultOptions.DefaultBlockOptions);
            var nextBuilder = builder.LinkTo(actionBlock, linkOptions, predicate);

            if (declinedTargetBlock != null)
            {
                // LinkTo declined target
                LinkHelper.Link((builder as BroadcastDataflowBuilder<T>).BroadcastBock, declinedTargetBlock, declinedLinkOptions ?? DataflowDefaultOptions.DefaultLinkOptions);
            }

            return nextBuilder;
        }

        /// <summary>
        /// Links to a target action.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="builder"></param>
        /// <param name="action"></param>
        /// <param name="blockOptions"></param>
        /// <param name="predicate"></param>
        /// <param name="declinedTargetBlock"></param>
        /// <returns></returns>
        public static IBroadcastDataflowBuilder<T> LinkTo<T>(
            this IBroadcastDataflowBuilder<T> builder
            , Func<T, Task> action
            , ExecutionDataflowBlockOptions blockOptions
            , Predicate<T> predicate
            , ITargetBlock<T> declinedTargetBlock = null)
        {
            return builder.LinkTo(action, blockOptions, DataflowDefaultOptions.DefaultLinkOptions, predicate, declinedTargetBlock);
        }

        /// <summary>
        /// Links to a target action.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="builder"></param>
        /// <param name="action"></param>
        /// <param name="blockOptions"></param>
        /// <param name="linkOptions"></param>
        /// <param name="predicate"></param>
        /// <param name="declinedAction"></param>
        /// <param name="declinedBlockOptions"></param>
        /// <param name="declinedLinkOptions"></param>
        /// <returns></returns>
        public static IBroadcastDataflowBuilder<T> LinkTo<T>(
            this IBroadcastDataflowBuilder<T> builder
            , Func<T, Task> action
            , ExecutionDataflowBlockOptions blockOptions
            , DataflowLinkOptions linkOptions
            , Predicate<T> predicate
            , Action<T> declinedAction
            , ExecutionDataflowBlockOptions declinedBlockOptions = null
            , DataflowLinkOptions declinedLinkOptions = null)
        {
            if (builder == null) return null;
            if (action == null) throw new ArgumentNullException("action");
            if (predicate == null) throw new ArgumentNullException("predicate");

            var actionBlock = new ActionBlock<T>(action, blockOptions ?? DataflowDefaultOptions.DefaultBlockOptions);
            var nextBuilder = builder.LinkTo(actionBlock, linkOptions, predicate);

            if (declinedAction != null)
            {
                // LinkTo declined target
                var declinedActionBlock = new ActionBlock<T>(declinedAction, declinedBlockOptions ?? DataflowDefaultOptions.DefaultBlockOptions);
                LinkHelper.Link((builder as BroadcastDataflowBuilder<T>).BroadcastBock, declinedActionBlock, declinedLinkOptions ?? DataflowDefaultOptions.DefaultLinkOptions);
            }

            return nextBuilder;

        }

        /// <summary>
        /// Links to a target action.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="builder"></param>
        /// <param name="action"></param>
        /// <param name="blockOptions"></param>
        /// <param name="predicate"></param>
        /// <param name="declinedAction"></param>
        /// <param name="declinedBlockOptions"></param>
        /// <returns></returns>
        public static IBroadcastDataflowBuilder<T> LinkTo<T>(
            this IBroadcastDataflowBuilder<T> builder
            , Func<T, Task> action
            , ExecutionDataflowBlockOptions blockOptions
            , Predicate<T> predicate
            , Action<T> declinedAction
            , ExecutionDataflowBlockOptions declinedBlockOptions = null)
        {
            return builder.LinkTo(action, DataflowDefaultOptions.DefaultBlockOptions, DataflowDefaultOptions.DefaultLinkOptions, predicate, declinedAction, declinedBlockOptions);
        }

        /// <summary>
        /// Links to a target action.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="builder"></param>
        /// <param name="action"></param>
        /// <param name="blockOptions"></param>
        /// <param name="linkOptions"></param>
        /// <param name="predicate"></param>
        /// <param name="declinedAction"></param>
        /// <param name="declinedBlockOptions"></param>
        /// <param name="declinedLinkOptions"></param>
        /// <returns></returns>
        public static IBroadcastDataflowBuilder<T> LinkTo<T>(
            this IBroadcastDataflowBuilder<T> builder
            , Func<T, Task> action
            , ExecutionDataflowBlockOptions blockOptions
            , DataflowLinkOptions linkOptions
            , Predicate<T> predicate
            , Func<T, Task> declinedAction
            , ExecutionDataflowBlockOptions declinedBlockOptions = null
            , DataflowLinkOptions declinedLinkOptions = null)
        {
            if (builder == null) return null;
            if (action == null) throw new ArgumentNullException("action");
            if (predicate == null) throw new ArgumentNullException("predicate");

            var actionBlock = new ActionBlock<T>(action, blockOptions ?? DataflowDefaultOptions.DefaultBlockOptions);
            var nextBuilder = builder.LinkTo(actionBlock, linkOptions, predicate);

            if (declinedAction != null)
            {
                // LinkTo declined target
                var declinedActionBlock = new ActionBlock<T>(declinedAction, declinedBlockOptions ?? DataflowDefaultOptions.DefaultBlockOptions);
                LinkHelper.Link((builder as BroadcastDataflowBuilder<T>).BroadcastBock, declinedActionBlock, declinedLinkOptions ?? DataflowDefaultOptions.DefaultLinkOptions);
            }

            return nextBuilder;

        }

        /// <summary>
        /// Links to a target action.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="builder"></param>
        /// <param name="action"></param>
        /// <param name="blockOptions"></param>
        /// <param name="predicate"></param>
        /// <param name="declinedAction"></param>
        /// <param name="declinedBlockOptions"></param>
        /// <returns></returns>
        public static IBroadcastDataflowBuilder<T> LinkTo<T>(
            this IBroadcastDataflowBuilder<T> builder
            , Func<T, Task> action
            , ExecutionDataflowBlockOptions blockOptions
            , Predicate<T> predicate
            , Func<T, Task> declinedAction
            , ExecutionDataflowBlockOptions declinedBlockOptions = null)
        {
            return builder.LinkTo(action, DataflowDefaultOptions.DefaultBlockOptions, DataflowDefaultOptions.DefaultLinkOptions, predicate, declinedAction, declinedBlockOptions);
        }

        #endregion
    }
}
