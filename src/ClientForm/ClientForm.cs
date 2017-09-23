using System;
using System.Drawing;
using System.Globalization;
using System.Net;
using System.Threading;
using System.Windows.Forms;
using HiLoSocket.CommandFormatter;
using HiLoSocket.Compressor;
using HiLoSocket.Model;
using HiLoSocket.SocketApp;
using MetroFramework.Forms;

namespace ClientForm
{
    public partial class ClientForm : MetroForm
    {
        private Client<SocketCommandModel> _client;
        private bool _isSending;
        private int _reStartTime;

        public ClientForm( )
        {
            InitializeComponent( );
            mcbFormatter.SelectedIndex = 0;
            mcbCompressor.SelectedIndex = 0;
        }

        private void AppendText( RichTextBox box, Color color, string text )
        {
            if ( text == null )
                return;

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
            if ( _isSending )
                return;

            Setup( );
            _isSending = true;

            new Thread( ( ) =>
            {
                while ( _isSending )
                {
                    try
                    {
                        _client.Send( new SocketCommandModel
                        {
                            CommandName = "AutoProgram",
                            Id = Guid.NewGuid( ),
                            Results = "true,true,true,true,false,true,true,true,true,true,true,true,false,true,true,true",
                            Time = DateTime.Now
                        } );
                    }
                    catch ( Exception )
                    {
                        if ( InvokeRequired )
                            Invoke( new Action( ( ) =>
                            {
                                lblStatus.Text = $@"Working {( ++_reStartTime ).ToString( )}";
                            } ) );
                    }
                    finally
                    {
                        Thread.Sleep( 5000 );
                    }
                }
                _isSending = false;
            } ).Start( );
            lblStatus.Text = @"Working";
        }

        private void btnStop_Click( object sender, EventArgs e )
        {
            _isSending = false;
            _reStartTime = 0;
            lblStatus.Text = @"Standby";
        }

        private void Client_OnAckCommandReceived( SocketCommandModel model )
        {
            if ( InvokeRequired )
                Invoke( new Action( ( ) =>
                {
                    AppendText( rtbMessage, Color.Red, model?.Id.ToString( ) );
                    rtbMessage.AppendText( "\n" );
                    AppendText( rtbMessage, Color.Black, model?.CommandName );
                    rtbMessage.AppendText( "\n" );
                    AppendText( rtbMessage, Color.Black, model?.Time.ToString( CultureInfo.InvariantCulture ) );
                    rtbMessage.AppendText( "\n" );
                    AppendText( rtbMessage, Color.Black, ( string ) model?.Results );
                    rtbMessage.AppendText( "\n" );
                    rtbMessage.SelectionStart = rtbMessage.Text.Length;
                    rtbMessage.ScrollToCaret( );
                } ) );
        }

        private void Logger_OnLog( LogModel logModel )
        {
            if ( InvokeRequired )
                Invoke( new Action( ( ) =>
                {
                    AppendText( rtbLog, Color.Green, logModel.Time.ToString( CultureInfo.InvariantCulture ) );
                    rtbLog.AppendText( "\n" );
                    AppendText( rtbLog, Color.Blue, logModel.Message.ToString( CultureInfo.InvariantCulture ) );
                    rtbLog.AppendText( "\n" );
                    rtbLog.SelectionStart = rtbLog.Text.Length;
                    rtbLog.ScrollToCaret( );
                } ) );
        }

        private void Setup( )
        {
            var logger = new FormLogger( );
            _client = new Client<SocketCommandModel>(
                new ClientConfigModel
                {
                    LocalIpEndPoint = new IPEndPoint( IPAddress.Parse( "127.0.0.1" ), 8001 ),
                    RemoteIpEndPoint = new IPEndPoint( IPAddress.Parse( "127.0.01" ), 8000 ),
                    FormatterType = ( FormatterType? ) Enum.Parse( typeof( FormatterType ), ( string ) mcbFormatter.SelectedItem ),
                    CompressType = ( CompressType? ) Enum.Parse( typeof( CompressType ), ( string ) mcbCompressor.SelectedItem ),
                    TimeOutTime = 5000
                }, logger );

            logger.OnLog += Logger_OnLog;
            _client.OnCommandModelReceived += Client_OnAckCommandReceived;
        }
    }
}