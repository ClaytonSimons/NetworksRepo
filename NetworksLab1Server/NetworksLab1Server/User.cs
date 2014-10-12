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
    /*
     Class Name: User
     Description:
        A client that uses the server.
     */
    class User
    {
        private String username;
        private TcpClient client;
        private StreamReader streamReader;
        private StreamWriter streamWriter;
        private ChatServer chatServer;
        private Thread serverThread;
        /*
         Method Name: User
         Arguments:
            c is the TcpClient.
            cs is the ChatServer to communicate with.
         Desctiption:
            Constructor for User.  It initializes data members and spins up a thread to listen for messages.
         */
        public User(TcpClient c,ChatServer cs)
        {
            chatServer = cs;
            client = c;
            streamReader = new StreamReader(client.GetStream());
            streamWriter = new StreamWriter(client.GetStream());
            serverThread = new Thread(new ThreadStart(loop));
            serverThread.Start();
        }
        /*
         Method Name: loop
         Description:
            Listens for Tcp messages and processes them.
         */
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
                        case "/NameUpdate"://Client asking to update it's name.
                            String old = username;
                            username = chatServer.assignName(streamReader.ReadLine());
                            chatServer.SendMessage(old + " Has changed names to " + username,this);
                            break;
                        case "/Disconnect"://Client asking to disconnect it from server.
                            quit = true;
                            break;
                        case "/MyName"://Client asking what it's name is.
                            streamWriter.WriteLine("/MyName");
                            streamWriter.Flush();
                            streamWriter.WriteLine(username);
                            streamWriter.Flush();
                            break;
                        default://Got a message from this client.
                            Console.WriteLine("Message received: " + incoming);
                            //streamWriter.WriteLine(incoming);
                            incoming = username + ": " + incoming;
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
        /*
         Method Name: write
         Arguments:
            message is the message to send to the client.
         Description:
            Sends a message to the client.
         */
        public void write(String message)
        {
            streamWriter.WriteLine( message);
            streamWriter.Flush();
        }
        /*
         Method Name: purge
         Description:
            Ends the connection to the client.
         */
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
        /*
         Method Name: setName
         Arguments:
            name is the string to set username to.
         Description:
            Sets username to name.
         */
        public void setName(String name)
        {
            username = name;
        }
        /*
         Method Name: getName
         Description:
            Returns username.
         */
        public String getName()
        {
            return username;
        }
        /*
         Method Name: getServerThread
         Description:
            Returns serverThread.
         */
        public Thread getServerThread()
        {
            return serverThread;
        }
    }
}
