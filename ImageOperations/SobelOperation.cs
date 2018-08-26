using FingerprintRecognition.util;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FingerprintRecognition.ImageOperations
{
    public class SobelOperation
    {
        public static double[,] CalculateAngles(Bitmap originalBitmap)
        {
            int[,] sobelOperator = { { -1, 0, 1 }, { -2, 0, 2 }, { -1, 0, 1 } };
            int[,] sobelOperator90Degree = { { -1, -2, -1 }, { 0, 0, 0 }, { 1, 2, 1 } };
            int[,] sobelArray = CalculateBitmapWithMask(originalBitmap, sobelOperator);
            int[,] sobel90DegreeArray = CalculateBitmapWithMask(originalBitmap, sobelOperator90Degree);
            Vector2[,] gradient = CreateGradient(sobelArray, sobel90DegreeArray);
            double[,] angles = CreateAngleArray(gradient);
            return AverageAngles(angles, originalBitmap);
        }

        private static int[,] CalculateBitmapWithMask(Bitmap originalBitmap, int[,] mask)
        {
            Bitmap newBitmap = new Bitmap(originalBitmap);
            BitmapData bitmapData = newBitmap.LockBits(new Rectangle(0, 0, newBitmap.Width, newBitmap.Height),
                                 System.Drawing.Imaging.ImageLockMode.ReadWrite,
                                 newBitmap.PixelFormat);
            int pixelSize = 4;

            int[,] resultArray = new int[bitmapData.Height, bitmapData.Width]; //czy są zera

            unsafe
            {
                for (int y = 0; y < bitmapData.Height; y++)
                {
                    byte* row = (byte*)bitmapData.Scan0 + (y * bitmapData.Stride);
                    for (int x = 0; x < bitmapData.Width; x++)
                    {
                        int sum = 0;
                        for (int k = -1; k < 2; k++)
                        {
                            for (int h = -1; h < 2; h++)
                            {
                                int bitmapValue = PixelToInt(row[x * pixelSize], row[x * pixelSize + 1], row[x * pixelSize + 2]);
                                int sobelValue = mask[k + 1, h + 1];
                                sum += bitmapValue * sobelValue;
                            }
                        }
                        resultArray[y, x] = sum;
                    }
                }
            }
            newBitmap.UnlockBits(bitmapData);
            return resultArray;
        }

        private static int PixelToInt(byte r, byte g, byte b)
        {
            if(r == 0 && g == 0 && b == 0)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        private static Vector2[,] CreateGradient(int[,] array1, int[,] array2)
        {
            int sizeY = array1.GetLength(0);
            int sizeX = array1.GetLength(1);

            int sizeYY = array1.GetLength(0);
            int sizeXX = array1.GetLength(1);

            Vector2[,] gradientArray = null;

            if (sizeX == sizeXX && sizeY == sizeYY)
            {
                gradientArray = new Vector2[sizeY, sizeX];

                for (int j = 0; j < sizeY; j++)
                {
                    for (int i = 0; i < sizeX; i++)
                    {
                        if (array2[j, i] < 0)
                        {
                            array1[j, i] *= -1;
                            array2[j, i] *= -1;
                        }

                        gradientArray[j, i] = new Vector2(array1[j, i], array2[j, i]);
                    }
                }
            }

            return gradientArray;
        }

        private static double[,] CreateAngleArray(Vector2[,] array)
        {
            int sizeY = array.GetLength(0);
            int sizeX = array.GetLength(1);

            double[,] angles = new double[sizeY, sizeX];

            for (int j = 0; j < sizeY; j++)
            {
                for (int i = 0; i < sizeX; i++)
                {
                    if (array[j, i].x == 0 && array[j, i].y == 0)
                    {
                        angles[j, i] = 0;
                    }
                    angles[j, i] = array[j, i].GetAngleBetweenVectorAndOx();
                    if (Double.IsNaN(angles[j, i]))
                    {
                        angles[j, i] = 0;
                    }
                }
            }

            return angles;
        }

        private static double[,] AverageAngles(double[,] array, Bitmap originalBitmap)
        {
            Bitmap newBitmap = new Bitmap(originalBitmap);
            BitmapData bitmapData = newBitmap.LockBits(new Rectangle(0, 0, newBitmap.Width, newBitmap.Height),
                                 System.Drawing.Imaging.ImageLockMode.ReadWrite,
                                 newBitmap.PixelFormat);
            int pixelSize = 4;

            int sizeY = array.GetLength(0);
            int sizeX = array.GetLength(1);

            double[,] angles = new double[sizeY, sizeX];
            unsafe
            {
                for (int j = 1; j < sizeY - 1; j++)
                {
                    byte* row = (byte*)bitmapData.Scan0 + (j * bitmapData.Stride);
                    for (int i = 1; i < sizeX - 1; i++)
                    {
                        int cnt = 0;
                        double sum = 0;
                        for (int k = -1; k < 2; k++)
                        {
                            for (int l = -1; l < 2; l++)
                            {
                                int x = i + l;
                                int y = j + k;

                                if (array[y, x] != 0)
                                {
                                    cnt++;
                                    sum += array[y, x];
                                }
                            }
                        }
                        sum /= cnt;
                        //Color pixelColor = GetColorPixel(row[i * pixelSize], row[i * pixelSize + 1], row[i * pixelSize + 2]);
                        //if (pixelColor.A == 255 && pixelColor.R == 0 && pixelColor.G == 0 && pixelColor.B == 0)
                        //{
                        //    angles[j, i] = sum;
                        //}
                        if(row[i * pixelSize] == 0 && row[i * pixelSize + 1] == 0 && row[i * pixelSize + 2] == 0)
                        {
                            angles[j, i] = sum;
                        }
                    }
                }
            }
            return angles;
        }
    }
}
