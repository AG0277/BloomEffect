using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Drawing.Imaging;
using System.Security.Policy;

namespace BloomEffectImplementation
{
    public class BloomEffectImplementation
    {
        private int threads;
        private int sigma;
        private int size;
        private bool asm;
        [DllImport(@"C:\Users\Artur\source\repos\BloomEffect\x64\Debug\JAAsm.dll", CallingConvention = CallingConvention.Cdecl)]
   
        public static extern int MyProc1(
        byte[] originalPixels,
        byte[] bloomPixels,
        int width,
        int height
       );
        [DllImport(@"C:\Users\Artur\source\repos\BloomEffect\x64\Debug\JAAsm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int MyProc2(byte[] sourcePixels,
        byte[] destPixels,
        int x,
        int y,
        double[] kernel,
        int radius,
        int stride,
        int height);

        [DllImport(@"C:\Users\Artur\source\repos\BloomEffect\x64\Debug\JAAsm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int MyProc3(byte[] sourcePixels,
        byte[] destPixels,
        int x,
        int y,
        double[] kernel,
        int radius,
        int stride,
        int width);

        public BloomEffectImplementation()
        {
            threads = 1;
            sigma = 25;
            size = 34;
            asm = true;
        }
        public int GetThreads()
        {
            return threads;
        }

        public void SetThreads(int value)
        {
            threads = value;
        }
        public void SetAsm(bool value)
        {
            asm = value;
        }

        public async Task<Bitmap> ApplyBloomEffects(Bitmap originalImage)
        {


            Bitmap bloomImage = ApplyThresholdMapping(originalImage);
            Bitmap blurredBloom = ApplyGaussianBlur(bloomImage);
            Bitmap finalImage = MergePictures(originalImage, blurredBloom);
            
             return finalImage;
        }
        private Bitmap MergePictures(Bitmap original, Bitmap blurred)
        {
            int width = original.Width;
            int height = original.Height;

            Bitmap finalImage = new Bitmap(width, height);

            BitmapData originalData = original.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            BitmapData blurredData = blurred.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            BitmapData finalData = finalImage.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);

            int bytesPerPixel = 4;
            int stride = width * bytesPerPixel;

            byte[] originalPixels = new byte[width * height * bytesPerPixel];
            byte[] blurredPixels = new byte[width * height * bytesPerPixel];
            byte[] finalPixels = new byte[width * height * bytesPerPixel];

            Marshal.Copy(originalData.Scan0, originalPixels, 0, originalPixels.Length);
            Marshal.Copy(blurredData.Scan0, blurredPixels, 0, blurredPixels.Length);

            for (int i = 0; i < originalPixels.Length; i++)
            {
                byte originalValue = originalPixels[i];
                byte blurredValue = blurredPixels[i];

                int finalValue = Math.Min(originalValue + blurredValue, 255);

                finalPixels[i] = (byte)finalValue;
            }

            Marshal.Copy(finalPixels, 0, finalData.Scan0, finalPixels.Length);

            original.UnlockBits(originalData);
            blurred.UnlockBits(blurredData);
            finalImage.UnlockBits(finalData);


            return finalImage;

        }
        private Bitmap ApplyThresholdMapping(Bitmap originalImage)
        {
            int width = originalImage.Width;
            int height = originalImage.Height;

            Bitmap bloomImage = new Bitmap(width, height);
            BitmapData originalData = originalImage.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            BitmapData bloomData = bloomImage.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);

            try
            {
                int bytesPerPixel = 4;
                int stride = originalData.Stride;

                byte[] originalPixels = new byte[width * height * bytesPerPixel];
                byte[] bloomPixels = new byte[width * height * bytesPerPixel];

                Marshal.Copy(originalData.Scan0, originalPixels, 0, originalPixels.Length);


                if (asm)
                {
                    MyProc1(
                    originalPixels,
                    bloomPixels,
                    width,
                    height
                );
                }
                else
                {
                    for (int y = 0; y < height; y++)
                    {
                        for (int x = 0; x < width; x++)
                        {
                            int index = y * stride + x * bytesPerPixel;

                            byte blue = originalPixels[index];
                            byte green = originalPixels[index + 1];
                            byte red = originalPixels[index + 2];

                            float brightness = (red + green + blue) / 3.0f;

                            byte resultBlue, resultGreen, resultRed;

                            if (brightness > 191f)
                            {
                                resultBlue = blue;
                                resultGreen = green;
                                resultRed = red;
                            }
                            else
                            {
                                resultBlue = 0;
                                resultGreen = 0;
                                resultRed = 0;
                            }

                            int resultIndex = y * stride + x * bytesPerPixel;

                            bloomPixels[resultIndex] = resultBlue;
                            bloomPixels[resultIndex + 1] = resultGreen;
                            bloomPixels[resultIndex + 2] = resultRed;
                            bloomPixels[resultIndex + 3] = 255;
                        }
                    };
                }

                Marshal.Copy(bloomPixels, 0, bloomData.Scan0, bloomPixels.Length);
            }
            finally
            {
                originalImage.UnlockBits(originalData);
                bloomImage.UnlockBits(bloomData);
            }
            return bloomImage;
        }
        private Bitmap ApplyGaussianBlur(Bitmap image)
        {
            Bitmap result = new Bitmap(image.Width, image.Height);

            double[] kernel = Kernel1D(size, sigma);

            Bitmap tempResult = new Bitmap(image.Width, image.Height);

            ApplyBlur(image, tempResult, kernel, true);

            ApplyBlur(tempResult, result, kernel, false);

            return result;
        }

        private void ApplyBlur(Bitmap source, Bitmap destination, double[] kernel, bool horizontal)
        {
            int radius = kernel.Length / 2;
            int width = source.Width;
            int height = source.Height;

            BitmapData sourceData = source.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            BitmapData destData = destination.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);

            int bytesPerPixel = 4;
            int stride = sourceData.Stride;

            byte[] sourcePixels = new byte[width * height * bytesPerPixel];
            byte[] destPixels = new byte[width * height * bytesPerPixel];

            Marshal.Copy(sourceData.Scan0, sourcePixels, 0, sourcePixels.Length);

            if (asm)
            {
                if (horizontal)
                {

                    Parallel.For(0, height, new ParallelOptions
                    {
                        MaxDegreeOfParallelism = this.threads
                    }, y =>
                    {

                        for (int x = 0; x < width; x++)
                        {


                            MyProc2(sourcePixels,
                            destPixels,
                            x, y,
                            kernel,
                            radius,
                            stride,
                            height);

                        }
                    });
                }
                else
                {
                    Parallel.For(0, height, new ParallelOptions
                    {
                        MaxDegreeOfParallelism = this.threads
                    }, y =>
                    {
                        for (int x = 0; x < width; x++)
                        {

                            MyProc3(sourcePixels,
                            destPixels,
                            x, y,
                            kernel,
                            radius,
                            stride,
                            width);
                        }
                    });
                }
            }
            else
            {
                if (horizontal)
                {

                    Parallel.For(0, height, new ParallelOptions
                    {
                        MaxDegreeOfParallelism = this.threads
                    }, y =>
                    {

                        for (int x = 0; x < width; x++)
                        {
                            double red = 0, green = 0, blue = 0;
                            for (int i = 0; i < kernel.Length; i++)
                            {

                                int yOffset = y + i - radius;

                                if (yOffset >= 0 && yOffset < height)
                                {
                                    int index = (yOffset * stride) + (x * bytesPerPixel);

                                    double weight = kernel[i];
                                    red += sourcePixels[index + 2] * weight;
                                    green += sourcePixels[index + 1] * weight;
                                    blue += sourcePixels[index] * weight;
                                }
                            }

                            int destIndex = (y * stride) + (x * bytesPerPixel);
                            destPixels[destIndex] = (byte)blue;
                            destPixels[destIndex + 1] = (byte)green;
                            destPixels[destIndex + 2] = (byte)red;
                            destPixels[destIndex + 3] = 255;



                        }
                    });
                }
                else
                {
                    Parallel.For(0, height, new ParallelOptions
                    {
                        MaxDegreeOfParallelism = this.threads
                    }, y =>
                    {
                        for (int x = 0; x < width; x++)
                        {
                            double red = 0.0, green = 0.0, blue = 0.0;

                            for (int i = 0; i < kernel.Length; i++)
                            {
                                int xOffset = x + i - radius;

                                if (xOffset >= 0 && xOffset < width)
                                {
                                    int index = (y * stride) + (xOffset * bytesPerPixel);

                                    double weight = kernel[i];
                                    red += sourcePixels[index + 2] * weight;
                                    green += sourcePixels[index + 1] * weight;
                                    blue += sourcePixels[index] * weight;
                                }
                            }

                            int destIndex = (y * stride) + (x * bytesPerPixel);
                            destPixels[destIndex] = (byte)blue;
                            destPixels[destIndex + 1] = (byte)green;
                            destPixels[destIndex + 2] = (byte)red;
                            destPixels[destIndex + 3] = 255;
                        }

                    });
                }
            }

            Marshal.Copy(destPixels, 0, destData.Scan0, destPixels.Length);

            source.UnlockBits(sourceData);
            destination.UnlockBits(destData);
        }
        private double[] Kernel1D(int size, double sigma)
        {
            double[] kernel = new double[size];
            double sum = 0.0;

            for (int i = 0; i < size; i++)
            {
                double x = i - size / 2;
                kernel[i] = Math.Exp(-(x * x) / (2 * sigma * sigma));
                sum += kernel[i];
            }


            for (int i = 0; i < size; i++)
            {
                kernel[i] /= sum;
            }

            return kernel;
        }

    }
}
