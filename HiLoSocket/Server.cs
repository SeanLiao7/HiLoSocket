using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using HiLoSocket.AckCommand;
using HiLoSocket.CommandFormatter;
using HiLoSocket.SocketCommand;

namespace HiLoSocket
{
    public class Server : SocketBase
    {
        // Thread signal.
        private readonly ManualResetEvent _allDone = new ManualResetEvent( false );

        public IPEndPoint LocalIpEndPoint { get; set; }

        public ISocketHandShake SocketHandShake { get; set; }

        public event Action<SocketCommandBase> OnSocketCommandRecevied;

        public Server( IPEndPoint localIpEndPoint, ISocketHandShake socketHandShake, FormatterType formatterType )
            : base( formatterType )
        {
            LocalIpEndPoint = localIpEndPoint;
            SocketHandShake = socketHandShake;
        }

        public void StartListening( )
        {
            // Create a TCP/IP socket.
            var listener = new Socket( AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp );

            // Bind the socket to the local endpoint and listen for incoming connections.
            try
            {
                listener.Bind( LocalIpEndPoint );
                listener.Listen( 100 );

                while ( true )
                {
                    // Set the event to nonsignaled state.
                    _allDone.Reset( );

                    // Start an asynchronous socket to listen for connections.
                    Console.WriteLine( "Waiting for a connection..." );
                    listener.BeginAccept( AcceptCallback, listener );

                    // Wait until a connection is made before continuing.
                    _allDone.WaitOne( );
                }
            }
            catch ( Exception e )
            {
                Console.WriteLine( e.ToString( ) );
            }
        }

        private void AcceptCallback( IAsyncResult asyncResult )
        {
            // Signal the main thread to continue.
            _allDone.Set( );

            // Get the socket that handles the client request.
            if ( asyncResult.AsyncState is Socket listener )
            {
                var handler = listener.EndAccept( asyncResult );

                // Create the state object.
                var state = new StateObject
                {
                    WorkSocket = handler
                };

                handler.BeginReceive( state.Buffer, 0, StateObject.DataInfoSize, 0, ReadTotalLength, state );
            }
        }

        private void ReadAllData( IAsyncResult asyncResult )
        {
            // Retrieve the state object and the handler socket
            // from the asynchronous state object.
            if ( asyncResult.AsyncState is StateObject state )
            {
                var handler = state.WorkSocket;

                // Read data from the client socket.
                var bytesRead = handler.EndReceive( asyncResult );

                if ( bytesRead == StateObject.DataInfoSize )
                {
                    var socketCommand = CommandFormatter.Deserialize<SocketCommandBase>( state.Buffer );
                    var ackCommand = SocketHandShake.CreateAckCommand( socketCommand );
                    Send( handler, ackCommand );
                    OnSocketCommandRecevied?.Invoke( socketCommand );
                }
            }
        }

        private void ReadTotalLength( IAsyncResult asyncResult )
        {
            // Retrieve the state object and the handler socket
            // from the asynchronous state object.
            if ( asyncResult.AsyncState is StateObject state )
            {
                var handler = state.WorkSocket;

                // Read data from the client socket.
                var totalBufferSize = BitConverter.ToInt32( state.Buffer, 0 );
                state.Buffer = new byte[ totalBufferSize ];

                // Not all data received. Get more.
                handler.BeginReceive( state.Buffer, 0, totalBufferSize, SocketFlags.None, ReadAllData, state );
            }
        }

        private void Send( Socket handler, AckCommandBase ackCommandBase )
        {
            var commandBytetoSend = CommandFormatter.Serialize( ackCommandBase );
            var commandBytetoSendWithLength = CreateBytsToSendWithSize( commandBytetoSend );
            // Begin sending the data to the remote device.
            handler.BeginSend( commandBytetoSendWithLength, 0, commandBytetoSendWithLength.Length, 0, SendCallback, handler );
        }

        private void SendCallback( IAsyncResult asyncResult )
        {
            try
            {
                // Retrieve the socket from the state object.
                // Complete sending the data to the remote device.
                if ( asyncResult.AsyncState is Socket handler )
                {
                    var bytesSent = handler.EndSend( asyncResult );
                    Console.WriteLine( $"Sent {bytesSent} bytes to client." );
                    handler.Shutdown( SocketShutdown.Both );
                    handler.Close( );
                }
            }
            catch ( Exception e )
            {
                Console.WriteLine( e.ToString( ) );
            }
        }
    }
}