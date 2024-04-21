using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageEncryptCompress
{
   
    internal class ImageCompression
    {
        static Dictionary<byte, int> Blue= new Dictionary<byte, int>();
        static Dictionary<byte, int> Red = new Dictionary<byte, int>();
        static Dictionary<byte, int> Green = new Dictionary<byte, int>();
        static Dictionary<byte, string> EncodingBlue = new Dictionary<byte, string>();
        static Dictionary<byte, string> EncodingRed = new Dictionary<byte, string>();
        static Dictionary<byte, string> EncodingGreen = new Dictionary<byte, string>();

        internal class Node : IComparable
        {
            public Node left;
            public Node right;
            public int value;
            public int frequency;

            public Node(int value,int frequency)
            {
                this.frequency = frequency;
                left = right = null;
                this.value = value;
            }
            public Node(int frequency)
            {
                this.frequency = frequency;
                left = right = null;
            }

            public int CompareTo(object obj)
            {
                Node other = obj as Node;
                if (other == null)
                {
                    throw new NullReferenceException();
                }
                return frequency.CompareTo(other.frequency);
            }
        }

        public static void CalcFrequency(RGBPixel[,] Image)
        {
            Blue.Clear();
            Red.Clear();
            Green.Clear();
            int row = Image.GetLength(0);
            int col = Image.GetLength(1);
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    if (!Blue.ContainsKey(Image[i, j].blue))
                    {
                        Blue.Add(Image[i, j].blue, 1);
                    }
                    else
                    {
                        Blue[Image[i, j].blue]++;
                    }
                    if (!Red.ContainsKey(Image[i, j].red))
                    {
                        Red.Add(Image[i, j].red, 1);
                    }
                    else
                    {
                        Red[Image[i, j].red]++;
                    }
                    if (!Green.ContainsKey(Image[i, j].green))
                    {
                        Green.Add(Image[i, j].green, 1);
                    }
                    else
                    {
                        Green[Image[i, j].green]++;
                    }
                }
            }
        }

        private static void CompressValues(Dictionary<byte,string> Encoding,Node root,string currCode)
        {
            if (root == null)
                return;
            if (root.left != null)
                CompressValues(Encoding,root.left, currCode + '0');
            if (root.right != null)
                CompressValues(Encoding, root.right, currCode + '1');
            if(root.left == null && root.right == null)
            {
                if (currCode.Length == 0)
                    Encoding.Add((byte)root.value, "0");
                Encoding.Add((byte)root.value, currCode);
            }
        }

        private static void HuffmanEncode()
        {
            PriorityQueue<Node> pqBlue = new PriorityQueue<Node>();
            foreach (KeyValuePair<byte,int> item in Blue)
            {
                Node node = new Node(item.Key,item.Value);
                pqBlue.Enqueue(node);
            };

            for(int i = 0; i < Blue.Count - 1; i++)
            {
                Node minim = pqBlue.Dequeue();
                Node secondMinim = pqBlue.Dequeue();

                Node node = new Node(-1,minim.frequency + secondMinim.frequency);
                node.right = minim;
                node.left = secondMinim;

                pqBlue.Enqueue(node);
            }
            Node rootB = pqBlue.Dequeue();
            CompressValues(EncodingBlue,rootB,"");


            PriorityQueue<Node> pqRed = new PriorityQueue<Node>();
            foreach (KeyValuePair<byte, int> item in Red)
            {
                Node node = new Node(item.Key, item.Value);
                pqRed.Enqueue(node);
            };

            for (int i = 0; i < Red.Count - 1; i++)
            {
                Node minim = pqRed.Dequeue();
                Node secondMinim = pqRed.Dequeue();

                Node node = new Node(-1, minim.frequency + secondMinim.frequency);
                node.right = minim;
                node.left = secondMinim;

                pqRed.Enqueue(node);
            }
            Node rootR = pqRed.Dequeue();
            CompressValues(EncodingRed, rootR, "");


            PriorityQueue<Node> pqGreen = new PriorityQueue<Node>();
            foreach (KeyValuePair<byte, int> item in Green)
            {
                Node node = new Node(item.Key, item.Value);
                pqGreen.Enqueue(node);
            };

            for (int i = 0; i < Green.Count - 1; i++)
            {
                Node minim = pqGreen.Dequeue();
                Node secondMinim = pqGreen.Dequeue();

                Node node = new Node(-1, minim.frequency + secondMinim.frequency);
                node.right = minim;
                node.left = secondMinim;

                pqGreen.Enqueue(node);
            }
            Node rootG = pqGreen.Dequeue();
            CompressValues(EncodingGreen, rootG, "");


        }
        public static RGBPixel[,] Compress(RGBPixel[,] Image)
        {
           
            CalcFrequency(Image);
            HuffmanEncode();

            throw new NotImplementedException();
        }
        public static RGBPixel[,] Decompress(RGBPixel[,] Image)
        {
            throw new NotImplementedException();
        }
    }
}
