using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net.Sockets;
using System.IO;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Http;
using Compression;

namespace NetworksLab3CompressionServer
{
    /// <summary>
    /// Simple remoting class/server.
    /// </summary>
    public class HTTPS
    {
        static String indexfile = "index.html";
        static String root;
        int port;
        public HTTPS(int thePort)
        {
            HttpChannel channel = new HttpChannel(8085);
            ChannelServices.RegisterChannel(channel);
            RemotingConfiguration.RegisterWellKnownServiceType(typeof(Compression.Compressor), 
                "RemotingServer",WellKnownObjectMode.Singleton);
        }

    }
}
