namespace NetworksLab3Compression
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
            this.serverAddressTxt = new System.Windows.Forms.TextBox();
            this.serverAdressLbl = new System.Windows.Forms.TextBox();
            this.huffmanRadBtn = new System.Windows.Forms.RadioButton();
            this.compBtn = new System.Windows.Forms.Button();
            this.decompBtn = new System.Windows.Forms.Button();
            this.FeedbackTxt = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // serverAddressTxt
            // 
            this.serverAddressTxt.Location = new System.Drawing.Point(191, 12);
            this.serverAddressTxt.Name = "serverAddressTxt";
            this.serverAddressTxt.Size = new System.Drawing.Size(174, 20);
            this.serverAddressTxt.TabIndex = 0;
            // 
            // serverAdressLbl
            // 
            this.serverAdressLbl.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.serverAdressLbl.Location = new System.Drawing.Point(26, 12);
            this.serverAdressLbl.Name = "serverAdressLbl";
            this.serverAdressLbl.ReadOnly = true;
            this.serverAdressLbl.Size = new System.Drawing.Size(100, 13);
            this.serverAdressLbl.TabIndex = 1;
            this.serverAdressLbl.Text = "Server Address";
            // 
            // huffmanRadBtn
            // 
            this.huffmanRadBtn.AutoSize = true;
            this.huffmanRadBtn.Location = new System.Drawing.Point(272, 53);
            this.huffmanRadBtn.Name = "huffmanRadBtn";
            this.huffmanRadBtn.Size = new System.Drawing.Size(93, 17);
            this.huffmanRadBtn.TabIndex = 2;
            this.huffmanRadBtn.TabStop = true;
            this.huffmanRadBtn.Text = "Huffman Code";
            this.huffmanRadBtn.UseVisualStyleBackColor = true;
            // 
            // compBtn
            // 
            this.compBtn.Location = new System.Drawing.Point(7, 53);
            this.compBtn.Name = "compBtn";
            this.compBtn.Size = new System.Drawing.Size(114, 24);
            this.compBtn.TabIndex = 6;
            this.compBtn.Text = "Compress";
            this.compBtn.UseVisualStyleBackColor = true;
            this.compBtn.Click += new System.EventHandler(this.compBtn_Click);
            // 
            // decompBtn
            // 
            this.decompBtn.Location = new System.Drawing.Point(127, 53);
            this.decompBtn.Name = "decompBtn";
            this.decompBtn.Size = new System.Drawing.Size(106, 24);
            this.decompBtn.TabIndex = 7;
            this.decompBtn.Text = "Decompress";
            this.decompBtn.UseVisualStyleBackColor = true;
            this.decompBtn.Click += new System.EventHandler(this.decompBtn_Click);
            // 
            // FeedbackTxt
            // 
            this.FeedbackTxt.BackColor = System.Drawing.SystemColors.Control;
            this.FeedbackTxt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.FeedbackTxt.ForeColor = System.Drawing.SystemColors.Control;
            this.FeedbackTxt.Location = new System.Drawing.Point(26, 31);
            this.FeedbackTxt.Name = "FeedbackTxt";
            this.FeedbackTxt.Size = new System.Drawing.Size(144, 13);
            this.FeedbackTxt.TabIndex = 8;
            // 
            // ClientForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(377, 87);
            this.Controls.Add(this.FeedbackTxt);
            this.Controls.Add(this.decompBtn);
            this.Controls.Add(this.compBtn);
            this.Controls.Add(this.huffmanRadBtn);
            this.Controls.Add(this.serverAdressLbl);
            this.Controls.Add(this.serverAddressTxt);
            this.Name = "ClientForm";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox serverAddressTxt;
        private System.Windows.Forms.TextBox serverAdressLbl;
        private System.Windows.Forms.RadioButton huffmanRadBtn;
        private System.Windows.Forms.Button compBtn;
        private System.Windows.Forms.Button decompBtn;
        private System.Windows.Forms.TextBox FeedbackTxt;
    }
}

