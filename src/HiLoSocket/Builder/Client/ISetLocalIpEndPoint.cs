using System.Net;

namespace HiLoSocket.Builder.Client
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
        /// <returns>ISetRemoteIpEndPoint.</returns>
        ISetRemoteIpEndPoint<T> SetLocalIpEndPoint( IPEndPoint localIpEndPoint );
    }
}