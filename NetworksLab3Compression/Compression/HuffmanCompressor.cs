using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compression
{
    public class HuffmanCompressor : Compresser
    {

        HuffmanTree tree;
        public HuffmanCompressor()
        {
            tree = new HuffmanTree();
        }
        public override BitArray Compress(byte[] data, String name)
        {
            tree.Build(data);
            return tree.Encode(data);
        }
        public override byte[] Decompress(BitArray data, String name)
        {
            return tree.Decode(data);
        }
    }
}
