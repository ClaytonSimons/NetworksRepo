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
using System.Text.RegularExpressions;

namespace NetworksLab2FTPClient
{
    public partial class FtpClientUI : Form
    {
        FtpWebRequest ftpRequest;
        FtpWebResponse ftpResponse;
        StreamReader ftpReader;
        Stream responseStream;
        String userName;
        String passWord;
        String serverAddress;
        String createdFile;
        public FtpClientUI()
        {
            InitializeComponent();
            ftpRequest = null;
            ftpResponse = null;
            ftpReader = null;
            responseStream = null;
        }
        /*
         * Connects to ftp server.
         */
        private void connectBtn_Click(object sender, EventArgs e)
        {
            if (userName == null || passWord == null || serverAddress == null)
                contentLstBx.Items.Add("missing credential information");
            else
            {
                ftpRequest = WebRequest.Create("ftp://" + serverAddress) as FtpWebRequest;
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
                contentLstBx.Items.Clear();
                foreach (String file in files)
                {
                    contentLstBx.Items.Add(file);
                }
                ftpResponse.Close();
            }
        }
        /*
         * Create
         */
        private void createBtn_Click(object sender, EventArgs e)
        {
            String fileName = utilityRTxt.Text;
            Regex isFolder = new Regex(@"\w+\.\w+");
            if (!isFolder.IsMatch(fileName))
            {
                ftpRequest = FtpWebRequest.Create("ftp://" + serverAddress + "/" + fileName) as FtpWebRequest;
                ftpRequest.Method = WebRequestMethods.Ftp.MakeDirectory;

                ftpRequest.Credentials = new NetworkCredential(userName, passWord);

                ftpResponse = ftpRequest.GetResponse() as FtpWebResponse;
                contentLstBx.Items.Add(ftpResponse.StatusCode);
            }
            else
            {
                ftpRequest = FtpWebRequest.Create("ftp://" + serverAddress + "/" + fileName) as FtpWebRequest;
                ftpRequest.Method = WebRequestMethods.Ftp.UploadFile;

                ftpRequest.Credentials = new NetworkCredential(userName, passWord);
                byte[] file;
                try
                {
                    StreamReader source = new StreamReader(utilityRTxt.Text);
                    file = Encoding.UTF8.GetBytes(source.ReadToEnd());
                    source.Close();
                }
                catch(Exception ex)
                {
                    FileCreation newfile = new FileCreation(fileName,this);
                    newfile.ShowDialog();
                    file = Encoding.UTF8.GetBytes(createdFile);
                }
                ftpRequest.ContentLength = file.Length;

                responseStream = ftpRequest.GetRequestStream();
                responseStream.Write(file, 0, file.Length);
                responseStream.Close();

                ftpResponse = ftpRequest.GetResponse() as FtpWebResponse;

                contentLstBx.Items.Add(ftpResponse.StatusCode);
            }
        }
        private void deleteBtnClick(object sender, EventArgs e)
        {
            String fileName = utilityRTxt.Text;
            //retrieve file
            ftpRequest = WebRequest.Create("ftp://" + serverAddress + "/" + fileName) as FtpWebRequest;
            ftpRequest.Method = WebRequestMethods.Ftp.DeleteFile;
            ftpRequest.Credentials = new NetworkCredential(userName, passWord);

            ftpResponse = ftpRequest.GetResponse() as FtpWebResponse;

            responseStream = ftpResponse.GetResponseStream();
            ftpReader = new StreamReader(responseStream);

            contentLstBx.Items.Add(ftpResponse.StatusDescription);

            ftpReader.Close();
            ftpResponse.Close();

        }
        private void retrieveBtnClick(object sender, EventArgs e)
        {
            String fileName = utilityRTxt.Text;
            //retrieve file
            ftpRequest = WebRequest.Create("ftp://" + serverAddress + "/" + fileName) as FtpWebRequest;
            ftpRequest.Method = WebRequestMethods.Ftp.DownloadFile;
            ftpRequest.Credentials = new NetworkCredential(userName, passWord);

            ftpResponse = ftpRequest.GetResponse() as FtpWebResponse;

            responseStream = ftpResponse.GetResponseStream();
            ftpReader = new StreamReader(responseStream);
            String content = ftpReader.ReadToEnd();
            contentLstBx.Items.Add(content);

            contentLstBx.Items.Add(ftpResponse.StatusDescription);

            ftpReader.Close();
            ftpResponse.Close();

        }
        private void updateBtnClick(object sender, EventArgs e)
        {
            String fileName = utilityRTxt.Text;
            //retrieve file
            ftpRequest = WebRequest.Create("ftp://" + serverAddress + "/" + fileName) as FtpWebRequest;
            ftpRequest.Method = WebRequestMethods.Ftp.DownloadFile;

            ftpRequest.Credentials = new NetworkCredential(userName, passWord);

            ftpResponse = ftpRequest.GetResponse() as FtpWebResponse;

            responseStream = ftpResponse.GetResponseStream();
            ftpReader = new StreamReader(responseStream);
            //start filecreation form to edit file
            FileCreation newfile = new FileCreation(fileName, this);
            newfile.setContent(ftpReader.ReadToEnd());
            newfile.ShowDialog();
            uploadFile();

        }
        /*
         */
        private void uploadFile()
        {
            String fileName = utilityRTxt.Text;
            ftpRequest = FtpWebRequest.Create("ftp://" + serverAddress + "/" + fileName) as FtpWebRequest;
            ftpRequest.Method = WebRequestMethods.Ftp.UploadFile;

            ftpRequest.Credentials = new NetworkCredential(userName, passWord);
            byte[] file;

            file = Encoding.UTF8.GetBytes(createdFile);
            ftpRequest.ContentLength = file.Length;

            responseStream = ftpRequest.GetRequestStream();
            responseStream.Write(file, 0, file.Length);
            responseStream.Close();

            ftpResponse = ftpRequest.GetResponse() as FtpWebResponse;

            contentLstBx.Items.Add(ftpResponse.StatusCode);
            
        }
        public void setFile(String txt)
        {
            createdFile = txt;
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
