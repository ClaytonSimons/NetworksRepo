using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.IO;
using System.Threading;

namespace NetworksLab1Server
{
    class User
    {
        private String username;
        private TcpClient client;
        private StreamReader streamReader;
        private StreamWriter streamWriter;
        private ChatServer chatServer;
        private Thread serverThread;
        public User(TcpClient c,ChatServer cs)
        {
            chatServer = cs;
            client = c;
            streamReader = new StreamReader(client.GetStream());
            streamWriter = new StreamWriter(client.GetStream());
            serverThread = new Thread(new ThreadStart(loop));
            serverThread.Start();
        }
        public void loop()
        {
            try
            {
                String incoming = streamReader.ReadLine();
                bool quit = false;
                while (incoming != "/quit" && incoming != "/q" && quit == false)
                {
                    switch(incoming)
                    {
                        case "/NameUpdate":
                            streamWriter.WriteLine("/Name?");
                            streamWriter.Flush();
                            username = streamReader.ReadLine();
                            break;
                        case "/Disconnect":
                            quit = true;
                            break;
                        case "/MyName":
                            streamWriter.WriteLine("/MyName");
                            streamWriter.Flush();
                            streamWriter.WriteLine(username);
                            streamWriter.Flush();
                            break;
                        default:
                            Console.WriteLine("Message received: " + incoming);
                            //streamWriter.WriteLine(incoming);
                            chatServer.SendMessage(incoming,this);
                            Console.WriteLine("Message Sent to chat room: " + incoming);
                            break;
                    }
                    incoming = streamReader.ReadLine();
                }
                streamWriter.WriteLine("/Disconnected");
                streamWriter.Flush();
            }
            catch (Exception e)
            {
                Console.WriteLine(e + " " + e.StackTrace);
            }
            chatServer.markForDeath(this);
            purge();
        }
        public void write(String message,User user)
        {
            streamWriter.WriteLine(user.getName() + ": " + message);
            streamWriter.Flush();
        }
        public void purge()
        {
            try
            {
                
                streamReader.Close();
                streamWriter.Close();
                client.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
            Console.WriteLine("Disconnected User: " + username);
        }
        public void setName(String name)
        {
            username = name;
        }
        public String getName()
        {
            return username;
        }
        public Thread getServerThread()
        {
            return serverThread;
        }
    }
}
