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
            this.compareImagesButton = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.Encrypt_button = new System.Windows.Forms.Button();
            this.initial_seed = new System.Windows.Forms.TextBox();
            this.Label_initial_seed = new System.Windows.Forms.Label();
            this.Label_tap_position = new System.Windows.Forms.Label();
            this.tap_position = new System.Windows.Forms.TextBox();
            this.Decrypt_button = new System.Windows.Forms.Button();
            this.Compressbtn = new System.Windows.Forms.Button();
            this.CompressExistedImage = new System.Windows.Forms.RadioButton();
            this.Decompressbtn = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.breakNTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.breakEncryptionButton = new System.Windows.Forms.Button();
            this.openPictureBox2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
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
            this.btnOpen.Click += new System.EventHandler(this.OpenImageButton_Click);
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
            // compareImagesButton
            // 
            this.compareImagesButton.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.compareImagesButton.Location = new System.Drawing.Point(584, 267);
            this.compareImagesButton.Name = "compareImagesButton";
            this.compareImagesButton.Size = new System.Drawing.Size(109, 72);
            this.compareImagesButton.TabIndex = 5;
            this.compareImagesButton.Text = "Compare Images";
            this.compareImagesButton.UseVisualStyleBackColor = true;
            this.compareImagesButton.Click += new System.EventHandler(this.CompareImagesButton_Click);
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
            // 
            // Encrypt_button
            // 
            this.Encrypt_button.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Encrypt_button.Location = new System.Drawing.Point(510, 176);
            this.Encrypt_button.Name = "Encrypt_button";
            this.Encrypt_button.Size = new System.Drawing.Size(94, 37);
            this.Encrypt_button.TabIndex = 17;
            this.Encrypt_button.Text = "Encrypt";
            this.Encrypt_button.UseVisualStyleBackColor = true;
            this.Encrypt_button.Click += new System.EventHandler(this.EncryptButton_Click);
            // 
            // initial_seed
            // 
            this.initial_seed.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.initial_seed.Location = new System.Drawing.Point(658, 70);
            this.initial_seed.Name = "initial_seed";
            this.initial_seed.Size = new System.Drawing.Size(155, 24);
            this.initial_seed.TabIndex = 19;
            // 
            // Label_initial_seed
            // 
            this.Label_initial_seed.AutoSize = true;
            this.Label_initial_seed.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label_initial_seed.Location = new System.Drawing.Point(491, 65);
            this.Label_initial_seed.Name = "Label_initial_seed";
            this.Label_initial_seed.Size = new System.Drawing.Size(161, 24);
            this.Label_initial_seed.TabIndex = 20;
            this.Label_initial_seed.Text = "Enter Initial Seed :";
            // 
            // Label_tap_position
            // 
            this.Label_tap_position.AutoSize = true;
            this.Label_tap_position.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label_tap_position.Location = new System.Drawing.Point(491, 129);
            this.Label_tap_position.Name = "Label_tap_position";
            this.Label_tap_position.Size = new System.Drawing.Size(168, 24);
            this.Label_tap_position.TabIndex = 21;
            this.Label_tap_position.Text = "Enter Tap position:";
            // 
            // tap_position
            // 
            this.tap_position.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tap_position.Location = new System.Drawing.Point(658, 130);
            this.tap_position.Name = "tap_position";
            this.tap_position.Size = new System.Drawing.Size(155, 24);
            this.tap_position.TabIndex = 22;
            // 
            // Decrypt_button
            // 
            this.Decrypt_button.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Decrypt_button.Location = new System.Drawing.Point(658, 176);
            this.Decrypt_button.Name = "Decrypt_button";
            this.Decrypt_button.Size = new System.Drawing.Size(94, 37);
            this.Decrypt_button.TabIndex = 23;
            this.Decrypt_button.Text = "Decrypt";
            this.Decrypt_button.UseVisualStyleBackColor = true;
            this.Decrypt_button.Click += new System.EventHandler(this.DecryptButton_Click);
            // 
            // Compressbtn
            // 
            this.Compressbtn.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.Compressbtn.Location = new System.Drawing.Point(904, 425);
            this.Compressbtn.Name = "Compressbtn";
            this.Compressbtn.Size = new System.Drawing.Size(97, 69);
            this.Compressbtn.TabIndex = 24;
            this.Compressbtn.Text = "Compress";
            this.Compressbtn.UseVisualStyleBackColor = true;
            this.Compressbtn.Click += new System.EventHandler(this.compressbtn_Click);
            // 
            // CompressExistedImage
            // 
            this.CompressExistedImage.AutoSize = true;
            this.CompressExistedImage.Checked = true;
            this.CompressExistedImage.Location = new System.Drawing.Point(1017, 437);
            this.CompressExistedImage.Name = "CompressExistedImage";
            this.CompressExistedImage.Size = new System.Drawing.Size(122, 17);
            this.CompressExistedImage.TabIndex = 25;
            this.CompressExistedImage.TabStop = true;
            this.CompressExistedImage.Text = "Compress this Image";
            this.CompressExistedImage.UseVisualStyleBackColor = true;
            // 
            // Decompressbtn
            // 
            this.Decompressbtn.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.Decompressbtn.Location = new System.Drawing.Point(904, 512);
            this.Decompressbtn.Name = "Decompressbtn";
            this.Decompressbtn.Size = new System.Drawing.Size(117, 69);
            this.Decompressbtn.TabIndex = 27;
            this.Decompressbtn.Text = "Decompress";
            this.Decompressbtn.UseVisualStyleBackColor = true;
            this.Decompressbtn.Click += new System.EventHandler(this.Decompressbtn_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(510, 529);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 25);
            this.label2.TabIndex = 28;
            this.label2.Text = "Enter N";
            // 
            // breakNTextBox
            // 
            this.breakNTextBox.Location = new System.Drawing.Point(600, 531);
            this.breakNTextBox.Name = "breakNTextBox";
            this.breakNTextBox.Size = new System.Drawing.Size(103, 20);
            this.breakNTextBox.TabIndex = 29;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(466, 442);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(373, 31);
            this.label3.TabIndex = 30;
            this.label3.Text = "Breaking the encrypted image";
            // 
            // breakEncryptionButton
            // 
            this.breakEncryptionButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.breakEncryptionButton.Location = new System.Drawing.Point(708, 524);
            this.breakEncryptionButton.Name = "breakEncryptionButton";
            this.breakEncryptionButton.Size = new System.Drawing.Size(105, 30);
            this.breakEncryptionButton.TabIndex = 31;
            this.breakEncryptionButton.Text = "Break Image";
            this.breakEncryptionButton.UseVisualStyleBackColor = true;
            this.breakEncryptionButton.Click += new System.EventHandler(this.breakEncryptionButton_Click);
            // 
            // openPictureBox2
            // 
            this.openPictureBox2.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.openPictureBox2.Location = new System.Drawing.Point(1167, 389);
            this.openPictureBox2.Name = "openPictureBox2";
            this.openPictureBox2.Size = new System.Drawing.Size(94, 72);
            this.openPictureBox2.TabIndex = 32;
            this.openPictureBox2.Text = "Open Image";
            this.openPictureBox2.UseVisualStyleBackColor = true;
            this.openPictureBox2.Click += new System.EventHandler(this.openPictureBox2_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Silver;
            this.ClientSize = new System.Drawing.Size(1335, 681);
            this.Controls.Add(this.openPictureBox2);
            this.Controls.Add(this.breakEncryptionButton);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.breakNTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Decompressbtn);
            this.Controls.Add(this.CompressExistedImage);
            this.Controls.Add(this.Compressbtn);
            this.Controls.Add(this.Decrypt_button);
            this.Controls.Add(this.tap_position);
            this.Controls.Add(this.Label_tap_position);
            this.Controls.Add(this.Label_initial_seed);
            this.Controls.Add(this.initial_seed);
            this.Controls.Add(this.Encrypt_button);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.compareImagesButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnOpen);
            this.Name = "MainForm";
            this.Text = "Image Enctryption and Compression...";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
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
        private System.Windows.Forms.Button compareImagesButton;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button Encrypt_button;
        private System.Windows.Forms.TextBox initial_seed;
        private System.Windows.Forms.Label Label_initial_seed;
        private System.Windows.Forms.Label Label_tap_position;
        private System.Windows.Forms.TextBox tap_position;
        private System.Windows.Forms.Button Decrypt_button;
        private System.Windows.Forms.Button Compressbtn;
        private System.Windows.Forms.RadioButton CompressExistedImage;
        private System.Windows.Forms.Button Decompressbtn;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox breakNTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button breakEncryptionButton;
        private System.Windows.Forms.Button openPictureBox2;
    }
}

