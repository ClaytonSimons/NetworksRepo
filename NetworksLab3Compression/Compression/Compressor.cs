using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compression
{
    public class Compressor : System.MarshalByRefObject
    {
        Compresser comp;
        public Compressor()
        {
            comp = null;
        }
        public bool setHuff()
        {
            bool answer = false;
            if (comp == null || comp.GetType() != typeof(HuffmanCompressor))
            {
                comp = new HuffmanCompressor();
            }
            if (comp != null)
                answer = true;
            return answer;
        }
        public BitArray Compress(byte[] data)
        {
            BitArray answer = null;
            if(comp != null)
                answer = comp.Compress(data);
            return answer;
        }
        public byte[] Decompress(BitArray data)
        {
            byte[] answer = null;
            if(comp != null)
                answer = comp.Decompress(data);
            return answer;
        }
        public List<int> Compress(List<byte> source)
        {
            List<int> answer = null;
            if (comp != null)
                answer = comp.Compress(source);
            return answer;
        }
    }
}
