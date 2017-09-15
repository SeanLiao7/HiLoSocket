using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using HiLoSocket.AckCommand;
using HiLoSocket.CommandFormatter;
using HiLoSocket.SocketCommand;

namespace HiLoSocket
{
    public class Client : SocketBase
    {
        private readonly ManualResetEvent _connectDone = new ManualResetEvent( false );
        private readonly ManualResetEvent _receiveDone = new ManualResetEvent( false );
        private readonly ManualResetEvent _sendDone = new ManualResetEvent( false );

        public IPEndPoint IpEndPoint { get; set; }
        public IPEndPoint RemoteIpEndPoint { get; set; }

        public event Action<AckCommandBase> OnAckCommandReceived;

        public Client( IPEndPoint ipEndPoint, IPEndPoint remoteIpEndPoint, FormatterType formatterType )
            : base( formatterType )
        {
            IpEndPoint = ipEndPoint;
            RemoteIpEndPoint = remoteIpEndPoint;
        }

        public void Send( Socket client, SocketCommandBase socketCommand )
        {
            var commandBytetoSend = CommandFormatter.Serialize( socketCommand );
            var commandBytetoSendWithSize = CreateBytsToSendWithSize( commandBytetoSend );

            // Begin sending the data to the remote device.
            client.BeginSend( commandBytetoSendWithSize, 0, commandBytetoSendWithSize.Length, 0, SendCallback, client );
        }

        public void StartClient( SocketCommandBase socketCommand )
        {
            // Connect to a remote device.
            try
            {
                // Create a TCP/IP socket.
                var client = new Socket( AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp );

                // Connect to the remote endpoint.
                client.BeginConnect( RemoteIpEndPoint, ConnectCallback, client );
                _connectDone.WaitOne( );

                // Send test data to the remote device.
                Send( client, socketCommand );
                _sendDone.WaitOne( );

                // Receive the _response from the remote device.
                Receive( client );
                _receiveDone.WaitOne( );
            }
            catch ( Exception e )
            {
                Console.WriteLine( e.ToString( ) );
            }
        }

        private void ConnectCallback( IAsyncResult asyncResult )
        {
            try
            {
                // Retrieve the socket from the state object.
                // Complete the connection.
                if ( asyncResult.AsyncState is Socket client )
                {
                    client.EndConnect( asyncResult );

                    Console.WriteLine( $"Socket connected to {client.RemoteEndPoint}" );
                }

                // Signal that the connection has been made.
                _connectDone.Set( );
            }
            catch ( Exception e )
            {
                Console.WriteLine( e.ToString( ) );
            }
        }

        private void ReadAllData( IAsyncResult asyncResult )
        {
            try
            {
                // Retrieve the state object and the client socket
                // from the asynchronous state object.
                if ( asyncResult.AsyncState is StateObject state )
                {
                    var client = state.WorkSocket;

                    // Read data from the remote device.
                    var bytesRead = client.EndReceive( asyncResult );

                    if ( bytesRead == state.Buffer.Length )
                    {
                        var command = CommandFormatter.Deserialize<AckCommandBase>( state.Buffer );
                        OnAckCommandReceived?.Invoke( command );
                    }

                    _receiveDone.Set( );
                }
            }
            catch ( Exception e )
            {
                Console.WriteLine( e.ToString( ) );
            }
        }

        private void ReadTotalLength( IAsyncResult asyncResult )
        {
            try
            {
                // Retrieve the state object and the client socket
                // from the asynchronous state object.
                if ( asyncResult.AsyncState is StateObject state )
                {
                    var client = state.WorkSocket;

                    // Read data from the remote device.
                    var bytesRead = client.EndReceive( asyncResult );

                    if ( bytesRead == StateObject.DataInfoSize )
                    {
                        var totalBufferSize = BitConverter.ToInt32( state.Buffer, 0 );
                        state.Buffer = new byte[ totalBufferSize ];

                        // Get the rest of the data.
                        client.BeginReceive( state.Buffer, 0, totalBufferSize, 0, ReadAllData, state );
                    }
                }
            }
            catch ( Exception e )
            {
                Console.WriteLine( e.ToString( ) );
            }
        }

        private void Receive( Socket client )
        {
            try
            {
                // Create the state object.
                var state = new StateObject
                {
                    WorkSocket = client
                };

                // Begin receiving the data from the remote device.
                client.BeginReceive( state.Buffer, 0, StateObject.DataInfoSize, 0, ReadTotalLength, state );
            }
            catch ( Exception e )
            {
                Console.WriteLine( e.ToString( ) );
            }
        }

        private void SendCallback( IAsyncResult asyncResult )
        {
            try
            {
                // Retrieve the socket from the state object.
                // Complete sending the data to the remote device.
                if ( asyncResult.AsyncState is Socket client )
                {
                    var bytesSent = client.EndSend( asyncResult );
                    Console.WriteLine( $"Sent {bytesSent} bytes to server." );
                }

                // Signal that all bytes have been sent.
                _sendDone.Set( );
            }
            catch ( Exception e )
            {
                Console.WriteLine( e.ToString( ) );
            }
        }
    }
}