using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Net;


namespace NetworksLab2FTPClient
{
    public partial class Form1 : Form
    {
        FtpWebRequest ftpServer;
        FtpWebResponse ftpResponse;
        StreamReader ftpReader;
        StreamWriter ftpWriter;
        public Form1()
        {
            InitializeComponent();
        }

        private void connectBtn_Click(object sender, EventArgs e)
        {
            if(ftpServer != null)

        }
    }
}
