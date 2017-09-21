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
            this.SuspendLayout();
            // 
            // rtbMessage
            // 
            this.rtbMessage.Location = new System.Drawing.Point(17, 64);
            this.rtbMessage.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.rtbMessage.Name = "rtbLog";
            this.rtbMessage.Size = new System.Drawing.Size(298, 158);
            this.rtbMessage.TabIndex = 0;
            this.rtbMessage.Text = "";
            // 
            // btnListen
            // 
            this.btnListen.FontSize = MetroFramework.MetroButtonSize.Tall;
            this.btnListen.Location = new System.Drawing.Point(391, 326);
            this.btnListen.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnListen.Name = "btnListen";
            this.btnListen.Size = new System.Drawing.Size(129, 82);
            this.btnListen.TabIndex = 1;
            this.btnListen.Text = "Listen";
            this.btnListen.UseSelectable = true;
            this.btnListen.Click += new System.EventHandler(this.btnListen_Click);
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.lblStatus.Location = new System.Drawing.Point(337, 64);
            this.lblStatus.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(73, 25);
            this.lblStatus.TabIndex = 2;
            this.lblStatus.Text = "Standby";
            // 
            // btnStop
            // 
            this.btnStop.FontSize = MetroFramework.MetroButtonSize.Tall;
            this.btnStop.Location = new System.Drawing.Point(391, 219);
            this.btnStop.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(129, 82);
            this.btnStop.TabIndex = 3;
            this.btnStop.Text = "Stop";
            this.btnStop.UseSelectable = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // rtbLog
            // 
            this.rtbLog.Location = new System.Drawing.Point(17, 250);
            this.rtbLog.Margin = new System.Windows.Forms.Padding(2);
            this.rtbLog.Name = "rtbLog";
            this.rtbLog.Size = new System.Drawing.Size(298, 158);
            this.rtbLog.TabIndex = 4;
            this.rtbLog.Text = "";
            // 
            // ServerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(579, 438);
            this.Controls.Add(this.rtbLog);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.btnListen);
            this.Controls.Add(this.rtbMessage);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "ServerForm";
            this.Padding = new System.Windows.Forms.Padding(15, 48, 15, 16);
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
    }
}

