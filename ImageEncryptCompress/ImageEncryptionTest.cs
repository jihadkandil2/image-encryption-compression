using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageEncryptCompress
{
    internal class EncryptionTestCase
    {
        public string InitialSeed;
        public int TapPosition;
        public string InputImagePath;
        public string OutputImagePath;

        public EncryptionTestCase(string initialSeed, int tapPosition, string inputImagePath, string outputImagePath)
        {
            InitialSeed = initialSeed;
            TapPosition = tapPosition;
            InputImagePath = inputImagePath;
            OutputImagePath = outputImagePath;
        }
    }
    internal class ImageEncryptionTest
    {
        private readonly EncryptionTestCase[] testCases;

        public ImageEncryptionTest()
        {
            testCases = new EncryptionTestCase[]
            {
            new EncryptionTestCase(
                "10001111",
                6,
                "D:\\Collegue\\6th semester\\Algorithm\\Project\\[1] Image Encryption and Compression\\Sample Test\\SampleCases_Encryption\\INPUT\\Sample1.bmp\\",
                "D:\\Collegue\\6th semester\\Algorithm\\Project\\[1] Image Encryption and Compression\\Sample Test\\SampleCases_Encryption\\OUTPUT\\Sample1Output.bmp"),

            new EncryptionTestCase(
                "1110001011",  // 10- 8+1
                8,
                "E:\\Algorithms\\5 Project\\RELEASE\\[1] Image Encryption and Compression\\Sample Test\\SampleCases_Encryption\\INPUT\\Sample2.bmp",
                "E:\\Algorithms\\5 Project\\RELEASE\\[1] Image Encryption and Compression\\Sample Test\\SampleCases_Encryption\\OUTPUT\\Sample2Output.bmp"),

            new EncryptionTestCase(
                "01101000010",
                2,
                "E:\\Algorithms\\5 Project\\RELEASE\\[1] Image Encryption and Compression\\Sample Test\\SampleCases_Encryption\\INPUT\\Sample3.bmp",
                "E:\\Algorithms\\5 Project\\RELEASE\\[1] Image Encryption and Compression\\Sample Test\\SampleCases_Encryption\\OUTPUT\\Sample3Output.bmp"),

            new EncryptionTestCase(
                "011010000101001",
                10,
                "E:\\Algorithms\\5 Project\\RELEASE\\[1] Image Encryption and Compression\\Sample Test\\SampleCases_Encryption\\INPUT\\Sample4.bmp",
                "E:\\Algorithms\\5 Project\\RELEASE\\[1] Image Encryption and Compression\\Sample Test\\SampleCases_Encryption\\OUTPUT\\Sample4Output.bmp"),

            new EncryptionTestCase(
                "011010000101001",
                16,
                "E:\\Algorithms\\5 Project\\RELEASE\\[1] Image Encryption and Compression\\Sample Test\\SampleCases_Encryption\\INPUT\\Sample5.bmp",
                "E:\\Algorithms\\5 Project\\RELEASE\\[1] Image Encryption and Compression\\Sample Test\\SampleCases_Encryption\\OUTPUT\\Sample5Output.bmp"),

            new EncryptionTestCase(
                "10011100",
                0,
                "E:\\Algorithms\\5 Project\\RELEASE\\[1] Image Encryption and Compression\\Sample Test\\SampleCases_Encryption\\INPUT\\Sample6.bmp",
                "E:\\Algorithms\\5 Project\\RELEASE\\[1] Image Encryption and Compression\\Sample Test\\SampleCases_Encryption\\OUTPUT\\Sample6Output.bmp")
            };
        }

        public void TestSamples()
        {
            foreach (var testCase in testCases)
            {
                RGBPixel[,] inputImage = ImageOperations.OpenImage(testCase.InputImagePath);
                RGBPixel[,] expectedOutputImage = ImageOperations.OpenImage(testCase.OutputImagePath);
                RGBPixel[,] actualOutputImage = ImageEncryption.Encrypt(inputImage);
                if (ImageOperations.CompareTwoImages(expectedOutputImage, actualOutputImage))
                    Console.WriteLine($"SAMPLE {testCase.InputImagePath} ENCRYPTION SUCCESS!");
                else
                    Console.WriteLine($"SAMPLE {testCase.InputImagePath} ENCRYPTION FAIL!");
            }
        }
    }
}