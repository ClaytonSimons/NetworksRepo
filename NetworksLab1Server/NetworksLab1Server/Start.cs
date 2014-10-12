using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

/*
 Program Name: NetworksLab1Server
 Assignment:Lab1
 Class: Networks
 Author: Clayton Simons
 Last Modified: 10/12/14
 Description:
 This is the server for lab1 of Networks class. It facilitates communication between multiple clients.
 */
namespace NetworksLab1Server
{
    /*
     Class Name: Start
     Description:
        Start point for the program.  It instantiates a ChatServer and runs it.
     */
    class Start
    {
        /*
         Method Name: Main
         Arguments:
            args is any command line arguments.
         Description:
            The start point for the program.
         */
        public static void Main(String[] args)
        {
            ChatServer server = new ChatServer();
            server.start();
        }
    }
}
