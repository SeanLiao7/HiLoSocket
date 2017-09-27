using System.Net;

namespace HiLoSocket.Builder.Server
{
    /// <summary>
    /// ISetLocalIpEndPoint.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ISetLocalIpEndPoint<T>
        where T : class
    {
        /// <summary>
        /// Sets the local ip end point.
        /// </summary>
        /// <param name="localIpEndPoint">The local ip end point.</param>
        /// <returns>ISetFormatterType.</returns>
        ISetFormatterType<T> SetLocalIpEndPoint( IPEndPoint localIpEndPoint );
    }
}