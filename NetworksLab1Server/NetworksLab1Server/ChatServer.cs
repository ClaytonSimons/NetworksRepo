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
    /*
     Class Name: ChatServer
     Description:
        This is the server class that controls communication between clients, and connection to the server.
     */
    class ChatServer
    {
        private List<User> users;
        private List<User> toRemove;
        private TcpListener server;
        private Thread cleanUpThread;
        private int portNumber;
        private int userNumber;
        /*
         Method Name: ChatServer
         Description:
            Constructor for the ChatServer.  Initializes data members.
         */
        public ChatServer()
        {
            userNumber = 0;
            portNumber = 1711;
            users = new List<User>();
            toRemove = new List<User>();
            //The port number standard to this application
            server = new TcpListener(portNumber);
        }
        /*
         Method Name: start
         Description:
            The loop that listens for Tcp connections from clients, and accepts them.
         */
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
        /*
         Method Name: cleanUp
         Description:
            Removes disconnected clients from the list and sends a message to other clients that they
         have been disconnected.
         */
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
                        remove(user);
                    }
                    toRemove.Clear();
                    Monitor.Exit(toRemove);
                    Monitor.Exit(users);
                }

                Thread.Yield();
            }
        }
        /*
         Method Name: SendMessageToAll
         Arguments:
            msg is the message to send.
         Description:
            Sends a Tcp message to all clients.
         */
        public void SendMessageToAll(String msg)
        {
            foreach (User user in users)
            {
                user.write(msg);
            }
        }
        /*
         Method Name: SendMessage
         Arguments:
            msg is the message to send.
            from is the user the message was from.
         Description:
            This method sends a Tcp message to all clients other than from.
         */
        public void SendMessage(String msg, User from)
        {
            foreach (User user in users)
            {
                if(user != from)
                    user.write(msg);
            }
        }
        /*
         Method Name: add
         Arguments:
            client is the client to add to users.
         Description:
            Adds client to the list of TcpClients called users.
         */
        public void add(TcpClient client)
        {
            Monitor.Enter(users);
            User user = new User(client, this);
            user.setName(assignName());
            users.Add(user);
            Console.WriteLine("User: " + user.getName() + " added");
            SendMessage(user.getName() + " Has joined",user);
            Monitor.Exit(users);
        }
        /*
         Method Name: markForDeath
         Arguments:
            user is the user to be removed from users.
         Description:
            Adds a user to be removed from users.
         */
        public void markForDeath(User user)
        {
            toRemove.Add(user);
        }
        /*
         Method Name: remove
         Arguments:
            user is the user to be removed from users.
         Description:
            Removes user from users.
         */
        private void remove(User user)
        {
            Monitor.Enter(users);
            SendMessage(user.getName() + " Has disconnected", user);
            users.Remove(user);
            Monitor.Exit(users);
        }
        /*
         Method Name: ~ChatServer
         Description:
            Clears all elements in users.
         */
        ~ChatServer()
        {
            users.Clear();
        }
        /*
         Method Name: assignName
         Description:
            Creates and assigns a name to a client.
         */
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
        /*
         Method Name: assignName
         Arguments: 
            name is the requested name from the client
         Description:
            Assigns the requested name to the user, or a name that has a number appended to the end to keep
         from redundant names on the server.
         */
        public String assignName(String name)
        {
            String answer = name;
            Random rand = new Random();
            bool continu = true;
            while (continu)
            {
                continu = false;
                foreach (User user in users)
                {
                    if (answer == user.getName())
                    {
                        continu = true;
                        answer = name + rand.Next(1000).ToString();
                        break;
                    }
                }
            }
            return answer;
        }
    }
}
