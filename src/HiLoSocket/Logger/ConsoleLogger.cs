using System.Diagnostics;
using HiLoSocket.Model;

namespace HiLoSocket.Logger
{
    /// <inheritdoc />
    /// <summary>
    /// Console Logger.
    /// </summary>
    /// <seealso cref="T:HiLoSocket.Logger.ILogger" />
    public class ConsoleLogger : ILogger
    {
        /// <inheritdoc />
        /// <summary>
        /// Logs the specified log model.
        /// </summary>
        /// <param name="logModel">The log model.</param>
        public void Log( LogModel logModel )
        {
            Trace.WriteLine( $"Time : {logModel.Time}, Message : {logModel.Message}" );
        }
    }
}