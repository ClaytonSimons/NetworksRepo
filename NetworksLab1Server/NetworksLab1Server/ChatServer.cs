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
        private List<User> toRemove;
        private TcpListener server;
        private Thread cleanUpThread;
        private int portNumber;
        private int userNumber;
        public ChatServer()
        {
            userNumber = 0;
            portNumber = 1711;
            users = new List<User>();
            toRemove = new List<User>();
            //The port number standard to this application
            server = new TcpListener(portNumber);
        }
        public void start()
        {
            try
            {
                cleanUpThread = new Thread(new ThreadStart(cleanUp));
                cleanUpThread.Start();
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
        private void cleanUp()
        {
            while (true)
            {

                if (users.Count > 0 && toRemove.Count > 0)
                {
                    Monitor.Enter(users);
                    Monitor.Enter(toRemove);
                    foreach (User user in toRemove)
                    {
                        users.Remove(user);

                    }
                    toRemove.Clear();
                    Monitor.Exit(toRemove);
                    Monitor.Exit(users);
                }

                Thread.Yield();
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
        public void markForDeath(User user)
        {
            toRemove.Add(user);
        }
        private void remove(User user)
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
            String answer;
            userNumber++;
            answer = "User" + userNumber.ToString();
            bool found = false;
            foreach (User user in users)
            {
                if (answer == user.getName())
                {
                    found = true;
                    userNumber++;
                    answer = "User" + userNumber.ToString();
                }
            }
            return answer;
        }
    }
}
