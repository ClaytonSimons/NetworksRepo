using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworksLab3CompressionServer
{
    class Program
    {
        static void Main(string[] args)
        {
            int port;
            String root;
            Console.Write("Enter root directory");
            root = Console.ReadLine();
            try
            {
                Console.Write("Enter port");
                port = Int32.Parse(Console.ReadLine());
                if (port < 0 || port > 65535)
                    port = 80;
            }
            catch (Exception e)
            {
                port = 80;
            }
            HTTPS server = new HTTPS(root,port);
        }
    }
}
