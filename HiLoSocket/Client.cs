using System;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using HiLoSocket.Model;

namespace HiLoSocket
{
    public class Client<TModel> : SocketBase<TModel> where TModel : class
    {
        private readonly ManualResetEventSlim _connectDone = new ManualResetEventSlim( );
        private readonly ManualResetEventSlim _receiveDone = new ManualResetEventSlim( );
        private readonly ManualResetEventSlim _sendDone = new ManualResetEventSlim( );

        public IPEndPoint LocalIpEndPoint { get; }
        public IPEndPoint RemoteIpEndPoint { get; }

        public Client( ClientModel clientModel )
            : base( clientModel?.FormatterType )
        {
            if ( clientModel == null )
            {
                throw new ArgumentNullException( nameof( clientModel ),
                    $@"時間 : {DateTime.Now.GetDateTimeString( )};
類別 : {nameof( Client<TModel> )};
方法 : Constructor;
內容 : 你沒初始化 {nameof( clientModel )} 喔。" );
            }

            if ( clientModel.ValidateObject( out var errorMessages ) == false )
            {
                throw new ValidationException(
                    $@"時間 : {DateTime.Now.GetDateTimeString( )};
類別 : {nameof( Client<TModel> )};
方法 : Constructor;
內容 : {string.Join( "\n", errorMessages )}" );
            }

            LocalIpEndPoint = clientModel.LocalIpEndPoint;
            RemoteIpEndPoint = clientModel.RemoteIpEndPoint;
        }

        public void StartClient( TModel model )
        {
            try
            {
                var client = new Socket( AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp );

                client.BeginConnect( RemoteIpEndPoint, ConnectCallback, client );
                _connectDone.Wait( );

                Send( client, model );
                _sendDone.Wait( );

                Receive( client );
                _receiveDone.Wait( );
            }
            catch ( Exception e )
            {
                Console.WriteLine( e.ToString( ) );
                throw;
            }
        }

        protected override void ReadModel( IAsyncResult asyncResult )
        {
            base.ReadModel( asyncResult );
            _receiveDone.Set( );
        }

        protected override void SendCallback( IAsyncResult asyncResult )
        {
            base.SendCallback( asyncResult );
            _sendDone.Set( );
        }

        private void ConnectCallback( IAsyncResult asyncResult )
        {
            try
            {
                if ( asyncResult.AsyncState is Socket client )
                {
                    client.EndConnect( asyncResult );
                    Console.WriteLine( $"Socket connected to {client.RemoteEndPoint}" );
                }

                _connectDone.Set( );
            }
            catch ( Exception e )
            {
                Console.WriteLine( e.ToString( ) );
                throw;
            }
        }

        private void Receive( Socket client )
        {
            try
            {
                var state = new StateObject
                {
                    WorkSocket = client
                };

                client.BeginReceive( state.Buffer, 0, StateObject.DataInfoSize, 0, ReadTotalLength, state );
            }
            catch ( Exception e )
            {
                Console.WriteLine( e.ToString( ) );
                throw;
            }
        }
    }
}