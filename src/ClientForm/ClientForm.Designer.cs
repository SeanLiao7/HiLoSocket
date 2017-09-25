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
            this.btnStop = new MetroFramework.Controls.MetroButton();
            this.rtbLog = new System.Windows.Forms.RichTextBox();
            this.lblCompressor = new MetroFramework.Controls.MetroLabel();
            this.lblFormatter = new MetroFramework.Controls.MetroLabel();
            this.mcbCompressor = new MetroFramework.Controls.MetroComboBox();
            this.mcbFormatter = new MetroFramework.Controls.MetroComboBox();
            this.lblStatus = new MetroFramework.Controls.MetroLabel();
            this.lblDelayTime = new MetroFramework.Controls.MetroLabel();
            this.nudDelayTime = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.nudDelayTime)).BeginInit();
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
            this.btnSend.Location = new System.Drawing.Point(433, 321);
            this.btnSend.Margin = new System.Windows.Forms.Padding(2);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(129, 82);
            this.btnSend.TabIndex = 3;
            this.btnSend.Text = "Send";
            this.btnSend.UseSelectable = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // btnStop
            // 
            this.btnStop.FontSize = MetroFramework.MetroButtonSize.Tall;
            this.btnStop.Location = new System.Drawing.Point(433, 225);
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
            // lblCompressor
            // 
            this.lblCompressor.AutoSize = true;
            this.lblCompressor.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.lblCompressor.Location = new System.Drawing.Point(326, 142);
            this.lblCompressor.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblCompressor.Name = "lblCompressor";
            this.lblCompressor.Size = new System.Drawing.Size(103, 25);
            this.lblCompressor.TabIndex = 13;
            this.lblCompressor.Text = "Compressor";
            // 
            // lblFormatter
            // 
            this.lblFormatter.AutoSize = true;
            this.lblFormatter.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.lblFormatter.Location = new System.Drawing.Point(340, 99);
            this.lblFormatter.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblFormatter.Name = "lblFormatter";
            this.lblFormatter.Size = new System.Drawing.Size(85, 25);
            this.lblFormatter.TabIndex = 12;
            this.lblFormatter.Text = "Formatter";
            // 
            // mcbCompressor
            // 
            this.mcbCompressor.FormattingEnabled = true;
            this.mcbCompressor.ItemHeight = 23;
            this.mcbCompressor.Items.AddRange(new object[] {
            "Default",
            "GZip"});
            this.mcbCompressor.Location = new System.Drawing.Point(433, 142);
            this.mcbCompressor.Margin = new System.Windows.Forms.Padding(2);
            this.mcbCompressor.Name = "mcbCompressor";
            this.mcbCompressor.Size = new System.Drawing.Size(157, 29);
            this.mcbCompressor.TabIndex = 11;
            this.mcbCompressor.UseSelectable = true;
            // 
            // mcbFormatter
            // 
            this.mcbFormatter.FormattingEnabled = true;
            this.mcbFormatter.ItemHeight = 23;
            this.mcbFormatter.Items.AddRange(new object[] {
            "BinaryFormatter",
            "JSonFormatter",
            "MessagePackFormatter"});
            this.mcbFormatter.Location = new System.Drawing.Point(433, 99);
            this.mcbFormatter.Margin = new System.Windows.Forms.Padding(2);
            this.mcbFormatter.Name = "mcbFormatter";
            this.mcbFormatter.Size = new System.Drawing.Size(157, 29);
            this.mcbFormatter.TabIndex = 10;
            this.mcbFormatter.UseSelectable = true;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.lblStatus.Location = new System.Drawing.Point(350, 59);
            this.lblStatus.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(73, 25);
            this.lblStatus.TabIndex = 9;
            this.lblStatus.Text = "Standby";
            // 
            // lblDelayTime
            // 
            this.lblDelayTime.AutoSize = true;
            this.lblDelayTime.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.lblDelayTime.Location = new System.Drawing.Point(328, 186);
            this.lblDelayTime.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblDelayTime.Name = "lblDelayTime";
            this.lblDelayTime.Size = new System.Drawing.Size(101, 25);
            this.lblDelayTime.TabIndex = 14;
            this.lblDelayTime.Text = " Delay Time";
            // 
            // nudDelayTime
            // 
            this.nudDelayTime.Font = new System.Drawing.Font("PMingLiU", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.nudDelayTime.Increment = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.nudDelayTime.Location = new System.Drawing.Point(433, 186);
            this.nudDelayTime.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nudDelayTime.Name = "nudDelayTime";
            this.nudDelayTime.Size = new System.Drawing.Size(157, 27);
            this.nudDelayTime.TabIndex = 15;
            this.nudDelayTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nudDelayTime.Value = new decimal(new int[] {
            3000,
            0,
            0,
            0});
            // 
            // ClientForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(607, 438);
            this.Controls.Add(this.nudDelayTime);
            this.Controls.Add(this.lblDelayTime);
            this.Controls.Add(this.lblCompressor);
            this.Controls.Add(this.lblFormatter);
            this.Controls.Add(this.mcbCompressor);
            this.Controls.Add(this.mcbFormatter);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.rtbLog);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.metroUserControl1);
            this.Controls.Add(this.rtbMessage);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "ClientForm";
            this.Padding = new System.Windows.Forms.Padding(15, 60, 15, 16);
            this.Text = "Client";
            ((System.ComponentModel.ISupportInitialize)(this.nudDelayTime)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox rtbMessage;
        private MetroFramework.Controls.MetroUserControl metroUserControl1;
        private MetroFramework.Controls.MetroButton btnSend;
        private MetroFramework.Controls.MetroButton btnStop;
        private System.Windows.Forms.RichTextBox rtbLog;
        private MetroFramework.Controls.MetroLabel lblCompressor;
        private MetroFramework.Controls.MetroLabel lblFormatter;
        private MetroFramework.Controls.MetroComboBox mcbCompressor;
        private MetroFramework.Controls.MetroComboBox mcbFormatter;
        private MetroFramework.Controls.MetroLabel lblStatus;
        private MetroFramework.Controls.MetroLabel lblDelayTime;
        private System.Windows.Forms.NumericUpDown nudDelayTime;
    }
}

