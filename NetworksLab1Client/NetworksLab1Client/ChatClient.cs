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
    public class ChatClient:TcpClient
    {
        StreamWriter streamWriter;
        StreamReader streamReader;
        String username;
        String hostName;
        Thread readingThread;
        String incomeMessage;
        ClientInterface clientInterface;
        public ChatClient(String host, ClientInterface inter)
        {
            clientInterface = inter;
            streamWriter = null;
            streamReader = null;
            //If localhost, use it.  Otherwise, whatever is in host
            hostName = host == "localhost" ? "127.0.0.1" : host;
        }
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
        public void disconnect()
        {
            try
            {
                if (streamWriter != null)
                {
                    streamWriter.WriteLine("/Disconnect");
                    streamWriter.Flush();
                }
                if (readingThread != null)
                    readingThread.Join();
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
        public void send(String msg)
        {
            streamWriter.WriteLine(msg);
            streamWriter.Flush();
        }
        public void read()
        {
            String msg;
            while (true)
            {
                try
                {
                    msg = streamReader.ReadLine();
                    switch (msg)
                    {
                        case "/Name?"://asking what our name is
                            streamWriter.WriteLine(username);
                            streamWriter.Flush();
                            break;
                        case"/MyName"://telling us what our name is
                            incomeMessage = streamReader.ReadLine();
                            break;
                        case "/Disconnected"://you have been disconnected
                            streamWriter.Close();
                            streamWriter = null;
                            disconnect();
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
        public void updateUsername()
        {
            try
            {

                streamWriter.WriteLine("/NameUpdate");
                streamWriter.Flush();
            }
            catch(Exception e)
            {
                Debug.WriteLine(e.StackTrace);
            }
        }
        public void setUsername(String name)
        {
            username = name;
        }
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
