using System;
using System.Drawing;
using System.Globalization;
using System.Net;
using System.Threading;
using System.Windows.Forms;
using HiLoSocket.Builder.Client;
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
            var delay = ( int ) nudDelayTime.Value;
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
                        this.UpdateUi( ( ) =>
                        {
                            lblStatus.Text = $@"Working {( ++_reStartTime ).ToString( )}";
                        } );
                    }
                    finally
                    {
                        Thread.Sleep( delay );
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
            this.UpdateUi( ( ) =>
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
            } );
        }

        private void Logger_OnLog( LogModel logModel )
        {
            this.UpdateUi( ( ) =>
            {
                AppendText( rtbLog, Color.Green, logModel.Time.ToString( CultureInfo.InvariantCulture ) );
                rtbLog.AppendText( "\n" );
                AppendText( rtbLog, Color.Blue, logModel.Message.ToString( CultureInfo.InvariantCulture ) );
                rtbLog.AppendText( "\n" );
                rtbLog.SelectionStart = rtbLog.Text.Length;
                rtbLog.ScrollToCaret( );
            } );
        }

        private void Setup( )
        {
            var logger = new FormLogger( );
            _client = ClientBuilder<SocketCommandModel>.CreateNew( )
                .SetLocalIpEndPoint( new IPEndPoint( IPAddress.Parse( "127.0.0.1" ), 8001 ) )
                .SetRemoteIpEndPoint( new IPEndPoint( IPAddress.Parse( "127.0.0.1" ), 8000 ) )
                .SetFormatterType( ( FormatterType? ) Enum.Parse( typeof( FormatterType ),
                    ( string ) mcbFormatter.SelectedItem ) )
                .SetCompressType( ( CompressType? ) Enum.Parse( typeof( CompressType ),
                    ( string ) mcbCompressor.SelectedItem ) )
                .SetTimeoutTime( 2000 )
                .SetLogger( logger )
                .Build( );

            logger.OnLog += Logger_OnLog;
            _client.OnCommandModelReceived += Client_OnAckCommandReceived;
        }
    }
}