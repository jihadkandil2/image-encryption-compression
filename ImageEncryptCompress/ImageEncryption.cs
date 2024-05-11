using Microsoft.SqlServer.Server;
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
            string shifted = s.Substring(1);
            return shifted;
        }                                              // 8-6+1 = 1
        public static string LFSR(string initialSeed, int tapPosition, int k, out byte password)
        {
            //StringBuilder for in-place representation of a string to avoid creating a new string every time
            StringBuilder initialSeedBuilder = new StringBuilder(initialSeed);
            StringBuilder bits = new StringBuilder();//should be k bits
            int len = initialSeed.Length;
            for (int i = 0; i < k; i++) // 1101010111     10 -(6+1) = 3
            {
                int deleted_left = initialSeedBuilder[0] - '0';//always zero/one even after changing to alphanumeric  --> O(1)

                int index_tap_position = len - (tapPosition + 1); //O(1)
                int element_at_tap_postion = initialSeedBuilder[index_tap_position] - '0';//O(1)

                initialSeedBuilder.Remove(0, 1);//O(L)

                int result_of_xor = deleted_left ^ element_at_tap_postion; // O(1)

                char concatenated_right_bit = (char)(result_of_xor + '0');//O(1)
                initialSeedBuilder.Append(concatenated_right_bit); //O(1) 
                bits.Append(concatenated_right_bit); //O(1)
            }
            password = Convert.ToByte(bits.ToString(), 2);//always 8 characters so O(1)
            return initialSeedBuilder.ToString();//always 8 characters so O(1)
        }
        public static string Key_stream_generation(string resulted_seed)
        {
            //we have to handle if the lenght <= 8 but in test cases the following scenario is not happening

            int index_to_start_ur_key_stream = resulted_seed.Length - 8;
            string key_stream = resulted_seed.Substring(index_to_start_ur_key_stream);
            return key_stream;
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

        //should be deleted
        /*public static byte[] Xor(byte[] Color_Array , string Color_key)
        {
            int len = Color_Array.Length;
            byte[] Encrypted_color = new byte[len];
            for (int i =0; i < len; i++)
            {
                int element = Color_Array[i]; 
                
                int decimalNumberOfKey = Convert.ToInt32(Color_key, 2); 
                int result = decimalNumberOfKey ^ element;
                Encrypted_color[i]= (byte)result;
            }
            return Encrypted_color;
        }*/
        public static byte XorElement(byte Color_Array, string Color_key)
        {
            int decimalNumberOfKey = Convert.ToInt32(Color_key, 2);
            byte result = (byte)(Color_Array ^ decimalNumberOfKey);
            return result;
        }

        /// <summary>
        /// 
        /// Steps:-
        /// -------
        /// [1] Extract each color from a pixel 
        ///   * red_array[w*h]
        ///   * blue_array[w*h]
        ///   * green_array[w*h]
        /// [2] generate key for each color to encrypt 
        ///   * using LFSR removing left element , adding the new element resulted from XOR remover element with element at tap position
        ///   * do this k times , k=8 to make the stream always byte = 8 bits
        /// [3] XOR each color with its key(8 bits) outputing => new color for red , blue , green  
        /// [4] set pixels to this color again
        /// </summary>
        /// <param name="Image"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>

        //H : Height of the Image .. W : Width of the Image .. L : Length of the seed

        //Total time complexity : O(H*W*L) ---> O(H*W) we can neglect L as its value is very small compared to H*W
        public static RGBPixel[,] Encrypt(RGBPixel[,] Image, string initial_seed, int tap_position)
        { 

            int height = ImageOperations.GetHeight(Image); //O(1)
            int width = ImageOperations.GetWidth(Image);  //O(1)

            string seed = initial_seed;//O(L)

            StringBuilder seedBuilder = new StringBuilder();
            //O(L) 
            for (int i = 0; i < seed.Length; i++)
            {
                if (seed[i] != '0' && seed[i] != '1')
                {
                    string BinaryString = Convert.ToString(seed[i], 2).PadLeft(8, '0');
                    seedBuilder.Append(BinaryString);//O(1) --> 8 characters always
                }
                else
                    seedBuilder.Append(seed[i]);//O(1)
            }
            seed = seedBuilder.ToString();//O(L)
            //O(H*W)
            RGBPixel[,] Encrypted_Image = new RGBPixel[height, width];

            //O(H*W*L)
            byte password;
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    seed = LFSR(seed, tap_position, 8, out password);//O(L)
                    Encrypted_Image[y, x].red = (byte)(Image[y, x].red ^ password);//O(1)

                    seed = LFSR(seed, tap_position, 8, out password);//O(L)
                    Encrypted_Image[y, x].green = (byte)(Image[y, x].green ^ password);//O(1)

                    seed = LFSR(seed, tap_position, 8, out password);//(O(L))
                    Encrypted_Image[y, x].blue = (byte)(Image[y, x].blue ^ password);//O(1)
                }
            }


            return Encrypted_Image;
        }

        //Total time complexity : O(H*W*L) ---> O(H*W) we can neglect L as its value is very small compared to H*W
        public static RGBPixel[,] Decrypt(RGBPixel[,] Image, string initialSeed, int tapPosition)
        {

            return Encrypt(Image, initialSeed, tapPosition);

        }

        public static KeyValuePair<string,int> BreakEncryption(RGBPixel[,] EncryptedImage, RGBPixel[,] OriginalImage, int N)
        {
            int Height = ImageOperations.GetHeight(EncryptedImage);
            int Width = ImageOperations.GetWidth(EncryptedImage);

            
            return solve(EncryptedImage,OriginalImage,new StringBuilder(), N);

        }

        public static KeyValuePair<string,int> solve(RGBPixel[,] EncryptedImage, RGBPixel[,] OriginalImage, StringBuilder currSeed, int N)
        {
            if(currSeed.Length > N)
            {
                return new KeyValuePair<string, int>("", -1);
            }
            if(currSeed.Length == N)
            {
                int Height = ImageOperations.GetHeight(EncryptedImage);
                int Width = ImageOperations.GetWidth(EncryptedImage);

                for (int i = 0; i < N; i++)
                {
                    string seed = currSeed.ToString();
                    RGBPixel[,] DecryptedImage = Decrypt(EncryptedImage, seed, i);
                    if (ImageOperations.CompareTwoImages(DecryptedImage, OriginalImage))
                    {
                        return new KeyValuePair<string, int>(seed,i);
                    }
                }
            }

            currSeed.Append('0');
            int currIndex = currSeed.Length - 1;
            KeyValuePair<string,int> ret1 = solve(EncryptedImage, OriginalImage, currSeed, N);
            currSeed.Remove(currIndex, currSeed.Length - currIndex);
            currSeed.Append('1');
            currIndex = currSeed.Length - 1;
            KeyValuePair<string, int> ret2 = solve(EncryptedImage, OriginalImage, currSeed, N);
            currSeed.Remove(currIndex, currSeed.Length - currIndex);
            if (!ret1.Key.Equals("")) return ret1;
            if (!ret2.Key.Equals("")) return ret2;
            return new KeyValuePair<string, int>("",-1);
        }
    }
}
