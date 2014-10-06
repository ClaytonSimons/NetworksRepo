using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace NetworksLab1Server
{
    class Start
    {
        public static void Main(String[] args)
        {
            ChatServer server = new ChatServer();
            server.start();
        }
    }
}
