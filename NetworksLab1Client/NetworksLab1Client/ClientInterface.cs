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
        public ClientInterface()
        {
            InitializeComponent();
            client = new ChatClient("PRINCEofPERSIA",this);
            if (client.start())
                updateChatBox("connected!");
            else
                updateChatBox("couldn't connect");
        }
        public void updateChatBox(String msg)
        {
            if (ChatBoxRTxt.Text.Length == 0)
                ChatBoxRTxt.AppendText(msg);
            else
                ChatBoxRTxt.AppendText("\n" + msg);
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
            String tempname = client.getUsernameFromServer();
            client.disconnect();
            client = new ChatClient("PRINCEofPERSIA", this);
            client.setUsername(tempname);
            if (client.start())
            {
                updateChatBox("connected!");
                client.updateUsername();
            }
            else
                updateChatBox("couldn't connect");
        }

       
    }
}
