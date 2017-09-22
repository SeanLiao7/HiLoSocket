using HiLoSocket.Model;

namespace HiLoSocket.Logger
{
    /// <summary>
    /// ILogger.
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// Logs the specified log model.
        /// </summary>
        /// <param name="logModel">The log model.</param>
        void Log( LogModel logModel );
    }
}