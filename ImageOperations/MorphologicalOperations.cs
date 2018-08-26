using FingerprintRecognition.util;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FingerprintRecognition.ImageOperations
{
    public class MorphologicalOperations
    {
        
        #region EROSION
        public static Bitmap Erosion(Bitmap originalBitmap, int[,] mask)
        {
            Bitmap newBitmap = null;

            if (originalBitmap != null)
            {
                newBitmap = CalculateErosion(originalBitmap, mask);
                Debug.Print("Finish calculate erosion");
            }
            else
            {
                Debug.Print("Original bitmap is null");
            }

            return newBitmap;
        }

        private static Bitmap CalculateErosion(Bitmap originalBitmap, int[,] mask)
        {
            Bitmap newBitmap = new Bitmap(originalBitmap);
            BitmapData bitmapData = newBitmap.LockBits(new Rectangle(0, 0, newBitmap.Width, newBitmap.Height),
                              System.Drawing.Imaging.ImageLockMode.ReadWrite,
                              newBitmap.PixelFormat);
            int pixelSize = 4;

            int width = newBitmap.Width;
            int height = newBitmap.Height;

            int maskWidth = mask.GetLength(1);
            int maskHeight = mask.GetLength(0);

            int maskWidthStart = -(maskWidth / 2);
            int maskHeightStart = -(maskHeight / 2);
            Debug.Print("h " + maskHeightStart + " w " + maskWidthStart);

            unsafe
            {
                for (int y = 1; y < height - 1; y++) //odsunąć na inną odległość przy zmianie szerokości i wysokości maski
                {
                    byte* row = (byte*)bitmapData.Scan0 + (y * bitmapData.Stride);
                    for (int x = 1; x < width - 1; x++)
                    {
                        bool canErasePixel = false;
                        for (int j = maskHeightStart; j <= -maskHeightStart; j++)
                        {
                            for (int i = maskWidthStart; i <= -maskHeightStart; i++)
                            {
                                int pixelX = x + i;
                                int pixelY = y + j;
                                //Color pixelColor = originalBitmap.GetPixel(pixelX, pixelY);
                                Color pixelColor = ImageUtils.GetColorPixelFromRGB(row[x * pixelSize], row[x * pixelSize + 1], row[x * pixelSize + 2]);
                                int pixelValue = GetPixelValue(pixelColor);
                                int maskValue = mask[i + (-(maskWidthStart)), j + (-(maskHeightStart))];

                                if (maskValue == 1 && pixelValue == 0)
                                {
                                    canErasePixel = true;
                                }
                            }
                        }

                        if (canErasePixel == true)
                        {
                            //newBitmap.SetPixel(x, y, Color.White);
                            row[x * pixelSize] = 255;
                            row[x * pixelSize + 1] = 255;
                            row[x * pixelSize + 2] = 255;
                        }
                    }
                }
            }
            newBitmap.UnlockBits(bitmapData);
            return newBitmap;
        }
        #endregion


        #region DILATATION
        public static Bitmap Dilatation(Bitmap originalBitmap, int[,] mask)
        {
            Bitmap newBitmap = null;

            if (originalBitmap != null)
            {
                newBitmap = CalculateDilatation(originalBitmap, mask);
                Debug.Print("Finish calculate dilatation");
            }
            else
            {
                Debug.Print("Original bitmap is null");
            }

            return newBitmap;
        }

        private static Bitmap CalculateDilatation(Bitmap originalBitmap, int[,] mask)
        {
            Bitmap newBitmap = new Bitmap(originalBitmap);

            int width = newBitmap.Width;
            int height = newBitmap.Height;

            int maskWidth = mask.GetLength(1);
            int maskHeight = mask.GetLength(0);

            int maskWidthStart = -(maskWidth / 2);
            int maskHeightStart = -(maskHeight / 2);
            Debug.Print("h " + maskHeightStart + " w " + maskWidthStart);

            int startWidth = -maskWidthStart;
            int startHeight = -maskHeightStart;

            for (int y = startHeight; y < height - startHeight; y++) //odsunąć na inną odległość przy zmianie szerokości i wysokości maski
            {
                for (int x = startWidth; x < width - startWidth; x++)
                {
                    bool canDrawPixel = false;
                    for (int j = maskHeightStart; j <= -maskHeightStart; j++)
                    {
                        for (int i = maskWidthStart; i <= -maskHeightStart; i++)
                        {
                            int pixelX = x + i;
                            int pixelY = y + j;
                            Color pixelColor = originalBitmap.GetPixel(pixelX, pixelY);
                            int pixelValue = GetPixelValue( pixelColor);
                          
                            int maskValue = mask[ j + (-(maskHeightStart)), i + (-(maskWidthStart))];

                            if(maskValue == 1 && pixelValue == maskValue)
                            {
                                canDrawPixel = true;
                            }
                        }
                    }
                    int centerPixelValue = GetPixelValue(originalBitmap.GetPixel(x, y));
                    if (canDrawPixel == true || centerPixelValue == 1)
                    {
                        newBitmap.SetPixel(x, y, Color.Black);
                    }

                }
            }

            return newBitmap;
        }
        #endregion

        public delegate void StatusUpdateHandler(object sender, ProgressEventArgs e);
        public static event StatusUpdateHandler OnUpdateErosionStatus;
        public static event StatusUpdateHandler OnUpdateDilatationStatus;

        public static Bitmap Opening(Bitmap originalBitmap, int[,] erosionMask, int[,] dilatationMask)
        {
            Bitmap erosionBitmap = Erosion(originalBitmap, erosionMask);
            Bitmap dilatationBitmap = Dilatation(erosionBitmap, dilatationMask);
            return dilatationBitmap;
        }

        public static Bitmap ErosionDirectional(Bitmap originalBitmap, double[,] angles)
        {
            Bitmap newBitmap = new Bitmap(originalBitmap);
            BitmapData bitmapData = newBitmap.LockBits(new Rectangle(0, 0, newBitmap.Width, newBitmap.Height),
                         System.Drawing.Imaging.ImageLockMode.ReadWrite,
                         newBitmap.PixelFormat);
            int pixelSize = 4;
            int height = originalBitmap.Height;
            int width = originalBitmap.Width;

            
            int stop = 1;

            //int[,] maskHorizontal = { { 0, 1, 0 }, { 1, 1, 1 }, { 0, 1, 0 } };
            //int[,] maskVertical = { { 0, 1, 0 }, { 0, 1, 0 }, { 0, 1, 0 } };
            //int[,] maskCross1 = { { 0, 0, 1 }, { 0, 1, 0 }, { 1, 0, 0 } };
            //int[,] maskCross2 = { { 1, 0, 0 }, { 0, 1, 0 }, { 0, 0, 1 } };

            int[,] maskHorizontal = { { 0, 0, 0 }, { 0, 1, 1 }, { 0, 1, 0 } };
            int[,] maskVertical = { { 0, 1, 0 }, { 0, 1, 0 }, { 1, 0, 0 } };
            int[,] maskCross1 = { { 0, 0, 1 }, { 0, 1, 0 }, { 1, 0, 0 } };
            int[,] maskCross2 = { { 1, 0, 0 }, { 0, 1, 0 }, { 0, 0, 1 } };

            int[,] currentMask = maskHorizontal;

            unsafe
            {
                for (int y = stop; y < height - stop; y++)
                {
                    byte* row = (byte*)bitmapData.Scan0 + (y * bitmapData.Stride);
                    for (int x = stop; x < width - stop; x++)
                    {
                        double angle = angles[y, x];
                        if (angle >= 10 && angle <= 45)
                        {
                            //Debug.Print("maskCross1");
                            currentMask = maskCross1;
                        }
                        else if (angle > 135 && angle <= 170)
                        {
                            currentMask = maskCross2;
                            //Debug.Print("maskCross2");
                        }
                        else if ((angle > 0 && angle < 10) || (angle > 170 && angle <= 180))
                        {
                            currentMask = maskHorizontal;
                            //Debug.Print("maskHorizontal");
                        }
                        else if (angle > 45 && angle <= 135)
                        {
                            // Debug.Print("maskVertical");
                            currentMask = maskVertical;
                        }

                        int maskWidth = currentMask.GetLength(1);
                        int maskHeight = currentMask.GetLength(0);
                        int maskWidthStart = -(maskWidth / 2);
                        int maskHeightStart = -(maskHeight / 2);
                        //Debug.Print("h " + maskHeightStart + " w " + maskWidthStart);

                        int startWidth = -maskWidthStart;
                        int startHeight = -maskHeightStart;

                        bool canErasePixel = false;

                        for (int j = maskHeightStart; j <= -maskHeightStart; j++)
                        {
                            //row = (byte*)bitmapData.Scan0 + (y + j * bitmapData.Stride);
                            for (int i = maskWidthStart; i <= -maskHeightStart; i++)
                            {
                                int pixelX = x + i;
                                int pixelY = y + j;
                               
                                //Color pixelColor = ImageUtils.GetColorPixelFromRGB(row[pixelX * pixelSize],
                                //    row[pixelX * pixelSize + 1], row[pixelX * pixelSize + 2]);
                                Color pixelColor = originalBitmap.GetPixel(pixelX, pixelY);
                                //Debug.Print(pixelColor.ToArgb().ToString() + " === " + pixelColorTmp.ToArgb().ToString());
                                int pixelValue = GetPixelValue(pixelColor);
                                int maskValue = currentMask[i + (-(maskWidthStart)), j + (-(maskHeightStart))];

                                if (maskValue == 1 && pixelValue == 0)
                                {
                                    canErasePixel = true;
                                }
                            }
                        }

                        if (canErasePixel == true)
                        {
                            //newBitmap.SetPixel(x, y, Color.White);
                            row[x * pixelSize] = 255;
                            row[x * pixelSize + 1] = 255;
                            row[x * pixelSize + 2] = 255;
                        }

                        if (OnUpdateErosionStatus != null)
                        {
                            OnUpdateErosionStatus(new Object(), new ProgressEventArgs(width, height, x, y));
                        }
                    }
                }
            }
            newBitmap.UnlockBits(bitmapData);
            return newBitmap;
        }


        public static Bitmap DilatationDirectional(Bitmap originalBitmap, double[,] angles)
        {
            Bitmap newBitmap = new Bitmap(originalBitmap);
            BitmapData bitmapData = newBitmap.LockBits(new Rectangle(0, 0, newBitmap.Width, newBitmap.Height),
             System.Drawing.Imaging.ImageLockMode.ReadWrite,
             newBitmap.PixelFormat);
            int pixelSize = 4;
            int height = originalBitmap.Height;
            int width = originalBitmap.Width;

            int stop = 1;

            int[,] maskHorizontal = { { 0, 1, 0 }, { 1, 1, 1 }, { 0, 1, 0 } };
            int[,] maskVertical = { { 0, 1, 0 }, { 0, 1, 0 }, { 0, 1, 0 } };
            int[,] maskCross1 = { { 0, 0, 1 }, { 0, 1, 0 }, { 1, 0, 0 } };
            int[,] maskCross2 = { { 1, 0, 0 }, { 0, 1, 0 }, { 0, 0, 1 } };

            int[,] currentMask = maskHorizontal;

            unsafe
            {
                for (int y = stop; y < height - stop; y++)
                {
                    byte* row = (byte*)bitmapData.Scan0 + (y * bitmapData.Stride);
                    for (int x = stop; x < width - stop; x++)
                    {
                        double angle = angles[y, x];
                        if (angle >= 10 && angle <= 45)
                        {
                            //Debug.Print("maskCross1");
                            currentMask = maskCross1;
                        }
                        else if (angle > 135 && angle <= 170)
                        {
                            currentMask = maskCross2;
                            //Debug.Print("maskCross2");
                        }
                        else if ((angle > 0 && angle < 10) || (angle > 170 && angle <= 180))
                        {
                            currentMask = maskHorizontal;
                            //Debug.Print("maskHorizontal");
                        }
                        else if (angle > 45 && angle <= 135)
                        {
                            // Debug.Print("maskVertical");
                            currentMask = maskVertical;
                        }

                        int maskWidth = currentMask.GetLength(1);
                        int maskHeight = currentMask.GetLength(0);
                        int maskWidthStart = -(maskWidth / 2);
                        int maskHeightStart = -(maskHeight / 2);
                        //Debug.Print("h " + maskHeightStart + " w " + maskWidthStart);

                        int startWidth = -maskWidthStart;
                        int startHeight = -maskHeightStart;

                        bool canDrawPixel = false;

                        for (int j = maskHeightStart; j <= -maskHeightStart; j++)
                        {
                            for (int i = maskWidthStart; i <= -maskHeightStart; i++)
                            {
                                int pixelX = x + i;
                                int pixelY = y + j;
                                //Color pixelColor = ImageUtils.GetColorPixelFromRGB(row[x * pixelSize], row[x * pixelSize + 1], row[x * pixelSize + 2]);
                                Color pixelColor = originalBitmap.GetPixel(pixelX, pixelY);
                                int pixelValue = GetPixelValue(pixelColor);

                                int maskValue = currentMask[j + (-(maskHeightStart)), i + (-(maskWidthStart))];

                                if (maskValue == 1 && pixelValue == maskValue)
                                {
                                    canDrawPixel = true;
                                }
                            }
                        }

                        int centerPixelValue = GetPixelValue(originalBitmap.GetPixel(x, y));
                        if (canDrawPixel == true || centerPixelValue == 1)
                        {
                            //newBitmap.SetPixel(x, y, Color.Black);
                            row[x * pixelSize] = 0;
                            row[x * pixelSize + 1] = 0;
                            row[x * pixelSize + 2] = 0;
                        }

                        if (OnUpdateDilatationStatus != null)
                        {
                            OnUpdateDilatationStatus(new Object(), new ProgressEventArgs(width, height, x, y));
                        }
                    }
                }
            }
            newBitmap.UnlockBits(bitmapData);
            return newBitmap;
        }

        private static int GetPixelValue(Color color)
        {
            if(color.ToArgb() == Color.Black.ToArgb())
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public static Bitmap DeleteLonelyPixel(Bitmap originalBitmap)
        {
            int width = originalBitmap.Width;
            int height = originalBitmap.Height;

            Bitmap newBitmap = new Bitmap(originalBitmap);

            for (int y = 1; y < height-1; y++)
            {
                for (int x = 1; x < width-1; x++)
                {
                    int counter = 0;
                    for (int j = -1; j <= 1; j++)
                    {
                        for (int i = -1; i <= 1; i++)
                        {
                            int pixelX = x + i;
                            int pixelY = y + j;
                            Color pixelColor = originalBitmap.GetPixel( pixelX, pixelY );
                            int pixelValue = GetPixelValue( pixelColor );

                            if(pixelValue == 0)
                            {
                                if (i == 0 && j == 0 && pixelValue == 1)
                                {
                                    continue;
                                }
                                counter++;
                            }

                        }
                        if(counter == 8) //8
                        {
                            newBitmap.SetPixel(x, y, Color.White);
                        }
                    }
                }
              
            }
            return newBitmap;
        }
    }
}
