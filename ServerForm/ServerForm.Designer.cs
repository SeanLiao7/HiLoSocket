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
            this.rtbLog = new System.Windows.Forms.RichTextBox();
            this.btnListen = new MetroFramework.Controls.MetroButton();
            this.lblStatus = new MetroFramework.Controls.MetroLabel();
            this.SuspendLayout();
            // 
            // rtbLog
            // 
            this.rtbLog.Location = new System.Drawing.Point(23, 80);
            this.rtbLog.Name = "rtbLog";
            this.rtbLog.Size = new System.Drawing.Size(396, 429);
            this.rtbLog.TabIndex = 0;
            this.rtbLog.Text = "";
            // 
            // btnListen
            // 
            this.btnListen.FontSize = MetroFramework.MetroButtonSize.Tall;
            this.btnListen.Location = new System.Drawing.Point(521, 407);
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
            this.lblStatus.Location = new System.Drawing.Point(449, 80);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(77, 25);
            this.lblStatus.TabIndex = 2;
            this.lblStatus.Text = "Standby";
            // 
            // ServerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(772, 547);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.btnListen);
            this.Controls.Add(this.rtbLog);
            this.Name = "ServerForm";
            this.Text = "Server";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox rtbLog;
        private MetroFramework.Controls.MetroButton btnListen;
        private MetroFramework.Controls.MetroLabel lblStatus;
    }
}

