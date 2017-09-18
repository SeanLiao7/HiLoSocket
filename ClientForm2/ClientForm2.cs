using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Net;
using System.Threading;
using System.Windows.Forms;
using HiLoSocket.Logger;
using HiLoSocket.Model;
using HiLoSocket.SocketApp;
using MetroFramework.Forms;

namespace ClientForm2
{
    public partial class ClientForm2 : MetroForm
    {
        private readonly Client<SocketCommandModel> _client = new Client<SocketCommandModel>( new ClientModel
        {
            LocalIpEndPoint = new IPEndPoint( IPAddress.Parse( "127.0.0.1" ), 8082 ),
            RemoteIpEndPoint = new IPEndPoint( IPAddress.Parse( "127.0.0.1" ), 8000 )
        }, new ConsoleLogger( ) );

        private bool _canSend = true;

        public ClientForm2( )
        {
            InitializeComponent( );
            _client.OnCommandModelRecieved += Client_OnAckCommandReceived;
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

        private void btnSend_Click( object sender, EventArgs e )
        {
            new Thread( ( ) =>
            {
                while ( true )
                {
                    try
                    {
                        while ( true )
                        {
                            if ( _canSend )
                                break;
                            Thread.Sleep( 100 );
                        }
                        _canSend = false;
                        _client.Send( new SocketCommandModel
                        {
                            CommandName = "Test2",
                            Id = Guid.NewGuid( ),
                            Results = new List<string> { "333", "111" },
                            Time = DateTime.Now
                        } );
                    }
                    catch ( Exception )
                    {
                        Trace.WriteLine( "Client Stop!" );
                        _canSend = true;
                        break;
                    }
                }
            } ).Start( );
        }

        private void Client_OnAckCommandReceived( SocketCommandModel model )
        {
            _canSend = true;

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