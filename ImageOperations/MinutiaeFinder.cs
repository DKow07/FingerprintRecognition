using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FingerprintRecognition.ImageOperations
{
    public class MinutiaeFinder
    {
        public Bitmap ShowMinutiae(Bitmap originalBitmap)
        {
            return CalculateMinutiaePosition(originalBitmap);
        }

        private Bitmap CalculateMinutiaePosition(Bitmap bitmapMinutiae)
        {
            Bitmap newBitmap = new Bitmap(bitmapMinutiae);
            Bitmap drawBitmap = new Bitmap(newBitmap);
            int pixelSize = 4;
            int width = newBitmap.Width;
            int height = newBitmap.Height;
            unsafe
            {
                for (int y = 1; y < height - 1; y++)
                {
                    for (int x = 1; x < width - 1; x++)
                    {
                        //Color pixelColor = ImageUtils.GetColorPixelFromRGB(row[x * pixelSize], row[x * pixelSize + 1], row[x * pixelSize + 2]);
                        Color pixelColor = newBitmap.GetPixel(x, y);
                        if (pixelColor.A == 255 && pixelColor.R == 255 && pixelColor.G == 255 && pixelColor.B == 255)
                        {
                            continue;
                        }

                        if (pixelColor.ToArgb() == Color.Black.ToArgb())
                        {
                            int cn = CalculateCrossingNumber(x, y, newBitmap);

                            if (cn == 1)
                            {
                                DrawMinutiae(x, y, Pens.Red, ref drawBitmap);
                            }
                            else if (cn == 4)
                            {
                                DrawMinutiae(x, y, Pens.Gold, ref drawBitmap);
                            }
                            else if (cn == 3)
                            {
                                DrawMinutiae(x, y, Pens.Blue, ref drawBitmap);
                            }

                        }
                    }
                }
            }
            return drawBitmap;
        }


        private unsafe int CalculateCrossingNumber(int x, int y, Bitmap bitmapMinutiae)
        {
            double sum = 0;
            Point[] cells = { new Point(1, 0), new Point(1, -1), new Point(0, -1), new Point(-1, -1), new Point(-1, 0), new Point(-1, 1), new Point(0, 1), new Point(1, 1) };


            for (int i = 0; i < 8; i++)
            {
                int value = bitmapMinutiae.GetPixel(cells[i].X + x, cells[i].Y + y).ToArgb() == Color.Black.ToArgb() ? 1 : 0;
                int idx = i + 1;
                if (idx == 8)
                {
                    idx = 0;
                }
                int valueNext = bitmapMinutiae.GetPixel(cells[idx].X + x, cells[idx].Y + y).ToArgb() == Color.Black.ToArgb() ? 1 : 0;

                sum += Math.Abs(value - valueNext);
            }

            sum *= 0.5;

            //  if (sum < 1.0)
            //      sum = 0;

            return (int)sum;

        }

        private void DrawMinutiae(int x, int y, Pen brushColor, ref Bitmap draw)
        {
            int size = (int)(draw.Height / 90);
            using (Graphics gr = Graphics.FromImage(draw))
            {
                //gr.DrawRectangle(brushColor, x - size, y - size, size*2, size*2);
                gr.DrawEllipse(brushColor, x - size, y - size, size * 2, size * 2);
            }
        }
    }
}
