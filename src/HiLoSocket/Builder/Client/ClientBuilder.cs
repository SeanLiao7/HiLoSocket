using System.Net;
using HiLoSocket.CommandFormatter;
using HiLoSocket.Compressor;
using HiLoSocket.Logger;
using HiLoSocket.Model.InternalOnly;
using HiLoSocket.SocketApp;

namespace HiLoSocket.Builder.Client
{
    /// <summary>
    /// ClientBuilder.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="T:HiLoSocket.Builder.Client.ISetLocalIpEndPoint`1" />
    /// <seealso cref="T:HiLoSocket.Builder.Client.ISetRemoteIpEndPoint`1" />
    /// <seealso cref="T:HiLoSocket.Builder.Client.ISetFormatterType`1" />
    /// <seealso cref="T:HiLoSocket.Builder.Client.ISetCompressType`1" />
    /// <seealso cref="T:HiLoSocket.Builder.Client.ISetTimeoutTime`1" />
    /// <seealso cref="T:HiLoSocket.Builder.Client.ISetLogger`1" />
    /// <seealso cref="T:HiLoSocket.Builder.Client.IClientBuilder`1" />
    public sealed class ClientBuilder<T> :
        ISetLocalIpEndPoint<T>,
        ISetRemoteIpEndPoint<T>,
        ISetFormatterType<T>,
        ISetCompressType<T>,
        ISetTimeoutTime<T>,
        ISetLogger<T>,
        IClientBuilder<T>
        where T : class
    {
        private CompressType? _compressType;
        private FormatterType? _formatterType;
        private IPEndPoint _localIpEndPoint;
        private ILogger _logger;
        private IPEndPoint _remoteIpEndPoint;
        private int _timeoutTime;

        private ClientBuilder( )
        {
        }

        /// <summary>
        /// Creates the new.
        /// </summary>
        /// <returns>ISetLocalIpEndPoint.</returns>
        public static ISetLocalIpEndPoint<T> CreateNew( )
        {
            return new ClientBuilder<T>( );
        }

        /// <inheritdoc />
        /// <summary>
        /// Builds this instance.
        /// </summary>
        /// <returns>Client.</returns>
        public Client<T> Build( )
        {
            return new Client<T>(
                new ClientConfigModel
                {
                    LocalIpEndPoint = _localIpEndPoint,
                    RemoteIpEndPoint = _remoteIpEndPoint,
                    FormatterType = _formatterType,
                    CompressType = _compressType,
                    TimeOutTime = _timeoutTime,
                }, _logger );
        }

        /// <inheritdoc />
        /// <summary>
        /// Sets the type of the compress.
        /// </summary>
        /// <param name="compressType">Type of the compress.</param>
        /// <returns>
        /// ISetTimeoutTime
        /// </returns>
        public ISetTimeoutTime<T> SetCompressType( CompressType? compressType )
        {
            _compressType = compressType;
            return this;
        }

        /// <inheritdoc />
        /// <summary>
        /// Sets the type of the formatter.
        /// </summary>
        /// <param name="formatterType">Type of the formatter.</param>
        /// <returns>
        /// ISetCompressType.
        /// </returns>
        public ISetCompressType<T> SetFormatterType( FormatterType? formatterType )
        {
            _formatterType = formatterType;
            return this;
        }

        /// <inheritdoc />
        /// <summary>
        /// Sets the local ip end point.
        /// </summary>
        /// <param name="localIpEndPoint">The localp end point.</param>
        /// <returns>ISetRemoteIpEndPoint.</returns>
        public ISetRemoteIpEndPoint<T> SetLocalIpEndPoint( IPEndPoint localIpEndPoint )
        {
            _localIpEndPoint = localIpEndPoint;
            return this;
        }

        /// <inheritdoc />
        /// <summary>
        /// Sets the logger.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <returns>
        /// IClientBuilder.
        /// </returns>
        public IClientBuilder<T> SetLogger( ILogger logger )
        {
            _logger = logger;
            return this;
        }

        /// <inheritdoc />
        /// <summary>
        /// Sets the remote ip end point.
        /// </summary>
        /// <param name="remoteIpEndPoint">The remote ip end point.</param>
        /// <returns>
        /// ISetFormatterType.
        /// </returns>
        public ISetFormatterType<T> SetRemoteIpEndPoint( IPEndPoint remoteIpEndPoint )
        {
            _remoteIpEndPoint = remoteIpEndPoint;
            return this;
        }

        /// <inheritdoc />
        /// <summary>
        /// Sets the timeout time.
        /// </summary>
        /// <param name="timeoutTime">The timeout time.</param>
        /// <returns>
        /// ISetLogger.
        /// </returns>
        public ISetLogger<T> SetTimeoutTime( int timeoutTime )
        {
            _timeoutTime = timeoutTime;
            return this;
        }
    }
}