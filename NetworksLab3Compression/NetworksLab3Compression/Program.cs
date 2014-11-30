using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
/*
 *Project Name: Networks Lab3 Compression
 *Author: Clay Simons
 *Last Modified: 11/28/14
 * Description:
 *  This program will compress and decompress files using a huffman code algorithm.
 * It uses a webservice and remoting class in order to compress and decompress.
 * 
 */
namespace NetworksLab3Compression
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new ClientForm());
        }
    }
}
