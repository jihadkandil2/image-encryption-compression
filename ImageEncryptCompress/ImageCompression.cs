using System;
using System.Collections;
using System.Collections.Generic;
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


        static string CompressedBlue = "";
        static string CompressedRed = "";
        static string CompressedGreen = "";

        static Node redRoot, greenRoot, blueRoot;
        static int RowSize, ColSize;
        static string colorDelimiter = "45";
        static string nodeDelimiter = "35";

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

        private static void SaveTreeIntoFile(BinaryWriter binaryWriter, Node root,Dictionary<byte,string> CompressionEncoding,string currCode)
        {
            if (root == null)
                return; 
            //binaryWriter.Write((byte)root.value);//1
            SaveTreeIntoFile(binaryWriter, root.left,CompressionEncoding,currCode + '0');
            SaveTreeIntoFile(binaryWriter, root.right, CompressionEncoding, currCode + '1');
            if (root.left == null && root.right == null)
            {
                binaryWriter.Write(currCode.ToArray());
                binaryWriter.Write("#");
                binaryWriter.Write((byte)root.value);
                CompressionEncoding.Add((byte)root.value, currCode);
            }

        }
        private static void BuildEncodingFromTree(BinaryReader binaryReader,Dictionary<string, byte> DecompressionEncoding)
        {
            StringBuilder buffer = new StringBuilder();
            do
            {
                byte x = binaryReader.ReadByte();
                if (x.ToString().Equals(colorDelimiter))
                    break;
                if (x.ToString().Equals(nodeDelimiter))
                {
                    buffer.Remove(buffer.Length - 1, 1);//BinaryWriter adds some weird stuff at the end after the buffer is over so we remove it
                    
                    string bits = buffer.ToString();
                    buffer.Clear();
                    byte symbol = binaryReader.ReadByte();
                    DecompressionEncoding.Add(bits, symbol);

                }
                else
                    buffer.Append(Convert.ToChar(x));


            } while (true);
        }

        private static Node BuildTree(RGBPixel[,] Image,Dictionary<byte, string> CompressedEncoding, Dictionary<byte, int> ColorFequency)
        {
            PriorityQueue<Node> priorityQueue = new PriorityQueue<Node>();
            foreach (KeyValuePair<byte, int> item in ColorFequency)
            {
                Node node = new Node(item.Key, item.Value);
                priorityQueue.Enqueue(node);
            }
            while(priorityQueue.Count > 1) { 
                Node minim = priorityQueue.Dequeue();
                Node secondMinim = priorityQueue.Dequeue();

                Node parent = new Node(-1, minim.frequency + secondMinim.frequency);
                parent.right = minim;
                parent.left = secondMinim;

                priorityQueue.Enqueue(parent);

            }

            return priorityQueue.Dequeue();
        }

        private static void HuffmanEncode(RGBPixel[,] Image)
        {
            redRoot = BuildTree(Image, CompressionEncodingRed, RedFrequency);
            greenRoot = BuildTree(Image, CompressionEncodingGreen, GreenFrequency);
            blueRoot = BuildTree(Image, CompressionEncodingBlue, BlueFrequency);
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
                    if(Encoding.ContainsKey(color))
                    {
                        ctr++;
                        binaryCode.Append(Encoding[color]);
                    }
                }
            }
            Console.WriteLine(ctr);
            return binaryCode.ToString();
        }

        static KeyValuePair<byte[],int> ConvertToByte(string Binarycode)
        {
            int numOfBads = 8 - Binarycode.Length % 8;

            if(numOfBads != 8)
            {
                string BadedBits = new string('0', numOfBads);
                Binarycode += BadedBits;            // Originallength-BadedBits
            }

            int size = Binarycode.Length / 8;
            byte[] arr = new byte[size];

            for (int a = 0; a < size; a++)
            {
                arr[a] = Convert.ToByte(Binarycode.Substring(a * 8, 8), 2);
            }

            return new KeyValuePair<byte[],int>(arr,numOfBads);
        }
        static KeyValuePair<byte[],int> GetImageComponentBytes(RGBPixel[,] Image,string CompressedBinaryEncoding,char Color)
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

            CompressedBinaryEncoding = ReplaceBinaryCode(Image, CompressionEncoding, colorSelector);
            return ConvertToByte(CompressedBinaryEncoding);
        }

        static void SaveImageIntoFile(RGBPixel[,] Image,int RowSize,int ColSize)
        {
            string filePath = "E:\\Algorithms\\5 Project\\RELEASE\\[1] Image Encryption and Compression\\Startup Code\\[TEMPLATE] ImageEncryptCompress\\compressed.bin";
            using (BinaryWriter binaryWriter = new BinaryWriter(new FileStream(filePath, FileMode.Create), Encoding.UTF8))
            {
                binaryWriter.Write(RowSize);
                binaryWriter.Write(ColSize);
                SaveTreeIntoFile(binaryWriter, redRoot,CompressionEncodingRed,"");
                binaryWriter.Write('-');
                SaveTreeIntoFile(binaryWriter, greenRoot,CompressionEncodingGreen,"");
                binaryWriter.Write('-');
                SaveTreeIntoFile(binaryWriter,blueRoot,CompressionEncodingBlue,"");
                binaryWriter.Write('-');
                
                KeyValuePair<byte[],int> RedBytes = GetImageComponentBytes(Image,CompressedRed,'r');
                binaryWriter.Write(RedBytes.Key);
                binaryWriter.Write('-');
                binaryWriter.Write(RedBytes.Value);

                KeyValuePair<byte[], int> GreenBytes = GetImageComponentBytes(Image, CompressedGreen,'g') ;
                binaryWriter.Write(GreenBytes.Key);
                binaryWriter.Write('-');
                binaryWriter.Write(GreenBytes.Value);

                KeyValuePair<byte[], int> BlueBytes = GetImageComponentBytes(Image,CompressedBlue,'b');
                binaryWriter.Write(BlueBytes.Key);
                binaryWriter.Write('-');
                binaryWriter.Write(BlueBytes.Value);
            }

        }

        static void TransformCompressedFileToImage(BinaryReader binaryReader, Dictionary<string, byte> Encoding, RGBPixel[,] Image,char Color)
        {
            List<byte> buffer = new List<byte>();
            do
            {
                byte x = binaryReader.ReadByte();
                if (x.ToString().Equals(colorDelimiter))
                    break;
                buffer.Add(x);
            } while (true);
            //17 : 17+7 = 24
            int padded = binaryReader.ReadInt32();
            int totalBitsSize;
            StringBuilder bitsBuilder = new StringBuilder();
            foreach (byte b in buffer)
            {
                // Convert each byte to its binary representation and append it to the string builder
                bitsBuilder.Append(Convert.ToString(b, 2).PadLeft(8, '0'));
            }
            string bits = bitsBuilder.ToString();

            //BitArray bits = new BitArray(buffer.ToArray());
            if (padded != 8)
                totalBitsSize = bits.Length - padded;
            else
                totalBitsSize = bits.Length;
            StringBuilder currBits = new StringBuilder();
            string temp = "";
            int pixelRow = 0, pixelCol = 0;
            for (int i = 0; i < totalBitsSize; i++)
            {
                if(i == totalBitsSize-1)
                    Console.WriteLine();
                currBits.Append(bits[i]);//00 01 02 03 .. 0 621
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

            BuildEncodingFromTree(binaryReader,DecompressionRedEncoding);
            BuildEncodingFromTree(binaryReader, DecompressionGreenEncoding);
            BuildEncodingFromTree(binaryReader, DecompressionBlueEncoding);

            TransformCompressedFileToImage(binaryReader, DecompressionRedEncoding, OriginalImage, 'r');
            TransformCompressedFileToImage(binaryReader, DecompressionGreenEncoding, OriginalImage, 'g');
            TransformCompressedFileToImage(binaryReader, DecompressionBlueEncoding, OriginalImage, 'b');

            return OriginalImage;

        }
        public static void Compress(RGBPixel[,] Image)
        {
            CompressionEncodingRed.Clear();
            CompressionEncodingBlue.Clear();
            CompressionEncodingGreen.Clear();
            CalcFrequency(Image);
            HuffmanEncode(Image);
            int RowSize = Image.GetLength(0);
            int ColSize = Image.GetLength(1);
            SaveImageIntoFile(Image,RowSize,ColSize);

        }
        public static RGBPixel[,] Decompress(string compressedFilePath)
        {
            DecompressionRedEncoding.Clear();
            DecompressionGreenEncoding.Clear();
            DecompressionBlueEncoding.Clear();
            compressedFilePath = "E:\\Algorithms\\5 Project\\RELEASE\\[1] Image Encryption and Compression\\Startup Code\\[TEMPLATE] ImageEncryptCompress\\compressed.bin";
            RGBPixel[,] OriginalImage;
            using (BinaryReader binaryReader = new BinaryReader(new FileStream(compressedFilePath, FileMode.Open), Encoding.ASCII))
            {
                RowSize = binaryReader.ReadInt32();
                ColSize = binaryReader.ReadInt32();
                //ReadTreesFromFile(binaryReader);
                OriginalImage = GetImageFromDecompressedFile(binaryReader);
            }
            return OriginalImage;
        }
    }
}
