namespace DipCs
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            pictureBox1 = new PictureBox();
            pictureBox2 = new PictureBox();
            btnLoadImage = new Button();
            openFileDialog_LoadImage = new OpenFileDialog();
            btnSaveImage = new Button();
            btnBasicCopy = new Button();
            btnGrayScale = new Button();
            btnInvert = new Button();
            btnHistogram = new Button();
            btnSepia = new Button();
            saveFileDialog1 = new SaveFileDialog();
            label1 = new Label();
            label3 = new Label();
            label4 = new Label();
            label2 = new Label();
            label5 = new Label();
            label6 = new Label();
            label7 = new Label();
            btnFlipHorizontal = new Button();
            btnFlipVertical = new Button();
            btnSubtract = new Button();
            btnLoadBackgroundImage = new Button();
            pictureBox3 = new PictureBox();
            label8 = new Label();
            trackBarBrightness = new TrackBar();
            trackBarContrast = new TrackBar();
            trackBarRotate = new TrackBar();
            btnScale = new Button();
            textBoxScaleWidth = new TextBox();
            textBoxScaleHeight = new TextBox();
            openFileDialog_LoadBackgroundImage = new OpenFileDialog();
            label9 = new Label();
            label10 = new Label();
            label11 = new Label();
            btnLoadWebcam = new Button();
            btnReset = new Button();
            label12 = new Label();
            timer1 = new System.Windows.Forms.Timer(components);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBarBrightness).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBarContrast).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBarRotate).BeginInit();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.BorderStyle = BorderStyle.FixedSingle;
            pictureBox1.Location = new Point(50, 55);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(400, 400);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            pictureBox2.BorderStyle = BorderStyle.FixedSingle;
            pictureBox2.Location = new Point(486, 55);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(400, 400);
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox2.TabIndex = 0;
            pictureBox2.TabStop = false;
            // 
            // btnLoadImage
            // 
            btnLoadImage.Location = new Point(59, 585);
            btnLoadImage.Name = "btnLoadImage";
            btnLoadImage.Size = new Size(115, 48);
            btnLoadImage.TabIndex = 1;
            btnLoadImage.Text = "Load Image";
            btnLoadImage.UseVisualStyleBackColor = true;
            btnLoadImage.Click += btnLoadImage_Click;
            // 
            // openFileDialog_LoadImage
            // 
            openFileDialog_LoadImage.FileName = "openFileDialog1";
            openFileDialog_LoadImage.FileOk += openFileDialog1_FileOk_LoadImage;
            // 
            // btnSaveImage
            // 
            btnSaveImage.Location = new Point(59, 639);
            btnSaveImage.Name = "btnSaveImage";
            btnSaveImage.Size = new Size(180, 48);
            btnSaveImage.TabIndex = 2;
            btnSaveImage.Text = "Save Processed Image";
            btnSaveImage.UseVisualStyleBackColor = true;
            btnSaveImage.Click += btnSaveImage_Click;
            // 
            // btnBasicCopy
            // 
            btnBasicCopy.Location = new Point(487, 585);
            btnBasicCopy.Name = "btnBasicCopy";
            btnBasicCopy.Size = new Size(115, 48);
            btnBasicCopy.TabIndex = 3;
            btnBasicCopy.Text = "Basic Copy";
            btnBasicCopy.UseVisualStyleBackColor = true;
            btnBasicCopy.Click += btnBasicCopy_Click;
            // 
            // btnGrayScale
            // 
            btnGrayScale.Location = new Point(608, 585);
            btnGrayScale.Name = "btnGrayScale";
            btnGrayScale.Size = new Size(115, 48);
            btnGrayScale.TabIndex = 4;
            btnGrayScale.Text = "Grayscale";
            btnGrayScale.UseVisualStyleBackColor = true;
            btnGrayScale.Click += btnGrayScale_Click;
            // 
            // btnInvert
            // 
            btnInvert.Location = new Point(729, 585);
            btnInvert.Name = "btnInvert";
            btnInvert.Size = new Size(115, 48);
            btnInvert.TabIndex = 5;
            btnInvert.Text = "Invert";
            btnInvert.UseVisualStyleBackColor = true;
            btnInvert.Click += btnInvert_Click;
            // 
            // btnHistogram
            // 
            btnHistogram.Location = new Point(850, 585);
            btnHistogram.Name = "btnHistogram";
            btnHistogram.Size = new Size(115, 48);
            btnHistogram.TabIndex = 6;
            btnHistogram.Text = "Histogram";
            btnHistogram.UseVisualStyleBackColor = true;
            btnHistogram.Click += btnHistogram_Click;
            // 
            // btnSepia
            // 
            btnSepia.Location = new Point(487, 639);
            btnSepia.Name = "btnSepia";
            btnSepia.Size = new Size(115, 48);
            btnSepia.TabIndex = 7;
            btnSepia.Text = "Sepia";
            btnSepia.UseVisualStyleBackColor = true;
            btnSepia.Click += btnSepia_Click;
            // 
            // saveFileDialog1
            // 
            saveFileDialog1.FileOk += saveFileDialog1_FileOk;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(148, 458);
            label1.Name = "label1";
            label1.Size = new Size(212, 20);
            label1.TabIndex = 8;
            label1.Text = " Original Image/Video Preview";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(487, 544);
            label3.Name = "label3";
            label3.Size = new Size(250, 20);
            label3.TabIndex = 10;
            label3.Text = "Image/Video Processing Operations:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(50, 544);
            label4.Name = "label4";
            label4.Size = new Size(158, 20);
            label4.TabIndex = 11;
            label4.Text = "Image File Operations:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(569, 458);
            label2.Name = "label2";
            label2.Size = new Size(221, 20);
            label2.TabIndex = 12;
            label2.Text = "Processed Image/Video Preview";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(486, 757);
            label5.Name = "label5";
            label5.Size = new Size(77, 20);
            label5.TabIndex = 13;
            label5.Text = "Brightness";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(486, 810);
            label6.Name = "label6";
            label6.Size = new Size(64, 20);
            label6.TabIndex = 14;
            label6.Text = "Contrast";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(486, 863);
            label7.Name = "label7";
            label7.Size = new Size(53, 20);
            label7.TabIndex = 15;
            label7.Text = "Rotate";
            // 
            // btnFlipHorizontal
            // 
            btnFlipHorizontal.Location = new Point(608, 639);
            btnFlipHorizontal.Name = "btnFlipHorizontal";
            btnFlipHorizontal.Size = new Size(115, 48);
            btnFlipHorizontal.TabIndex = 16;
            btnFlipHorizontal.Text = "Flip Horizontal";
            btnFlipHorizontal.UseVisualStyleBackColor = true;
            btnFlipHorizontal.Click += btnFlipHorizontal_Click;
            // 
            // btnFlipVertical
            // 
            btnFlipVertical.Location = new Point(729, 639);
            btnFlipVertical.Name = "btnFlipVertical";
            btnFlipVertical.Size = new Size(115, 48);
            btnFlipVertical.TabIndex = 17;
            btnFlipVertical.Text = "Flip Vertical";
            btnFlipVertical.UseVisualStyleBackColor = true;
            btnFlipVertical.Click += btnFlipVertical_Click;
            // 
            // btnSubtract
            // 
            btnSubtract.Location = new Point(850, 639);
            btnSubtract.Name = "btnSubtract";
            btnSubtract.Size = new Size(115, 48);
            btnSubtract.TabIndex = 18;
            btnSubtract.Text = "Subtract";
            btnSubtract.UseVisualStyleBackColor = true;
            btnSubtract.Click += btnSubtract_Click;
            // 
            // btnLoadBackgroundImage
            // 
            btnLoadBackgroundImage.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            btnLoadBackgroundImage.Location = new Point(180, 585);
            btnLoadBackgroundImage.Name = "btnLoadBackgroundImage";
            btnLoadBackgroundImage.Size = new Size(180, 48);
            btnLoadBackgroundImage.TabIndex = 19;
            btnLoadBackgroundImage.Text = "Load Background Image";
            btnLoadBackgroundImage.UseVisualStyleBackColor = true;
            btnLoadBackgroundImage.Click += btnLoadBackgroundImage_Click;
            // 
            // pictureBox3
            // 
            pictureBox3.BorderStyle = BorderStyle.FixedSingle;
            pictureBox3.Location = new Point(920, 55);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(400, 400);
            pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox3.TabIndex = 20;
            pictureBox3.TabStop = false;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(974, 458);
            label8.Name = "label8";
            label8.Size = new Size(292, 20);
            label8.TabIndex = 21;
            label8.Text = "Background Image Preview for Subtraction";
            // 
            // trackBarBrightness
            // 
            trackBarBrightness.Location = new Point(569, 757);
            trackBarBrightness.Maximum = 100;
            trackBarBrightness.Minimum = -100;
            trackBarBrightness.Name = "trackBarBrightness";
            trackBarBrightness.Size = new Size(516, 56);
            trackBarBrightness.TabIndex = 22;
            trackBarBrightness.Scroll += trackBarBrightness_Scroll;
            // 
            // trackBarContrast
            // 
            trackBarContrast.Location = new Point(569, 810);
            trackBarContrast.Maximum = 100;
            trackBarContrast.Minimum = -100;
            trackBarContrast.Name = "trackBarContrast";
            trackBarContrast.Size = new Size(516, 56);
            trackBarContrast.TabIndex = 23;
            trackBarContrast.Scroll += trackBarContrast_Scroll;
            // 
            // trackBarRotate
            // 
            trackBarRotate.Location = new Point(569, 863);
            trackBarRotate.Maximum = 180;
            trackBarRotate.Minimum = -180;
            trackBarRotate.Name = "trackBarRotate";
            trackBarRotate.Size = new Size(516, 56);
            trackBarRotate.TabIndex = 24;
            trackBarRotate.Scroll += trackBarRotate_Scroll;
            // 
            // btnScale
            // 
            btnScale.Location = new Point(487, 693);
            btnScale.Name = "btnScale";
            btnScale.Size = new Size(115, 48);
            btnScale.TabIndex = 25;
            btnScale.Text = "Scale";
            btnScale.UseVisualStyleBackColor = true;
            btnScale.Click += btnScale_Click;
            // 
            // textBoxScaleWidth
            // 
            textBoxScaleWidth.Location = new Point(608, 704);
            textBoxScaleWidth.Name = "textBoxScaleWidth";
            textBoxScaleWidth.PlaceholderText = "Enter width";
            textBoxScaleWidth.Size = new Size(84, 27);
            textBoxScaleWidth.TabIndex = 26;
            textBoxScaleWidth.Tag = "";
            // 
            // textBoxScaleHeight
            // 
            textBoxScaleHeight.Location = new Point(738, 704);
            textBoxScaleHeight.Name = "textBoxScaleHeight";
            textBoxScaleHeight.PlaceholderText = "Enter height";
            textBoxScaleHeight.Size = new Size(86, 27);
            textBoxScaleHeight.TabIndex = 27;
            // 
            // openFileDialog_LoadBackgroundImage
            // 
            openFileDialog_LoadBackgroundImage.FileName = "openFileDialog2";
            openFileDialog_LoadBackgroundImage.FileOk += openFileDialog2_FileOk_LoadBackgroundImage;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.BackColor = Color.Transparent;
            label9.Location = new Point(698, 707);
            label9.Name = "label9";
            label9.Size = new Size(25, 20);
            label9.TabIndex = 28;
            label9.Text = "px";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(828, 707);
            label10.Name = "label10";
            label10.Size = new Size(25, 20);
            label10.TabIndex = 29;
            label10.Text = "px";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(50, 721);
            label11.Name = "label11";
            label11.Size = new Size(165, 20);
            label11.TabIndex = 30;
            label11.Text = "Webcam/Video Option:";
            // 
            // btnLoadWebcam
            // 
            btnLoadWebcam.Location = new Point(59, 757);
            btnLoadWebcam.Name = "btnLoadWebcam";
            btnLoadWebcam.Size = new Size(180, 48);
            btnLoadWebcam.TabIndex = 31;
            btnLoadWebcam.Text = "Load Webcam/Camera";
            btnLoadWebcam.UseVisualStyleBackColor = true;
            btnLoadWebcam.Click += btnLoadWebcam_Click;
            // 
            // btnReset
            // 
            btnReset.Location = new Point(59, 855);
            btnReset.Name = "btnReset";
            btnReset.Size = new Size(115, 48);
            btnReset.TabIndex = 32;
            btnReset.Text = "Reset";
            btnReset.UseVisualStyleBackColor = true;
            btnReset.Click += btnReset_Click;
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new Point(50, 832);
            label12.Name = "label12";
            label12.Size = new Size(99, 20);
            label12.TabIndex = 33;
            label12.Text = "Other Option:";
            // 
            // timer1
            // 
            timer1.Interval = 10;
            timer1.Tick += timer1_Tick;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1371, 947);
            Controls.Add(label12);
            Controls.Add(btnReset);
            Controls.Add(btnLoadWebcam);
            Controls.Add(label11);
            Controls.Add(label10);
            Controls.Add(label9);
            Controls.Add(textBoxScaleHeight);
            Controls.Add(textBoxScaleWidth);
            Controls.Add(btnScale);
            Controls.Add(trackBarRotate);
            Controls.Add(trackBarContrast);
            Controls.Add(trackBarBrightness);
            Controls.Add(label8);
            Controls.Add(pictureBox3);
            Controls.Add(btnLoadBackgroundImage);
            Controls.Add(btnSubtract);
            Controls.Add(btnFlipVertical);
            Controls.Add(btnFlipHorizontal);
            Controls.Add(label7);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label2);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label1);
            Controls.Add(btnSepia);
            Controls.Add(btnHistogram);
            Controls.Add(btnInvert);
            Controls.Add(btnGrayScale);
            Controls.Add(btnBasicCopy);
            Controls.Add(btnSaveImage);
            Controls.Add(btnLoadImage);
            Controls.Add(pictureBox2);
            Controls.Add(pictureBox1);
            Name = "Form1";
            Text = "Basic Image Processing Program";
            FormClosing += Form1_FormClosing;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBarBrightness).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBarContrast).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBarRotate).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pictureBox1;
        private Button btnLoadImage;
        private OpenFileDialog openFileDialog_LoadImage;
        private Button btnSaveImage;
        private Button btnBasicCopy;
        private Button btnGrayScale;
        private Button btnInvert;
        private Button btnHistogram;
        private Button btnSepia;
        private SaveFileDialog saveFileDialog1;
        private Label label1;
        private Label label3;
        private Label label4;
        private Label label2;
        private Label label5;
        private Label label6;
        private Label label7;
        private Button btnFlipHorizontal;
        private Button btnFlipVertical;
        private Button btnSubtract;
        private Button btnLoadBackgroundImage;
        private PictureBox pictureBox3;
        private Label label8;
        private TrackBar trackBarBrightness;
        private TrackBar trackBarContrast;
        private TrackBar trackBarRotate;
        private Button btnScale;
        private TextBox textBoxScaleWidth;
        private TextBox textBoxScaleHeight;
        private OpenFileDialog openFileDialog_LoadBackgroundImage;
        private Label label9;
        private Label label10;
        private Label label11;
        private Button btnLoadWebcam;
        private Button btnReset;
        private Label label12;
        private PictureBox pictureBox2;
        private System.Windows.Forms.Timer timer1;
    }
}