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
        Thread serverThread;
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
                while (incoming != "/quit" && incoming != "/q")
                {
                    switch(incoming)
                    {
                        case "/NameUpdate":
                            streamWriter.WriteLine("Name?");
                            username = streamReader.ReadLine();
                            break;
                        case "/Disconnect":
                            purge();
                            chatServer.remove(this);
                            break;
                        case "/MyName":
                            streamWriter.WriteLine(username);
                            streamWriter.Flush();
                            break;
                        default:
                            Console.WriteLine("Message received: " + incoming);
                            //streamWriter.WriteLine(incoming);
                            chatServer.SendMessage(incoming,this);
                            streamWriter.Flush();
                            Console.WriteLine("Message Sent to chat room: " + incoming);
                            break;
                    }
                    incoming = streamReader.ReadLine();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e + " " + e.StackTrace);
            }
            chatServer.remove(this);
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
                serverThread.Abort();
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
    }
}
