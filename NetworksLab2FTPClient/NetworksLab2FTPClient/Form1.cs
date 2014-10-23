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
        FtpWebRequest ftpRequest;
        FtpWebResponse ftpResponse;
        StreamReader ftpReader;
        Stream responseStream;
        String userName;
        String passWord;
        String serverAddress;
        public Form1()
        {
            InitializeComponent();
            ftpRequest = null;
            ftpResponse = null;
            ftpReader = null;
            responseStream = null;
        }

        private void connectBtn_Click(object sender, EventArgs e)
        {
            if (userName == null || passWord == null || serverAddress == null)
                contentLstBx.Items.Add("missing credential information");
            else
            {
                ftpRequest = WebRequest.Create("ftp://" + userName + ":" + passWord + serverAddress) as FtpWebRequest;
                ftpRequest.Method = WebRequestMethods.Ftp.ListDirectoryDetails;
                ftpRequest.Credentials = new NetworkCredential(userName, passWord);
                ftpResponse = ftpRequest.GetResponse() as FtpWebResponse;
                responseStream = ftpResponse.GetResponseStream();
                ftpReader = new StreamReader(responseStream);
                LinkedList<String> files = new LinkedList<String>();
                while (!ftpReader.EndOfStream)
                {
                    files.AddLast(ftpReader.ReadLine());
                }
                foreach (String file in files)
                {
                    contentLstBx.Items.Add(file);
                }
                ftpReader.Close();
                responseStream.Close();
                ftpResponse.Close();
            }
        }

        private void usernameRTxtChange(object sender, EventArgs e)
        {
            userName = usernameRTxt.Text;
        }

        private void passwordRTxtChange(object sender, EventArgs e)
        {
            passWord = passwordRTxt.Text;
        }

        private void serverAddressRTxtChange(object sender, EventArgs e)
        {
            serverAddress = serverAddressRTxt.Text;
        }
    }
}
