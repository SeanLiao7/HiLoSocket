using System;
using System.Globalization;
using System.Net;
using System.Threading;
using System.Windows.Forms;
using HiLoSocket;
using HiLoSocket.AckCommand;
using HiLoSocket.CommandFormatter;
using HiLoSocket.SocketCommand;

namespace ClientForm2
{
    public partial class ClientForm2 : Form
    {
        private readonly Client _client = new Client(
            new IPEndPoint( IPAddress.Parse( "127.0.0.1" ), 8081 ),
            new IPEndPoint( IPAddress.Parse( "127.0.0.1" ), 8000 ),
            FormatterType.BinaryFormmater );

        public ClientForm2( )
        {
            InitializeComponent( );
            _client.OnAckCommandReceived += Client_OnAckCommandReceived;
        }

        private void btnSend_Click( object sender, EventArgs e )
        {
            new Thread( ( ) =>
            {
                while ( true )
                {
                    _client.StartClient( new AutoProgram( ) );
                    Thread.Sleep( 500 );
                }
            } ).Start( );
        }

        private void Client_OnAckCommandReceived( AckCommandBase ackCommandBase )
        {
            if ( InvokeRequired )
                Invoke( new Action( ( ) =>
                {
                    rtbLog.AppendText( DateTime.Now.ToString( CultureInfo.InvariantCulture ) );
                    rtbLog.AppendText( ackCommandBase.ToString( ) );
                    rtbLog.AppendText( "\n" );
                } ) );
        }
    }
}