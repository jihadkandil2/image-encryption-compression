using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageEncryptCompress
{
   
    internal class ImageCompression
    {
        static Dictionary<byte, int> BlueFrequency = new Dictionary<byte, int>();
        static Dictionary<byte, int> RedFrequency = new Dictionary<byte, int>();
        static Dictionary<byte, int> GreenFrequency = new Dictionary<byte, int>();
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
            BlueFrequency.Clear();
            RedFrequency.Clear();
            GreenFrequency.Clear();
            int row = Image.GetLength(0);
            int col = Image.GetLength(1);
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    if (!BlueFrequency.ContainsKey(Image[i, j].blue))
                    {
                        BlueFrequency.Add(Image[i, j].blue, 1);
                    }
                    else
                    {
                        BlueFrequency[Image[i, j].blue]++;
                    }
                    if (!RedFrequency.ContainsKey(Image[i, j].red))
                    {
                        RedFrequency.Add(Image[i, j].red, 1);
                    }
                    else
                    {
                        RedFrequency[Image[i, j].red]++;
                    }
                    if (!GreenFrequency.ContainsKey(Image[i, j].green))
                    {
                        GreenFrequency.Add(Image[i, j].green, 1);
                    }
                    else
                    {
                        GreenFrequency[Image[i, j].green]++;
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

        private static void BuildTree(RGBPixel[,] Image,Dictionary<byte, string> BytesEncoding, Dictionary<byte, int> ColorFequency)
        {
            PriorityQueue<Node> priorityQueue = new PriorityQueue<Node>();
            foreach (KeyValuePair<byte, int> item in ColorFequency)
            {
                Node node = new Node(item.Key, item.Value);
                priorityQueue.Enqueue(node);
            }
            for (int i = 0; i < ColorFequency.Count - 1; i++)
            {
                Node minim = priorityQueue.Dequeue();
                Node secondMinim = priorityQueue.Dequeue();

                Node parent = new Node(-1, minim.frequency + secondMinim.frequency);
                parent.right = minim;
                parent.left = secondMinim;

                priorityQueue.Enqueue(parent);

            }

            Node root = priorityQueue.Dequeue();
            CompressValues(BytesEncoding, root, "");
        }

        private static void HuffmanEncode(RGBPixel[,] Image)
        {
           

            BuildTree(Image, EncodingRed, RedFrequency);
            CompressedRed = ReplaceBinaryCode(Image, EncodingRed, (pixel) => pixel.red);

            BuildTree(Image, EncodingGreen, GreenFrequency);
            CompressedGreen = ReplaceBinaryCode(Image, EncodingGreen, (pixel) => pixel.green);

            BuildTree(Image, EncodingBlue, BlueFrequency);
            CompressedBlue = ReplaceBinaryCode(Image, EncodingBlue, (pixel) => pixel.blue);
            
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
