using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

/*
Program Name: NetworksLab1Client
Assignment:Lab1
Class: Networks
Author: Clayton Simons
Last Modified: 10/12/14
Description:
    This is the client for the Networks lab1. The job of this program is to communicate
with the server, display received messages, change username for the user, connect, and
disconnect with the server.
*/
namespace NetworksLab1Client
{
    /*
    Class Name: Start
    Description:
        The starting point for the program.  It starts the user interface.
    */
    public class Start
    {
        public static void Main(String[] args)
        {
            ClientInterface UI = new ClientInterface();
            System.Windows.Forms.Application.Run(UI);
        }
    }
}