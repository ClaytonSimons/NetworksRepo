using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace NetworksLab3Compression
{
    public partial class ClientForm : Form
    {
        Compression.Compressor comp;
        public ClientForm()
        {
            InitializeComponent();
        }

        private void compBtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog FD = new OpenFileDialog();
            DialogResult result = FD.ShowDialog();
            if (result == DialogResult.OK)
            {
                string file = FD.FileName;
                try
                {
                    byte[] data = File.ReadAllBytes(file);
                    byte[] resultdata = null;
                    String compressedName = file + ".comp";
                    List<byte> ne = new List<byte>();
                    if (setCompressor())
                    {
                        BitArray arry = comp.Compress(data, file);
                        resultdata = new byte[(arry.Length - 1) / 8 + 1];
                        arry.CopyTo(resultdata, 0);
                        File.WriteAllBytes(compressedName, resultdata);
                    }
                    

                    //serverAddressTxt.Text = client.test();
                    //string text = File.ReadAllText(file);
                    //size = text.Length;
                }
                catch (IOException)
                {

                }

            }
        }
        private void decompBtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog FD = new OpenFileDialog();
            DialogResult result = FD.ShowDialog();
            if (result == DialogResult.OK)
            {
                string file = FD.FileName;
                try
                {
                    byte[] data = File.ReadAllBytes(file);
                    byte[] resultdata = null;

                    String decompressedName = file;
                    if(decompressedName.Contains(".comp"))
                    {
                        decompressedName = decompressedName.Remove(file.Length - 5, 5);
                    }
                    decompressedName = String.Concat(decompressedName,".decomp");
                    List<byte> ne = new List<byte>();/////////************************************
                    if (setCompressor())
                    {
                        BitArray dat = new BitArray(data);
                        resultdata = comp.Decompress(dat, file);
                        File.WriteAllBytes(decompressedName, resultdata);
                    }


                    //serverAddressTxt.Text = client.test();
                    //string text = File.ReadAllText(file);
                    //size = text.Length;
                }
                catch (IOException)
                {

                }

            }
        }
        private bool setCompressor()
        {
            bool answer = false;
            if (comp == null)
                comp = (Compression.Compressor)Activator.GetObject(typeof(Compression.Compressor), "http://" + serverAddressTxt.Text + ":8085/RemotingServer");
                //comp = new Compression.Compressor();
            if (huffmanRadBtn.Checked)
            {
                bool response = comp.setHuff();
                answer = true;
            }
            else if (lzRadBtn.Checked)
            {
                comp.setHuff();
                answer = true;
            }
            else if (runLengthRadBtn.Checked)
            {
                comp.setHuff();
                answer = true;
            }
            else if (arithmeticRadBtn.Checked)
            {
                comp.setHuff();
                answer = true;
            }
            return answer;
        }
        private bool unsetCompressor()
        {
            bool answer = false;
            comp = null;
            answer = true;
            return answer;
        }


    }
}
