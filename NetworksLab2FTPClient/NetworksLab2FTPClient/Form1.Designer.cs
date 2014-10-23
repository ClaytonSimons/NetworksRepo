namespace NetworksLab2FTPClient
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.usernameRTxt = new System.Windows.Forms.RichTextBox();
            this.contentLstBx = new System.Windows.Forms.ListBox();
            this.passwordRTxt = new System.Windows.Forms.RichTextBox();
            this.serverAddressRTxt = new System.Windows.Forms.RichTextBox();
            this.connectBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // usernameRTxt
            // 
            this.usernameRTxt.Location = new System.Drawing.Point(12, 12);
            this.usernameRTxt.Multiline = false;
            this.usernameRTxt.Name = "usernameRTxt";
            this.usernameRTxt.Size = new System.Drawing.Size(141, 25);
            this.usernameRTxt.TabIndex = 0;
            this.usernameRTxt.Text = "Username";
            this.usernameRTxt.TextChanged += new System.EventHandler(this.usernameRTxtChange);
            // 
            // contentLstBx
            // 
            this.contentLstBx.FormattingEnabled = true;
            this.contentLstBx.Location = new System.Drawing.Point(12, 74);
            this.contentLstBx.Name = "contentLstBx";
            this.contentLstBx.Size = new System.Drawing.Size(298, 212);
            this.contentLstBx.TabIndex = 1;
            // 
            // passwordRTxt
            // 
            this.passwordRTxt.Location = new System.Drawing.Point(12, 43);
            this.passwordRTxt.Multiline = false;
            this.passwordRTxt.Name = "passwordRTxt";
            this.passwordRTxt.Size = new System.Drawing.Size(141, 25);
            this.passwordRTxt.TabIndex = 2;
            this.passwordRTxt.Text = "Password";
            this.passwordRTxt.TextChanged += new System.EventHandler(this.passwordRTxtChange);
            // 
            // serverAddressRTxt
            // 
            this.serverAddressRTxt.Location = new System.Drawing.Point(159, 12);
            this.serverAddressRTxt.Multiline = false;
            this.serverAddressRTxt.Name = "serverAddressRTxt";
            this.serverAddressRTxt.Size = new System.Drawing.Size(151, 25);
            this.serverAddressRTxt.TabIndex = 3;
            this.serverAddressRTxt.Text = "ServerAddress";
            this.serverAddressRTxt.TextChanged += new System.EventHandler(this.serverAddressRTxtChange);
            // 
            // connectBtn
            // 
            this.connectBtn.Location = new System.Drawing.Point(159, 45);
            this.connectBtn.Name = "connectBtn";
            this.connectBtn.Size = new System.Drawing.Size(151, 23);
            this.connectBtn.TabIndex = 4;
            this.connectBtn.Text = "Connect";
            this.connectBtn.UseVisualStyleBackColor = true;
            this.connectBtn.Click += new System.EventHandler(this.connectBtn_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(434, 389);
            this.Controls.Add(this.connectBtn);
            this.Controls.Add(this.serverAddressRTxt);
            this.Controls.Add(this.passwordRTxt);
            this.Controls.Add(this.contentLstBx);
            this.Controls.Add(this.usernameRTxt);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox usernameRTxt;
        private System.Windows.Forms.ListBox contentLstBx;
        private System.Windows.Forms.RichTextBox passwordRTxt;
        private System.Windows.Forms.RichTextBox serverAddressRTxt;
        private System.Windows.Forms.Button connectBtn;
    }
}

