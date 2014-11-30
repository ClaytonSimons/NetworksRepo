using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compression
{
    /// <summary>
    /// Abstract compression class.
    /// </summary>
    public abstract class Compresser
    {
        abstract public BitArray Compress(byte[] data);
        abstract public byte[] Decompress(BitArray data);
    }
}
