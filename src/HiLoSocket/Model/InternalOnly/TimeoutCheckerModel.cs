using System;
using HiLoSocket.Logger;

namespace HiLoSocket.Model.InternalOnly
{
    /// <summary>
    /// TimeoutCheckerModel
    /// </summary>
    internal class TimeoutCheckerModel<T> where T : class
    {
        /// <summary>
        /// Gets or sets the logger.
        /// </summary>
        /// <value>
        /// The logger.
        /// </value>
        public ILogger Logger { get; set; }

        /// <summary>
        /// Gets or sets the on timeout action.
        /// </summary>
        /// <value>
        /// The on timeout action.
        /// </value>
        public Action<T> OnTimeoutAction { get; set; }

        /// <summary>
        /// Gets the target.
        /// </summary>
        /// <value>
        /// The target.
        /// </value>
        public T Target { get; set; }

        /// <summary>
        /// Gets or sets the timeout time.
        /// </summary>
        /// <value>
        /// The timeout time.
        /// </value>
        public int TimeoutTime { get; set; }
    }
}