using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace FluentDataflow
{
    /// <summary>
    /// Extensions methods for fluently create data flow options
    /// </summary>
    public static class DataflowOptionsExtensions
    {
        #region ExecutionDataflowBlockOptions

        /// <summary>
        /// Sets MaxDegreeOfParallelism
        /// </summary>
        /// <param name="options"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public static ExecutionDataflowBlockOptions MaxDegreeOfParallelism(this ExecutionDataflowBlockOptions options, int val)
        {
            if (options == null) return null;

            options.MaxDegreeOfParallelism = val;

            return options;
        }

        /// <summary>
        /// Sets SingleProducerConstrained
        /// </summary>
        /// <param name="options"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public static ExecutionDataflowBlockOptions SingleProducerConstrained(this ExecutionDataflowBlockOptions options, bool val = true)
        {
            if (options == null) return null;

            options.SingleProducerConstrained = val;

            return options;
        }

        /// <summary>
        /// Sets TaskScheduler
        /// </summary>
        /// <param name="options"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public static ExecutionDataflowBlockOptions TaskScheduler(this ExecutionDataflowBlockOptions options, TaskScheduler val)
        {
            if (options == null) return null;

            options.TaskScheduler = val;

            return options;
        }

        /// <summary>
        /// Sets CancellationToken
        /// </summary>
        /// <param name="options"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public static ExecutionDataflowBlockOptions CancellationToken(this ExecutionDataflowBlockOptions options, CancellationToken val)
        {
            if (options == null) return null;

            options.CancellationToken = val;

            return options;
        }

        /// <summary>
        /// Sets MaxMessagesPerTask
        /// </summary>
        /// <param name="options"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public static ExecutionDataflowBlockOptions MaxMessagesPerTask(this ExecutionDataflowBlockOptions options, int val)
        {
            if (options == null) return null;

            options.MaxMessagesPerTask = val;

            return options;
        }

        /// <summary>
        /// Sets BoundedCapacity
        /// </summary>
        /// <param name="options"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public static ExecutionDataflowBlockOptions BoundedCapacity(this ExecutionDataflowBlockOptions options, int val)
        {
            if (options == null) return null;

            options.BoundedCapacity = val;

            return options;
        }

        /// <summary>
        /// Sets NameFormat
        /// </summary>
        /// <param name="options"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public static ExecutionDataflowBlockOptions NameFormat(this ExecutionDataflowBlockOptions options, string val)
        {
            if (options == null) return null;

            options.NameFormat = val;

            return options;
        }

        /// <summary>
        /// Sets EnsureOrdered
        /// </summary>
        /// <param name="options"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public static ExecutionDataflowBlockOptions EnsureOrdered(this ExecutionDataflowBlockOptions options, bool val = true)
        {
            if (options == null) return null;

            options.EnsureOrdered = val;

            return options;
        }

        #endregion

        #region GroupingDataflowBlockOptions

        /// <summary>
        /// Sets MaxNumberOfGroups
        /// </summary>
        /// <param name="options"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public static GroupingDataflowBlockOptions MaxNumberOfGroups(this GroupingDataflowBlockOptions options, int val)
        {
            if (options == null) return null;

            options.MaxNumberOfGroups = val;

            return options;
        }

        /// <summary>
        /// Sets Greedy
        /// </summary>
        /// <param name="options"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public static GroupingDataflowBlockOptions Greedy(this GroupingDataflowBlockOptions options, bool val = true)
        {
            if (options == null) return null;

            options.Greedy = val;

            return options;
        }

        /// <summary>
        /// Sets TaskScheduler
        /// </summary>
        /// <param name="options"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public static GroupingDataflowBlockOptions TaskScheduler(this GroupingDataflowBlockOptions options, TaskScheduler val)
        {
            if (options == null) return null;

            options.TaskScheduler = val;

            return options;
        }

        /// <summary>
        /// Sets CancellationToken
        /// </summary>
        /// <param name="options"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public static GroupingDataflowBlockOptions CancellationToken(this GroupingDataflowBlockOptions options, CancellationToken val)
        {
            if (options == null) return null;

            options.CancellationToken = val;

            return options;
        }

        /// <summary>
        /// Sets MaxMessagesPerTask
        /// </summary>
        /// <param name="options"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public static GroupingDataflowBlockOptions MaxMessagesPerTask(this GroupingDataflowBlockOptions options, int val)
        {
            if (options == null) return null;

            options.MaxMessagesPerTask = val;

            return options;
        }

        /// <summary>
        /// Sets BoundedCapacity
        /// </summary>
        /// <param name="options"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public static GroupingDataflowBlockOptions BoundedCapacity(this GroupingDataflowBlockOptions options, int val)
        {
            if (options == null) return null;

            options.BoundedCapacity = val;

            return options;
        }

        /// <summary>
        /// Sets NameFormat
        /// </summary>
        /// <param name="options"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public static GroupingDataflowBlockOptions NameFormat(this GroupingDataflowBlockOptions options, string val)
        {
            if (options == null) return null;

            options.NameFormat = val;

            return options;
        }

        /// <summary>
        /// Sets EnsureOrdered
        /// </summary>
        /// <param name="options"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public static GroupingDataflowBlockOptions EnsureOrdered(this GroupingDataflowBlockOptions options, bool val = true)
        {
            if (options == null) return null;

            options.EnsureOrdered = val;

            return options;
        }

        #endregion

        #region DataflowLinkOptions

        /// <summary>
        /// Sets PropagateCompletion
        /// </summary>
        /// <param name="options"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public static DataflowLinkOptions PropagateCompletion(this DataflowLinkOptions options, bool val = true)
        {
            if (options == null) return null;

            options.PropagateCompletion = val;

            return options;
        }

        /// <summary>
        /// Sets Append
        /// </summary>
        /// <param name="options"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public static DataflowLinkOptions Append(this DataflowLinkOptions options, bool val = true)
        {
            if (options == null) return null;

            options.Append = val;

            return options;
        }

        /// <summary>
        /// Sets MaxMessages
        /// </summary>
        /// <param name="options"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public static DataflowLinkOptions MaxMessages(this DataflowLinkOptions options, int val)
        {
            if (options == null) return null;

            options.MaxMessages = val;

            return options;
        }

        #endregion
    }
}
