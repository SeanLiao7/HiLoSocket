using System;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using HiLoSocket.Model;

namespace HiLoSocket
{
    public class Server<TModel> : SocketBase<TModel> where TModel : class
    {
        public const int MaxPendingConnectionLength = 4;
        private readonly ManualResetEvent _allDone = new ManualResetEvent( false );

        public IPEndPoint LocalIpEndPoint { get; }

        public Server( ServerModel serverModel )
            : base( serverModel?.FormatterType )
        {
            if ( serverModel == null )
                throw new ArgumentNullException( nameof( serverModel ), $"你沒初始化 {nameof( serverModel )} 喔。" );

            if ( serverModel.ValidateObject( out var errorMessages ) == false )
                throw new ValidationException( string.Join( "\n", errorMessages ) );

            LocalIpEndPoint = serverModel.LocalIpEndPoint;
        }

        public void StartListening( )
        {
            var listener = new Socket( AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp );

            try
            {
                listener.Bind( LocalIpEndPoint );
                listener.Listen( MaxPendingConnectionLength );

                while ( true )
                {
                    _allDone.Reset( );
                    Console.WriteLine( "Waiting for a connection..." );
                    listener.BeginAccept( AcceptCallback, listener );
                    _allDone.WaitOne( );
                }
            }
            catch ( Exception e )
            {
                Console.WriteLine( e.ToString( ) );

                // TODO : log
                throw;
            }
        }

        protected override void SendCallback( IAsyncResult asyncResult )
        {
            base.SendCallback( asyncResult );

            try
            {
                if ( asyncResult.AsyncState is Socket handler )
                {
                    handler.Shutdown( SocketShutdown.Both );
                    handler.Close( );
                }
            }
            catch ( Exception e )
            {
                Console.WriteLine( e );

                // TODO : log
                throw;
            }
        }

        private void AcceptCallback( IAsyncResult asyncResult )
        {
            _allDone.Set( );

            if ( asyncResult.AsyncState is Socket listener )
            {
                var handler = listener.EndAccept( asyncResult );
                var state = new StateObject
                {
                    WorkSocket = handler
                };

                handler.BeginReceive( state.Buffer, 0, StateObject.DataInfoSize, 0, ReadTotalLength, state );
            }
        }
    }
}