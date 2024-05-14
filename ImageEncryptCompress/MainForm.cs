using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace ImageEncryptCompress
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        RGBPixel[,] OriginalImage;
        RGBPixel[,] DecompressedImage;
        private void compressButton_Click(object sender, EventArgs e)
        {
            //I want to get the folder path to be created not an existing file path
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
                double sizeBeforeCompressionInBytes = ImageOperations.GetHeight(OriginalImage) * ImageOperations.GetWidth(OriginalImage) * 3;
                double sizeAfterCompression = ImageCompression.Compress(OriginalImage,CompressedFilePath);
                double sizeAfterCompressionInBytes = sizeAfterCompression * 1024;
                ImageOperations.DisplayImage(OriginalImage, pictureBox1);
                MessageBox.Show("Compression Ratio: " + (sizeAfterCompressionInBytes / sizeBeforeCompressionInBytes)*100 + " %");
                MessageBox.Show("Compressed from (" +  sizeBeforeCompressionInBytes +") KB" + " to (" + sizeAfterCompression + ") KB");
            }
        }

        private void decompressButton_Click(object sender, EventArgs e)
        {
            /*double sigma = double.Parse(txtGaussSigma.Text);
            int maskSize = (int)nudMaskSize.Value ;
            ImageMatrix = ImageOperations.GaussianFilter1D(ImageMatrix, maskSize, sigma);*/
            //bool x = ImageOperations.CompareTwoImages(NewImageMatrix, ImageMatrix);

            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                //Open the browsed image and display it
                string OpenedFilePath = openFileDialog1.FileName;
                DecompressedImage = ImageCompression.Decompress(OpenedFilePath);
                ImageOperations.DisplayImage(DecompressedImage, pictureBox2);
            }

        }
        
        private void comparisonButton_Click(object sender,EventArgs e)
        {
            if (OriginalImage == null || DecompressedImage == null)
            {
                MessageBox.Show("One of the images is missing!");
                return;
            }
            bool result = ImageOperations.CompareTwoImages(OriginalImage, DecompressedImage);
            string message;
            if (result)
                message = "The compressed and decompressed images are the same!";
            else
                message = "The compressed and the decompressed images are not the same!";
            MessageBox.Show(message);
        }




    }
}