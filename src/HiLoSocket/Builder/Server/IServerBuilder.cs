using HiLoSocket.SocketApp;

namespace HiLoSocket.Builder.Server
{
    /// <summary>
    /// IServerBuilder.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IServerBuilder<T>
        where T : class
    {
        /// <summary>
        /// Builds this instance.
        /// </summary>
        /// <returns>Server.</returns>
        Server<T> Build( );
    }
}