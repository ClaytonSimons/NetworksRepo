using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworksLab3CompressionServer
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            HTTPS server = new HTTPS(8085);
            Console.WriteLine("To end press enter");
            Console.ReadLine();
        }
    }
}
