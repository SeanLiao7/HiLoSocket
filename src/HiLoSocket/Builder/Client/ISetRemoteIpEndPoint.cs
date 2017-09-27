using System.Net;

namespace HiLoSocket.Builder.Client
{
    /// <summary>
    /// ISetRemoteIpEndPoint.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ISetRemoteIpEndPoint<T>
        where T : class
    {
        /// <summary>
        /// Sets the remote ip end point.
        /// </summary>
        /// <param name="remoteIpEndPoint">The remote ip end point.</param>
        /// <returns>ISetFormatterType.</returns>
        ISetFormatterType<T> SetRemoteIpEndPoint( IPEndPoint remoteIpEndPoint );
    }
}