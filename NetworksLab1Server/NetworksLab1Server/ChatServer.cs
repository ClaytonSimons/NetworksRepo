using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.IO;
using System.Threading;
using System.Net;

namespace NetworksLab1Server
{
    class ChatServer
    {
        private List<User> users;
        private TcpListener server;
        private int portNumber;
        public ChatServer()
        {
            portNumber = 1711;
            users = new List<User>();
            //The port number standard to this application
            server = new TcpListener(portNumber);
        }
        public void start()
        {
            try
            {
                //starts listening
                server.Start();

                while (true)
                {
                    //
                    Console.WriteLine("Waiting for a connection.....");
                    //Accept new client
                    users.Add(new User(server.AcceptTcpClient(),this));
                    //
                    Console.WriteLine("Connection accepted");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e + " " + e.StackTrace);
            }
            finally
            {
                server.Stop();
            }
        }
        public void SendMessage(String msg, User from)
        {
            foreach (User user in users)
            {
                if(user != from)
                    user.write(msg,from);
            }
        }
        public void remove(User user)
        {
            users.Remove(user);
        }
        ~ChatServer()
        {
            users.Clear();
        }
    }
}
