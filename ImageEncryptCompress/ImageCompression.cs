using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageEncryptCompress
{
   
    internal class ImageCompression
    {
        static Dictionary<byte, int> Blue = new Dictionary<byte, int>();
        static Dictionary<byte, int> Red = new Dictionary<byte, int>();
        static Dictionary<byte, int> Green = new Dictionary<byte, int>();
        static Dictionary<byte, string> EncodingBlue = new Dictionary<byte, string>();
        static Dictionary<byte, string> EncodingRed = new Dictionary<byte, string>();
        static Dictionary<byte, string> EncodingGreen = new Dictionary<byte, string>();

        static string CompressedBlue = "";
        static string CompressedRed = "";
        static string CompressedGreen = ""; 

        internal class Node : IComparable
        {
            public Node left;
            public Node right;
            public int value;
            public int frequency;

            public Node(int value, int frequency)
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

        private static void CompressValues(Dictionary<byte, string> Encoding, Node root, string currCode)
        {
            if (root == null)
                return;
            if (root.left != null)
                CompressValues(Encoding, root.left, currCode + '0');
            if (root.right != null)
                CompressValues(Encoding, root.right, currCode + '1');
            if (root.left == null && root.right == null)
            {
                if (currCode.Length == 0)
                    Encoding.Add((byte)root.value, "0");
                Encoding.Add((byte)root.value, currCode);
            }
        }

        private static void HuffmanEncode(RGBPixel[,] Image)
        {
            PriorityQueue<Node> pqBlue = new PriorityQueue<Node>();
            foreach (KeyValuePair<byte, int> item in Blue)
            {
                Node node = new Node(item.Key, item.Value);
                pqBlue.Enqueue(node);
            };

            for (int i = 0; i < Blue.Count - 1; i++)
            {
                Node minim = pqBlue.Dequeue();
                Node secondMinim = pqBlue.Dequeue();

                Node node = new Node(-1, minim.frequency + secondMinim.frequency);
                node.right = minim;
                node.left = secondMinim;

                pqBlue.Enqueue(node);
            }
            Node rootB = pqBlue.Dequeue();
            CompressValues(EncodingBlue, rootB, "");
            CompressedBlue = ReplaceBinaryCode(Image, EncodingBlue, (pixel) => pixel.blue);

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
            CompressedRed = ReplaceBinaryCode(Image, EncodingRed, (pixel) => pixel.red);

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
            CompressedGreen = ReplaceBinaryCode(Image, EncodingGreen, (pixel) => pixel.green);
        }

        private static string ReplaceBinaryCode(RGBPixel[,] Image, Dictionary<byte, string> Encoding, Func<RGBPixel, byte> colorSelector)
        {
            int rows = Image.GetLength(0);
            int cols = Image.GetLength(1);
            StringBuilder binaryCode = new StringBuilder();

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    byte color = colorSelector(Image[i, j]);
                    if(Encoding.ContainsKey(color))
                    {
                        binaryCode.Append(Encoding[color]);
                    }
                }
            }
           return binaryCode.ToString();
        }

        static byte[] ConvertToByte(string Binarycode)
        {
            int numOfBads = 8 - Binarycode.Length % 8;

            if(numOfBads != 8)
            {
                string BadedBits = new string('0', numOfBads);
                Binarycode += BadedBits;            // Originallength-BadedBits-1
            }

            int size = Binarycode.Length / 8;
            byte[] arr = new byte[size];

            for (int a = 0; a < size; a++)
            {
                arr[a] = Convert.ToByte(Binarycode.Substring(a * 8, 8), 2);
            }

            return arr;
        }

        static void SaveImageIntoFile()
        {
           byte[] redBytes = ConvertToByte(CompressedRed);
           byte[] GreenBytes = ConvertToByte(CompressedGreen);
           byte[] blueBytes = ConvertToByte(CompressedBlue);
           throw new NotImplementedException();
        }

        public static void Compress(RGBPixel[,] Image)
        {
            CalcFrequency(Image);
            HuffmanEncode(Image);
            SaveImageIntoFile();
        }
        public static RGBPixel[,] Decompress(RGBPixel[,] Image)
        {
            throw new NotImplementedException();
        }
    }
}
