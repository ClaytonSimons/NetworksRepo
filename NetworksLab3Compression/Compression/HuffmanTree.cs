using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Runtime.InteropServices;
using System.IO;

namespace Compression
{
    public class HuffmanTree
    {
        private List<HuffNode> nodes = new List<HuffNode>();
        public HuffNode Root { get; set; }
        public Dictionary<byte, int> Frequencies = null;
        public void Build()
        {
            foreach (KeyValuePair<byte, int> symbol in Frequencies)
            {
                nodes.Add(new HuffNode()
                {
                    Symbol = symbol.Key,
                    Frequency = symbol.Value
                });
            }

            while (nodes.Count > 1)
            {
                List<HuffNode> orderedNodes = nodes.OrderBy(node => node.Frequency).ToList<HuffNode>();

                if (orderedNodes.Count >= 2)
                {
                    // Take first two items
                    List<HuffNode> taken = orderedNodes.Take(2).ToList<HuffNode>();

                    // Create a parent node by combining the frequencies
                    HuffNode parent = new HuffNode()
                    {
                        Symbol = (byte)'*',
                        Frequency = taken[0].Frequency + taken[1].Frequency,
                        Left = taken[0],
                        Right = taken[1]
                    };

                    nodes.Remove(taken[0]);
                    nodes.Remove(taken[1]);
                    nodes.Add(parent);
                }

                this.Root = nodes.FirstOrDefault();

            }
        }
        public void Build(byte[] source)
        {
            Frequencies = new Dictionary<byte, int>();
            for (int i = 0; i < source.Length; i++)
            {
                if (!Frequencies.ContainsKey(source[i]))
                {
                    //table.Add(source[i], 0);
                    Frequencies.Add(source[i], 0);
                    //ary.Add(new KeyValuePair<byte, int>(source[i],0));
                }
                Frequencies[source[i]]++;
                //int temp = ary.ElementAt(i).Value;
                //ary.RemoveAt(i);
                //ary.Insert(i, new KeyValuePair<byte, int>(source[i],++temp));
                //int temp = (int)table[source[i]];
                //table.Remove(source[i]);
                //table.Add(source[i], +temp);
            }
            
            foreach (KeyValuePair<byte, int> symbol in Frequencies)
            {
                nodes.Add(new HuffNode() 
                { 
                    Symbol = symbol.Key, Frequency = symbol.Value 
                });
            }

            while (nodes.Count > 1)
            {
                List<HuffNode> orderedNodes = nodes.OrderBy(node => node.Frequency).ToList<HuffNode>();

                if (orderedNodes.Count >= 2)
                {
                    // Take first two items
                    List<HuffNode> taken = orderedNodes.Take(2).ToList<HuffNode>();

                    // Create a parent node by combining the frequencies
                    HuffNode parent = new HuffNode()
                    {
                        Symbol = (byte)'*',
                        Frequency = taken[0].Frequency + taken[1].Frequency,
                        Left = taken[0],
                        Right = taken[1]
                    };

                    nodes.Remove(taken[0]);
                    nodes.Remove(taken[1]);
                    nodes.Add(parent);
                }

                this.Root = nodes.FirstOrDefault();

            }

        }

        public BitArray Encode(byte[] source)
        {
            List<bool> encodedSource = new List<bool>();

            for (int i = 0; i < source.Length; i++)
            {
                List<bool> encodedSymbol = this.Root.Traverse(source[i], new List<bool>());
                encodedSource.AddRange(encodedSymbol);
            }

            BitArray bits = new BitArray(encodedSource.ToArray());

            MemoryStream ms = new MemoryStream();
            BinaryWriter bw = new BinaryWriter(ms);
            foreach (KeyValuePair<byte, int> kvp in Frequencies)
            {
                KVP<byte, int> temp = new KVP<byte, int>();
                temp.Key = kvp.Key;
                temp.Value = kvp.Value;
                bw.Write(temp.Key);
                bw.Write(temp.Value);
                //bs.Serialize(ms, temp);
            }
            BitArray freq = new BitArray(ms.ToArray());
            int[] value = {Frequencies.Count};
            BitArray freqsize = new BitArray(value);
            bits = Prepend(bits, freq);
            bits = Prepend(bits, freqsize);

            return bits;//*****************************************************
        }

        public byte[] Decode(BitArray bits)
        {
            int[] size = { 0 };
            ExtractFront(bits, sizeof(int) * 8).CopyTo(size, 0);
            bits = ExtractEnd(bits, bits.Length - sizeof(int) * 8);//bits is bits - size
            MemoryStream ms = new MemoryStream();
            Frequencies = new Dictionary<byte, int>();
            for(int i=0;i<size[0];i++)
            {
                BitArray freq = ExtractFront(bits, sizeof(byte)*8);//
                byte[] key = {0};
                freq.CopyTo(key, 0);
                bits = ExtractEnd(bits, bits.Length - sizeof(byte) * 8);
                freq = ExtractFront(bits, sizeof(int) * 8);
                int[] value = { 0 };
                freq.CopyTo(value, 0);
                bits = ExtractEnd(bits, bits.Length - sizeof(int) * 8);
                Frequencies.Add(key[0], value[0]);

            }

            Build();
            //Frequencies = (Dictionary<byte, int>)bs.Deserialize(ms);
            HuffNode current = this.Root;
            List<byte> decoded = new List<byte>();

            foreach (bool bit in bits)
            {
                if (bit)
                {
                    if (current.Right != null)
                    {
                        current = current.Right;
                    }
                }
                else
                {
                    if (current.Left != null)
                    {
                        current = current.Left;
                    }
                }

                if (IsLeaf(current))
                {
                    decoded.Add(current.Symbol);
                    current = this.Root;
                }
            }

            return decoded.ToArray();
        }

        public bool IsLeaf(HuffNode node)
        {
            return (node.Left == null && node.Right == null);
        }
        public BitArray Prepend(BitArray current, BitArray before)
        {
            var bools = new bool[current.Count + before.Count];
            before.CopyTo(bools, 0);
            current.CopyTo(bools, before.Count);
            return new BitArray(bools);
        }

        public BitArray Append(BitArray current, BitArray after)
        {
            var bools = new bool[current.Count + after.Count];
            current.CopyTo(bools, 0);
            after.CopyTo(bools, current.Count);
            return new BitArray(bools);
        }
        public BitArray ExtractFront(BitArray target, int size)
        {
            bool[] temp = new bool[target.Length];
            target.CopyTo(temp, 0);
            bool[] bools = new bool[size];
            for(int i=0;i<size;i++)
                bools[i] = temp[i];
            return new BitArray(bools);
        }
        public BitArray ExtractEnd(BitArray target, int size)
        {
            bool[] temp = new bool[target.Length];
            target.CopyTo(temp, 0);
            bool[] bools = new bool[size];
            int diff = target.Length - size;
            for (int i = diff; i < temp.Length; i++)
                bools[i - diff] = temp[i];
            return new BitArray(bools);
        }
    }
    [Serializable]
    public struct KVP<K,V>
    {
        public K Key {get;set;}
        public V Value{get;set;}
    }
}
