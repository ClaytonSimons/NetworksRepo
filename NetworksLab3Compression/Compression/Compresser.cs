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
        abstract public BitArray Compress(byte[] data, String name);
        abstract public byte[] Decompress(BitArray data, String name);
    }
}
