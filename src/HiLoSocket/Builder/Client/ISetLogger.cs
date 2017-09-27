using HiLoSocket.Logger;

namespace HiLoSocket.Builder.Client
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
        /// <returns>IClientBuilder.</returns>
        IClientBuilder<T> SetLogger( ILogger logger );
    }
}