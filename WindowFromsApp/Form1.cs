using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Windows.Forms;


namespace WindowFromsApp
{
    public partial class Form1 : Form
    {
        [DllImport(@"C:\Users\Artur\source\repos\BloomEffect\x64\Debug\JAAsm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int MyProc1(int a, int b, int c);

        BloomEffectImplementation.BloomEffectImplementation bloomEffectImplementation;
        Stopwatch stopwatch;
        public Form1()
        {
            bloomEffectImplementation = new BloomEffectImplementation.BloomEffectImplementation();
            InitializeComponent();
            checkRadioButton();
            stopwatch  = new Stopwatch();
        }

        private void checkRadioButton()
        {
            int processorCount = Environment.ProcessorCount;
            bloomEffectImplementation.SetThreads(processorCount);
            switch (processorCount)
            {
                case 1:
                    radioButton1.Checked = true;
                    break;
                case 2:
                    radioButton2.Checked = true;
                    break;
                case 4:
                    radioButton3.Checked = true;
                    break;
                case 8:
                    radioButton4.Checked = true;
                    break;
                case 16:
                    radioButton5.Checked = true;
                    break;
                case 32:
                    radioButton6.Checked = true;
                    break;
                case 64:
                    radioButton7.Checked = true;
                    break;

                default:
                    break;
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog opFile = new OpenFileDialog();
                opFile.Filter = "Image Files (*.bmp,*.png,*.jpg,*.avif)|*.bmp;*.png;*.jpg;*.avif";
                if (DialogResult.OK == opFile.ShowDialog())
                {
                    if (UploadedPicture.Image != null)
                    {
                        UploadedPicture.Image.Dispose();
                    }

                    UploadedPicture.Image = new Bitmap(opFile.FileName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            stopwatch.Restart();
            List<long> test = new List<long>();
            int iterations = 1;
            for (int i = 0; i < iterations; i++)
            {
                stopwatch.Start();

                Bitmap coppy = new Bitmap((Bitmap)this.UploadedPicture.Image);
                this.ModifiedPicture.Image = bloomEffectImplementation.ApplyBloomEffects(coppy);

                stopwatch.Stop();
                test.Add(stopwatch.ElapsedMilliseconds);
            }
            label9.Text = $"Average time per iteration: {stopwatch.ElapsedMilliseconds / iterations}";
        }

        private void Save_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "JPEG files(*.jpeg)|*.jpeg";// Saving files in JPEG Format
            if (DialogResult.OK == sfd.ShowDialog())
            {

                this.ModifiedPicture.Image.Save(sfd.FileName, ImageFormat.Jpeg);
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            bloomEffectImplementation.SetThreads(1);
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            bloomEffectImplementation.SetThreads(2);
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            bloomEffectImplementation.SetThreads(4);
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
           bloomEffectImplementation.SetThreads(8);
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            bloomEffectImplementation.SetThreads(16);
        }

        private void radioButton6_CheckedChanged(object sender, EventArgs e)
        {
           bloomEffectImplementation.SetThreads(32);
        }

        private void radioButton7_CheckedChanged(object sender, EventArgs e)
        {
           bloomEffectImplementation.SetThreads(64);
        }
    }
}
