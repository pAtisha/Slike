using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace Slike
{
    public partial class Form1 : Form
    {
        //Initialization

        private System.Drawing.Bitmap m_Bitmap;
        private double Zoom = 1.0;

        public Form1()
        {
            InitializeComponent();

            m_Bitmap = new Bitmap(2, 2, PixelFormat.Format24bppRgb);

        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            g.DrawImage(m_Bitmap, new Rectangle(this.AutoScrollPosition.X, this.AutoScrollPosition.Y, (int)(m_Bitmap.Width * Zoom), (int)(m_Bitmap.Height * Zoom)));
        }

        //ToolStripMenuItems

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure?", "Exit", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                System.Windows.Forms.Application.Exit();
            }
        }

        private void openToolStripMenuItem1_Click_1(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.InitialDirectory = "c:\\";
            openFileDialog.Filter = "Bitmap files (*.bmp)|*.bmp|Jpeg files (*.jpg)|*.jpg|GIF files(*.gif)|*.gif|PNG files(*.png)|*.png|All valid files|*.bmp/*.jpg/*.gif/*.png";
            openFileDialog.FilterIndex = 2;
            openFileDialog.RestoreDirectory = true;

            if (DialogResult.OK == openFileDialog.ShowDialog())
            {
                m_Bitmap = (Bitmap)Bitmap.FromFile(openFileDialog.FileName, false);
                this.AutoScroll = true;
                this.AutoScrollMinSize = new Size((int)(m_Bitmap.Width * Zoom), (int)(m_Bitmap.Height * Zoom));
                this.Invalidate();
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();

            saveFileDialog.InitialDirectory = "c:\\";
            saveFileDialog.Filter = "Bitmap files (*.bmp)|*.bmp|Jpeg files (*.jpg)|*.jpg|All valid files (*.bmp/*.jpg)|*.bmp/*.jpg";
            saveFileDialog.FilterIndex = 1;
            saveFileDialog.RestoreDirectory = true;

            if (DialogResult.OK == saveFileDialog.ShowDialog())
            {
                m_Bitmap.Save(saveFileDialog.FileName);
                MessageBox.Show("Successfully saved.", "Picture", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void rGBToYUVToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            m_Bitmap = ConvertRGBtoYUV(m_Bitmap);
            m_Bitmap = ConvertRGBtoYUV(m_Bitmap);
            m_Bitmap = ConvertRGBtoYUV(m_Bitmap);
            this.Invalidate();
        }

        private void downsampleUAndVToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m_Bitmap = Downsample(m_Bitmap);
            this.Invalidate();
        }

        private void compressImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            byte[] arr = BitmapToByteArray(m_Bitmap);
            byte[] newArr = Compress(arr);
            Bitmap bitmap = ByteArrayToBitmap(newArr);
            m_Bitmap = bitmap;
            this.Invalidate();
        }

        private void contrastToolStripMenuItem_Click(object sender, EventArgs e)
        {
            labelFilter.Text = "Contrast Value (min: -100, max: 100)";
            changeVisibilityForFiltersTrue();

        }

        private void sharpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            labelFilter.Text = "Sharpnes Value (min: 1, max: 10)";
            changeVisibilityForFiltersTrue();
        }

        private void edgeDetectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            labelFilter.Text = "Edge Detect Difference Value (min: 0.5, max: 2.0)";
            changeVisibilityForFiltersTrue();

        }

        private void randomJitterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            labelFilter.Text = "Random Jitter Value (min: 1, max: 10)";
            changeVisibilityForFiltersTrue();
        }

        //Showing and hiding elements (like form)

        private void btnOk_Click(object sender, EventArgs e)
        {
            changeVisibilityForFiltersFalse();

            if (labelFilter.Text == "Contrast Value (min: -100, max: 100)")
            {
                int num = int.Parse(txtBoxFilterValue.Text);
                m_Bitmap = ContrastFilter(num, m_Bitmap);
            }
            if (labelFilter.Text == "Sharpnes Value (min: 1, max: 10)")
            {
                float num = float.Parse(txtBoxFilterValue.Text);
                m_Bitmap = SharpenImage(m_Bitmap, num);
            }
            if (labelFilter.Text == "Edge Detect Difference Value (min: 0.5, max: 2.0)")
            {
                float num = float.Parse(txtBoxFilterValue.Text);
                m_Bitmap = EdgeDetectDifference(m_Bitmap, num);
            }
            if (labelFilter.Text == "Random Jitter Value (min: 1, max: 10)")
            {
                int num = int.Parse(txtBoxFilterValue.Text);
                m_Bitmap = RandomJitter(m_Bitmap, num);
            }

            this.Invalidate();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            changeVisibilityForFiltersFalse();

        }

        private void changeVisibilityForFiltersTrue()
        {
            txtBoxFilterValue.Visible = true;
            labelFilter.Visible = true;
            btnCancel.Visible = true;
            btnOk.Visible = true;
        }

        private void changeVisibilityForFiltersFalse()
        {
            txtBoxFilterValue.Visible = false;
            labelFilter.Visible = false;
            btnCancel.Visible = false;
            btnOk.Visible = false;
        }

        //Main functions
        
        //RGB to YUV conversion

        public static Bitmap ConvertRGBtoYUV(Bitmap input)
        {
            Bitmap output = new Bitmap(input.Width, input.Height, PixelFormat.Format24bppRgb);
            BitmapData inputData = input.LockBits(new Rectangle(0, 0, input.Width, input.Height), ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
            BitmapData outputData = output.LockBits(new Rectangle(0, 0, output.Width, output.Height), ImageLockMode.WriteOnly, PixelFormat.Format24bppRgb);

            unsafe
            {
                byte* inputPtr = (byte*)inputData.Scan0.ToPointer();
                byte* outputPtr = (byte*)outputData.Scan0.ToPointer();

                for (int y = 0; y < input.Height; y++)
                {
                    for (int x = 0; x < input.Width; x++)
                    {
                        byte r = inputPtr[2];
                        byte g = inputPtr[1];
                        byte b = inputPtr[0];

                        byte yVal = (byte)((0.299 * r + 0.587 * g + 0.114 * b));
                        byte uVal = (byte)((-0.169 * r - 0.331 * g + 0.5 * b) + 128);
                        byte vVal = (byte)((0.5 * r - 0.419 * g - 0.081 * b) + 128);

                        outputPtr[2] = yVal;
                        outputPtr[1] = uVal;
                        outputPtr[0] = vVal;

                        inputPtr += 3;
                        outputPtr += 3;
                    }
                    inputPtr += inputData.Stride - input.Width * 3;
                    outputPtr += outputData.Stride - output.Width * 3;
                }
            }

            input.UnlockBits(inputData);
            output.UnlockBits(outputData);


            return output;
        }

        //Downsample function

        public static Bitmap Downsample(Bitmap input)
        {
            Bitmap bmp = input;

            BitmapData bmpData = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadWrite, bmp.PixelFormat);

            IntPtr ptr = bmpData.Scan0;

            int stride = bmpData.Stride;

            unsafe
            {
                byte* p = (byte*)ptr.ToPointer();
                for (int y = 0; y < bmp.Height; y++)
                {
                    for (int x = 0; x < bmp.Width + (bmp.Width / 2); x += 2)
                    {
                        byte u = p[y * stride + x * 2 + 1];
                        byte v = p[y * stride + x * 2 + 3];

                        byte u2 = (byte)((u + p[y * stride + (x + 1) * 2 + 1]) / 2);
                        byte v2 = (byte)((v + p[y * stride + (x + 1) * 2 + 3]) / 2);

                        p[y * stride + x * 2 + 1] = u2;
                        p[y * stride + x * 2 + 3] = v2;
                    }
                }
            }

            bmp.UnlockBits(bmpData);

            return bmp;
        }

        //Shannon-Fano Compress

        public static byte[] Compress(byte[] input)
        {
            var symbols = Enumerable.Range(0, 16).Select(i => (byte)i).ToArray();

            var counts = new int[16];
            foreach (var b in input)
            {
                counts[b >> 4]++;
                counts[b & 0x0F]++;
            }
            var probabilities = symbols.Select(s => (double)counts[s] / input.Length).ToArray();

            var sortedSymbols = symbols.OrderByDescending(s => probabilities[s]).ToArray();

            var codes = new List<bool>[16];
            for (int i = 0; i < codes.Length; i++)
            {
                codes[i] = new List<bool>();
            }
            CalculateCodes(codes, sortedSymbols, probabilities, 0, input.Length - 1);

            var output = new List<byte>();

            output.Add((byte)symbols.Length);

            output.AddRange(symbols);

            var bits = new List<bool>();
            foreach (var b in input)
            {
                bits.AddRange(codes[b >> 4]);
                bits.AddRange(codes[b & 0x0F]);
            }
            while (bits.Count % 8 != 0)
            {
                bits.Add(false);
            }
            for (int i = 0; i < bits.Count; i += 8)
            {
                byte b = 0;
                for (int j = 0; j < 8; j++)
                {
                    b <<= 1;
                    if (bits[i + j])
                    {
                        b |= 1;
                    }
                }
                output.Add(b);
            }

            return output.ToArray();
        }

        // Recursive function
        private static void CalculateCodes(List<bool>[] codes, byte[] symbols, double[] probabilities, int start, int end)
        {
            if (start == end)
            {
                return;
            }

            double sum = 0;
            for (int i = start; i <= end; i++)
            {
                sum += probabilities[symbols[i]]; //Problem with index range...
            }

            double halfSum = 0;
            int divideIndex = -1;

            for (int i = start; i <= end; i++)
            {
                halfSum += probabilities[symbols[i]];
                if (halfSum >= sum / 2)
                {
                    divideIndex = i;
                    break;
                }
            }
            for (int i = start; i <= divideIndex; i++)
            {
                codes[symbols[i]].Add(false);
            }
            for (int i = divideIndex + 1; i <= end; i++)
            {
                codes[symbols[i]].Add(true);
            }

            CalculateCodes(codes, symbols, probabilities, start, divideIndex);
            CalculateCodes(codes, symbols, probabilities, divideIndex + 1, end);
        }

        // Decompress
        public static byte[] Decompress(byte[] input)
        {
            int numSymbols = input[0];
            var symbols = new byte[numSymbols];
            Array.Copy(input, 1, symbols, 0, numSymbols);

            var dictionary = new Dictionary<List<bool>, byte>();
            var bits = new List<bool>();
            for (int i = numSymbols + 1; i < input.Length; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    bits.Add((input[i] & (1 << (7 - j))) != 0);
                    if (dictionary.ContainsKey(bits))
                    {
                        throw new ArgumentException("Invalid input: code is prefix of another code");
                    }
                }
                if (bits.Count % 8 == 0)
                {
                    dictionary[bits.GetRange(0, bits.Count / 8)] = symbols[i - numSymbols - 1];
                    bits.Clear();
                }
            }

            var output = new List<byte>();
            bits.Clear();
            for (int i = numSymbols + 1; i < input.Length; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    bits.Add((input[i] & (1 << (7 - j))) != 0);
                    if (dictionary.TryGetValue(bits, out byte symbol))
                    {
                        output.Add(symbol);
                        bits.Clear();
                    }
                }
            }

            return output.ToArray();
        }

        //Helper functions for Shannon-Fano compression

        public static byte[] BitmapToByteArray(Bitmap bitmap)
        {

            BitmapData bmpdata = null;

            try
            {
                bmpdata = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadOnly, bitmap.PixelFormat);
                int numbytes = bmpdata.Stride * bitmap.Height;
                byte[] bytedata = new byte[numbytes];
                IntPtr ptr = bmpdata.Scan0;

                Marshal.Copy(ptr, bytedata, 0, numbytes);

                return bytedata;
            }
            finally
            {
                if (bmpdata != null)
                    bitmap.UnlockBits(bmpdata);
            }

        }

        public static Bitmap ByteArrayToBitmap(byte[] input)
        {
            Bitmap bmp;
            using (var ms = new MemoryStream(input))
            {
                bmp = new Bitmap(ms);
            }

            return bmp;
        }


        //Filter functions

        //Contrast

        public static Bitmap ContrastFilter(int num, Bitmap bmp)
        {

            double pixel = 0, contrast = (100.0 + num) / 100.0;

            contrast *= contrast;

            BitmapData bmData = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb); // PixelFormat.Format24bppRgb);

            int stride = bmData.Stride;
            System.IntPtr Scan0 = bmData.Scan0;

            unsafe
            {
                byte* p = (byte*)(void*)Scan0;

                int nOffset = stride - bmp.Width;

                for (int y = 0; y < bmp.Height * 3; ++y)
                {
                    for (int x = 0; x < bmp.Width; ++x)
                    {


                        pixel = p[0] / 255.0;
                        pixel -= 0.5;
                        pixel *= contrast;
                        pixel += 0.5;
                        pixel *= 255;
                        if (pixel < 0) pixel = 0;
                        if (pixel > 255) pixel = 255;
                        p[0] = (byte)pixel;
                        p++;
                    }
                    p += stride - bmp.Width * 3;
                }
            }

            bmp.UnlockBits(bmData);

            return bmp;
        }


        //Sharp
        public static Bitmap SharpenImage(Bitmap image, float strength)
        {
            Bitmap sharpenedImage = new Bitmap(image.Width, image.Height);

            float[,] sharpenMatrix = {
                { -1*strength, -1*strength, -1*strength },
                { -1*strength, 9*strength, -1*strength },
                { -1*strength, -1*strength, -1*strength }
            };

            BitmapData sourceData = image.LockBits(new Rectangle(0, 0, image.Width, image.Height), ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
            BitmapData destData = sharpenedImage.LockBits(new Rectangle(0, 0, sharpenedImage.Width, sharpenedImage.Height), ImageLockMode.WriteOnly, PixelFormat.Format24bppRgb);

            int sourceStride = sourceData.Stride;
            int destStride = destData.Stride;

            IntPtr sourceScan0 = sourceData.Scan0;
            IntPtr destScan0 = destData.Scan0;

            unsafe
            {
                byte* sourcePtr = (byte*)sourceScan0.ToPointer();
                byte* destPtr = (byte*)destScan0.ToPointer();

                for (int y = 1; y < image.Height - 1; y++)
                {
                    for (int x = 1; x < image.Width - 1; x++)
                    {
                        float sumR = 0, sumG = 0, sumB = 0;

                        for (int i = -1; i <= 1; i++)
                        {
                            for (int j = -1; j <= 1; j++)
                            {
                                int index = (y + j) * sourceStride + (x + i) * 3;
                                sumR += sourcePtr[index + 2] * sharpenMatrix[i + 1, j + 1];
                                sumG += sourcePtr[index + 1] * sharpenMatrix[i + 1, j + 1];
                                sumB += sourcePtr[index] * sharpenMatrix[i + 1, j + 1];
                            }
                        }

                        int r = Math.Min(Math.Max((int)sumR, 0), 255);
                        int g = Math.Min(Math.Max((int)sumG, 0), 255);
                        int b = Math.Min(Math.Max((int)sumB, 0), 255);

                        int index2 = y * destStride + x * 3;
                        destPtr[index2 + 2] = (byte)r;
                        destPtr[index2 + 1] = (byte)g;
                        destPtr[index2] = (byte)b;
                    }
                }
            }

            image.UnlockBits(sourceData);
            sharpenedImage.UnlockBits(destData);

            return sharpenedImage;
        }

        //Edges
        public static Bitmap EdgeDetectDifference(Bitmap image, float strength)
        {
            Bitmap filteredImage = new Bitmap(image.Width, image.Height);

            float[,] edgeDetectDifferenceMatrix = {
                { -1*strength, -1*strength, -1*strength },
                { -1*strength, 8*strength, -1*strength },
                { -1*strength, -1*strength, -1*strength }
            };

            BitmapData sourceData = image.LockBits(new Rectangle(0, 0, image.Width, image.Height), ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
            BitmapData destData = filteredImage.LockBits(new Rectangle(0, 0, filteredImage.Width, filteredImage.Height), ImageLockMode.WriteOnly, PixelFormat.Format24bppRgb);

            int sourceStride = sourceData.Stride;
            int destStride = destData.Stride;

            IntPtr sourceScan0 = sourceData.Scan0;
            IntPtr destScan0 = destData.Scan0;

            unsafe
            {
                byte* sourcePtr = (byte*)sourceScan0.ToPointer();
                byte* destPtr = (byte*)destScan0.ToPointer();

                for (int y = 1; y < image.Height - 1; y++)
                {
                    for (int x = 1; x < image.Width - 1; x++)
                    {
                        float sumR = 0, sumG = 0, sumB = 0;

                        for (int i = -1; i <= 1; i++)
                        {
                            for (int j = -1; j <= 1; j++)
                            {
                                int index = (y + j) * sourceStride + (x + i) * 3;
                                sumR += sourcePtr[index + 2] * edgeDetectDifferenceMatrix[i + 1, j + 1];
                                sumG += sourcePtr[index + 1] * edgeDetectDifferenceMatrix[i + 1, j + 1];
                                sumB += sourcePtr[index] * edgeDetectDifferenceMatrix[i + 1, j + 1];
                            }
                        }

                        int r = Math.Min(Math.Max((int)sumR, 0), 255);
                        int g = Math.Min(Math.Max((int)sumG, 0), 255);
                        int b = Math.Min(Math.Max((int)sumB, 0), 255);

                        int index2 = y * destStride + x * 3;
                        destPtr[index2 + 2] = (byte)r;
                        destPtr[index2 + 1] = (byte)g;
                        destPtr[index2] = (byte)b;
                    }
                }
            }

            image.UnlockBits(sourceData);
            filteredImage.UnlockBits(destData);

            return filteredImage;
        }


        //Jitter
        public static Bitmap RandomJitter(Bitmap image, int jitterAmount)
        {
            Bitmap filteredImage = new Bitmap(image.Width, image.Height);

            BitmapData sourceData = image.LockBits(new Rectangle(0, 0, image.Width, image.Height), ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
            BitmapData destData = filteredImage.LockBits(new Rectangle(0, 0, filteredImage.Width, filteredImage.Height), ImageLockMode.WriteOnly, PixelFormat.Format24bppRgb);

            int sourceStride = sourceData.Stride;
            int destStride = destData.Stride;

            IntPtr sourceScan0 = sourceData.Scan0;
            IntPtr destScan0 = destData.Scan0;

            Random random = new Random();

            unsafe
            {
                byte* sourcePtr = (byte*)sourceScan0.ToPointer();
                byte* destPtr = (byte*)destScan0.ToPointer();

                for (int y = 0; y < image.Height; y++)
                {
                    for (int x = 0; x < image.Width; x++)
                    {
                        int xOffset = random.Next(-jitterAmount, jitterAmount + 1);
                        int yOffset = random.Next(-jitterAmount, jitterAmount + 1);

                        int sourceIndex = y * sourceStride + x * 3;
                        int jitteredIndex = (y + yOffset) * sourceStride + (x + xOffset) * 3;

                        byte r = 0, g = 0, b = 0;

                        if (jitteredIndex >= 0 && jitteredIndex < sourceData.Stride * sourceData.Height) // check if jittered pixel is within image bounds
                        {
                            r = sourcePtr[jitteredIndex + 2];
                            g = sourcePtr[jitteredIndex + 1];
                            b = sourcePtr[jitteredIndex];
                        }

                        int destIndex = y * destStride + x * 3;
                        destPtr[destIndex + 2] = r;
                        destPtr[destIndex + 1] = g;
                        destPtr[destIndex] = b;
                    }
                }
            }

            image.UnlockBits(sourceData);
            filteredImage.UnlockBits(destData);

            return filteredImage;
        }
    }
}