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
    /*
     Class Name: ClientInterface
     Description: 
        The user interface for this program.  It is a Form.
     */
    public partial class ClientInterface : Form
    {
        ChatClient client;
        String serverHost;
        /*
         Method Name: ClientInterface
         Description:
            Initializes data members, including connecting to the server.
         */
        public ClientInterface()
        {
            InitializeComponent();
        }
        /*
         Method Name: enableUI
         Description:
            Makes the SendBtn and UpdateBtn usable.
         */
        public void enableUI()
        {
            SendBtn.Enabled = true;
            UpdateBtn.Enabled = true;
        }
        /*
         Method Name: disableUI
         Description:
            Makes SendBtn and UpdateBtn unusable and grayed out.
         */
        public void disableUI()
        {
            SendBtn.Enabled = false;
            UpdateBtn.Enabled = false;
        }
        /*
         Method Name: updateChatBox
         Arguemnts:
            msg is the string to be added to the chat box.
         Description:
            Adds msg to the chat box.  These are messages from the server and are
         either originating from other clients or a server message.
         */
        public void updateChatBox(String msg)
        {
            if (ChatBoxRTxt.ToString().Length == 0)
                ChatBoxRTxt.AppendText(msg);
            else
                ChatBoxRTxt.AppendText("\n" + msg);
            ChatBoxRTxt.ScrollToCaret();
        }
        /*
         Method Name: updateUsernameRTxt
         Arguments:
            msg is the string to be put in the UsernameRTxt object.
         Description:
            Sets the UsernameRTxt object to display msg.
         */

        public void updateUsernameRTxt(String msg)
        {
            UsernameRTxt.ResetText();
            UsernameRTxt.AppendText(msg);
        }
        /*
         Method Name: getUsernameRTxt
         Description: 
            Returns the UsernameRTxt object.
         */

        public String getUsernameRTxt()
        {
            return UsernameRTxt.Text.ToString();
        }
        /*
         Method Name: Send
         Arguments:
            sender is the object that the event happened to.
            e is any arguments passed.
         Description:
            When the SendBtn is clicked, this method sends the text that is in the SendRTxt object
         to the server.  It then eliminates the text in that object.
         */

        private void Send(object sender, EventArgs e)
        {
            String text = SendRTxt.Text;
            client.send(text);
            SendRTxt.ResetText();
            //ChatBoxRTxt.AppendText(client.getUsernameFromServer() + text);
        }
        /*
         Method Name: UpdateUsername
         Arguments:
            sender is the object that the event happened to.
            e is any arguments passed.
         Description:
            Updates the username for this client on the server side.
         */

        private void UpdateUsernameClick(object sender, EventArgs e)
        {
            client.updateUsername();//updates the username server side.
        }
        /*
         Method Name: UsernameRTxtChange
         Arguments:
            sender is the object that the event happened to.
            e is any arguments passed.
         Description:
            Sets the username in the client to the text in the UsernameRTxt object.
         */

        private void UsernameRTxtChange(object sender, EventArgs e)
        {
            client.setUsername(UsernameRTxt.Text);
        }
        /*
         Method Name: FormIsClosing
         Arguments:
            sender is the object that the event happened to.
            e is any arguments passed.
         Description:
            Disconnects the client and ends the environment.  This happens when the form
         is closing.
         */

        private void FormIsClosing(object sender, FormClosingEventArgs e)
        {
            client.disconnect();
            Environment.Exit(0);
        }
        /*
         Method Name: ConnectBtnClick
         Arguments:
            sender is the object that the event happened to.
            e is any arguments passed.
         Description:
            When the ConnectBrn is clicked, this method disconnects the client and then
         connects it again.
         */

        private void ConnectBtnClick(object sender, EventArgs e)
        {
            if(client != null)
                client.disconnect();
            disableUI();
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
        /*
         * Method Name: serverNameRTxtChange
         * Arguments:
         *      sender is the object that the event happened to.
         *      e is any arguments passed.
         * Description:
         *      When the text in the serverNameRTxt object is changed, we update
         *   the serverhost datamember.
         */
        private void serverNameRTxtChange(object sender, EventArgs e)
        {
            serverHost = serverNameRTxt.Text;
        }
    }
}
