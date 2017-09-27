using System.Net;
using HiLoSocket.CommandFormatter;
using HiLoSocket.Compressor;
using HiLoSocket.Logger;
using HiLoSocket.Model.InternalOnly;
using HiLoSocket.SocketApp;

namespace HiLoSocket.Builder.Server
{
    /// <summary>
    /// ServerBuilder.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="T:HiLoSocket.Builder.Server.ISetLocalIpEndPoint`1" />
    /// <seealso cref="T:HiLoSocket.Builder.Server.ISetFormatterType`1" />
    /// <seealso cref="T:HiLoSocket.Builder.Server.ISetCompressType`1" />
    /// <seealso cref="T:HiLoSocket.Builder.Server.ISetLogger`1" />
    /// <seealso cref="T:HiLoSocket.Builder.Server.IServerBuilder`1" />
    public sealed class ServerBuilder<T> :
        ISetLocalIpEndPoint<T>,
        ISetFormatterType<T>,
        ISetCompressType<T>,
        ISetLogger<T>,
        IServerBuilder<T>
        where T : class

    {
        private CompressType? _compressType;
        private FormatterType? _formatterType;
        private IPEndPoint _localIpEndPoint;
        private ILogger _logger;

        private ServerBuilder( )
        {
        }

        /// <summary>
        /// Creates the new.
        /// </summary>
        /// <returns>ServerBuilder.</returns>
        public static ISetLocalIpEndPoint<T> CreateNew( )
        {
            return new ServerBuilder<T>( );
        }

        /// <inheritdoc />
        /// <summary>
        /// Builds this instance.
        /// </summary>
        /// <returns>
        /// Server.
        /// </returns>
        public Server<T> Build( )
        {
            return new Server<T>(
                new ServerConfigModel
                {
                    LocalIpEndPoint = _localIpEndPoint,
                    FormatterType = _formatterType,
                    CompressType = _compressType,
                }, _logger );
        }

        /// <inheritdoc />
        /// <summary>
        /// Sets the type of the compress.
        /// </summary>
        /// <param name="compressType">Type of the compress.</param>
        /// <returns>
        /// ISetLogger.
        /// </returns>
        public ISetLogger<T> SetCompressType( CompressType? compressType )
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
        /// <param name="localIpEndPoint">The local ip end point.</param>
        /// <returns>
        /// ISetFormatterType.
        /// </returns>
        public ISetFormatterType<T> SetLocalIpEndPoint( IPEndPoint localIpEndPoint )
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
        /// IServerBuilder.
        /// </returns>
        public IServerBuilder<T> SetLogger( ILogger logger )
        {
            _logger = logger;
            return this;
        }
    }
}