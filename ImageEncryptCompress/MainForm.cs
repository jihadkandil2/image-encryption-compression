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
            
            }
            catch (FormatException)
            {
           
            }

            RGBPixel[,] encryptedImage = ImageEncryption.Encrypt(ImageMatrix , userInput_initial_seed, intValue);

            // Display the encrypted image in pictureBox2
            //i will update current status here to be displayed
            currentStatus = ImageStatus.Encrypted;
            UpdateImageStatus(currentStatus);
            ImageOperations.DisplayImage(encryptedImage, pictureBox2);
            string imagePath= "D:\\Collegue\\6th semester\\Algorithm\\Project\\[1] Image Encryption and Compression\\Sample Test\\SampleCases_Encryption\\OUTPUT\\Sample2Output.bmp";
            
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image Files (*.jpg;*.jpeg;*.png;*.bmp)|*.jpg;*.jpeg;*.png;*.bmp|All files (*.*)|*.*";
               
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                     imagePath = openFileDialog.FileName;
                }
            }
            
            RGBPixel[,] stored_encrypted_image = ImageOperations.OpenImage(imagePath);
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

        private void label3_Click(object sender, EventArgs e)
        {

        }


        //--------------------------------------------------------------------------------------------------
        private void ClearRichTextBoxOriginalImage()
        {
            if (InvokeRequired)
            {
                Invoke((Action)(() => image_pixels_info.Clear()));
                //Invoke((Action)(() => result_encrypted_pixels.Clear()));
            }
            else
            {
                image_pixels_info.Clear();
                //   result_encrypted_pixels.Clear();
            }
        }
        private void AppendTextSafeOriginalImage(string text)
        {
            if (InvokeRequired)
            {
                Invoke((Action)(() => image_pixels_info.AppendText(text)));
                //  Invoke((Action)(() => result_encrypted_pixels.AppendText(text)));
            }
            else
            {
                image_pixels_info.AppendText(text);
                //   result_encrypted_pixels.AppendText(text);
            }
        }
        public  void ExtractPixelColorsOriginalImage(string imagePath)
        {
            // Load the image
            Bitmap image = new Bitmap(imagePath);

            // Clear previous content in RichTextBox
            ClearRichTextBoxOriginalImage();


            // Display pixel colors
            for (int y = 0; y < image.Height; y++)
            {
                for (int x = 0; x < image.Width; x++)
                {
                    Color pixelColor = image.GetPixel(x, y);
                    AppendTextSafeOriginalImage($"Pixel at ({x}, {y}): R={pixelColor.R}, G={pixelColor.G}, B={pixelColor.B}\n");
                }
            }
            MessageBox.Show("Pixels are displayed");
            // Dispose the image
            image.Dispose();
        }


        //helper 
       
      
        private void ClearRichTextBoxEncryptedImage()
        {
            if (InvokeRequired)
            {
                Invoke((Action)(() => image_pixels_info.Clear()));
                //Invoke((Action)(() => result_encrypted_pixels.Clear()));
            }
            else
            {
                image_pixels_info.Clear();
                //   result_encrypted_pixels.Clear();
            }
        }
        private void AppendTextSafeEncryptedImage(string text)
        {
            if (InvokeRequired)
            {
                //Invoke((Action)(() => image_pixels_info.AppendText(text)));
                  Invoke((Action)(() => result_encrypted_pixels.AppendText(text)));
            }
            else
            {
                //image_pixels_info.AppendText(text);
                   result_encrypted_pixels.AppendText(text);
            }
        }
        public void ExtractPixelColorsEncryptedImage(string imagePath)
        {
            // Load the image
            Bitmap image = new Bitmap(imagePath);

            // Clear previous content in RichTextBox
            ClearRichTextBoxEncryptedImage();


            // Display pixel colors
            for (int y = 0; y < image.Height; y++)
            {
                for (int x = 0; x < image.Width; x++)
                {
                    Color pixelColor = image.GetPixel(x, y);
                    AppendTextSafeEncryptedImage($"Pixel at ({x}, {y}): R={pixelColor.R}, G={pixelColor.G}, B={pixelColor.B}\n");
                }
            }
            MessageBox.Show("Encrypted Pixels are displayed succsesfully");
            // Dispose the image
            image.Dispose();
        }
        private async void button1_Click_1(object sender, EventArgs e)  // display pixels of encrypted 
        {
            // Open file dialog to select an image
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image Files (*.jpg;*.jpeg;*.png;*.bmp)|*.jpg;*.jpeg;*.png;*.bmp|All files (*.*)|*.*";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string imagePath = openFileDialog.FileName;

                    // Call the function to extract pixel colors and display them
                    await Task.Run(() => ExtractPixelColorsEncryptedImage(imagePath));
                }
            }
        }

        
        private async void Display_image_info_Click(object sender, EventArgs e) //  display pixels original image
        {
            // Open file dialog to select an image
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image Files (*.jpg;*.jpeg;*.png;*.bmp)|*.jpg;*.jpeg;*.png;*.bmp|All files (*.*)|*.*";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string imagePath = openFileDialog.FileName;

                    // Call the function to extract pixel colors and display them
                    await Task.Run(() => ExtractPixelColorsOriginalImage(imagePath));
                }
            }
        }

        private void image_pixels_info_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

       
        private void result_encrypted_pixels_TextChanged(object sender, EventArgs e)
        {

        }
    }
}