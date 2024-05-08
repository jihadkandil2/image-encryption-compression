using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Odbc;
using System.IO;
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
        static Dictionary<byte, string> CompressionEncodingBlue = new Dictionary<byte, string>();
        static Dictionary<byte, string> CompressionEncodingRed = new Dictionary<byte, string>();
        static Dictionary<byte, string> CompressionEncodingGreen = new Dictionary<byte, string>();

        static Dictionary<string, byte> DecompressionRedEncoding = new Dictionary<string, byte>();
        static Dictionary<string, byte> DecompressionGreenEncoding = new Dictionary<string, byte>();
        static Dictionary<string, byte> DecompressionBlueEncoding = new Dictionary<string, byte>();

        static Node redRoot, greenRoot, blueRoot;
        static int RowSize, ColSize;
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

        private static void SaveTreeIntoFile(BinaryWriter binaryWriter, Node root, Dictionary<byte, string> CompressionEncoding, StringBuilder currCode)
        {
            if (root == null)
                return;
            currCode.Append('0');
            int currIndex = currCode.Length-1;
            SaveTreeIntoFile(binaryWriter, root.left, CompressionEncoding, currCode);
            currCode.Remove(currIndex,currCode.Length - currIndex);
            currCode.Append('1');
            currIndex = currCode.Length - 1;
            SaveTreeIntoFile(binaryWriter, root.right, CompressionEncoding, currCode);
            currCode.Remove(currIndex, currCode.Length - currIndex);
            if (root.left == null && root.right == null)
            {

                List<byte> currCodeBytes = new List<byte>();
                string toBeAddedCode = currCode.ToString();
                int pads = 8 - currCode.Length % 8;
                if (pads != 8)
                {
                    StringBuilder temp = new StringBuilder();
                    temp.Append('0', pads);
                    currCode.Append(temp);
                }
                for (int i = 0; i < currCode.Length / 8; i++)
                {
                    currCodeBytes.Add(Convert.ToByte(currCode.ToString().Substring(i * 8, 8), 2));
                }
                binaryWriter.Write((byte)pads);
                binaryWriter.Write((byte)currCodeBytes.Count);//to be optimized
                binaryWriter.Write(currCodeBytes.ToArray());
                binaryWriter.Write((byte)root.value);
                CompressionEncoding.Add((byte)root.value, toBeAddedCode);
            }

        }
        private static void BuildEncodingFromTree(BinaryReader binaryReader, Dictionary<string, byte> DecompressionEncoding)
        {
            int leavesNumber = binaryReader.ReadInt32();
            for (int i = 0; i < leavesNumber; i++)
            {
                int pads = Convert.ToInt32(binaryReader.ReadByte());
                int bytesNumber = Convert.ToInt32(binaryReader.ReadByte());
                byte[] bytes = binaryReader.ReadBytes(bytesNumber);
                StringBuilder str = new StringBuilder();
                foreach (byte b in bytes)
                {
                    str.Append(Convert.ToString(b, 2).PadLeft(8, '0'));
                }
                string code = str.ToString();
                byte symbol = binaryReader.ReadByte();
                if (pads != 8)
                    DecompressionEncoding.Add(code.Substring(0, code.Length - pads), symbol);
                else
                    DecompressionEncoding.Add(code, symbol);

            }
        }

        private static Node BuildTree(Dictionary<byte, int> ColorFequency)
        {
            PriorityQueue<Node> priorityQueue = new PriorityQueue<Node>();
            Node node;
            foreach (KeyValuePair<byte, int> item in ColorFequency)
            {
                node = new Node(item.Key, item.Value);
                priorityQueue.Enqueue(node);
            }
            Node minim, secondMinim,parent;
            while (priorityQueue.Count > 1)
            {
                minim = priorityQueue.Dequeue();
                secondMinim = priorityQueue.Dequeue();

                parent = new Node(-1, minim.frequency + secondMinim.frequency);
                parent.left = minim;
                parent.right = secondMinim;

                priorityQueue.Enqueue(parent);

            }

            return priorityQueue.Dequeue();
        }

        private static void HuffmanEncode()
        {
            redRoot = BuildTree(RedFrequency);
            greenRoot = BuildTree(GreenFrequency);
            blueRoot = BuildTree(BlueFrequency);
        }

        private static string ReplaceBinaryCode(RGBPixel[,] Image, Dictionary<byte, string> Encoding, Func<RGBPixel, byte> colorSelector)
        {

            int rows = Image.GetLength(0);
            int cols = Image.GetLength(1);
            StringBuilder binaryCode = new StringBuilder();
            int ctr = 0;

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    byte color = colorSelector(Image[i, j]);
                    if (Encoding.ContainsKey(color))
                    {
                        ctr += Encoding[color].Length;
                        binaryCode.Append(Encoding[color]);
                    }
                }
            }
            Console.WriteLine(ctr);
            return binaryCode.ToString();
        }

        static string GetImageComponentBytes(RGBPixel[,] Image, char Color)
        {
            Dictionary<byte, string> CompressionEncoding;
            Func<RGBPixel, byte> colorSelector;

            if (Color.Equals('r'))
            {
                CompressionEncoding = CompressionEncodingRed;
                colorSelector = (pixel) => pixel.red;
            }
            else if (Color.Equals('g'))
            {
                CompressionEncoding = CompressionEncodingGreen;
                colorSelector = (pixel) => pixel.green;
            }
            else if (Color.Equals('b'))
            {
                CompressionEncoding = CompressionEncodingBlue;
                colorSelector = (pixel) => pixel.blue;
            }
            else
                throw new Exception("Unavailable color!");

            return ReplaceBinaryCode(Image, CompressionEncoding, colorSelector);
        }

        static int GetNumberOfLeaves(Node node)
        {
            if (node == null)
                return 0;
            int leaves = 0;
            leaves += GetNumberOfLeaves(node.left);
            leaves += GetNumberOfLeaves(node.right);
            if (node.left == null && node.right == null)
                return leaves + 1;
            return leaves;
        }
        

        static double SaveImageIntoFile(RGBPixel[,] Image, int RowSize, int ColSize, string filePath,string initalSeed,int tapPosition)
        {
            long totalAfterCompressionInBytes = 0;
            using (BinaryWriter binaryWriter = new BinaryWriter(new FileStream(filePath, FileMode.Create), Encoding.UTF8))
            {
                binaryWriter.Write(initalSeed);
                binaryWriter.Write(tapPosition);
                binaryWriter.Write(RowSize);
                binaryWriter.Write(ColSize);
                int NumOfRedLeaves = GetNumberOfLeaves(redRoot);
                binaryWriter.Write(NumOfRedLeaves);
                StringBuilder currCode = new StringBuilder();
                SaveTreeIntoFile(binaryWriter, redRoot, CompressionEncodingRed, currCode);
                currCode.Clear();
                int NumOfGreenLeaves = GetNumberOfLeaves(greenRoot);
                binaryWriter.Write(NumOfGreenLeaves);
                SaveTreeIntoFile(binaryWriter, greenRoot, CompressionEncodingGreen, currCode);
                currCode.Clear();
                int NumOfBlueLeaves = GetNumberOfLeaves(blueRoot);
                binaryWriter.Write(NumOfBlueLeaves);
                currCode.Clear();
                SaveTreeIntoFile(binaryWriter, blueRoot, CompressionEncodingBlue, currCode);

                string RedBits = GetImageComponentBytes(Image, 'r');
                List<byte> RcurrCodeBytes = new List<byte>();
                int RPads = 8 - RedBits.Length % 8;
                if (RPads != 8)
                {
                    StringBuilder temp = new StringBuilder();
                    temp.Append('0', RPads);
                    RedBits += temp.ToString();
                }
                for (int i = 0; i < RedBits.Length / 8; i++)
                {
                    RcurrCodeBytes.Add(Convert.ToByte(RedBits.Substring(i * 8, 8), 2));
                }
                binaryWriter.Write((byte)RPads);
                binaryWriter.Write(RcurrCodeBytes.Count);
                binaryWriter.Write(RcurrCodeBytes.ToArray());


                string GreenBits = GetImageComponentBytes(Image, 'g');
                List<byte> GcurrCodeBytes = new List<byte>();
                int GPads = 8 - GreenBits.Length % 8;
                if (GPads != 8)
                {
                    StringBuilder temp = new StringBuilder();
                    temp.Append('0', GPads);
                    GreenBits += temp.ToString();
                }
                for (int i = 0; i < GreenBits.Length / 8; i++)
                {
                    GcurrCodeBytes.Add(Convert.ToByte(GreenBits.Substring(i * 8, 8), 2));
                }
                binaryWriter.Write((byte)GPads);
                binaryWriter.Write(GcurrCodeBytes.Count);
                binaryWriter.Write(GcurrCodeBytes.ToArray());


                string BlueBits = GetImageComponentBytes(Image, 'b');
                List<byte> BcurrCodeBytes = new List<byte>();
                int BPads = 8 - BlueBits.Length % 8;
                if (BPads != 8)
                {
                    StringBuilder temp = new StringBuilder();
                    temp.Append('0', BPads);
                    BlueBits += temp.ToString();
                }
                for (int i = 0; i < BlueBits.Length / 8; i++)
                {
                    BcurrCodeBytes.Add(Convert.ToByte(BlueBits.Substring(i * 8, 8), 2));
                }
                binaryWriter.Write((byte)BPads);
                binaryWriter.Write(BcurrCodeBytes.Count);
                binaryWriter.Write(BcurrCodeBytes.ToArray());

                totalAfterCompressionInBytes = new FileInfo(filePath).Length;
            }
            double totalAfterCompressionInKB = totalAfterCompressionInBytes / (1024.0);
            return totalAfterCompressionInKB;

        }

        static void TransformCompressedFileToImage(BinaryReader binaryReader, Dictionary<string, byte> Encoding, RGBPixel[,] Image, char Color)
        {

            int pads = Convert.ToInt32(binaryReader.ReadByte());
            int bytesNumber = binaryReader.ReadInt32();
            byte[] bytes = binaryReader.ReadBytes(bytesNumber);
            StringBuilder str = new StringBuilder();
            foreach (byte b in bytes)
            {
                str.Append(Convert.ToString(b, 2).PadLeft(8, '0'));
            }

            string bits = str.ToString();
            if (pads != 8)
                bits = bits.Substring(0, bits.Length - pads);
            int totalBitsSize = bits.Length;
            StringBuilder currBits = new StringBuilder();
            string temp = "";
            int pixelRow = 0, pixelCol = 0;
            for (int i = 0; i < totalBitsSize; i++)
            {

                currBits.Append(bits[i]);
                temp = currBits.ToString();
                if (Encoding.ContainsKey(temp))
                {
                    if (Color.Equals('r'))
                        Image[pixelRow, pixelCol].red = Encoding[temp];
                    else if (Color.Equals('g'))
                        Image[pixelRow, pixelCol].green = Encoding[temp];
                    else
                        Image[pixelRow, pixelCol].blue = Encoding[temp];

                    if (pixelCol == ColSize - 1)
                    {
                        pixelRow++;
                        pixelCol = 0;
                    }
                    else
                        pixelCol++;
                    currBits.Clear();

                }
            }
        }


        static RGBPixel[,] GetImageFromDecompressedFile(BinaryReader binaryReader)
        {
            RGBPixel[,] OriginalImage = new RGBPixel[RowSize, ColSize];

            BuildEncodingFromTree(binaryReader, DecompressionRedEncoding);
            BuildEncodingFromTree(binaryReader, DecompressionGreenEncoding);
            BuildEncodingFromTree(binaryReader, DecompressionBlueEncoding);

            TransformCompressedFileToImage(binaryReader, DecompressionRedEncoding, OriginalImage, 'r');
            TransformCompressedFileToImage(binaryReader, DecompressionGreenEncoding, OriginalImage, 'g');
            TransformCompressedFileToImage(binaryReader, DecompressionBlueEncoding, OriginalImage, 'b');

            return OriginalImage;

        }
        public static double Compress(RGBPixel[,] Image, string FileToBeCompressedPath,string initialSeed,int tapPosition)
        {
            CompressionEncodingRed.Clear();
            CompressionEncodingBlue.Clear();
            CompressionEncodingGreen.Clear();
            CalcFrequency(Image);
            HuffmanEncode();
            int RowSize = Image.GetLength(0);
            int ColSize = Image.GetLength(1);
            return SaveImageIntoFile(Image, RowSize, ColSize, FileToBeCompressedPath,initialSeed,tapPosition);

        }
        public static RGBPixel[,] Decompress(string compressedFilePath,out string initialSeed , out int tapPosition)
        {
            
            DecompressionRedEncoding.Clear();
            DecompressionGreenEncoding.Clear();
            DecompressionBlueEncoding.Clear();
            RGBPixel[,] OriginalImage;
            using (BinaryReader binaryReader = new BinaryReader(new FileStream(compressedFilePath, FileMode.Open), Encoding.ASCII))
            {
                initialSeed = binaryReader.ReadString();
                tapPosition = binaryReader.ReadInt32();
                RowSize = binaryReader.ReadInt32();
                ColSize = binaryReader.ReadInt32();
                //ReadTreesFromFile(binaryReader);
                OriginalImage = GetImageFromDecompressedFile(binaryReader);
            }
            return OriginalImage;
        }
    }
}
