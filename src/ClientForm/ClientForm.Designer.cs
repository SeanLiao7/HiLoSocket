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
            this.rtbMessage.Location = new System.Drawing.Point(35, 74);
            this.rtbMessage.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.rtbMessage.Name = "rtbMessage";
            this.rtbMessage.Size = new System.Drawing.Size(396, 189);
            this.rtbMessage.TabIndex = 0;
            this.rtbMessage.Text = "";
            // 
            // metroUserControl1
            // 
            this.metroUserControl1.Location = new System.Drawing.Point(613, 335);
            this.metroUserControl1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.metroUserControl1.Name = "metroUserControl1";
            this.metroUserControl1.Size = new System.Drawing.Size(8, 8);
            this.metroUserControl1.TabIndex = 2;
            this.metroUserControl1.UseSelectable = true;
            // 
            // btnSend
            // 
            this.btnSend.FontSize = MetroFramework.MetroButtonSize.Tall;
            this.btnSend.Location = new System.Drawing.Point(577, 401);
            this.btnSend.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(172, 102);
            this.btnSend.TabIndex = 3;
            this.btnSend.Text = "Send";
            this.btnSend.UseSelectable = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // btnStop
            // 
            this.btnStop.FontSize = MetroFramework.MetroButtonSize.Tall;
            this.btnStop.Location = new System.Drawing.Point(577, 281);
            this.btnStop.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(172, 102);
            this.btnStop.TabIndex = 5;
            this.btnStop.Text = "Stop";
            this.btnStop.UseSelectable = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // rtbLog
            // 
            this.rtbLog.Location = new System.Drawing.Point(35, 306);
            this.rtbLog.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.rtbLog.Name = "rtbLog";
            this.rtbLog.Size = new System.Drawing.Size(396, 196);
            this.rtbLog.TabIndex = 6;
            this.rtbLog.Text = "";
            // 
            // lblCompressor
            // 
            this.lblCompressor.AutoSize = true;
            this.lblCompressor.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.lblCompressor.Location = new System.Drawing.Point(435, 178);
            this.lblCompressor.Name = "lblCompressor";
            this.lblCompressor.Size = new System.Drawing.Size(109, 25);
            this.lblCompressor.TabIndex = 13;
            this.lblCompressor.Text = "Compressor";
            // 
            // lblFormatter
            // 
            this.lblFormatter.AutoSize = true;
            this.lblFormatter.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.lblFormatter.Location = new System.Drawing.Point(453, 124);
            this.lblFormatter.Name = "lblFormatter";
            this.lblFormatter.Size = new System.Drawing.Size(91, 25);
            this.lblFormatter.TabIndex = 12;
            this.lblFormatter.Text = "Formatter";
            // 
            // mcbCompressor
            // 
            this.mcbCompressor.FormattingEnabled = true;
            this.mcbCompressor.ItemHeight = 24;
            this.mcbCompressor.Items.AddRange(new object[] {
            "Default",
            "GZip"});
            this.mcbCompressor.Location = new System.Drawing.Point(577, 178);
            this.mcbCompressor.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.mcbCompressor.Name = "mcbCompressor";
            this.mcbCompressor.Size = new System.Drawing.Size(208, 30);
            this.mcbCompressor.TabIndex = 11;
            this.mcbCompressor.UseSelectable = true;
            // 
            // mcbFormatter
            // 
            this.mcbFormatter.FormattingEnabled = true;
            this.mcbFormatter.ItemHeight = 24;
            this.mcbFormatter.Items.AddRange(new object[] {
            "BinaryFormatter",
            "JSonFormatter",
            "MessagePackFormatter",
            "ProtobufFormatter"});
            this.mcbFormatter.Location = new System.Drawing.Point(577, 124);
            this.mcbFormatter.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.mcbFormatter.Name = "mcbFormatter";
            this.mcbFormatter.Size = new System.Drawing.Size(208, 30);
            this.mcbFormatter.TabIndex = 10;
            this.mcbFormatter.UseSelectable = true;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.lblStatus.Location = new System.Drawing.Point(467, 74);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(77, 25);
            this.lblStatus.TabIndex = 9;
            this.lblStatus.Text = "Standby";
            // 
            // lblDelayTime
            // 
            this.lblDelayTime.AutoSize = true;
            this.lblDelayTime.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.lblDelayTime.Location = new System.Drawing.Point(437, 232);
            this.lblDelayTime.Name = "lblDelayTime";
            this.lblDelayTime.Size = new System.Drawing.Size(107, 25);
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
            this.nudDelayTime.Location = new System.Drawing.Point(577, 232);
            this.nudDelayTime.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.nudDelayTime.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nudDelayTime.Name = "nudDelayTime";
            this.nudDelayTime.Size = new System.Drawing.Size(209, 31);
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
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(809, 548);
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
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "ClientForm";
            this.Padding = new System.Windows.Forms.Padding(20, 75, 20, 20);
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

