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
        /// <summary>
        /// Tree for the huffman algorithm
        /// </summary>
        HuffmanTree tree;
        /// <summary>
        /// Constructor initializes tree.
        /// </summary>
        public HuffmanCompressor()
        {
            tree = new HuffmanTree();
        }
        /// <summary>
        /// Compresses the data sent in.
        /// </summary>
        /// <param name="data"></param>
        /// The data.
        /// <returns></returns>
        public override BitArray Compress(byte[] data)
        {
            tree.Build(data);
            return tree.Encode(data);
        }
        /// <summary>
        /// Decompresses the data sent in.
        /// </summary>
        /// <param name="data"></param>
        /// The data.
        /// <returns></returns>
        public override byte[] Decompress(BitArray data)
        {
            return tree.Decode(data);
        }
    }
}
