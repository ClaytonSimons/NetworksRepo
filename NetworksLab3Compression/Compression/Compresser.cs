using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compression
{
    public abstract class Compresser
    {
        abstract public BitArray Compress(byte[] data);
        abstract public byte[] Decompress(BitArray data);
        abstract public List<int> Compress(List<byte> source);
        abstract public List<byte> Decompress(List<int> comp);
    }
}
