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
        public static RGBPixel[,] Compress(RGBPixel[,] Image)
        {
           
            CalcFrequency(Image);
            throw new NotImplementedException();
        }
        public static RGBPixel[,] Decompress(RGBPixel[,] Image)
        {
            throw new NotImplementedException();
        }
    }
}
