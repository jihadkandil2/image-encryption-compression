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
        }                                          
        public static string LFSR(string initialSeed , int tapPosition , int k )
        {
            int len = initialSeed.Length;
            for (int i = 0; i < k; i++) 
            {
                int deleted_left = int.Parse(initialSeed[0].ToString());

                int index_tap_position = len - (tapPosition + 1);
                int element_at_tap_postion = int.Parse(initialSeed[index_tap_position].ToString());

                string shifted_string = ShiftLeft(initialSeed); //remove left element

                int result_of_xor = deleted_left ^ element_at_tap_postion;

                string concatenated_right_bit = result_of_xor.ToString();
                initialSeed = string.Concat(shifted_string, concatenated_right_bit);
            }
            return initialSeed;
        }
        public static string Key_stream_generation(string resulted_seed)
        {
            //we have to handle if the lenght <= 8 but in test cases the following scenario is not happening

            int index_to_start_ur_key_stream = resulted_seed.Length - 8;
            string key_stream = resulted_seed.Substring(index_to_start_ur_key_stream);
            return key_stream;
        }

        public static byte XorElement(byte Color_Array, string Color_key)
        {
            int decimalNumberOfKey = Convert.ToInt32(Color_key, 2);
            byte result = (byte) (Color_Array ^ decimalNumberOfKey);
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
        /// [2] generate key for each element in color array to encrypt 
        ///   * using LFSR removing left element , adding the new element resulted from XOR remover element with element at tap position
        ///   * do this k times , k=8 to make the stream always byte = 8 bits
        /// [3] XOR each element in color array with its newly generated key(8 bits) outputing => new color for red , blue , green  
        /// [4] set pixels to this color again
        /// </summary>
        /// <param name="Image"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public static RGBPixel[,] Encrypt( RGBPixel[,] Image , string initial_seed , int tap_position)
        {
<<<<<<< HEAD
=======
            
>>>>>>> 251d96b3c46e864304f44fb1ec6f011a5890ae3d
            //[1]-
            int height = ImageOperations.GetHeight(Image); //row
            int width = ImageOperations.GetWidth(Image);  //col
            byte[] BlueArray = new byte[width *height];
            byte[] RedArray = new byte[width * height];
            byte[] GreenArray = new byte[width * height];

            int count = 0;
   
            for (int y = 0; y < height ; y++)  //row
            {
                for (int x = 0; x < width; x++)//col
                {
                    BlueArray[count] = Image[y, x].blue;
                    RedArray[count] = Image[y, x].red;
                    GreenArray[count] = Image[y, x].green;
                    count++;
                }
            }


            //[2]-[3]-
            string seed = initial_seed;
            string streamKey = "";
            byte[] newRed = new byte[width * height];
            byte[] newBlue = new byte[width * height];
            byte[] newGreen = new byte[width * height];
      
            
            for (int i =0; i< width * height;i++)
            {
                seed = LFSR(seed, tap_position, 8);
                streamKey = Key_stream_generation(seed);
                newRed[i] = XorElement(RedArray[i], streamKey);

                seed = LFSR(seed, tap_position, 8);
                streamKey = Key_stream_generation(seed);
                newGreen[i] = XorElement(GreenArray[i], streamKey);

                seed = LFSR(seed, tap_position, 8);
                streamKey = Key_stream_generation(seed);
                newBlue[i] = XorElement(BlueArray[i], streamKey);

            }

<<<<<<< HEAD
=======
           
>>>>>>> 251d96b3c46e864304f44fb1ec6f011a5890ae3d
            //[4]-
            RGBPixel[,] Encrypted_Image = new  RGBPixel[height, width];
            int counter = 0; 
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    // Access the pixel at position (x, y)
                    Encrypted_Image[y, x].red = newRed[counter];
                    Encrypted_Image[y, x].green = newGreen[counter];
                    Encrypted_Image[y, x].blue = newBlue[counter];
                    
                    counter++;
                }
            }
            return Encrypted_Image;
        }
        public static RGBPixel[,] Decrypt(RGBPixel[,] Image,string initialSeed,int tapPosition)
        {
            return Encrypt(Image,initialSeed,tapPosition);
        }
    }
}
