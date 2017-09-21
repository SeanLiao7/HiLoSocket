namespace ServerForm
{
    partial class ServerForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose( bool disposing )
        {
            if ( disposing && ( components != null ) )
            {
                components.Dispose( );
            }
            base.Dispose( disposing );
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent( )
        {
            this.rtbMessage = new System.Windows.Forms.RichTextBox();
            this.btnListen = new MetroFramework.Controls.MetroButton();
            this.lblStatus = new MetroFramework.Controls.MetroLabel();
            this.btnStop = new MetroFramework.Controls.MetroButton();
            this.rtbLog = new System.Windows.Forms.RichTextBox();
            this.mcbFormatter = new MetroFramework.Controls.MetroComboBox();
            this.mcbCompressor = new MetroFramework.Controls.MetroComboBox();
            this.lblFormatter = new MetroFramework.Controls.MetroLabel();
            this.lblCompressor = new MetroFramework.Controls.MetroLabel();
            this.SuspendLayout();
            // 
            // rtbMessage
            // 
            this.rtbMessage.Location = new System.Drawing.Point(23, 80);
            this.rtbMessage.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.rtbMessage.Name = "rtbMessage";
            this.rtbMessage.Size = new System.Drawing.Size(396, 196);
            this.rtbMessage.TabIndex = 0;
            this.rtbMessage.Text = "";
            // 
            // btnListen
            // 
            this.btnListen.FontSize = MetroFramework.MetroButtonSize.Tall;
            this.btnListen.Location = new System.Drawing.Point(521, 408);
            this.btnListen.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnListen.Name = "btnListen";
            this.btnListen.Size = new System.Drawing.Size(172, 102);
            this.btnListen.TabIndex = 1;
            this.btnListen.Text = "Listen";
            this.btnListen.UseSelectable = true;
            this.btnListen.Click += new System.EventHandler(this.btnListen_Click);
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.lblStatus.Location = new System.Drawing.Point(463, 80);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(77, 25);
            this.lblStatus.TabIndex = 2;
            this.lblStatus.Text = "Standby";
            // 
            // btnStop
            // 
            this.btnStop.FontSize = MetroFramework.MetroButtonSize.Tall;
            this.btnStop.Location = new System.Drawing.Point(521, 274);
            this.btnStop.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(172, 102);
            this.btnStop.TabIndex = 3;
            this.btnStop.Text = "Stop";
            this.btnStop.UseSelectable = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // rtbLog
            // 
            this.rtbLog.Location = new System.Drawing.Point(23, 312);
            this.rtbLog.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.rtbLog.Name = "rtbLog";
            this.rtbLog.Size = new System.Drawing.Size(396, 196);
            this.rtbLog.TabIndex = 4;
            this.rtbLog.Text = "";
            // 
            // mcbFormatter
            // 
            this.mcbFormatter.FormattingEnabled = true;
            this.mcbFormatter.ItemHeight = 24;
            this.mcbFormatter.Items.AddRange(new object[] {
            "BinaryFormatter",
            "JSonFormatter",
            "MessagePackFormatter"});
            this.mcbFormatter.Location = new System.Drawing.Point(546, 130);
            this.mcbFormatter.Name = "mcbFormatter";
            this.mcbFormatter.Size = new System.Drawing.Size(147, 30);
            this.mcbFormatter.TabIndex = 5;
            this.mcbFormatter.UseSelectable = true;
            // 
            // mcbCompressor
            // 
            this.mcbCompressor.FormattingEnabled = true;
            this.mcbCompressor.ItemHeight = 24;
            this.mcbCompressor.Items.AddRange(new object[] {
            "Default",
            "GZip"});
            this.mcbCompressor.Location = new System.Drawing.Point(546, 184);
            this.mcbCompressor.Name = "mcbCompressor";
            this.mcbCompressor.Size = new System.Drawing.Size(147, 30);
            this.mcbCompressor.TabIndex = 6;
            this.mcbCompressor.UseSelectable = true;
            // 
            // lblFormatter
            // 
            this.lblFormatter.AutoSize = true;
            this.lblFormatter.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.lblFormatter.Location = new System.Drawing.Point(449, 130);
            this.lblFormatter.Name = "lblFormatter";
            this.lblFormatter.Size = new System.Drawing.Size(91, 25);
            this.lblFormatter.TabIndex = 7;
            this.lblFormatter.Text = "Formatter";
            // 
            // lblCompressor
            // 
            this.lblCompressor.AutoSize = true;
            this.lblCompressor.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.lblCompressor.Location = new System.Drawing.Point(431, 184);
            this.lblCompressor.Name = "lblCompressor";
            this.lblCompressor.Size = new System.Drawing.Size(109, 25);
            this.lblCompressor.TabIndex = 8;
            this.lblCompressor.Text = "Compressor";
            // 
            // ServerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(772, 548);
            this.Controls.Add(this.lblCompressor);
            this.Controls.Add(this.lblFormatter);
            this.Controls.Add(this.mcbCompressor);
            this.Controls.Add(this.mcbFormatter);
            this.Controls.Add(this.rtbLog);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.btnListen);
            this.Controls.Add(this.rtbMessage);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "ServerForm";
            this.Padding = new System.Windows.Forms.Padding(20, 75, 20, 20);
            this.Text = "Server";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox rtbLog;
        private MetroFramework.Controls.MetroButton btnListen;
        private MetroFramework.Controls.MetroLabel lblStatus;
        private MetroFramework.Controls.MetroButton btnStop;
        private System.Windows.Forms.RichTextBox rtbMessage;
        private MetroFramework.Controls.MetroComboBox mcbFormatter;
        private MetroFramework.Controls.MetroComboBox mcbCompressor;
        private MetroFramework.Controls.MetroLabel lblFormatter;
        private MetroFramework.Controls.MetroLabel lblCompressor;
    }
}

