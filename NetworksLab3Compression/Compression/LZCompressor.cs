using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Compression
{
    class LZCompressor : Compresser
    {
        public override List<int> Compress(List<byte> source)
        {
            Dictionary<List<byte>, int> dictionary = new Dictionary<List<byte>, int>();
            for (int i = 0; i < 256; i++)
            {
                List<byte> temp = new List<byte>();
                temp.Add((byte)i);
                dictionary.Add(temp, i);
            }
            List<byte> w = new List<byte>();
            List<int> comp = new List<int>();
            foreach(byte c in source)
            {
                List<byte> wc = w;
                wc.Add(c);
                if(dictionary.ContainsKey(wc))
                {
                    w = wc;
                }
                else
                {
                    comp.Add((byte)(dictionary[w]));
                    dictionary.Add(wc, dictionary.Count);
                    w.Clear();
                    w.Add(c);
                }
            }
            if (w.Count > 0)
                comp.Add((dictionary[w]));
            return comp;
        }
        public override List<byte> Decompress(List<int> comp)
        {
            Dictionary<int, List<byte>> dictionary = new Dictionary<int, List<byte>>();
            for(int i=0; i<256;i++)
            {
                List<byte> temp = new List<byte>();
                temp.Add((byte)i);
                dictionary.Add(i, temp);
            }
            List<byte> w = dictionary[comp[0]];
            comp.RemoveAt(0);
            List<byte> decompressed = new List<byte>(w);

            foreach(int k in comp)
            {
                List<byte> entry = null;
                if (dictionary.ContainsKey(k))
                    entry = dictionary[k];
                else if (k == dictionary.Count)
                {
                    entry = new List<byte>(w);
                    entry.Add(w[0]);
                }

                decompressed.AddRange(entry);
                List<byte> temp = new List<byte>(w);
                temp.Add(entry[0]);
                dictionary.Add(dictionary.Count, temp);
                w = entry;
            }
            return decompressed;
        }
        public override System.Collections.BitArray Compress(byte[] data)
        {
            throw new NotImplementedException();
        }
        public override byte[] Decompress(System.Collections.BitArray data)
        {
            throw new NotImplementedException();
        }
    }
}
