using System.Data.SqlTypes;
using System.Runtime.InteropServices;
using FingerprintRecognition.models;
using System.Collections.Generic;
using System;
using System.Diagnostics;
using MathNet.Numerics.LinearAlgebra;

namespace FingerprintRecognition.Matching
{
    public class Matcher
    {
        private int[,,] accumulator;
        private int[] angles;
        private int bitmapWidth;
        private int bitmapHeight;
        private int angleCount;
        private int sizeX;
        private int sizeY;
        private double thetaBase = 30;
        
        public Matcher(int bitmapWidth, int bitmapHeight, int angleStep)
        {
            this.bitmapHeight = bitmapHeight;
            this.bitmapWidth = bitmapWidth;
            sizeX = (bitmapWidth * 2); // -1
            sizeY = (bitmapHeight * 2); // -1
            angleCount = InitAngleArray(angleStep);
            accumulator = new int[sizeX, sizeY, angleCount];
            accumulator.Initialize();
        }

        private Vector2 GetOffset(Minutiae m1, Minutiae m2, double angle)
        {
            double offsetX = m1.X - (Math.Cos(angle) * m2.X - Math.Sin(angle) * m2.Y);
            double offsetY = m1.Y - (Math.Sin(angle) * m2.X + Math.Cos(angle) * m2.Y);

            return new Vector2(offsetX, offsetY);
        }

        public Translation Matching(List<Minutiae> minutiaesOriginal, List<Minutiae> minutiaesScann)
        {
            for (int mi = 0; mi < minutiaesOriginal.Count; mi++)
            {
                for (int mj = 0; mj < minutiaesScann.Count; mj++)
                {
                    Minutiae m1 = minutiaesOriginal[mi];
                    Minutiae m2 = minutiaesScann[mj];
                    for (int t = 0; t < angles.Length; t++)
                    {
                        double theta = angles[t];

                        if (dd(m2.Angle + theta, m1.Angle) < thetaBase)
                        {
                            try
                            {
                                Vector2 position = GetOffset(m1, m2, theta);// CalculateNewPosition(m1, m2, theta);
                                int x = Convert.ToInt32(position.x);
                                int y = Convert.ToInt32(position.y);
                                int tt = Convert.ToInt32(theta);

                                Vote(x, y, tt);
                            }
                            catch (Exception e)
                            {
                                //Debug.Print(Convert.ToInt32(position.x) + " " + Convert.ToInt32(position.y) + " " + Convert.ToInt32(theta));
                            }
                        }
                    }
                }
            }

            return GetTranslation();
        }

        public void Vote(int x, int y, int t)
        {
            int xDiff = x + bitmapWidth;
            int yDiff = y + bitmapHeight;
            int tDiff = t / 10;
            if (IsInside(xDiff, yDiff, tDiff))
            {
                accumulator[xDiff, yDiff, tDiff]++;
                VoteNeighborhood(xDiff, yDiff, tDiff);
            }
        }
        private bool IsInside(int x, int y, int t)
        {
            return x >= 0 && x < sizeX &&
             y >= 0 && y < sizeY &&
             t >= 0 && t < angleCount;
        }

        private bool Overlap(Minutiae m1, Minutiae m2)
        {
            if (m1.Compare(m2))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private List<Minutiae> TransformMinutiaes(List<Minutiae> m, TranslationVotes o)
        {
            List<Minutiae> transformedMinutiae = new List<Minutiae>();
            foreach (Minutiae minutiae in m)
            {
                transformedMinutiae.Add(new Minutiae(minutiae.X + o.DeltaX, minutiae.Y + o.DeltaY, minutiae.Angle + o.DeltaTheta, minutiae.Type));
            }
            return transformedMinutiae;
        }

        private Vector2 CalculateNewPosition(Minutiae m1, Minutiae m2, double t)
        {
            Matrix<double> matrixI = Matrix<double>.Build.Dense(2, 1);
            Matrix<double> matrixAngle = Matrix<double>.Build.Dense(2, 2);
            Matrix<double> matrixJ = Matrix<double>.Build.Dense(2, 1);

            matrixI[0, 0] = m1.X; 
            matrixI[1, 0] = m1.Y;
            matrixJ[0, 0] = m2.X;
            matrixJ[1, 0] = m2.Y;

            matrixAngle[0, 0] = Math.Cos(t);
            matrixAngle[1, 0] = -Math.Sin(t);
            matrixAngle[0, 1] = Math.Sin(t);
            matrixAngle[1, 1] = Math.Cos(t);

            Matrix<double> multiplyMatrix = matrixAngle.Multiply(matrixJ);
            Matrix<double> result = matrixI.Subtract(multiplyMatrix);
            return new Vector2(Math.Round(result[0, 0]), Math.Round(result[1, 0]));
        }

        private double dd(double d1, double d2)
        {
            //return d1 - d2;
            return Math.Min(Math.Abs(d1 - d2), Math.Abs(d1 + Math.PI - d2));
        }

        public Translation GetTranslation()
        {
            Translation optimalTranslation = GetOptimalTranslation();
            int x = optimalTranslation.X - bitmapWidth;
            int y = optimalTranslation.Y - bitmapHeight;
            int t = optimalTranslation.T * 10;
            Translation translation = new Translation(x, y, t);
            return translation;
        }

        private Translation GetOptimalTranslation()
        {
            int max = accumulator[0, 0, 0];
            int indexI = 0;
            int indexJ = 0;
            int indexK = 0;

            for (int i = 1; i < accumulator.GetLength(0); i++)
            {
                for (int j = 1; j < accumulator.GetLength(1); j++)
                {
                    for (int k = 1; k < accumulator.GetLength(2); k++)
                    {
                        int tmpValue = accumulator[i, j, k];
                        if (tmpValue > max)
                        {
                            max = tmpValue;
                            indexI = i;
                            indexJ = j;
                            indexK = k;
                        }
                    }
                }
            }

            Translation translation = new Translation(indexI, indexJ, indexK);
            return translation;
        }

        private void VoteNeighborhood(int x, int y, int t)
        {
            for (int i = -1; i < 1; i++)
            {
                for (int j = -1; j < 1; j++)
                {
                    for (int k = -1; k < 1; k++)
                    {
                        if (IsInside(x + i, y + j, t + k))
                        {
                            accumulator[x + i, y + j, t + k]++;
                        }
                    }
                }
            }
        }
        
        private int InitAngleArray(int step)
        {
            int count = 360 / step;
            angles = new int[count];
            for (int i = 0; i < count; i++)
            {
                angles[i] = i * step;
            }

            return count;
        }
    }
}