using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
/*
 Assignment Name: Lab2
 * Author: Clay Simons
 * Last Modified: 10/26/14
 * Description:
 *      This is a client for a ftp server.  It supports creating text files, retrieving text files, editing text files, and deleting text files on an ftp server.
 */
namespace NetworksLab2FTPClient
{
    static class FTPClient
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FtpClientUI());
        }

    }
}
