using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageEncryptCompress
{
    internal class EncryptionTestCase
    {
        public string InitialSeed;
        public int TapPosition;
        public string InputImagePath;
        public string OutputImagePath;

        public EncryptionTestCase(string initialSeed, int tapPosition)
        {
            InitialSeed = initialSeed;
            TapPosition = tapPosition;
            InputImagePath = getPath();
            OutputImagePath = getPath();
        }
        public string getPath()
        {
            string imagePath = "";
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image Files (*.jpg;*.jpeg;*.png;*.bmp)|*.jpg;*.jpeg;*.png;*.bmp|All files (*.*)|*.*";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    imagePath = openFileDialog.FileName;
                }
            }
            return imagePath;
        }
    }
    internal class ImageEncryptionTest
    {

        private static readonly EncryptionTestCase[] testCases = new EncryptionTestCase[]
        {
            new EncryptionTestCase(  //sample 1
                "10001111",   
                6
                ),

            new EncryptionTestCase(   //sample 2
                "1110001011",  
                8
                ),

            new EncryptionTestCase( //sample 3
                "01101000010",
                2),

            new EncryptionTestCase( //sample 4
                "011010000101001",
                10
                ),

            new EncryptionTestCase( //sample 5
                "01101000010100010000",
                16),

            new EncryptionTestCase(  //sample 6
                "10011100",
                0)
        };

        public ImageEncryptionTest()
        {
            
        }

        static public void TestSamples()
        {
            List<bool> results = new List<bool>();
            foreach (var testCase in testCases)
            {

                RGBPixel[,] inputImage = ImageOperations.OpenImage(testCase.InputImagePath);
                RGBPixel[,] expectedOutputImage = ImageOperations.OpenImage(testCase.OutputImagePath);
                RGBPixel[,] actualOutputImage = ImageEncryption.Encrypt(inputImage,testCase.InitialSeed,testCase.TapPosition);
                if (ImageOperations.CompareTwoImages(expectedOutputImage, actualOutputImage))
                {
                    Console.WriteLine($"SAMPLE {testCase.InputImagePath} ENCRYPTION SUCCESS!");
                    results.Add(true);
                }
                else
                {
                    Console.WriteLine($"SAMPLE {testCase.InputImagePath} ENCRYPTION FAIL!");
                    results.Add(false);
                }
            }
            Console.WriteLine();
        }
    }
}