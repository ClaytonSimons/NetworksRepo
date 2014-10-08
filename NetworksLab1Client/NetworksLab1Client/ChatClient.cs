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
            }
            catch (Exception e)
            {
                Debug.WriteLine(e + " " + e.StackTrace);
                disconnect();
                return false;
            }
                //start the read thread
            readingThread = new Thread(new ThreadStart(read));
            readingThread.Start();
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
                    readingThread.Abort();
                streamWriter.Close();
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
                    if (msg == "Name?")
                    {
                        streamWriter.WriteLine(username);
                        streamWriter.Flush();
                    }
                    else
                        clientInterface.updateChatBox(msg);
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
                if (streamReader.ReadLine() == "Name?")
                {
                    streamWriter.WriteLine(username);
                    streamWriter.Flush();
                }
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
            Monitor.Enter(this);
            String answer = streamReader.ReadLine();
            Monitor.Exit(this);
            return answer;
        }
    }
}
