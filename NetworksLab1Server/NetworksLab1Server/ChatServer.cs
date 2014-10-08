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
                    add(server.AcceptTcpClient());
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
        public void add(TcpClient client)
        {
            Monitor.Enter(users);
            User user = new User(client, this);
            user.setName(assignName());
            users.Add(user);
            Console.WriteLine("User: " + user.getName() + " added");
            Monitor.Exit(users);
        }
        public void remove(User user)
        {
            Monitor.Enter(users);
            users.Remove(user);
            Monitor.Exit(users);
        }
        ~ChatServer()
        {
            users.Clear();
        }
        public String assignName()
        {
            StringBuilder name = new StringBuilder();
            int num = 0;
            name.Append("User");
            bool found = false;
            foreach (User user in users)
            {
                if (name.ToString() == user.getName())
                {
                    found = true;
                    name.Append(num.ToString());
                }
            }
            while (found)
            {
                foreach (User user in users)
                {
                    if (name.ToString() == user.getName())
                    {
                        name.Remove(name.Length, num.ToString().Length);
                        num++;
                        name.Append(num.ToString().Length);
                        continue;
                    }
                }
                found = false;
            }
            return name.ToString();
        }
    }
}
