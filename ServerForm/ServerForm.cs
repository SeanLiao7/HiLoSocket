using System;
using System.Drawing;
using System.Globalization;
using System.Net;
using System.Threading;
using System.Windows.Forms;
using HiLoSocket;
using HiLoSocket.Logger;
using HiLoSocket.Model;
using MetroFramework.Forms;

namespace ServerForm
{
    public partial class ServerForm : MetroForm
    {
        private Server<SocketCommandModel> _server = new Server<SocketCommandModel>( new ServerModel
        {
            LocalIpEndPoint = new IPEndPoint( IPAddress.Parse( "127.0.0.1" ), 8000 )
        }, new ConsoleLogger( ) );

        public ServerForm( )
        {
            InitializeComponent( );
            _server.OnSocketCommandModelRecieved += Server_OnSocketCommandRecevied;
        }

        private void AppendText( RichTextBox box, Color color, string text )
        {
            var start = box.TextLength;
            box.AppendText( text );
            var end = box.TextLength;

            // Textbox may transform chars, so (end-start) != text.Length
            box.Select( start, end - start );
            box.SelectionColor = color;
            // could set box.SelectionBackColor, box.SelectionFont too.
            box.SelectionLength = 0; // clear
        }

        private void btnListen_Click( object sender, EventArgs e )
        {
            if ( _server == null )
                _server = new Server<SocketCommandModel>( new ServerModel
                {
                    LocalIpEndPoint = new IPEndPoint( IPAddress.Parse( "127.0.0.1" ), 8000 )
                }, new ConsoleLogger( ) );

            new Thread( _server.StartListening ).Start( );
            lblStatus.Text = @"Listening";
        }

        private void btnStop_Click( object sender, EventArgs e )
        {
            _server.StopListening( );
        }

        private void Server_OnSocketCommandRecevied( SocketCommandModel model )
        {
            if ( InvokeRequired )
                Invoke( new Action( ( ) =>
                {
                    AppendText( rtbLog, Color.Red, model.Id.ToString( ) );
                    rtbLog.AppendText( "\n" );
                    rtbLog.AppendText( model.Time.ToString( CultureInfo.InvariantCulture ) );
                    rtbLog.AppendText( "\n" );
                } ) );
        }
    }
}