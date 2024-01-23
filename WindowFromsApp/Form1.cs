using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace WindowFromsApp
{
    public partial class Form1 : Form
    {

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
            radioButton2.Checked = true;
            bloomEffectImplementation.SetAsm(false);
            textBox1.Text = processorCount.ToString(); 
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

        private async void button1_Click(object sender, EventArgs e)
        {
            stopwatch.Restart();
            List<long> test = new List<long>();
            int iterations = 1;
            for (int i = 0; i < iterations; i++)
            {
                stopwatch.Start();

                Bitmap coppy = new Bitmap((Bitmap)this.UploadedPicture.Image);
                Bitmap bloomImage = await Task.Run(() => bloomEffectImplementation.ApplyBloomEffects(coppy));
                //this.Invoke((Action)(() => {
                    this.ModifiedPicture.Image = bloomImage;
               // }));

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

        private void radioButton1_CheckedChanged_1(object sender, EventArgs e)
        {
            bloomEffectImplementation.SetAsm(true);
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            bloomEffectImplementation.SetAsm(false);
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true; 
            }


        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            int threads = Int32.Parse(textBox1.Text);
            bloomEffectImplementation.SetThreads(threads);
        }
    }
}
