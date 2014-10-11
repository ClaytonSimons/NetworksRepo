using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

//start
namespace NetworksLab1Client
{
    public class Start
    {
        public static void Main(String[] args)
        {
            ClientInterface UI = new ClientInterface();
            System.Windows.Forms.Application.Run(UI);
        }
    }
}