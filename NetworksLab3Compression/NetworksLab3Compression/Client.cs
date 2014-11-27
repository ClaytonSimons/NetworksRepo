using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Http;

namespace NetworksLab3Compression
{
    class Client
    {
        String serverAddress;
        int port;
        public Client(String address,int thePort)
        {
            serverAddress = address;
            port = thePort;
        }
        public void AssignHuffman()
        {
        }
    }
}
