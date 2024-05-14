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
            BlueFrequency.Clear();//O(C)
            RedFrequency.Clear();//O(C)
            GreenFrequency.Clear();//O(C)
            int row = Image.GetLength(0);//O(1)
            int col = Image.GetLength(1);//O(1)

            //O(H*W)
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    //O(1)
                    if (!BlueFrequency.ContainsKey(Image[i, j].blue))
                    {
                        BlueFrequency.Add(Image[i, j].blue, 1);//O(1)
                    }
                    else
                    {
                        BlueFrequency[Image[i, j].blue]++;//O(1)
                    }
                    //O(1)
                    if (!RedFrequency.ContainsKey(Image[i, j].red))
                    {
                        RedFrequency.Add(Image[i, j].red, 1);//O(1)
                    }
                    else
                    {
                        RedFrequency[Image[i, j].red]++;//O(1)
                    }
                    //O(1)
                    if (!GreenFrequency.ContainsKey(Image[i, j].green))
                    {
                        GreenFrequency.Add(Image[i, j].green, 1);//O(1)
                    }
                    else
                    {
                        GreenFrequency[Image[i, j].green]++;//O(1)
                    }
                }
            }
        }

        private static void SaveTreeIntoFile(BinaryWriter binaryWriter, Node root, Dictionary<byte, string> CompressionEncoding, StringBuilder currCode)
        {
            if (root == null)
                return;
            currCode.Append('0');//O(1)
            int currIndex = currCode.Length-1;
            SaveTreeIntoFile(binaryWriter, root.left, CompressionEncoding, currCode);
            currCode.Remove(currIndex,currCode.Length - currIndex);//O(1)
            currCode.Append('1');//O(1)
            currIndex = currCode.Length - 1;
            SaveTreeIntoFile(binaryWriter, root.right, CompressionEncoding, currCode);
            currCode.Remove(currIndex, currCode.Length - currIndex);//max is going to be about 256 so it is always a constant number O(1)
            if (root.left == null && root.right == null)
            {

                List<byte> currCodeBytes = new List<byte>();
                string toBeAddedCode = currCode.ToString();//O(1)
                int pads = 8 - currCode.Length % 8;
                if (pads != 8)
                {
                    StringBuilder temp = new StringBuilder();
                    temp.Append('0', pads);
                    currCode.Append(temp);//O(1) cuz it is always between 0 and 7
                }
                //currcode is a multiple of 8 and will not exceed 256 so O(1) outer loop
                for (int i = 0; i < currCode.Length / 8; i++)
                {
                    //O(1) when adding and the substring is O(1) as well because it is always 8
                    currCodeBytes.Add(Convert.ToByte(currCode.ToString().Substring(i * 8, 8), 2));
                }
                binaryWriter.Write((byte)pads);
                binaryWriter.Write((byte)currCodeBytes.Count);//to be optimized
                binaryWriter.Write(currCodeBytes.ToArray());//O(1)
                binaryWriter.Write((byte)root.value);
                CompressionEncoding.Add((byte)root.value, toBeAddedCode);//O(1)
            }

        }
        private static void BuildEncodingFromTree(BinaryReader binaryReader, Dictionary<string, byte> DecompressionEncoding)
        {
            int leavesNumber = binaryReader.ReadInt32();
            StringBuilder str = new StringBuilder();
            for (int i = 0; i < leavesNumber; i++)
            {
                int pads = Convert.ToInt32(binaryReader.ReadByte());
                int bytesNumber = Convert.ToInt32(binaryReader.ReadByte());
                byte[] bytes = binaryReader.ReadBytes(bytesNumber);
                //O(C) ??????????????????????
                foreach (byte b in bytes)
                {
                    str.Append(Convert.ToString(b, 2).PadLeft(8, '0'));//O(1) because it is always 8 bits to 1 byte
                }
                string code = str.ToString();//O(C)
                byte symbol = binaryReader.ReadByte();
                if (pads != 8)
                    DecompressionEncoding.Add(code.Substring(0, code.Length - pads), symbol);//O(1) because the substring it is always between 0 and 7
                else
                    DecompressionEncoding.Add(code, symbol);//O(1)
                str.Clear();//O(1) as the max possible number for this string is 256 bits
            }
        }

        private static Node BuildTree(Dictionary<byte, int> ColorFequency)
        {
            PriorityQueue<Node> priorityQueue = new PriorityQueue<Node>();
            Node node;
            //O(C*log(C))
            foreach (KeyValuePair<byte, int> item in ColorFequency)
            {
                node = new Node(item.Key, item.Value);//O(1)
                priorityQueue.Enqueue(node);//O(log(C))
            }
            Node minim, secondMinim,parent;
            //O(C*log(C))
            while (priorityQueue.Count > 1)
            {
                minim = priorityQueue.Dequeue();//O(log(C))
                secondMinim = priorityQueue.Dequeue();//O(log(C))

                parent = new Node(-1, minim.frequency + secondMinim.frequency);//O(1)
                parent.left = minim;//O(1)
                parent.right = secondMinim;//O(1)

                priorityQueue.Enqueue(parent);//O(log(C))

            }
            //O(log(C))
            return priorityQueue.Dequeue();
        }

        private static void HuffmanEncode()
        {
            redRoot = BuildTree(RedFrequency);//O(C*log(C))
            greenRoot = BuildTree(GreenFrequency);//O(C*log(C))
            blueRoot = BuildTree(BlueFrequency);//O(C*log(C))
        }

        private static string ReplaceBinaryCode(RGBPixel[,] Image, Dictionary<byte, string> Encoding, Func<RGBPixel, byte> colorSelector)
        {

            int rows = Image.GetLength(0);
            int cols = Image.GetLength(1);
            StringBuilder binaryCode = new StringBuilder();
            //O(H*W)
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    byte color = colorSelector(Image[i, j]);//O(1)
                    if (Encoding.ContainsKey(color))//O(1)
                    {
                        binaryCode.Append(Encoding[color]);//O(1)
                    }
                }
            }
            return binaryCode.ToString(); //Max it could get to would be O(H*W*24) which is still O(H*W)
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
                //?????
                binaryWriter.Write(initalSeed);
                binaryWriter.Write(tapPosition);
                binaryWriter.Write(RowSize);
                binaryWriter.Write(ColSize);
                int NumOfRedLeaves = GetNumberOfLeaves(redRoot);//O(C)
                binaryWriter.Write(NumOfRedLeaves);
                StringBuilder currCode = new StringBuilder();
                SaveTreeIntoFile(binaryWriter, redRoot, CompressionEncodingRed, currCode);//O(C)
                currCode.Clear();
                int NumOfGreenLeaves = GetNumberOfLeaves(greenRoot);//O(C)
                binaryWriter.Write(NumOfGreenLeaves);
                SaveTreeIntoFile(binaryWriter, greenRoot, CompressionEncodingGreen, currCode);//O(C)
                currCode.Clear();
                int NumOfBlueLeaves = GetNumberOfLeaves(blueRoot);//O(C)
                binaryWriter.Write(NumOfBlueLeaves);
                currCode.Clear();
                SaveTreeIntoFile(binaryWriter, blueRoot, CompressionEncodingBlue, currCode);//O(C)

                string RedBits = GetImageComponentBytes(Image, 'r');//O(H*W)
                List<byte> RcurrCodeBytes = new List<byte>();
                int RPads = 8 - RedBits.Length % 8;
                if (RPads != 8)
                {
                    StringBuilder temp = new StringBuilder();
                    temp.Append('0', RPads);
                    RedBits += temp.ToString();//O(1) cuz it is always between 0 and 7
                }
                for (int i = 0; i < RedBits.Length / 8; i++)
                {
                    RcurrCodeBytes.Add(Convert.ToByte(RedBits.Substring(i * 8, 8), 2));
                    //O(1) when adding and the substring is O(1) as well because it is always 8

                }
                binaryWriter.Write((byte)RPads);
                binaryWriter.Write(RcurrCodeBytes.Count);
                binaryWriter.Write(RcurrCodeBytes.ToArray());//O(H*W)


                string GreenBits = GetImageComponentBytes(Image, 'g');//O(H*W)
                List<byte> GcurrCodeBytes = new List<byte>();
                int GPads = 8 - GreenBits.Length % 8;
                if (GPads != 8)
                {
                    StringBuilder temp = new StringBuilder();
                    temp.Append('0', GPads);
                    GreenBits += temp.ToString();//O(1) cuz it is always between 0 and 7

                }
                for (int i = 0; i < GreenBits.Length / 8; i++)
                {
                    GcurrCodeBytes.Add(Convert.ToByte(GreenBits.Substring(i * 8, 8), 2));
                    //O(1) when adding and the substring is O(1) as well because it is always 8

                }
                binaryWriter.Write((byte)GPads);
                binaryWriter.Write(GcurrCodeBytes.Count);
                binaryWriter.Write(GcurrCodeBytes.ToArray());//O(H*W)


                string BlueBits = GetImageComponentBytes(Image, 'b');//O(H*W)
                List<byte> BcurrCodeBytes = new List<byte>();
                int BPads = 8 - BlueBits.Length % 8;
                if (BPads != 8)
                {
                    StringBuilder temp = new StringBuilder();
                    temp.Append('0', BPads);
                    BlueBits += temp.ToString();//O(1) cuz it is always between 0 and 7
                }
                for (int i = 0; i < BlueBits.Length / 8; i++)
                {
                    BcurrCodeBytes.Add(Convert.ToByte(BlueBits.Substring(i * 8, 8), 2));
                    //O(1) when adding and the substring is O(1) as well because it is always 8
                }
                binaryWriter.Write((byte)BPads);
                binaryWriter.Write(BcurrCodeBytes.Count);
                binaryWriter.Write(BcurrCodeBytes.ToArray());//O(H*W)

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
            //O(H*W*3
            foreach (byte b in bytes)
            {
                str.Append(Convert.ToString(b, 2).PadLeft(8, '0'));//O(1) because it is always 8 bits to 1 byte
            }

            string bits = str.ToString();//O(H*W*3)
            if (pads != 8)
                bits = bits.Substring(0, bits.Length - pads);//O(1) because the substring it is always between 0 and 7
            int totalBitsSize = bits.Length;
            StringBuilder currBits = new StringBuilder();
            string temp = "";
            int pixelRow = 0, pixelCol = 0;
            //O(H*W*24)
            for (int i = 0; i < totalBitsSize; i++)
            {

                currBits.Append(bits[i]);
                temp = currBits.ToString();//O(1) because the max length of this string is 256 but it will always be less than that
                if (Encoding.ContainsKey(temp))//O(1)
                {
                    if (Color.Equals('r'))//O(1)
                        Image[pixelRow, pixelCol].red = Encoding[temp];//O(1)
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
                    currBits.Clear();//O(1)

                }
            }
        }


        static RGBPixel[,] GetImageFromDecompressedFile(BinaryReader binaryReader)
        {
            RGBPixel[,] OriginalImage = new RGBPixel[RowSize, ColSize];

            BuildEncodingFromTree(binaryReader, DecompressionRedEncoding);//O(C)
            BuildEncodingFromTree(binaryReader, DecompressionGreenEncoding);//O(C)
            BuildEncodingFromTree(binaryReader, DecompressionBlueEncoding);//O(C)

            TransformCompressedFileToImage(binaryReader, DecompressionRedEncoding, OriginalImage, 'r');//O(H*W)
            TransformCompressedFileToImage(binaryReader, DecompressionGreenEncoding, OriginalImage, 'g');//O(H*W)
            TransformCompressedFileToImage(binaryReader, DecompressionBlueEncoding, OriginalImage, 'b');//O(H*W)

            return OriginalImage;

        }

        //C : unique colors in the image .. H : Height of the Image .. W : Width of the Image
        //Total time complexity : O(H*W + C*log(C))
        public static double Compress(RGBPixel[,] Image, string FileToBeCompressedPath,string initialSeed,int tapPosition)
        {
            CompressionEncodingRed.Clear();//O(C)
            CompressionEncodingBlue.Clear();//O(C)
            CompressionEncodingGreen.Clear();//O(C)
            CalcFrequency(Image);//O(H*W)
            HuffmanEncode();//O(C*log(C))
            int RowSize = Image.GetLength(0);//O(1)
            int ColSize = Image.GetLength(1);//O(1)
            return SaveImageIntoFile(Image, RowSize, ColSize, FileToBeCompressedPath,initialSeed,tapPosition);//O(H*W)

        }
        public static RGBPixel[,] Decompress(string compressedFilePath,out string initialSeed , out int tapPosition)
        {
            
            DecompressionRedEncoding.Clear();//O(C)
            DecompressionGreenEncoding.Clear();//O(C)
            DecompressionBlueEncoding.Clear();//O(C)
            RGBPixel[,] OriginalImage;
            using (BinaryReader binaryReader = new BinaryReader(new FileStream(compressedFilePath, FileMode.Open), Encoding.ASCII))
            {
                initialSeed = binaryReader.ReadString();
                tapPosition = binaryReader.ReadInt32();
                RowSize = binaryReader.ReadInt32();
                ColSize = binaryReader.ReadInt32();
                //ReadTreesFromFile(binaryReader);
                OriginalImage = GetImageFromDecompressedFile(binaryReader);//O(H*W)
            }
            return OriginalImage;
        }
    }
}
