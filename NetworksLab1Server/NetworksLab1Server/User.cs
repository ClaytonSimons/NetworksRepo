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
        public User(TcpClient c,ChatServer cs)
        {
            chatServer = cs;
            client = c;
            streamReader = new StreamReader(client.GetStream());
            streamWriter = new StreamWriter(client.GetStream());
            Thread serverThread = new Thread(new ThreadStart(loop));
            serverThread.Start();
        }
        public void loop()
        {
            try
            {
                streamWriter.WriteLine("Name?");
                streamWriter.Flush();
                username = streamReader.ReadLine();
                String incoming = streamReader.ReadLine();
                while (incoming != "/quit" && incoming != "/q")
                {
                    Console.WriteLine("Message received: " + incoming);
                    //streamWriter.WriteLine(incoming);
                    chatServer.SendMessage(incoming,this);
                    streamWriter.Flush();
                    Console.WriteLine("Message Sent to chat room: " + incoming);
                    incoming = streamReader.ReadLine();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e + " " + e.StackTrace);
            }
            Console.WriteLine("Client sent '/quit': closing connection.");
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
                streamReader.Close();
                streamWriter.Close();
                client.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }
        public String getName()
        {
            return username;
        }
    }
}
