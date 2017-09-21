namespace ClientForm
{
    partial class ClientForm
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
            this.metroUserControl1 = new MetroFramework.Controls.MetroUserControl();
            this.btnSend = new MetroFramework.Controls.MetroButton();
            this.lblStatus = new MetroFramework.Controls.MetroLabel();
            this.btnStop = new MetroFramework.Controls.MetroButton();
            this.rtbLog = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // rtbMessage
            // 
            this.rtbMessage.Location = new System.Drawing.Point(26, 59);
            this.rtbMessage.Margin = new System.Windows.Forms.Padding(2);
            this.rtbMessage.Name = "rtbMessage";
            this.rtbMessage.Size = new System.Drawing.Size(298, 152);
            this.rtbMessage.TabIndex = 0;
            this.rtbMessage.Text = "";
            // 
            // metroUserControl1
            // 
            this.metroUserControl1.Location = new System.Drawing.Point(460, 268);
            this.metroUserControl1.Margin = new System.Windows.Forms.Padding(2);
            this.metroUserControl1.Name = "metroUserControl1";
            this.metroUserControl1.Size = new System.Drawing.Size(6, 6);
            this.metroUserControl1.TabIndex = 2;
            this.metroUserControl1.UseSelectable = true;
            // 
            // btnSend
            // 
            this.btnSend.FontSize = MetroFramework.MetroButtonSize.Tall;
            this.btnSend.Location = new System.Drawing.Point(393, 321);
            this.btnSend.Margin = new System.Windows.Forms.Padding(2);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(129, 82);
            this.btnSend.TabIndex = 3;
            this.btnSend.Text = "Send";
            this.btnSend.UseSelectable = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.lblStatus.Location = new System.Drawing.Point(363, 59);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(73, 25);
            this.lblStatus.TabIndex = 4;
            this.lblStatus.Text = "Standby";
            // 
            // btnStop
            // 
            this.btnStop.FontSize = MetroFramework.MetroButtonSize.Tall;
            this.btnStop.Location = new System.Drawing.Point(393, 225);
            this.btnStop.Margin = new System.Windows.Forms.Padding(2);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(129, 82);
            this.btnStop.TabIndex = 5;
            this.btnStop.Text = "Stop";
            this.btnStop.UseSelectable = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // rtbLog
            // 
            this.rtbLog.Location = new System.Drawing.Point(26, 245);
            this.rtbLog.Margin = new System.Windows.Forms.Padding(2);
            this.rtbLog.Name = "rtbLog";
            this.rtbLog.Size = new System.Drawing.Size(298, 158);
            this.rtbLog.TabIndex = 6;
            this.rtbLog.Text = "";
            // 
            // ClientForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(579, 438);
            this.Controls.Add(this.rtbLog);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.metroUserControl1);
            this.Controls.Add(this.rtbMessage);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "ClientForm";
            this.Padding = new System.Windows.Forms.Padding(15, 60, 15, 16);
            this.Text = "Client";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox rtbMessage;
        private MetroFramework.Controls.MetroUserControl metroUserControl1;
        private MetroFramework.Controls.MetroButton btnSend;
        private MetroFramework.Controls.MetroLabel lblStatus;
        private MetroFramework.Controls.MetroButton btnStop;
        private System.Windows.Forms.RichTextBox rtbLog;
    }
}

