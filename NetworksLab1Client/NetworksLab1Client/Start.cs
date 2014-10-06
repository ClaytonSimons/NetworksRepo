using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworksLab1Client
{
    public class Start
    {
        public static void Main(String[] args)
        {
            ChatClient client = new ChatClient("SCUTULATUS");
            client.start();
        }
    }
}