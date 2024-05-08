using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace ImageEncryptCompress
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        RGBPixel[,] OriginalImage;
        RGBPixel[,] EncryptedImage, DecryptedImage;

        static string initialSeed = "";
        static int tapPosition = 0;
        static long forwardTime = 0;
        static long backwardTime = 0;
        static Stopwatch stopwatch = new Stopwatch();


        private void OpenImageButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                //Open the browsed image and display it
                string OpenedFilePath = openFileDialog1.FileName;
                if (OpenedFilePath == null || OpenedFilePath.Equals(""))
                {
                    MessageBox.Show("Please select an image", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                OriginalImage = ImageOperations.OpenImage(OpenedFilePath);
                ImageOperations.DisplayImage(OriginalImage, pictureBox1);
            }
        }

        private void EncryptButton_Click(object sender, EventArgs e)
        {
            if (OriginalImage == null)
            {
                MessageBox.Show("Please open an image first", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            //take two input from user to help in encryption process
            string userInput_initial_seed = initial_seed.Text;
            string userInput_tap_position = tap_position.Text;

            /*if(userInput_initial_seed.Length < 8)
            {
                MessageBox.Show("Initial seed must be at least 8 bits", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }*/


            if (userInput_initial_seed == "" || userInput_tap_position == "" || userInput_initial_seed == null || userInput_tap_position == null)
            {
                MessageBox.Show("Please enter the initial seed and tap position", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            int parsedTapPosition = int.Parse(userInput_tap_position);
            if (parsedTapPosition >= userInput_initial_seed.Length)
            {
                MessageBox.Show("Invalid Tap Position", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            initialSeed = userInput_initial_seed;
            tapPosition = parsedTapPosition;
            stopwatch.Start();
            EncryptedImage = ImageEncryption.Encrypt(OriginalImage, userInput_initial_seed, parsedTapPosition);
            stopwatch.Stop();
            forwardTime += stopwatch.ElapsedMilliseconds;
            ImageOperations.DisplayImage(EncryptedImage, pictureBox2);
            MessageBox.Show("ENCRYPTION DONE after " + forwardTime + " ms");

        }

        private void DecryptButton_Click(object sender, EventArgs e)
        {

            if (EncryptedImage == null)
            {
                MessageBox.Show("Please encrypt the image first", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            /*
            string userInput_initial_seed = initial_seed.Text;
            string userInput_tap_position = tap_position.Text;

            if (userInput_initial_seed.Length < 8)
            {
                MessageBox.Show("Initial seed must be at least 8 bits", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (userInput_initial_seed == "" || userInput_tap_position == "" || userInput_initial_seed == null || userInput_tap_position == null)
            {
                MessageBox.Show("Please enter the initial seed and tap position", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int parsedTapPosition = int.Parse(userInput_tap_position);
            if(parsedTapPosition >= userInput_initial_seed.Length)
            {
                MessageBox.Show("Invalid Tap Position", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
*/
            stopwatch.Start();
            DecryptedImage = ImageEncryption.Decrypt(EncryptedImage, initialSeed, tapPosition);
            stopwatch.Stop();
            backwardTime += stopwatch.ElapsedMilliseconds;
            ImageOperations.DisplayImage(DecryptedImage, pictureBox2);
            MessageBox.Show("DECRYPTION DONE after " + stopwatch.ElapsedMilliseconds + " ms");
            MessageBox.Show("BACKWARD TIME TOTAL : " + backwardTime);

        }
        private void compressbtn_Click(object sender, EventArgs e)
        {
            double sizeBeforCompressionBytes = 0.0;
            double sizeAfterCompressionBytes = 0.0;
            if (CompressExistedImage.Checked)
            {
                if (EncryptedImage == null)
                {
                    MessageBox.Show("Please encrypt the image first", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                FolderBrowserDialog folderBrowserDialog1 = new FolderBrowserDialog();
                string CompressedFilePath;
                if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                {
                    CompressedFilePath = folderBrowserDialog1.SelectedPath + "\\CompressedImage.bin";
                    if (CompressedFilePath == null || CompressedFilePath.Equals(""))
                    {
                        MessageBox.Show("Please select a folder to create the compressed file at!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("Unexpected Error!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                sizeBeforCompressionBytes = ((double)ImageOperations.GetHeight(EncryptedImage) * (double)ImageOperations.GetWidth(EncryptedImage) * 24.0) / (8.0);
                stopwatch.Start();
                sizeAfterCompressionBytes = 1024.0 * ImageCompression.Compress(EncryptedImage, CompressedFilePath, initialSeed, tapPosition);
                stopwatch.Stop();
                forwardTime += stopwatch.ElapsedMilliseconds;
                MessageBox.Show("BINARY FILE FROM " + sizeBeforCompressionBytes + " bytes to : " + sizeAfterCompressionBytes + " bytes");
                MessageBox.Show("COMPRESSION TIME : " + stopwatch.ElapsedMilliseconds.ToString());
                MessageBox.Show("ELAPSED TIME AFTER FORWARD TOTAL IS : " + forwardTime);
                forwardTime = 0;
            }
            /*else if(DeviceImageCompress.Checked)
            {
                string userInput_initial_seed = initial_seed.Text;
                string userInput_tap_position = tap_position.Text;

                if (userInput_initial_seed.Length < 8)
                {
                    MessageBox.Show("Initial seed must be at least 8 bits", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }


                if (userInput_initial_seed == "" || userInput_tap_position == "" || userInput_initial_seed == null || userInput_tap_position == null)
                {
                    MessageBox.Show("Please enter the initial seed and tap position", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                int parsedTapPosition = int.Parse(userInput_tap_position);
                if (parsedTapPosition >= userInput_initial_seed.Length)
                {
                    MessageBox.Show("Invalid Tap Position", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }


                FolderBrowserDialog folderBrowserDialog1 = new FolderBrowserDialog();
                string CompressedFilePath;
                if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                {
                    CompressedFilePath = folderBrowserDialog1.SelectedPath + "\\CompressedImage.bin";
                    if (CompressedFilePath == null || CompressedFilePath.Equals(""))
                    {
                        MessageBox.Show("Please select a folder to create the compressed file at!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("Unexpected Error!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                OpenFileDialog openFileDialog1 = new OpenFileDialog();
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    //Open the browsed image and display it
                    string OpenedFilePath = openFileDialog1.FileName;
                    if (OpenedFilePath == null || OpenedFilePath.Equals(""))
                    {
                        MessageBox.Show("Please select an image", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    OriginalImage = ImageOperations.OpenImage(OpenedFilePath);
                    openFileDialog1.Dispose();
                    sizeAfterCompressionKB = ImageCompression.Compress(OriginalImage, CompressedFilePath,userInput_initial_seed,parsedTapPosition);
                    sizeBeforCompressionKB = ((double)ImageOperations.GetHeight(OriginalImage) * (double)ImageOperations.GetWidth(OriginalImage) * 24.0) / (8.0 * 1024.0);
                    ImageOperations.DisplayImage(OriginalImage, pictureBox2);
                }
            }*/
        }

        private void Decompressbtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {

                //Open the browsed image and display it
                string OpenedFilePath = openFileDialog1.FileName;
                stopwatch.Start();
                EncryptedImage = ImageCompression.Decompress(OpenedFilePath, out string outInitialSeed, out int outTapPosition);
                stopwatch.Stop();
                backwardTime += stopwatch.ElapsedMilliseconds;
                initialSeed = outInitialSeed;
                tapPosition = outTapPosition;
                ImageOperations.DisplayImage(EncryptedImage, pictureBox2);
                MessageBox.Show("Decompression Done! , Image In PictureBox Is The Decompressed Image");
                MessageBox.Show("DECOMPRESSION TIME : " + stopwatch.ElapsedMilliseconds.ToString());
                backwardTime = 0;
            }
        }

        private void CompareImagesButton_Click(object sender, EventArgs e)
        {
            bool imagesMatch = ImageOperations.CompareTwoImages(DecryptedImage, OriginalImage);
            if (imagesMatch)
            {
                MessageBox.Show("The images match!", "Image Comparison Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("The images do not match!", "Image Comparison Result", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}