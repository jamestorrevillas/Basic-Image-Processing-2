using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading;
using WebCamLib;
using System.Windows.Forms;
using AForge.Video;
using AForge.Video.DirectShow;
using AForge.Imaging.Filters;

namespace DipCs
{
    public partial class Form1 : Form
    {
        Bitmap loadImage, backgroundImage, resultImage;
        /*private Device selectedDevice;*/
        private bool isWebcamInUse = false;
        private bool isImageLoaded = false;
        private FilterInfoCollection videoDevices;
        private VideoCaptureDevice videoSource;
        private String Process;
        private Bitmap histogramBitmap;

        public Form1()
        {
            InitializeComponent();
        }

        private void openFileDialog1_FileOk_LoadImage(object sender, System.ComponentModel.CancelEventArgs e)
        {
            loadImage = new Bitmap(openFileDialog_LoadImage.FileName);
            pictureBox1.Image = loadImage;
        }

        private void openFileDialog2_FileOk_LoadBackgroundImage(object sender, System.ComponentModel.CancelEventArgs e)
        {
            backgroundImage = new Bitmap(openFileDialog_LoadBackgroundImage.FileName);
            pictureBox3.Image = backgroundImage;
        }

        private void saveFileDialog1_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            saveFileDialog1.FileName = Path.ChangeExtension(saveFileDialog1.FileName, "png");

            // Ensure that the file stream is closed after saving
            using (FileStream fs = new FileStream(saveFileDialog1.FileName, FileMode.Create))
            {
                resultImage.Save(fs, ImageFormat.Png);
            }
        }

        private void btnLoadImage_Click(object sender, EventArgs e)     // LOAD IMAGE
        {
            if (isWebcamInUse)
            {
                StopWebcam();
                isWebcamInUse = false;
            }

            openFileDialog_LoadImage.Filter = "Image Files|*.bmp;*.jpg;*.jpeg;*.gif;*.png;*.tif;*.tiff";
            if (openFileDialog_LoadImage.ShowDialog() == DialogResult.OK)
            {
                loadImage = new Bitmap(openFileDialog_LoadImage.FileName);
                pictureBox1.Image = loadImage;
                isImageLoaded = true;

                pictureBox2.Image = null;
            }
        }

        private void btnLoadBackgroundImage_Click(object sender, EventArgs e)       // LOAD A BACKGROUND IMAGE FOR SUBTRACTION
        {
            openFileDialog_LoadBackgroundImage.Filter = "Image Files|*.bmp;*.jpg;*.jpeg;*.gif;*.png;*.tif;*.tiff";
            openFileDialog_LoadBackgroundImage.ShowDialog();

        }

        private void btnLoadWebcam_Click(object sender, EventArgs e)        // LOAD WEBCAM
        {
            if (isWebcamInUse)
            {
                StopWebcam();
                isWebcamInUse = false;
                pictureBox1.Image = null;
                isImageLoaded = false;
            }

            videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);

            if (videoDevices.Count > 0)
            {
                videoSource = new VideoCaptureDevice(videoDevices[0].MonikerString);
                videoSource.NewFrame += new NewFrameEventHandler(videoSource_NewFrame);
                videoSource.Start();
                isWebcamInUse = true;
                isImageLoaded = false;
            }
            else
            {
                MessageBox.Show("No video devices found.");
            }
        }

        private void videoSource_NewFrame(object sender, NewFrameEventArgs eventArgs)       // WEBCAM HELPER
        {
            pictureBox1.Image = (Bitmap)eventArgs.Frame.Clone();
        }

        private void StopWebcam()       // STOP WEBCAM
        {
            if (isWebcamInUse)
            {
                videoSource.SignalToStop();
                videoSource.WaitForStop();
                timer1.Stop();
            }
        }

        private void resetTrackbarsValues()     // RESET TRACKBARS VALUES
        {
            trackBarBrightness.Value = 0;
            trackBarContrast.Value = 0;
            trackBarRotate.Value = 0;
        }

        private void resetScaleTextBoxValues()      // RESET SCALE TEXTBOX VALUES
        {
            textBoxScaleWidth.Text = null;
            textBoxScaleHeight.Text = null;
        }

        private void timer1_Tick(object sender, EventArgs e)        // TIMER
        {
            if (Process == "BasicCopy")
                btnBasicCopy_Click(sender, e);
            else if (Process == "GrayScale")
                btnGrayScale_Click(sender, e);
            else if (Process == "Invert")
                btnInvert_Click(sender, e);
            else if (Process == "Histogram")
                btnHistogram_Click(sender, e);
            else if (Process == "Sepia")
                btnSepia_Click(sender, e);
            else if (Process == "FlipHorizontal")
                btnFlipHorizontal_Click(sender, e);
            else if (Process == "FlipVertical")
                btnFlipVertical_Click(sender, e);
            else if (Process == "Subtract")
                btnSubtract_Click(sender, e);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)       // FORM CLOSING
        {
            StopWebcam();  // Stop the webcam when the form is closing
        }

        private void btnSaveImage_Click(object sender, EventArgs e)     // SAVE PROCESSED IMAGE
        {
            if (!isWebcamInUse)
            {
                if (pictureBox2.Image == null)
                {
                    MessageBox.Show("Please perform image processing before saving.");
                }
                else
                {
                    saveFileDialog1.ShowDialog();
                }
            }
            else
            {
                MessageBox.Show("Saving not supported with webcam.");
            }
        }

        private void btnReset_Click(object sender, EventArgs e)     // RESET
        {
            StopWebcam();
            resetScaleTextBoxValues();
            resetTrackbarsValues();

            pictureBox1.Image = null;
            pictureBox2.Image = null;
            pictureBox3.Image = null;
            isWebcamInUse = false;
            isImageLoaded = false;
        }

        private void btnBasicCopy_Click(object sender, EventArgs e)     // BASIC COPY
        {
            if (pictureBox1.Image == null)
            {
                MessageBox.Show("Please load an image or start the webcam before applying image processing operation.");
            }
            else if (isWebcamInUse || isImageLoaded)
            {
                resetTrackbarsValues();
                resetScaleTextBoxValues();

                if (isImageLoaded)
                {
                    // Clone the loaded image
                    resultImage = new Bitmap(loadImage);
                }
                else
                {
                    if (pictureBox1.Image != null)
                    {
                        // Clone the webcam frame directly
                        resultImage = (Bitmap)pictureBox1.Image.Clone();
                    }
                    else
                    {
                        MessageBox.Show("No image or webcam frame available.");
                        return;
                    }
                }

                // Apply basic copy directly
                BitmapData imageData = resultImage.LockBits(new Rectangle(0, 0, resultImage.Width, resultImage.Height),
                                                            ImageLockMode.ReadWrite,
                                                            PixelFormat.Format24bppRgb);

                int stride = imageData.Stride;
                IntPtr scan0 = imageData.Scan0;

                unsafe
                {
                    byte* p = (byte*)(void*)scan0;

                    for (int y = 0; y < resultImage.Height; y++)
                    {
                        for (int x = 0; x < resultImage.Width; x++)
                        {
                            // Copy the pixel values directly
                            p[0] = p[0];
                            p[1] = p[1];
                            p[2] = p[2];

                            p += 3; // Move to the next pixel
                        }

                        p += stride - resultImage.Width * 3; // Move to the next row
                    }
                }

                resultImage.UnlockBits(imageData);

                pictureBox2.Image = resultImage;

                if (isWebcamInUse)
                {
                    Process = "BasicCopy";
                    timer1.Start();
                }
            }
        }

        private void btnGrayScale_Click(object sender, EventArgs e)     // GRAYSCALE
        {
            if (pictureBox1.Image == null)
            {
                MessageBox.Show("Please load an image or start the webcam before applying image processing operation.");
            }
            else if (isWebcamInUse || isImageLoaded)
            {
                resetTrackbarsValues();
                resetScaleTextBoxValues();

                if (isImageLoaded)
                {
                    // Clone the loaded image
                    resultImage = new Bitmap(loadImage);
                }
                else
                {
                    if (pictureBox1.Image != null)
                    {
                        // Clone the webcam frame directly
                        resultImage = (Bitmap)pictureBox1.Image.Clone();
                    }
                    else
                    {
                        MessageBox.Show("No image or webcam frame available.");
                        return;
                    }
                }

                // Apply grayscale conversion directly
                BitmapData imageData = resultImage.LockBits(new Rectangle(0, 0, resultImage.Width, resultImage.Height),
                                                            ImageLockMode.ReadWrite,
                                                            PixelFormat.Format24bppRgb);

                int stride = imageData.Stride;
                IntPtr scan0 = imageData.Scan0;

                unsafe
                {
                    byte* p = (byte*)(void*)scan0;

                    for (int y = 0; y < resultImage.Height; y++)
                    {
                        for (int x = 0; x < resultImage.Width; x++)
                        {
                            int gray = (p[2] + p[1] + p[0]) / 3;
                            p[0] = (byte)gray;
                            p[1] = (byte)gray;
                            p[2] = (byte)gray;

                            p += 3; // Move to the next pixel
                        }

                        p += stride - resultImage.Width * 3; // Move to the next row
                    }
                }

                resultImage.UnlockBits(imageData);

                pictureBox2.Image = resultImage;

                if (isWebcamInUse)
                {
                    Process = "GrayScale";
                    timer1.Start();
                }
            }
        }

        private void btnInvert_Click(object sender, EventArgs e)        // INVERSION
        {
            if (pictureBox1.Image == null)
            {
                MessageBox.Show("Please load an image or start the webcam before applying image processing operation.");
            }
            else if (isWebcamInUse || isImageLoaded)
            {
                resetTrackbarsValues();
                resetScaleTextBoxValues();

                if (isImageLoaded)
                {
                    // Clone the loaded image
                    resultImage = new Bitmap(loadImage);
                }
                else
                {
                    if (pictureBox1.Image != null)
                    {
                        // Clone the webcam frame directly
                        resultImage = (Bitmap)pictureBox1.Image.Clone();
                    }
                    else
                    {
                        MessageBox.Show("No image or webcam frame available.");
                        return;
                    }
                }

                // Apply color inversion directly
                BitmapData imageData = resultImage.LockBits(new Rectangle(0, 0, resultImage.Width, resultImage.Height),
                                                            ImageLockMode.ReadWrite,
                                                            PixelFormat.Format24bppRgb);

                int stride = imageData.Stride;
                IntPtr scan0 = imageData.Scan0;

                unsafe
                {
                    byte* p = (byte*)(void*)scan0;

                    for (int y = 0; y < resultImage.Height; y++)
                    {
                        for (int x = 0; x < resultImage.Width; x++)
                        {
                            p[0] = (byte)(255 - p[0]); // Invert the red component
                            p[1] = (byte)(255 - p[1]); // Invert the green component
                            p[2] = (byte)(255 - p[2]); // Invert the blue component

                            p += 3; // Move to the next pixel
                        }

                        p += stride - resultImage.Width * 3; // Move to the next row
                    }
                }

                resultImage.UnlockBits(imageData);

                pictureBox2.Image = resultImage;

                if (isWebcamInUse)
                {
                    Process = "Invert";
                    timer1.Start();
                }
            }
        }

        private void btnHistogram_Click(object sender, EventArgs e)     // HISTOGRAM
        {
            if (pictureBox1.Image == null)
            {
                MessageBox.Show("Please load an image or start the webcam before applying image processing operation.");
            }
            else if (isWebcamInUse || isImageLoaded)
            {
                resetTrackbarsValues();
                resetScaleTextBoxValues();

                if (isImageLoaded)
                {
                    // Clone the loaded image
                    resultImage = new Bitmap(loadImage);
                }
                else
                {
                    if (pictureBox1.Image != null)
                    {
                        // Clone the webcam frame directly
                        resultImage = (Bitmap)pictureBox1.Image.Clone();
                    }
                    else
                    {
                        MessageBox.Show("No image or webcam frame available.");
                        return;
                    }
                }

                // Apply grayscale conversion directly
                BitmapData imageData = resultImage.LockBits(new Rectangle(0, 0, resultImage.Width, resultImage.Height),
                                                            ImageLockMode.ReadWrite,
                                                            PixelFormat.Format24bppRgb);

                int stride = imageData.Stride;
                IntPtr scan0 = imageData.Scan0;

                unsafe
                {
                    byte* p = (byte*)(void*)scan0;

                    for (int y = 0; y < resultImage.Height; y++)
                    {
                        for (int x = 0; x < resultImage.Width; x++)
                        {
                            int gray = (p[2] + p[1] + p[0]) / 3;
                            p[0] = (byte)gray;
                            p[1] = (byte)gray;
                            p[2] = (byte)gray;

                            p += 3; // Move to the next pixel
                        }

                        p += stride - resultImage.Width * 3; // Move to the next row
                    }
                }

                resultImage.UnlockBits(imageData);

                // Calculate and plot histogram

                int[] histogramData = new int[256];

                imageData = resultImage.LockBits(new Rectangle(0, 0, resultImage.Width, resultImage.Height),
                                                      ImageLockMode.ReadOnly,
                                                      PixelFormat.Format24bppRgb);

                unsafe
                {
                    byte* p = (byte*)(void*)scan0;

                    for (int y = 0; y < resultImage.Height; y++)
                    {
                        for (int x = 0; x < resultImage.Width; x++)
                        {
                            int gray = (p[2] + p[1] + p[0]) / 3;
                            histogramData[gray]++;
                            p += 3; // Move to the next pixel
                        }

                        p += stride - resultImage.Width * 3; // Move to the next row
                    }
                }

                resultImage.UnlockBits(imageData);

                // Plot Histogram
                int graphDataWidth = 256;
                int graphDataHeight = 200; // Adjusted height for better visualization
                histogramBitmap = new Bitmap(graphDataWidth, graphDataHeight);

                for (int x = 0; x < graphDataWidth; x++)
                {
                    for (int y = 0; y < graphDataHeight; y++)
                    {
                        histogramBitmap.SetPixel(x, y, Color.White);
                    }
                }

                // Plot histogramData
                for (int x = 0; x < graphDataWidth; x++)
                {
                    for (int y = 0; y < Math.Min(histogramData[x] / 5, graphDataHeight); y++)
                    {
                        histogramBitmap.SetPixel(x, (graphDataHeight - 1) - y, Color.Black);
                    }
                }

                pictureBox2.Image = histogramBitmap;

                if (isWebcamInUse)
                {
                    Process = "Histogram";
                    timer1.Start();
                }
            }
        }

        private void btnSepia_Click(object sender, EventArgs e)     // SEPIA
        {
            if (pictureBox1.Image == null)
            {
                MessageBox.Show("Please load an image or start the webcam before applying image processing operation.");
            }
            else if (isWebcamInUse || isImageLoaded)
            {
                resetTrackbarsValues();
                resetScaleTextBoxValues();

                if (isImageLoaded)
                {
                    // Clone the loaded image
                    resultImage = new Bitmap(loadImage);
                }
                else
                {
                    if (pictureBox1.Image != null)
                    {
                        // Clone the webcam frame directly
                        resultImage = (Bitmap)pictureBox1.Image.Clone();
                    }
                    else
                    {
                        MessageBox.Show("No image or webcam frame available.");
                        return;
                    }
                }

                // Apply sepia tone effect directly
                BitmapData imageData = resultImage.LockBits(new Rectangle(0, 0, resultImage.Width, resultImage.Height),
                                                            ImageLockMode.ReadWrite,
                                                            PixelFormat.Format24bppRgb);

                int stride = imageData.Stride;
                IntPtr scan0 = imageData.Scan0;

                unsafe
                {
                    byte* p = (byte*)(void*)scan0;

                    for (int y = 0; y < resultImage.Height; y++)
                    {
                        for (int x = 0; x < resultImage.Width; x++)
                        {
                            // Convert to grayscale
                            int gray = (p[2] + p[1] + p[0]) / 3;

                            // Apply sepia tone transformation
                            int r = Math.Min(255, (int)(gray * 0.393 + gray * 0.769 + gray * 0.189));
                            int g = Math.Min(255, (int)(gray * 0.349 + gray * 0.686 + gray * 0.168));
                            int b = Math.Min(255, (int)(gray * 0.272 + gray * 0.534 + gray * 0.131));

                            p[0] = (byte)b;
                            p[1] = (byte)g;
                            p[2] = (byte)r;

                            p += 3; // Move to the next pixel
                        }

                        p += stride - resultImage.Width * 3; // Move to the next row
                    }
                }

                resultImage.UnlockBits(imageData);

                pictureBox2.Image = resultImage;

                if (isWebcamInUse)
                {
                    Process = "Sepia";
                    timer1.Start();
                }
            }
        }

        private void btnFlipHorizontal_Click(object sender, EventArgs e)        // FLIP HORIZONTAL
        {
            if (pictureBox1.Image == null)
            {
                MessageBox.Show("Please load an image or start the webcam before applying image processing operation.");
            }
            else if (isWebcamInUse || isImageLoaded)
            {
                resetTrackbarsValues();
                resetScaleTextBoxValues();

                if (isImageLoaded)
                {
                    // Clone the loaded image
                    resultImage = new Bitmap(loadImage);
                }
                else
                {
                    if (pictureBox1.Image != null)
                    {
                        // Clone the webcam frame directly
                        resultImage = (Bitmap)pictureBox1.Image.Clone();
                    }
                    else
                    {
                        MessageBox.Show("No image or webcam frame available.");
                        return;
                    }
                }

                // Flip the image horizontally
                resultImage.RotateFlip(RotateFlipType.RotateNoneFlipX);

                pictureBox2.Image = resultImage;

                if (isWebcamInUse)
                {
                    Process = "FlipHorizontal";
                    timer1.Start();
                }
            }
        }

        private void btnFlipVertical_Click(object sender, EventArgs e)      // FLIP VERTICAL
        {
            if (pictureBox1.Image == null)
            {
                MessageBox.Show("Please load an image or start the webcam before applying image processing operation.");
            }
            else if (isWebcamInUse || isImageLoaded)
            {
                resetTrackbarsValues();
                resetScaleTextBoxValues();

                if (isImageLoaded)
                {
                    // Clone the loaded image
                    resultImage = new Bitmap(loadImage);
                }
                else
                {
                    if (pictureBox1.Image != null)
                    {
                        // Clone the webcam frame directly
                        resultImage = (Bitmap)pictureBox1.Image.Clone();
                    }
                    else
                    {
                        MessageBox.Show("No image or webcam frame available.");
                        return;
                    }
                }

                // Flip the image vertically
                resultImage.RotateFlip(RotateFlipType.RotateNoneFlipY);

                pictureBox2.Image = resultImage;

                if (isWebcamInUse)
                {
                    Process = "FlipVertical";
                    timer1.Start();
                }
            }
        }


        private void btnSubtract_Click(object sender, EventArgs e)      // SUBTRACTION
        {
            if (pictureBox1.Image == null)
            {
                MessageBox.Show("Please load an image or start the webcam before applying image processing operation.");
            }
            else if (pictureBox3.Image == null)
            {
                MessageBox.Show("Please load a background image before applying image processing operation.");
            }
            else if (isWebcamInUse || isImageLoaded)
            {
                resetTrackbarsValues();
                resetScaleTextBoxValues();

                if (isImageLoaded)
                {
                    // Clone the loaded image
                    resultImage = new Bitmap(loadImage);
                }
                else
                {
                    if (pictureBox1.Image != null)
                    {
                        // Clone the webcam frame directly
                        resultImage = (Bitmap)pictureBox1.Image.Clone();
                    }
                    else
                    {
                        MessageBox.Show("No image or webcam frame available.");
                        return;
                    }
                }

                loadImage = (Bitmap)pictureBox1.Image.Clone();
                int maxWidth = Math.Max(loadImage.Width, backgroundImage.Width);
                int maxHeight = Math.Max(loadImage.Height, backgroundImage.Height);

                // Resize images to match the maximum dimensions
                Bitmap resizedLoadImage = ResizeImage(loadImage, maxWidth, maxHeight);
                Bitmap resizedBackgroundImage = ResizeImage(backgroundImage, maxWidth, maxHeight);

                resultImage = new Bitmap(maxWidth, maxHeight);

                BitmapData imageDataA = resizedLoadImage.LockBits(new Rectangle(0, 0, resizedLoadImage.Width, resizedLoadImage.Height),
                                                  ImageLockMode.ReadOnly,
                                                  PixelFormat.Format24bppRgb);

                BitmapData imageDataB = resizedBackgroundImage.LockBits(new Rectangle(0, 0, resizedBackgroundImage.Width, resizedBackgroundImage.Height),
                                                                        ImageLockMode.ReadOnly,
                                                                        PixelFormat.Format24bppRgb);

                BitmapData resultImageData = resultImage.LockBits(new Rectangle(0, 0, resultImage.Width, resultImage.Height),
                                                                  ImageLockMode.WriteOnly,
                                                                  PixelFormat.Format24bppRgb);

                int strideA = imageDataA.Stride;
                int strideB = imageDataB.Stride;
                int strideResult = resultImageData.Stride;

                unsafe
                {
                    byte* pA = (byte*)(void*)imageDataA.Scan0;
                    byte* pB = (byte*)(void*)imageDataB.Scan0;
                    byte* pResult = (byte*)(void*)resultImageData.Scan0;

                    for (int y = 0; y < resultImage.Height; y++)
                    {
                        for (int x = 0; x < resultImage.Width; x++)
                        {
                            Color pixelA = Color.FromArgb(pA[2], pA[1], pA[0]);
                            Color pixelB = Color.FromArgb(pB[2], pB[1], pB[0]);

                            // Check if the pixel in loadImage has a significant green component
                            int greenThreshold = 100; // Adjust as needed
                            if (pixelA.G > greenThreshold && pixelA.G > pixelA.R && pixelA.G > pixelA.B)
                            {
                                // Add the background pixel (green) to the result
                                pResult[0] = pB[0];
                                pResult[1] = pB[1];
                                pResult[2] = pB[2];
                            }
                            else
                            {
                                // Add the non-green pixel to the result
                                pResult[0] = pA[0];
                                pResult[1] = pA[1];
                                pResult[2] = pA[2];
                            }

                            pA += 3; // Move to the next pixel in A
                            pB += 3; // Move to the next pixel in B
                            pResult += 3; // Move to the next pixel in the result
                        }

                        pA += strideA - resultImage.Width * 3; // Move to the next row in A
                        pB += strideB - resultImage.Width * 3; // Move to the next row in B
                        pResult += strideResult - resultImage.Width * 3; // Move to the next row in the result
                    }
                }

                resizedLoadImage.UnlockBits(imageDataA);
                resizedBackgroundImage.UnlockBits(imageDataB);
                resultImage.UnlockBits(resultImageData);

                pictureBox2.Image = resultImage;

                if (isWebcamInUse)
                {
                    Process = "Subtract";
                    timer1.Start();
                }
            }
        }

        private Bitmap ResizeImage(Image image, int width, int height)      // RESIZE IMAGE SUBTRACTION HELPER
        {
            Bitmap resizedImage = new Bitmap(width, height);
            using (Graphics g = Graphics.FromImage(resizedImage))
            {
                g.DrawImage(image, 0, 0, width, height);
            }
            return resizedImage;
        }

        private void btnScale_Click(object sender, EventArgs e)     // SCALE IMAGE
        {
            if (isWebcamInUse)
            {
                MessageBox.Show("Scale operation not supported with webcam.");
                return;
            }
            else if (pictureBox1.Image == null)
            {
                MessageBox.Show("Please load an image before applying image processing operation.");
            }
            else if (string.IsNullOrWhiteSpace(textBoxScaleWidth.Text) || string.IsNullOrWhiteSpace(textBoxScaleHeight.Text) ||
                     textBoxScaleWidth.Text.All(char.IsLetter) || textBoxScaleHeight.Text.All(char.IsLetter) ||
                     int.Parse(textBoxScaleWidth.Text) <= 0 || int.Parse(textBoxScaleHeight.Text) <= 0)
            {
                MessageBox.Show("Please enter valid positive integers for width and height.");
            }
            else
            {
                resetTrackbarsValues();

                int targetWidth = int.Parse(textBoxScaleWidth.Text);
                int targetHeight = int.Parse(textBoxScaleHeight.Text);
                int xTarget, yTarget, xSource, ySource;
                int sourceWidth = loadImage.Width;
                int sourceHeight = loadImage.Height;
                resultImage = new Bitmap(targetWidth, targetHeight);

                for (xTarget = 0; xTarget < targetWidth; xTarget++)
                {
                    for (yTarget = 0; yTarget < targetHeight; yTarget++)
                    {
                        xSource = xTarget * sourceWidth / targetWidth;
                        ySource = yTarget * sourceHeight / targetHeight;
                        resultImage.SetPixel(xTarget, yTarget, loadImage.GetPixel(xSource, ySource));
                    }
                }

                pictureBox2.Image = resultImage;
            }
        }

        private void trackBarBrightness_Scroll(object sender, EventArgs e)      // BRIGHTNESS
        {
            if (isWebcamInUse)
            {
                MessageBox.Show("Brightness operation not supported with webcam.");
                resetTrackbarsValues();
                return;
            }
            else if (pictureBox1.Image == null)
            {
                MessageBox.Show("Please load an image before applying image processing operation.");
                trackBarBrightness.Value = 0;
            }
            else
            {
                trackBarContrast.Value = 0;
                trackBarRotate.Value = 0;
                resetScaleTextBoxValues();

                resultImage = new Bitmap(loadImage.Width, loadImage.Height);

                for (int x = 0; x < loadImage.Width; x++)
                {
                    for (int y = 0; y < loadImage.Height; y++)
                    {
                        Color pixel = loadImage.GetPixel(x, y);

                        // Calculate the new brightness-adjusted color values
                        int newR = pixel.R + trackBarBrightness.Value;
                        int newG = pixel.G + trackBarBrightness.Value;
                        int newB = pixel.B + trackBarBrightness.Value;

                        // Ensure the color values are within the valid range
                        newR = Math.Min(255, Math.Max(0, newR));
                        newG = Math.Min(255, Math.Max(0, newG));
                        newB = Math.Min(255, Math.Max(0, newB));

                        resultImage.SetPixel(x, y, Color.FromArgb(newR, newG, newB));
                    }
                }

                pictureBox2.Image = resultImage;
            }
        }

        private void trackBarContrast_Scroll(object sender, EventArgs e)        // CONTRAST
        {
            if (isWebcamInUse)
            {
                MessageBox.Show("Contrast operation not supported with webcam.");
                resetTrackbarsValues();
                return;
            }
            else if (pictureBox1.Image == null)
            {
                MessageBox.Show("Please load an image before applying image processing operation.");
                trackBarContrast.Value = 0;
            }
            else
            {
                trackBarBrightness.Value = 0;
                trackBarRotate.Value = 0;
                resetScaleTextBoxValues();

                resultImage = new Bitmap(loadImage.Width, loadImage.Height);

                double contrastFactor = (100.0 + trackBarContrast.Value) / 100.0;

                for (int x = 0; x < loadImage.Width; x++)
                {
                    for (int y = 0; y < loadImage.Height; y++)
                    {
                        Color pixel = loadImage.GetPixel(x, y);

                        int newR = (int)(((pixel.R / 255.0 - 0.5) * contrastFactor + 0.5) * 255.0);
                        int newG = (int)(((pixel.G / 255.0 - 0.5) * contrastFactor + 0.5) * 255.0);
                        int newB = (int)(((pixel.B / 255.0 - 0.5) * contrastFactor + 0.5) * 255.0);

                        newR = Math.Min(255, Math.Max(0, newR));
                        newG = Math.Min(255, Math.Max(0, newG));
                        newB = Math.Min(255, Math.Max(0, newB));

                        resultImage.SetPixel(x, y, Color.FromArgb(newR, newG, newB));
                    }
                }

                pictureBox2.Image = resultImage;
            }
        }

        private void trackBarRotate_Scroll(object sender, EventArgs e)      // ROTATE
        {
            if (isWebcamInUse)
            {
                MessageBox.Show("Rotate operation not supported with webcam.");
                resetTrackbarsValues(); 
                return;
            }
            else if (pictureBox1.Image == null)
            {
                MessageBox.Show("Please load an image before applying image processing operation.");
                trackBarRotate.Value = 0;
            }
            else
            {
                trackBarBrightness.Value = 0;
                trackBarContrast.Value = 0;
                resetScaleTextBoxValues();

                resultImage = new Bitmap(loadImage.Width, loadImage.Height);

                using (Graphics g = Graphics.FromImage(resultImage))
                {
                    g.TranslateTransform(resultImage.Width / 2, resultImage.Height / 2); // Set the rotation point at the center
                    g.RotateTransform(trackBarRotate.Value);
                    g.TranslateTransform(-resultImage.Width / 2, -resultImage.Height / 2); // Reset the translation point

                    g.DrawImage(loadImage, new Point(0, 0));
                }

                pictureBox2.Image = resultImage;
            }
        }
    }
}