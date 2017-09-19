﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Net;
using System.Threading;
using System.Windows.Forms;
using HiLoSocket.CommandFormatter;
using HiLoSocket.Logger;
using HiLoSocket.Model;
using HiLoSocket.SocketApp;
using MetroFramework.Forms;

namespace ClientForm
{
    public partial class ClientForm : MetroForm
    {
        private readonly Client<byte[ ]> _client = new Client<byte[ ]>(
            new ClientModel
            {
                LocalIpEndPoint = new IPEndPoint( IPAddress.Parse( "127.0.0.1" ), 8081 ),
                RemoteIpEndPoint = new IPEndPoint( IPAddress.Parse( "127.0.0.1" ), 8000 ),
                FormatterType = FormatterType.MessagePackFormatter
            },
            new ConsoleLogger( ) );

        public ClientForm( )
        {
            InitializeComponent( );
            _client.OnCommandModelReceived += Client_OnAckCommandReceived;
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
                        //_client.Send( new SocketCommandModel
                        //{
                        //    CommandName = "Test2",
                        //    Id = Guid.NewGuid( ),
                        //    Results = new List<string> { "333", "111" },
                        //    Time = DateTime.Now
                        //} );

                        _client.Send( new byte[ ]
                        {
                            55,66
                        } );

                        Thread.Sleep( 1000 );
                    }
                    catch ( Exception )
                    {
                        Trace.WriteLine( "Client Stop!" );
                        if ( InvokeRequired )
                            Invoke( new Action( ( ) =>
                            {
                                lblStatus.Text = @"StandBy";
                            } ) );
                        break;
                    }
                }
            } ).Start( );
            lblStatus.Text = @"Working";
        }

        private void Client_OnAckCommandReceived( byte[ ] model )
        {
            if ( InvokeRequired )
                Invoke( new Action( ( ) =>
                {
                    AppendText( rtbLog, Color.Red, model[ 0 ] + model[ 1 ].ToString( ) );
                    //AppendText( rtbLog, Color.Red, model.Id.ToString( ) );
                    //rtbLog.AppendText( "\n" );
                    //rtbLog.AppendText( model.Time.ToString( CultureInfo.InvariantCulture ) );
                    rtbLog.AppendText( "\n" );
                } ) );
        }
    }
}