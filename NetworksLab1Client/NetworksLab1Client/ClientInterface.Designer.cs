namespace NetworksLab1Client
{
    partial class ClientInterface
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
            this.components = new System.ComponentModel.Container();
            this.SendBtn = new System.Windows.Forms.Button();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.SendRTxt = new System.Windows.Forms.RichTextBox();
            this.ChatBoxRTxt = new System.Windows.Forms.RichTextBox();
            this.UsernameRTxt = new System.Windows.Forms.RichTextBox();
            this.UsernameTxt = new System.Windows.Forms.TextBox();
            this.UpdateBtn = new System.Windows.Forms.Button();
            this.ConnectBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // SendBtn
            // 
            this.SendBtn.Enabled = false;
            this.SendBtn.Location = new System.Drawing.Point(386, 248);
            this.SendBtn.Name = "SendBtn";
            this.SendBtn.Size = new System.Drawing.Size(84, 23);
            this.SendBtn.TabIndex = 1;
            this.SendBtn.Text = "Send";
            this.SendBtn.UseVisualStyleBackColor = true;
            this.SendBtn.Click += new System.EventHandler(this.Send);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // SendRTxt
            // 
            this.SendRTxt.Location = new System.Drawing.Point(12, 248);
            this.SendRTxt.Name = "SendRTxt";
            this.SendRTxt.Size = new System.Drawing.Size(365, 23);
            this.SendRTxt.TabIndex = 3;
            this.SendRTxt.Text = "";
            // 
            // ChatBoxRTxt
            // 
            this.ChatBoxRTxt.Location = new System.Drawing.Point(12, 12);
            this.ChatBoxRTxt.Name = "ChatBoxRTxt";
            this.ChatBoxRTxt.ReadOnly = true;
            this.ChatBoxRTxt.Size = new System.Drawing.Size(365, 230);
            this.ChatBoxRTxt.TabIndex = 4;
            this.ChatBoxRTxt.Text = "";
            // 
            // UsernameRTxt
            // 
            this.UsernameRTxt.Location = new System.Drawing.Point(383, 32);
            this.UsernameRTxt.Name = "UsernameRTxt";
            this.UsernameRTxt.Size = new System.Drawing.Size(87, 23);
            this.UsernameRTxt.TabIndex = 5;
            this.UsernameRTxt.Text = "";
            this.UsernameRTxt.TextChanged += new System.EventHandler(this.UsernameRTxtChange);
            // 
            // UsernameTxt
            // 
            this.UsernameTxt.BackColor = System.Drawing.SystemColors.Control;
            this.UsernameTxt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.UsernameTxt.Location = new System.Drawing.Point(386, 9);
            this.UsernameTxt.Name = "UsernameTxt";
            this.UsernameTxt.Size = new System.Drawing.Size(83, 13);
            this.UsernameTxt.TabIndex = 6;
            this.UsernameTxt.Text = "Username";
            // 
            // UpdateBtn
            // 
            this.UpdateBtn.Enabled = false;
            this.UpdateBtn.Location = new System.Drawing.Point(383, 61);
            this.UpdateBtn.Name = "UpdateBtn";
            this.UpdateBtn.Size = new System.Drawing.Size(86, 23);
            this.UpdateBtn.TabIndex = 7;
            this.UpdateBtn.Text = "Update";
            this.UpdateBtn.UseVisualStyleBackColor = true;
            this.UpdateBtn.Click += new System.EventHandler(this.UpdateUsernameClick);
            // 
            // ConnectBtn
            // 
            this.ConnectBtn.Location = new System.Drawing.Point(383, 90);
            this.ConnectBtn.Name = "ConnectBtn";
            this.ConnectBtn.Size = new System.Drawing.Size(86, 23);
            this.ConnectBtn.TabIndex = 8;
            this.ConnectBtn.Text = "Connect";
            this.ConnectBtn.UseVisualStyleBackColor = true;
            this.ConnectBtn.Click += new System.EventHandler(this.ConnectBtnClick);
            // 
            // ClientInterface
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(482, 282);
            this.Controls.Add(this.ConnectBtn);
            this.Controls.Add(this.UpdateBtn);
            this.Controls.Add(this.UsernameTxt);
            this.Controls.Add(this.UsernameRTxt);
            this.Controls.Add(this.ChatBoxRTxt);
            this.Controls.Add(this.SendRTxt);
            this.Controls.Add(this.SendBtn);
            this.Name = "ClientInterface";
            this.Text = "NAT Messenger";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormIsClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button SendBtn;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.RichTextBox SendRTxt;
        private System.Windows.Forms.RichTextBox ChatBoxRTxt;
        private System.Windows.Forms.RichTextBox UsernameRTxt;
        private System.Windows.Forms.TextBox UsernameTxt;
        private System.Windows.Forms.Button UpdateBtn;
        private System.Windows.Forms.Button ConnectBtn;
    }
}