﻿using System;
using System.Diagnostics;
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

namespace ServerForm
{
    public partial class ServerForm : MetroForm
    {
        private Server<SocketCommandModel> _server;

        public ServerForm( )
        {
            InitializeComponent( );
            var logger = new FormLogger( );

            _server = new Server<SocketCommandModel>(
                new ServerConfigModel
                {
                    LocalIpEndPoint = new IPEndPoint( IPAddress.Parse( "127.0.0.1" ), 8000 ),
                    FormatterType = FormatterType.MessagePackFormatter,
                    CompressType = CompressType.GZip
                }, logger );

            logger.OnLog += Logger_OnLog;
            _server.OnCommandModelReceived += Server_OnSocketCommandRecevied;
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
            new Thread( ( ) =>
            {
                try
                {
                    _server.StartListening( );
                }
                catch ( Exception ex )
                {
                    Trace.WriteLine( $"Server fail : {ex.Message}" );
                    if ( InvokeRequired )
                        Invoke( new Action( ( ) =>
                        {
                            lblStatus.Text = @"Standby";
                        } ) );
                };
            } ).Start( );
            lblStatus.Text = @"Listening";
        }

        private void btnStop_Click( object sender, EventArgs e )
        {
            _server.StopListening( );
            lblStatus.Text = @"Standby";
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

        private void Server_OnSocketCommandRecevied( SocketCommandModel model )
        {
            if ( InvokeRequired )
                Invoke( new Action( ( ) =>
                {
                    AppendText( rtbMessage, Color.Red, model.Id.ToString( ) );
                    rtbMessage.AppendText( "\n" );
                    AppendText( rtbMessage, Color.Black, model.CommandName );
                    rtbMessage.AppendText( "\n" );
                    AppendText( rtbMessage, Color.Black, model.Time.ToString( CultureInfo.InvariantCulture ) );
                    rtbMessage.AppendText( "\n" );
                    AppendText( rtbMessage, Color.Black, ( string ) model.Results );
                    rtbMessage.AppendText( "\n" );
                    rtbMessage.SelectionStart = rtbMessage.Text.Length;
                    rtbMessage.ScrollToCaret( );
                } ) );
        }
    }
}