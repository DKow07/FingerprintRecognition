using FingerprintRecognition.ImageOperations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.LinearAlgebra;
using System.Diagnostics;
using FingerprintRecognition.models;

namespace FingerprintRecognition.Matching
{
    public class MatchingFingerprints
    {
      /*  private static double[] anglesArray;
        private static double theta_base = 180;
        private static int[, ,] accumulator;
        private static int xLength;
        private static int yLength;
        private static int angleLength;
        private static int angleStep;

        
        public static void SetAccumulatorDimension(int bitmapWidth, int bitmapHeight)
        {
            xLength = bitmapWidth;
            yLength = bitmapHeight;
            angleLength = 36;

            accumulator = new int[xLength, yLength, angleLength];
        }

        private static void InitAnglesArray(int step)
        {
            angleStep = step;
            int count = 360 / step;
            anglesArray = new double[count];
            for(int i = 0; i < anglesArray.Length; i++)
            {
                anglesArray[i] = i * step;
            }
        }

        public static TranslationVotes Matching(List<Minutiae> minutiaesOriginal, List<Minutiae> minutiaesScann)
        {
            InitAnglesArray(10);       
            for(int mi = 0; mi < minutiaesOriginal.Count; mi++)
            {
                for(int mj = 0; mj < minutiaesScann.Count; mj++)
                {
                    Minutiae m1 = minutiaesOriginal[mi];
                    Minutiae m2 = minutiaesScann[mj];
                    for(int t = 0; t < anglesArray.Length; t++)
                    {
                        double theta = anglesArray[t];

                        if (dd(m2.Angle + theta, m1.Angle) < theta_base)
                        {
                            Vector2 position = CalculateNewPosition(m1, m2, theta);
                            try
                            {
                                int x = Convert.ToInt32(position.x);
                                int y = Convert.ToInt32(position.y);
                                int tt = Convert.ToInt32(theta);

                                if (IsInside(x,y,tt))
                                {
                                    accumulator[x, y, tt]++;
                                    VoteNeighberhood(x, y, tt);
                                }
                            }
                            catch(Exception e)
                            {
                                Debug.Print(Convert.ToInt32(position.x) + " " + Convert.ToInt32(position.y) + " " + Convert.ToInt32(theta));
                            }
                        }
                    }
                }
            }

            Coordinate coordinate = FindOptimalTransformationInAccumulator();
            return new TranslationVotes(coordinate.IndexJ, coordinate.IndexI, coordinate.IndexK);
        }

        private static void VoteNeighberhood(int x, int y, int t)
        {
            for (int i = -1; i < 1; i++)
            {
                for (int j = -1; j < 1; j++)
                {
                    for (int k = -1; k < 1; k++)
                    {
                        if(IsInside(x+i, y+j, t+k))
                        {
                            accumulator[x + i, y + j, t + k]++;
                        }
                    }
                }
            }
        }

        private static bool IsInside(int x, int y, int t)
        {
            return x >= 0 && x < xLength &&
                  y >= 0 && y < yLength &&
                  t >= 0 && t < angleLength;
        }

       /* private static Coordinate FindOptimalTransformationInAccumulator()
        {
            int max = accumulator[0, 0, 0];
            int indexI = 0, indexJ = 0, indexK = 0;

            for (int i = 1; i < accumulator.GetLength(0); i++)
            {
                for (int j = 1; j < accumulator.GetLength(1); j++)
                {
                    for (int k = 1; k < accumulator.GetLength(2); k++)
                    {
                        int tmpValue = accumulator[i,j,k];
                        if(tmpValue > max)
                        {
                            max = tmpValue;
                            indexI = i;
                            indexJ = j;
                            indexK = k;
                        }
                    }
                }
            }

            return new Coordinate(indexI,indexJ,indexK);
        }

        private static double percentTolerance = 0.7;

        public static bool IsIdentical(List<Minutiae> m1, List<Minutiae> m2, TranslationVotes optimalTransform)
        {
            int tolerance = Convert.ToInt32(Math.Round(m1.Count * percentTolerance));
            int matchedMinutiaes = 0;
            List<Minutiae> transformed = TransformMinutiaes(m2, optimalTransform);

            foreach(Minutiae minutiae1 in m1)
            {
                foreach(Minutiae minutiae2 in m2)
                {
                    if(Overlap(minutiae1, minutiae2) && minutiae2.IsMatching == false)
                    {
                        minutiae2.IsMatching = true;
                        matchedMinutiaes++;
                        break;
                    }
                }
            }

            Debug.Print("Tolerance = " + tolerance + " matched = " + matchedMinutiaes);
            return matchedMinutiaes >= tolerance;
        }

        private static bool Overlap(Minutiae m1, Minutiae m2)
        {
            if(m1.Compare(m2))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private static List<Minutiae> TransformMinutiaes(List<Minutiae> m, TranslationVotes o)
        {
            List<Minutiae> transformedMinutiae = new List<Minutiae>();
            foreach(Minutiae minutiae in m)
            {
                transformedMinutiae.Add(new Minutiae(minutiae.X + o.DeltaX, minutiae.Y + o.DeltaY, minutiae.Angle + o.DeltaTheta, minutiae.Type));
            }
            return transformedMinutiae;
        }

        private static Vector2 CalculateNewPosition(Minutiae m1, Minutiae m2, double t)
        {
            Matrix<double> matrixI = Matrix<double>.Build.Dense(2, 1);
            Matrix<double> matrixAngle = Matrix<double>.Build.Dense(2, 2);
            Matrix<double> matrixJ = Matrix<double>.Build.Dense(2, 1);

            matrixI[0, 0] = m2.X; //m1
            matrixI[1, 0] = m2.Y;
            matrixJ[0, 0] = m1.X;
            matrixJ[1, 0] = m1.Y;

            matrixAngle[0,0] = Math.Cos(t);
            matrixAngle[1,0] = -Math.Sin(t);
            matrixAngle[0,1] = Math.Sin(t);  
            matrixAngle[1,1] = Math.Cos(t);

            Matrix<double> multiplyMatrix = matrixAngle.Multiply(matrixJ);
            Matrix<double> result = matrixI.Subtract(multiplyMatrix);
            return new Vector2(Math.Round(result[0, 0]), Math.Round(result[1, 0]));
        }

        private static double dd(double d1, double d2)
        {
            return d1 - d2;
            //return Math.Min(Math.Abs(d1 - d2), Math.Abs(d1 + Math.PI - d2));
        }*/
    }
}