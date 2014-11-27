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
            this.lzRadBtn = new System.Windows.Forms.RadioButton();
            this.runLengthRadBtn = new System.Windows.Forms.RadioButton();
            this.arithmeticRadBtn = new System.Windows.Forms.RadioButton();
            this.compBtn = new System.Windows.Forms.Button();
            this.decompBtn = new System.Windows.Forms.Button();
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
            this.huffmanRadBtn.Location = new System.Drawing.Point(260, 39);
            this.huffmanRadBtn.Name = "huffmanRadBtn";
            this.huffmanRadBtn.Size = new System.Drawing.Size(93, 17);
            this.huffmanRadBtn.TabIndex = 2;
            this.huffmanRadBtn.TabStop = true;
            this.huffmanRadBtn.Text = "Huffman Code";
            this.huffmanRadBtn.UseVisualStyleBackColor = true;
            // 
            // lzRadBtn
            // 
            this.lzRadBtn.AutoSize = true;
            this.lzRadBtn.Location = new System.Drawing.Point(260, 62);
            this.lzRadBtn.Name = "lzRadBtn";
            this.lzRadBtn.Size = new System.Drawing.Size(77, 17);
            this.lzRadBtn.TabIndex = 3;
            this.lzRadBtn.TabStop = true;
            this.lzRadBtn.Text = "Lempel-Ziv";
            this.lzRadBtn.UseVisualStyleBackColor = true;
            // 
            // runLengthRadBtn
            // 
            this.runLengthRadBtn.AutoSize = true;
            this.runLengthRadBtn.Location = new System.Drawing.Point(260, 86);
            this.runLengthRadBtn.Name = "runLengthRadBtn";
            this.runLengthRadBtn.Size = new System.Drawing.Size(81, 17);
            this.runLengthRadBtn.TabIndex = 4;
            this.runLengthRadBtn.TabStop = true;
            this.runLengthRadBtn.Text = "Run-Length";
            this.runLengthRadBtn.UseVisualStyleBackColor = true;
            // 
            // arithmeticRadBtn
            // 
            this.arithmeticRadBtn.AutoSize = true;
            this.arithmeticRadBtn.Location = new System.Drawing.Point(260, 109);
            this.arithmeticRadBtn.Name = "arithmeticRadBtn";
            this.arithmeticRadBtn.Size = new System.Drawing.Size(99, 17);
            this.arithmeticRadBtn.TabIndex = 5;
            this.arithmeticRadBtn.TabStop = true;
            this.arithmeticRadBtn.Text = "Arithmetic Code";
            this.arithmeticRadBtn.UseVisualStyleBackColor = true;
            // 
            // compBtn
            // 
            this.compBtn.Location = new System.Drawing.Point(7, 53);
            this.compBtn.Name = "compBtn";
            this.compBtn.Size = new System.Drawing.Size(114, 70);
            this.compBtn.TabIndex = 6;
            this.compBtn.Text = "Compress";
            this.compBtn.UseVisualStyleBackColor = true;
            this.compBtn.Click += new System.EventHandler(this.compBtn_Click);
            // 
            // decompBtn
            // 
            this.decompBtn.Location = new System.Drawing.Point(127, 53);
            this.decompBtn.Name = "decompBtn";
            this.decompBtn.Size = new System.Drawing.Size(106, 70);
            this.decompBtn.TabIndex = 7;
            this.decompBtn.Text = "Decompress";
            this.decompBtn.UseVisualStyleBackColor = true;
            this.decompBtn.Click += new System.EventHandler(this.decompBtn_Click);
            // 
            // ClientForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(377, 129);
            this.Controls.Add(this.decompBtn);
            this.Controls.Add(this.compBtn);
            this.Controls.Add(this.arithmeticRadBtn);
            this.Controls.Add(this.runLengthRadBtn);
            this.Controls.Add(this.lzRadBtn);
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
        private System.Windows.Forms.RadioButton lzRadBtn;
        private System.Windows.Forms.RadioButton runLengthRadBtn;
        private System.Windows.Forms.RadioButton arithmeticRadBtn;
        private System.Windows.Forms.Button compBtn;
        private System.Windows.Forms.Button decompBtn;
    }
}

