using HiLoSocket.Logger;

namespace HiLoSocket.Builder.Server
{
    /// <summary>
    /// ISetLogger.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ISetLogger<T>
        where T : class
    {
        /// <summary>
        /// Sets the logger.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <returns>IServerBuilder.</returns>
        IServerBuilder<T> SetLogger( ILogger logger );
    }
}