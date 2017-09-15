using System;
using System.Net;
using System.Threading;
using System.Windows.Forms;
using HiLoSocket;
using HiLoSocket.CommandFormatter;
using HiLoSocket.SocketCommand;

namespace ServerForm
{
    public partial class ServerForm : Form
    {
        private Server _server = new Server(
            new IPEndPoint( IPAddress.Parse( "127.0.0.1" ), 8000 ),
            new SocketHandShake( ),
            FormatterType.BinaryFormmater );

        public ServerForm( )
        {
            InitializeComponent( );
            _server.OnSocketCommandRecevied += Server_OnSocketCommandRecevied;
        }

        private void button1_Click( object sender, EventArgs e )
        {
            new Thread( _server.StartListening ).Start( );
        }

        private void Server_OnSocketCommandRecevied( SocketCommandBase socketCommand )
        {
            if ( InvokeRequired )
                Invoke( new Action( ( ) =>
                {
                    rtbLog.AppendText( socketCommand.ToString( ) );
                    rtbLog.AppendText( "\n" );
                } ) );
        }
    }
}