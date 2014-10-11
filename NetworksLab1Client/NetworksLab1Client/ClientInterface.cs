using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.IO;
using System.Threading;
using System.Diagnostics;

namespace NetworksLab1Client
{
    public partial class ClientInterface : Form
    {
        ChatClient client;
        String serverHost;
        public ClientInterface()
        {
            serverHost = "SCUTULATUS";
            InitializeComponent();
            client = new ChatClient(serverHost, this);
            if (client.start())
            {
                updateChatBox("connected!");
                enableUI();
            }
            else
                updateChatBox("couldn't connect");
        }
        public void enableUI()
        {
            SendBtn.Enabled = true;
            UpdateBtn.Enabled = true;
        }
        public void disableUI()
        {
            SendBtn.Enabled = false;
            UpdateBtn.Enabled = false;
        }
        public void updateChatBox(String msg)
        {
            if (ChatBoxRTxt.ToString().Length == 0)
                ChatBoxRTxt.AppendText(msg);
            else
                ChatBoxRTxt.AppendText("\n" + msg);
            ChatBoxRTxt.ScrollToCaret();
        }

        public void updateUsernameRTxt(String msg)
        {
            UsernameRTxt.ResetText();
            UsernameRTxt.AppendText(msg);
        }

        public String getUsernameRTxt()
        {
            return UsernameRTxt.Text.ToString();
        }

        private void Send(object sender, EventArgs e)
        {
            String text = SendRTxt.Text;
            client.send(text);
            //ChatBoxRTxt.AppendText(client.getUsernameFromServer() + text);
        }

        private void UpdateUsernameClick(object sender, EventArgs e)
        {
            client.updateUsername();//updates the username server side.
        }

        private void UsernameRTxtChange(object sender, EventArgs e)
        {
            client.setUsername(UsernameRTxt.Text);
        }

        private void FormIsClosing(object sender, FormClosingEventArgs e)
        {
            client.disconnect();
            Environment.Exit(0);
        }

        private void ConnectBtnClick(object sender, EventArgs e)
        {
            String tempname = client.getUsername();
            disableUI();
            client.disconnect();
            client = new ChatClient(serverHost, this);
            if (client.start())
            {
                updateChatBox("connected!");
                client.updateUsername();
                enableUI();
            }
            else
                updateChatBox("couldn't connect");
        }

       
    }
}
