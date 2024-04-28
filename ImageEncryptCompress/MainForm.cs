using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
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

        public enum ImageStatus
        {
            Original,
            Encrypted,
            Decrypted,
            Smoothed
        }
        private ImageStatus currentStatus = ImageStatus.Original; //store current status of photo
        private void UpdateImageStatus(ImageStatus status)
        {
            switch (status)
            {
                case ImageStatus.Original:
                    image_status.Text = "Original Image";
                    break;
                case ImageStatus.Encrypted:
                    image_status.Text = "Encrypted Image";
                    break;
                case ImageStatus.Decrypted:
                    image_status.Text = "Decrypted Image";
                    break;
                case ImageStatus.Smoothed:
                    image_status.Text = "Smoothed Image";
                    break;
                default:
                    image_status.Text = "Unknown Status";
                    break;
            }
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

        private void button1_Click(object sender, EventArgs e)   // encrypt button
        {
            //take two input from user to help in encryption process
            string userInput_initial_seed = initial_seed.Text;
            string userInput_tap_position = tap_position.Text;
            int intValue =8;
            try
            {
                 intValue = int.Parse(userInput_tap_position);
                ///just for checking :can be deleted later
                MessageBox.Show("Parsed integer value: " + intValue);
            }
            catch (FormatException)
            {
                ///just for checking :can be deleted later
                MessageBox.Show("Invalid input. Please enter a valid integer.");
            }

            RGBPixel[,] encryptedImage = ImageEncryption.Encrypt(ImageMatrix , userInput_initial_seed, intValue);

            // Display the encrypted image in pictureBox2
            //i will update current status here to be displayed
            currentStatus = ImageStatus.Encrypted;
            UpdateImageStatus(currentStatus);
            ImageOperations.DisplayImage(encryptedImage, pictureBox2);
            

            RGBPixel[,] stored_encrypted_image = ImageOperations.OpenImage("D:\\Collegue\\6th semester\\Algorithm\\Project\\[1] Image Encryption and Compression\\Sample Test\\SampleCases_Encryption\\OUTPUT\\Sample1Output.bmp");
            bool imagesMatch = ImageOperations.CompareTwoImages(encryptedImage, stored_encrypted_image);
            // Display the result in a message box
            if (imagesMatch == true)
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

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void Decrypt_button_Click(object sender, EventArgs e)
        {


            // Update the status to "Decrypted"
            currentStatus = ImageStatus.Decrypted;
            UpdateImageStatus(currentStatus);
        }
    }
}