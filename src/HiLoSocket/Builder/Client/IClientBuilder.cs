using HiLoSocket.SocketApp;

namespace HiLoSocket.Builder.Client
{
    /// <summary>
    /// IClientBuilder.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IClientBuilder<T>
        where T : class
    {
        /// <summary>
        /// Builds this instance.
        /// </summary>
        /// <returns>Client.</returns>
        Client<T> Build( );
    }
}