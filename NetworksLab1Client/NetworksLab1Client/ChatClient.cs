using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.IO;
using System.Threading;

namespace NetworksLab1Client
{
    public class ChatClient:TcpClient
    {
        StreamWriter streamWriter;
        StreamReader streamReader;
        String username;
        String hostName;
        public ChatClient(String host)
        {
            streamWriter = null;
            streamReader = null;
            //If localhost, use it.  Otherwise, whatever is in host
            hostName = host == "localhost" ? "127.0.0.1" : host;
            try
            {
                base.Connect(hostName, 1711);
            }
            catch (Exception e)
            {
                Console.WriteLine(e + " " + e.StackTrace);
            }
        }
        public void start()
        {
            try
            {
                Console.WriteLine("What is your username?");
                username = Console.ReadLine();
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

                //read thread
                Thread readingthread = new Thread(new ThreadStart(read));
                readingthread.Start();

                String input = "start";
                Console.WriteLine("Enter text: '/quit' to stop: ");

                while (input != "/quit" && input != "/q")
                {
                    //
                    input = Console.ReadLine();
                    //Sends message to the server
                    streamWriter.WriteLine(input);
                    streamWriter.Flush();
                }

                //Send final message to end connection
                streamWriter.WriteLine("/quit");
                streamWriter.Flush();
            }
            catch (Exception e)
            {
                Console.WriteLine(e + " " + e.StackTrace);
            }
            finally
            {
                //Close the connection incase exception was thrown
                if (streamWriter!=null) streamWriter.Close();
	            if (streamReader!=null) streamReader.Close();
            }
            Environment.Exit(0);
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
                        Console.WriteLine(msg);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.StackTrace);
                }
            }
        }
    }
}
