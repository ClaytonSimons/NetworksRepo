using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.IO;
using System.Threading;
using System.Diagnostics;

namespace NetworksLab1Client
{
    /*
     Class Name: ChatClient
     Description: 
        This is the TcpClient that connects with the server.  It is the backend to the 
     user interface of this program.
     */
    public class ChatClient:TcpClient
    {
        StreamWriter streamWriter;
        StreamReader streamReader;
        String username;
        String hostName;
        Thread readingThread;
        String incomeMessage;
        bool shouldEnd;
        ClientInterface clientInterface;
        /*
         Method Name: ChatClient
         Arguments: 
            host is the name or ip address of the computer that the server's on.
            inter is the user interface that is associated with this client.
         Description:
            Constructor for ChatClient.  Initializes data members.
         */
        public ChatClient(String host, ClientInterface inter)
        {
            shouldEnd = false;
            clientInterface = inter;
            streamWriter = null;
            streamReader = null;
            //If localhost, use it.  Otherwise, whatever is in host
            hostName = host == "localhost" ? "127.0.0.1" : host;
        }
        /*
         Method Name: start
         Description: 
            This is a start point for the connection and setup of communication with
         the server.
         */
        public bool start()
        {
            try
            {
                //try to connect to the server
                base.Connect(hostName, 1711);

                //Get the stream between server and client
                NetworkStream ns = GetStream();

                //check for previous stream
                if (streamWriter != null)
                    streamWriter.Close();
                //assign new stream
                streamWriter = new StreamWriter(ns);

                //check for previous stream
                if (streamReader != null)
                    streamReader.Close();
                //assign new stream
                streamReader = new StreamReader(ns);

                //start the read thread
                readingThread = new Thread(new ThreadStart(read));
                readingThread.Start();

                //Get assigned name
                string current = clientInterface.getUsernameRTxt();
                if (current == "")
                {
                    setUsername(getUsernameFromServer());
                    clientInterface.updateUsernameRTxt(username);
                }
                else
                    setUsername(current);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e + " " + e.StackTrace);
                disconnect();
                return false;
            }

            return true;
        }
        /*
         Method Name: disconnect
         Description:
            This method disconnects from the server.
         */
        public void disconnect()
        {
            if(this.Active)
                try
                {
                    if (streamWriter != null)
                    {
                        streamWriter.WriteLine("/Disconnect");
                        streamWriter.Flush();
                    }
                    shouldEnd = true;
                    if(streamWriter != null)
                        streamWriter.Close();
                    if(streamReader != null)
                        streamReader.Close();
                    Close();
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.StackTrace);
                    if(readingThread != null)
                        readingThread.Abort();
                
                    streamWriter = null;
                    streamReader = null;
                }
        }
        /*
         Method Name: send
         Arguments: 
            msg is the message to be sent.
         Description:
            This method sends a message to the server.
         */
        public void send(String msg)
        {
            streamWriter.WriteLine(msg);
            streamWriter.Flush();
        }
        /*
         Method Name: read
         Description:
            This method listens for Tcp messages from the server and
         processes them.
         */
        public void read()
        {
            String msg;
            while (!shouldEnd)
            {
                try
                {
                    msg = streamReader.ReadLine();
                    switch (msg)
                    {
                        case "/Name?"://server is asking what our name is
                            streamWriter.WriteLine(username);
                            streamWriter.Flush();
                            break;
                        case "/MyName"://server is telling us what our name is
                            incomeMessage = streamReader.ReadLine();
                            break;
                        case "/Disconnected"://you have been disconnected
                            streamWriter.Close();
                            streamWriter = null;
                            shouldEnd = true;
                            disconnect();
                            clientInterface.disableUI();
                            clientInterface.updateChatBox("disconnected");
                            break;
                        default://got a message from other users
                            clientInterface.updateChatBox(msg);
                            break;
                    }
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.StackTrace);
                }
            }
        }
        /*
         Method Name: updateUsername
         Description:
            Updates the username on the server for this client.
         */
        public void updateUsername()
        {
            try
            {
                streamWriter.WriteLine("/NameUpdate");
                streamWriter.Flush();
                streamWriter.WriteLine(username);
                streamWriter.Flush();
                username = getUsernameFromServer();
                clientInterface.updateUsernameRTxt(username);
            }
            catch(Exception e)
            {
                Debug.WriteLine(e.StackTrace);
            }
        }
        /*
         Method Name: getUsername
         Description:
            returns the username data member.
         */
        public String getUsername()
        {
            return username;
        }
        /*
         Method Name: setUsername
         Arguemnts:
            name is the string to set the username to.
         Description:
            Sets the username to name.
         */
        public void setUsername(String name)
        {
            username = name;
        }
        /*
         Method Name: getUsernameFromServer
         Description:
            Retrieves the username the server has stored for this client
         and returns it.
         */
        public String getUsernameFromServer()
        {
            try
            {
                streamWriter.WriteLine("/MyName");
                streamWriter.Flush();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.StackTrace);
            }
            while(true)
                if (incomeMessage != null)
                {
                    String answer = incomeMessage;
                    incomeMessage = null;
                    return answer;
                }
        }
    }
}
