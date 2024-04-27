using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ImageEncryptCompress
{
    internal class ImageEncryption
    {


         
        public static string ShiftLeft(string s)
        {
            int size = s.Length;
            string shifted = s.Substring(1);
            return shifted;
        }                                              // 8-6+1 = 1
        public static string LFSR(string initialSeed = "10001111", int tapPosition = 6, int k = 5)
        {
            int len = initialSeed.Length;
            for (int i = 0; i < k; i++)
            {
                int deleted = int.Parse(initialSeed[0].ToString());

                int index = len - (tapPosition + 1);

                int element_at_tap_postion = int.Parse(initialSeed[index].ToString());
                string shifted_string = ShiftLeft(initialSeed);
                int result_of_xor = deleted ^ element_at_tap_postion;
                string concatenated_right_bit = result_of_xor.ToString();
                initialSeed = string.Concat(shifted_string, concatenated_right_bit);
            }
            return initialSeed;
        }

        public static string IntToBinaryString(int value)
        {
            Stack<char> c = new Stack<char>();
            while (value > 0)
            {
                int remainder = value % 2;

                c.Push((char)(remainder + '0'));

                value /= 2;
            }
            string ans = "";
            while (c.Count != 0)
            {
                ans += c.Pop();
            }
            return ans;
        }


        static int BinaryStringToInt(string BinaryString)
        {
            int result = 0;
            int power = 0;

            for (int i = BinaryString.Length - 1; i >= 0; i--)
            {
                int digit = BinaryString[i] - '0';
                if (digit == 1)
                    result += (int)Math.Pow(2, power);

                power++;
            }
            return result;
        }


        public static byte[] Xor(byte[] ColorArray , string key)
        {
            int len = ColorArray.Length;
            byte[] Encrypted_color = new byte[len];
            for (int i =0; i < len; i++)
            {
                int element = (int) ColorArray[i]; 
                
                int decimalNumberOfKey = Convert.ToInt32(key, 2); 
                int result = decimalNumberOfKey ^ element;
              
                Encrypted_color.Append((byte)result);
            }
            return Encrypted_color;
        }
        /// <summary>
        /// 
        /// Steps:-
        /// -------
        /// [1] Extract each color from a pixel 
        /// [2] generate key for each color to encrypt 
        /// [3] XOR each color with its key(8 bits) outputing => new color fro red , blue , green  
        /// [4] set bixel to this color again
        /// </summary>
        /// <param name="Image"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public static RGBPixel[,] Encrypt( RGBPixel[,] Image)
        {
            int height = ImageOperations.GetHeight(Image); //row
            int width = ImageOperations.GetWidth(Image);  //col
            byte[] BlueArray = new byte[width *height];
            byte[] RedArray = new byte[width * height];
            byte[] GreenArray = new byte[width * height];

            int count = 0; 
            for (int y = 0; y < height ; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    // Access the pixel at position (x, y)
                    RGBPixel pixel = Image[x, y];
                    BlueArray[count] = pixel.blue;
                    RedArray[count] = pixel.red;
                    GreenArray[count] = pixel.green;
                    count++;
                }
            }
            //we need to access 3 input 



            string Red_key = LFSR("10001111",6 ,8);
            int index = Red_key.Length - 8;

            string R_key = Red_key.Substring(index); 

            string Green_key = LFSR(Red_key);
            string G_key = Green_key.Substring(index);

            string Blue_key = LFSR(Green_key);
            string B_key = Blue_key.Substring(index);


            byte[] newRed = new byte[width * height];
            byte[] newBlue = new byte[width * height];
            byte[] newGreen = new byte[width * height];

            newRed = Xor(RedArray , R_key);
            newBlue= Xor(BlueArray, B_key);
            newGreen = Xor(GreenArray, G_key);

            RGBPixel[,] Encrypted_Image = new  RGBPixel[width, height];

            int counter = 0; 
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    // Access the pixel at position (x, y)
                    RGBPixel pixel = Encrypted_Image[x, y];
                    pixel.red =  newRed[counter];
                    pixel.blue = newBlue[counter];
                    pixel.green =newGreen[counter];
                    counter++;
                }
            }

            return Encrypted_Image;
        }
        public static RGBPixel[,] Decrypt(RGBPixel[,] Image)
        {


            throw new NotImplementedException();
        }
    }
}
