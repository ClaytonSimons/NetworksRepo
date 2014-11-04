using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NetworksLab2FTPClient
{
    public partial class FileCreation : Form
    {
        FtpClientUI parent;
        /*
         * Method Name: FileCreation
         * Arguments:
         *      title is the title of the file to be created.
         *      p is the parent object that instantiated this form.
         * Description: 
         *      constructor for the form.  Initializes parent and titleTxt.
         */
        public FileCreation(String title, FtpClientUI p)
        {
            InitializeComponent();
            parent = p;
            titleTxt.Text = title;
        }
        //sets contentRTxt
        public void setContent(String content)
        {
            contentRTxt.Text = content;
        }
        /*
         * Method Name: sendBtn_Click
         * Description:
         *      Sends file to the parent object and closes this form.
         */
        private void sendBtn_Click(object sender, EventArgs e)
        {
            parent.setFile(contentRTxt.Text);
            this.Close();
        }
    }
}
