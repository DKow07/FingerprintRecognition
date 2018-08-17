using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FingerprintRecognition.ImageOperations
{
    public class Thinning
    {
        private static bool[][] Image2Bool(Bitmap originalBitmap)
        {
            Bitmap newBitmap = new Bitmap(originalBitmap);
            BitmapData bitmapData = newBitmap.LockBits(new Rectangle(0, 0, newBitmap.Width, newBitmap.Height),
                                System.Drawing.Imaging.ImageLockMode.ReadWrite,
                                newBitmap.PixelFormat);
            int pixelSize = 4;
            bool[][] boolArray = new bool[newBitmap.Height][];
            unsafe
            {
                for (int y = 0; y < newBitmap.Height; y++)
                {
                    byte* row = (byte*)bitmapData.Scan0 + (y * bitmapData.Stride);
                    boolArray[y] = new bool[newBitmap.Width];
                    for (int x = 0; x < newBitmap.Width; x++)
                    {
                        if (row[x * pixelSize] < 80
                            && row[x * pixelSize + 1] < 80
                            && row[x * pixelSize + 2] < 80)
                        {
                            boolArray[y][x] = true;
                        }
                        else
                        {
                            boolArray[y][x] = false;
                        }
                    }
                }
            }
            newBitmap.UnlockBits(bitmapData);
            return boolArray;

        }

        private static Bitmap Bool2Image(bool[][] boolArray)
        {
            Bitmap newBitmap = new Bitmap(boolArray[0].Length, boolArray.Length);
            using (Graphics g = Graphics.FromImage(newBitmap)) 
            {
                g.Clear(Color.White);
            }
            BitmapData bitmapData = newBitmap.LockBits(new Rectangle(0, 0, newBitmap.Width, newBitmap.Height),
                                System.Drawing.Imaging.ImageLockMode.ReadWrite,
                                newBitmap.PixelFormat);
            int pixelSize = 4;
            unsafe
            {
                for (int y = 0; y < newBitmap.Height; y++)
                {
                    byte* row = (byte*)bitmapData.Scan0 + (y * bitmapData.Stride);
                    for (int x = 0; x < newBitmap.Width; x++)
                    {
                        if(boolArray[y][x])
                        {
                            row[x * pixelSize] = 0;
                            row[x * pixelSize + 1] = 0;
                            row[x * pixelSize + 2] = 0;
                            row[x * pixelSize + 3] = 255;
                        }
                    }
                }
            }
            newBitmap.UnlockBits(bitmapData);
            return newBitmap;
        }

        private static T[][] ArrayClone<T>(T[][] A)
        {
            return A.Select(a => a.ToArray()).ToArray(); 
        }

        private static bool[][] ZhangSuenThinning(bool[][] boolArray)
        {
            bool[][] tmpBoolArray = ArrayClone(boolArray);  
            int count = 0;
            do  
            {
                count = Step(1, tmpBoolArray, boolArray);
                tmpBoolArray = ArrayClone(boolArray);
                count += Step(2, tmpBoolArray, boolArray);
                tmpBoolArray = ArrayClone(boolArray);     
            }
            while (count > 0);

            return boolArray;
        }

        private static int Step(int stepNo, bool[][] temp, bool[][] s)
        {
            int count = 0;

            for (int a = 1; a < temp.Length - 1; a++)
            {
                for (int b = 1; b < temp[0].Length - 1; b++)
                {
                    if (SuenThinningAlg(a, b, temp, stepNo == 2))
                    {
                        if (s[a][b]) count++;
                        s[a][b] = false;
                    }
                }
            }
            return count;
        }

        private static bool SuenThinningAlg(int x, int y, bool[][] s, bool even)
        {
            bool p2 = s[x][y - 1];
            bool p3 = s[x + 1][y - 1];
            bool p4 = s[x + 1][y];
            bool p5 = s[x + 1][y + 1];
            bool p6 = s[x][y + 1];
            bool p7 = s[x - 1][y + 1];
            bool p8 = s[x - 1][y];
            bool p9 = s[x - 1][y - 1];


            int bp1 = NumberOfNonZeroNeighbors(x, y, s);
            if (bp1 >= 2 && bp1 <= 6) 
            {
                if (NumberOfZeroToOneTransitionFromP9(x, y, s) == 1)
                {
                    if (even)
                    {
                        if (!((p2 && p4) && p8))
                        {
                            if (!((p2 && p6) && p8))
                            {
                                return true;
                            }
                        }
                    }
                    else
                    {
                        if (!((p2 && p4) && p6))
                        {
                            if (!((p4 && p6) && p8))
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }

        private static int NumberOfZeroToOneTransitionFromP9(int x, int y, bool[][] s)
        {
            bool p2 = s[x][y - 1];
            bool p3 = s[x + 1][y - 1];
            bool p4 = s[x + 1][y];
            bool p5 = s[x + 1][y + 1];
            bool p6 = s[x][y + 1];
            bool p7 = s[x - 1][y + 1];
            bool p8 = s[x - 1][y];
            bool p9 = s[x - 1][y - 1];

            int A = Convert.ToInt32((!p2 && p3)) + Convert.ToInt32((!p3 && p4)) +
                    Convert.ToInt32((!p4 && p5)) + Convert.ToInt32((!p5 && p6)) +
                    Convert.ToInt32((!p6 && p7)) + Convert.ToInt32((!p7 && p8)) +
                    Convert.ToInt32((!p8 && p9)) + Convert.ToInt32((!p9 && p2));
            return A;
        }

        private static int NumberOfNonZeroNeighbors(int x, int y, bool[][] s)
        {
            int count = 0;
            if (s[x - 1][y]) count++;
            if (s[x - 1][y + 1]) count++;
            if (s[x - 1][y - 1]) count++;
            if (s[x][y + 1]) count++;
            if (s[x][y - 1]) count++;
            if (s[x + 1][y]) count++;
            if (s[x + 1][y + 1]) count++;
            if (s[x + 1][y - 1]) count++;
            return count;
        }

        public static Bitmap Thin(Bitmap originalBitmap)
        {
            bool[][] tmpBoolArray = Image2Bool(originalBitmap);
            tmpBoolArray = ZhangSuenThinning(tmpBoolArray);
            return Bool2Image(tmpBoolArray);
        }

    }
}
