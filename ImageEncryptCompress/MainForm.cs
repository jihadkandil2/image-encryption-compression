using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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

        private void OpenImageButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                //Open the browsed image and display it
                string OpenedFilePath = openFileDialog1.FileName;
                if(OpenedFilePath == null || OpenedFilePath.Equals(""))
                {
                    MessageBox.Show("Please select an image", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                OriginalImage = ImageOperations.OpenImage(OpenedFilePath);
                ImageOperations.DisplayImage(OriginalImage, pictureBox1);

                txtWidth.Text = ImageOperations.GetWidth(OriginalImage).ToString();
                txtHeight.Text = ImageOperations.GetHeight(OriginalImage).ToString();
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

            if(userInput_initial_seed.Length < 8)
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

            EncryptedImage = ImageEncryption.Encrypt(OriginalImage , userInput_initial_seed, parsedTapPosition);

            ImageOperations.DisplayImage(EncryptedImage, pictureBox2);
           
        }

        private void DecryptButton_Click(object sender, EventArgs e)
        {

            if(EncryptedImage == null)
            {
                MessageBox.Show("Please encrypt the image first", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

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

            DecryptedImage = ImageEncryption.Decrypt(EncryptedImage, userInput_initial_seed, parsedTapPosition);

            ImageOperations.DisplayImage(DecryptedImage, pictureBox2);


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