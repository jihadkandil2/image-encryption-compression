using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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

        RGBPixel[,] ImageMatrix;

        private void btnOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                //Open the browsed image and display it
                string OpenedFilePath = openFileDialog1.FileName;
                ImageMatrix = ImageOperations.OpenImage(OpenedFilePath);
                ImageOperations.DisplayImage(ImageMatrix, pictureBox1);
            }
            txtWidth.Text = ImageOperations.GetWidth(ImageMatrix).ToString();
            txtHeight.Text = ImageOperations.GetHeight(ImageMatrix).ToString();

           // RGBPixel[,] image2 = ImageOperations.OpenImage("D:\\Collegue\\6th semester\\Algorithm\\Project\\[1] Image Encryption and Compression\\Sample Test\\SampleCases_Encryption\\OUTPUT\\Sample1Output.bmp");
           // RGBPixel[,] output_image = ImageEncryption.Encrypt(ImageMatrix);
           // bool x = ImageOperations.CompareTwoImages(output_image, image2);
            //Console.WriteLine();
            
        }

        private void btnGaussSmooth_Click(object sender, EventArgs e)
        {
            double sigma = double.Parse(txtGaussSigma.Text);
            int maskSize = (int)nudMaskSize.Value ;
            ImageMatrix = ImageOperations.GaussianFilter1D(ImageMatrix, maskSize, sigma);
            ImageOperations.DisplayImage(ImageMatrix, pictureBox2);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            RGBPixel[,] encryptedImage = ImageEncryption.Encrypt(ImageMatrix);

            // Display the encrypted image in pictureBox2
             ImageOperations.DisplayImage(encryptedImage, pictureBox2);
             RGBPixel[,] stored_encrypted_image = ImageOperations.OpenImage("D:\\Collegue\\6th semester\\Algorithm\\Project\\[1] Image Encryption and Compression\\Sample Test\\SampleCases_Encryption\\OUTPUT\\Sample1Output.bmp");

            bool imagesMatch = ImageOperations.CompareTwoImages(encryptedImage, stored_encrypted_image);

            // Display the result in a message box
            if (imagesMatch)
            {
                MessageBox.Show("The images match!", "Image Comparison Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("The images do not match!", "Image Comparison Result", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}