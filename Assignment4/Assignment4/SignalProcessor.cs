using System.Drawing;
using System;

namespace Assignment4
{
    public static class SignalProcessor
    {
        public static double[] GetGaussianFilter1D(double sigma)
        {
            int filterSize = (int)Math.Ceiling(sigma * 6);
            if (filterSize % 2 == 0)
            {
                filterSize++;
            }
            double[] ret = new double[filterSize];

            int m = filterSize / 2;
            double c = 1 / (Math.Sqrt(2 * Math.PI) * sigma);
            double expNumerator = 2 * sigma * sigma;

            for (int x = -m; x <= m; x++)
            {
                ret[x + m] = c * Math.Exp(-(x * x) / expNumerator);
            }

            return ret;
        }

        public static double[] Convolve1D(double[] signal, double[] filter)
        {
            double[] ret = new double[signal.Length];

            int m = filter.Length / 2;
            for (int i = 0; i < ret.Length; i++)
            {
                double value = 0;
                for (int j = 0; j < filter.Length; j++)
                {
                    int x = i + m - j;
                    if (x < 0 || x >= ret.Length)
                    {
                        continue;
                    }
                    value += signal[x] * filter[j];
                }
                ret[i] = value;
            }
            return ret;
        }

        public static double[,] GetGaussianFilter2D(double sigma)
        {
            int filterSize = (int)Math.Ceiling(sigma * 6);
            if (filterSize % 2 == 0)
            {
                filterSize++;
            }
            double[,] ret = new double[filterSize, filterSize];

            int m = filterSize / 2;
            double c = 1 / (2 * Math.PI * sigma * sigma);
            double expNumerator = 2 * sigma * sigma;

            for (int x = -m; x <= m; x++)
            {
                for (int y = -m; y <= m; y++)
                {
                    ret[x + m, y + m] = c * Math.Exp(-((x * x) + (y * y)) / expNumerator);
                }
            }
            return ret;
        }

        public static Bitmap ConvolveImage(Bitmap bitmap, double[,] filter)
        {
            Bitmap newBitmap = new Bitmap(bitmap.Width, bitmap.Height);
            int filterLength = filter.GetLength(0);
            int m = filterLength / 2;
            for (int x = 0; x < newBitmap.Width; x++)
            {
                for (int y = 0; y < newBitmap.Height; y++)
                {
                    double r = 0;
                    double g = 0;
                    double b = 0;

                    for (int i = 0; i < filterLength; i++)
                    {
                        for (int j = 0; j < filterLength; j++)
                        {
                            int cX = x + m - i;
                            int cY = y + m - j;
                            if ((cX < 0 || cX >= newBitmap.Width) || (cY < 0 || cY >= newBitmap.Height))
                            {
                                continue;
                            }
                            Color pixelColor = bitmap.GetPixel(cX, cY);
                            r += (double)pixelColor.R * filter[i, j];
                            g += (double)pixelColor.G * filter[i, j];
                            b += (double)pixelColor.B * filter[i, j];
                        }
                    }
                    int rInt = r < 0 ? 0 : (int)r;
                    int gInt = g < 0 ? 0 : (int)g;
                    int bInt = b < 0 ? 0 : (int)b;
                    newBitmap.SetPixel(x, y, Color.FromArgb(rInt, gInt, bInt));
                }
            }
            return newBitmap;
        }
    }
}
