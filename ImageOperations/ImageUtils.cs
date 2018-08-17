using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FingerprintRecognition.ImageOperations
{
    public class ImageUtils
    {

        public static Bitmap Binarized(Bitmap originalBitmap, byte treshold)
        {
            Bitmap newBitmap = new Bitmap(originalBitmap);
            BitmapData bitmapData = newBitmap.LockBits(new Rectangle(0, 0, newBitmap.Width, newBitmap.Height),
                                  System.Drawing.Imaging.ImageLockMode.ReadWrite,
                                  newBitmap.PixelFormat);
            int pixelSize = 4;
            byte blackValue = 0;
            byte whiteValue = 255;

            unsafe
            {
                for (int y = 0; y < bitmapData.Height; y++)
                {
                    byte* row = (byte*)bitmapData.Scan0 + (y * bitmapData.Stride);
                    for (int x = 0; x < bitmapData.Width; x++)
                    {
                        row[x * pixelSize] = (row[x * pixelSize] < treshold) ? blackValue : whiteValue;
                        row[x * pixelSize + 1] = (row[x * pixelSize + 1] < treshold) ? blackValue : whiteValue;
                        row[x * pixelSize + 2] = (row[x * pixelSize + 2] < treshold) ? blackValue : whiteValue;
                        row[x * pixelSize + 3] = 255;
                    }
                }
            }
            newBitmap.UnlockBits(bitmapData);
            return newBitmap;
        }
    }
}
