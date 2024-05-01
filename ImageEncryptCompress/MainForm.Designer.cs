namespace ImageEncryptCompress
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.btnOpen = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnGaussSmooth = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtHeight = new System.Windows.Forms.TextBox();
            this.nudMaskSize = new System.Windows.Forms.NumericUpDown();
            this.txtWidth = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtGaussSigma = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.Encrypt_button = new System.Windows.Forms.Button();
            this.initial_seed = new System.Windows.Forms.TextBox();
            this.Label_initial_seed = new System.Windows.Forms.Label();
            this.Label_tap_position = new System.Windows.Forms.Label();
            this.tap_position = new System.Windows.Forms.TextBox();
            this.Decrypt_button = new System.Windows.Forms.Button();
            this.image_status = new System.Windows.Forms.TextBox();
            this.image_pixels_info = new System.Windows.Forms.RichTextBox();
            this.LabelForImagePixelsInfo = new System.Windows.Forms.Label();
            this.Display_image_info = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.Display_encrypted_pixels = new System.Windows.Forms.Button();
            this.result_encrypted_pixels = new System.Windows.Forms.RichTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMaskSize)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.White;
            this.pictureBox1.Location = new System.Drawing.Point(3, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(427, 360);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.White;
            this.pictureBox2.Location = new System.Drawing.Point(2, 3);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(412, 360);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox2.TabIndex = 1;
            this.pictureBox2.TabStop = false;
            // 
            // btnOpen
            // 
            this.btnOpen.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOpen.Location = new System.Drawing.Point(183, 425);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(94, 72);
            this.btnOpen.TabIndex = 2;
            this.btnOpen.Text = "Open Image";
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(163, 393);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(130, 19);
            this.label1.TabIndex = 3;
            this.label1.Text = "Original Image";
            // 
            // btnGaussSmooth
            // 
            this.btnGaussSmooth.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGaussSmooth.Location = new System.Drawing.Point(460, 212);
            this.btnGaussSmooth.Name = "btnGaussSmooth";
            this.btnGaussSmooth.Size = new System.Drawing.Size(109, 72);
            this.btnGaussSmooth.TabIndex = 5;
            this.btnGaussSmooth.Text = "Apply Operation (Example)";
            this.btnGaussSmooth.UseVisualStyleBackColor = true;
            this.btnGaussSmooth.Click += new System.EventHandler(this.btnGaussSmooth_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(591, 212);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 16);
            this.label3.TabIndex = 7;
            this.label3.Text = "Mask Size";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(591, 255);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(87, 16);
            this.label4.TabIndex = 9;
            this.label4.Text = "Gauss Sigma";
            // 
            // txtHeight
            // 
            this.txtHeight.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtHeight.Location = new System.Drawing.Point(92, 466);
            this.txtHeight.Name = "txtHeight";
            this.txtHeight.ReadOnly = true;
            this.txtHeight.Size = new System.Drawing.Size(57, 23);
            this.txtHeight.TabIndex = 8;
            // 
            // nudMaskSize
            // 
            this.nudMaskSize.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nudMaskSize.Increment = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.nudMaskSize.Location = new System.Drawing.Point(685, 212);
            this.nudMaskSize.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.nudMaskSize.Minimum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.nudMaskSize.Name = "nudMaskSize";
            this.nudMaskSize.Size = new System.Drawing.Size(57, 23);
            this.nudMaskSize.TabIndex = 10;
            this.nudMaskSize.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // txtWidth
            // 
            this.txtWidth.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtWidth.Location = new System.Drawing.Point(92, 425);
            this.txtWidth.Name = "txtWidth";
            this.txtWidth.ReadOnly = true;
            this.txtWidth.Size = new System.Drawing.Size(57, 23);
            this.txtWidth.TabIndex = 11;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(37, 428);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(45, 16);
            this.label5.TabIndex = 12;
            this.label5.Text = "Width";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(37, 469);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(49, 16);
            this.label6.TabIndex = 13;
            this.label6.Text = "Height";
            // 
            // txtGaussSigma
            // 
            this.txtGaussSigma.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtGaussSigma.Location = new System.Drawing.Point(685, 248);
            this.txtGaussSigma.Name = "txtGaussSigma";
            this.txtGaussSigma.Size = new System.Drawing.Size(57, 23);
            this.txtGaussSigma.TabIndex = 14;
            this.txtGaussSigma.Text = "1";
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.AutoScrollMinSize = new System.Drawing.Size(1, 1);
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(438, 371);
            this.panel1.TabIndex = 15;
            // 
            // panel2
            // 
            this.panel2.AutoScroll = true;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Controls.Add(this.pictureBox2);
            this.panel2.Location = new System.Drawing.Point(845, 12);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(421, 371);
            this.panel2.TabIndex = 16;
            this.panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.panel2_Paint);
            // 
            // Encrypt_button
            // 
            this.Encrypt_button.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Encrypt_button.Location = new System.Drawing.Point(475, 140);
            this.Encrypt_button.Name = "Encrypt_button";
            this.Encrypt_button.Size = new System.Drawing.Size(94, 37);
            this.Encrypt_button.TabIndex = 17;
            this.Encrypt_button.Text = "Encrypt";
            this.Encrypt_button.UseVisualStyleBackColor = true;
            this.Encrypt_button.Click += new System.EventHandler(this.button1_Click);
            // 
            // initial_seed
            // 
            this.initial_seed.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.initial_seed.Location = new System.Drawing.Point(623, 34);
            this.initial_seed.Name = "initial_seed";
            this.initial_seed.Size = new System.Drawing.Size(155, 24);
            this.initial_seed.TabIndex = 19;
            this.initial_seed.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // Label_initial_seed
            // 
            this.Label_initial_seed.AutoSize = true;
            this.Label_initial_seed.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label_initial_seed.Location = new System.Drawing.Point(456, 29);
            this.Label_initial_seed.Name = "Label_initial_seed";
            this.Label_initial_seed.Size = new System.Drawing.Size(161, 24);
            this.Label_initial_seed.TabIndex = 20;
            this.Label_initial_seed.Text = "Enter Initial Seed :";
            // 
            // Label_tap_position
            // 
            this.Label_tap_position.AutoSize = true;
            this.Label_tap_position.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label_tap_position.Location = new System.Drawing.Point(456, 93);
            this.Label_tap_position.Name = "Label_tap_position";
            this.Label_tap_position.Size = new System.Drawing.Size(168, 24);
            this.Label_tap_position.TabIndex = 21;
            this.Label_tap_position.Text = "Enter Tap position:";
            this.Label_tap_position.Click += new System.EventHandler(this.label7_Click);
            // 
            // tap_position
            // 
            this.tap_position.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tap_position.Location = new System.Drawing.Point(623, 94);
            this.tap_position.Name = "tap_position";
            this.tap_position.Size = new System.Drawing.Size(155, 24);
            this.tap_position.TabIndex = 22;
            // 
            // Decrypt_button
            // 
            this.Decrypt_button.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Decrypt_button.Location = new System.Drawing.Point(623, 140);
            this.Decrypt_button.Name = "Decrypt_button";
            this.Decrypt_button.Size = new System.Drawing.Size(94, 37);
            this.Decrypt_button.TabIndex = 23;
            this.Decrypt_button.Text = "Decrypt";
            this.Decrypt_button.UseVisualStyleBackColor = true;
            this.Decrypt_button.Click += new System.EventHandler(this.Decrypt_button_Click);
            // 
            // image_status
            // 
            this.image_status.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.image_status.Location = new System.Drawing.Point(941, 393);
            this.image_status.Name = "image_status";
            this.image_status.Size = new System.Drawing.Size(246, 35);
            this.image_status.TabIndex = 24;
            // 
            // image_pixels_info
            // 
            this.image_pixels_info.Location = new System.Drawing.Point(895, 498);
            this.image_pixels_info.Name = "image_pixels_info";
            this.image_pixels_info.Size = new System.Drawing.Size(311, 281);
            this.image_pixels_info.TabIndex = 25;
            this.image_pixels_info.Text = "";
            this.image_pixels_info.TextChanged += new System.EventHandler(this.image_pixels_info_TextChanged);
            // 
            // LabelForImagePixelsInfo
            // 
            this.LabelForImagePixelsInfo.AutoSize = true;
            this.LabelForImagePixelsInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelForImagePixelsInfo.Location = new System.Drawing.Point(901, 466);
            this.LabelForImagePixelsInfo.Name = "LabelForImagePixelsInfo";
            this.LabelForImagePixelsInfo.Size = new System.Drawing.Size(186, 24);
            this.LabelForImagePixelsInfo.TabIndex = 26;
            this.LabelForImagePixelsInfo.Text = "Original Image pixels";
            // 
            // Display_image_info
            // 
            this.Display_image_info.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Display_image_info.Location = new System.Drawing.Point(1112, 455);
            this.Display_image_info.Name = "Display_image_info";
            this.Display_image_info.Size = new System.Drawing.Size(94, 37);
            this.Display_image_info.TabIndex = 27;
            this.Display_image_info.Text = "Display";
            this.Display_image_info.UseVisualStyleBackColor = true;
            this.Display_image_info.Click += new System.EventHandler(this.Display_image_info_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(1249, 469);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(212, 24);
            this.label2.TabIndex = 28;
            this.label2.Text = "Encrypted Image pixels \r\n";
            // 
            // Display_encrypted_pixels
            // 
            this.Display_encrypted_pixels.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Display_encrypted_pixels.Location = new System.Drawing.Point(1462, 460);
            this.Display_encrypted_pixels.Name = "Display_encrypted_pixels";
            this.Display_encrypted_pixels.Size = new System.Drawing.Size(94, 37);
            this.Display_encrypted_pixels.TabIndex = 29;
            this.Display_encrypted_pixels.Text = "Display";
            this.Display_encrypted_pixels.UseVisualStyleBackColor = true;
            this.Display_encrypted_pixels.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // result_encrypted_pixels
            // 
            this.result_encrypted_pixels.Location = new System.Drawing.Point(1245, 501);
            this.result_encrypted_pixels.Name = "result_encrypted_pixels";
            this.result_encrypted_pixels.Size = new System.Drawing.Size(311, 281);
            this.result_encrypted_pixels.TabIndex = 30;
            this.result_encrypted_pixels.Text = "";
            this.result_encrypted_pixels.TextChanged += new System.EventHandler(this.result_encrypted_pixels_TextChanged);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Silver;
            this.ClientSize = new System.Drawing.Size(1650, 1027);
            this.Controls.Add(this.result_encrypted_pixels);
            this.Controls.Add(this.Display_encrypted_pixels);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Display_image_info);
            this.Controls.Add(this.LabelForImagePixelsInfo);
            this.Controls.Add(this.image_pixels_info);
            this.Controls.Add(this.image_status);
            this.Controls.Add(this.Decrypt_button);
            this.Controls.Add(this.tap_position);
            this.Controls.Add(this.Label_tap_position);
            this.Controls.Add(this.Label_initial_seed);
            this.Controls.Add(this.initial_seed);
            this.Controls.Add(this.Encrypt_button);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.txtGaussSigma);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtWidth);
            this.Controls.Add(this.nudMaskSize);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtHeight);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnGaussSmooth);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnOpen);
            this.Name = "MainForm";
            this.Text = "Image Enctryption and Compression...";
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMaskSize)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnGaussSmooth;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtHeight;
        private System.Windows.Forms.NumericUpDown nudMaskSize;
        private System.Windows.Forms.TextBox txtWidth;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtGaussSigma;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button Encrypt_button;
        private System.Windows.Forms.TextBox initial_seed;
        private System.Windows.Forms.Label Label_initial_seed;
        private System.Windows.Forms.Label Label_tap_position;
        private System.Windows.Forms.TextBox tap_position;
        private System.Windows.Forms.Button Decrypt_button;
        private System.Windows.Forms.TextBox image_status;
        private System.Windows.Forms.RichTextBox image_pixels_info;
        private System.Windows.Forms.Label LabelForImagePixelsInfo;
        private System.Windows.Forms.Button Display_image_info;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button Display_encrypted_pixels;
        private System.Windows.Forms.RichTextBox result_encrypted_pixels;
    }
}

