using FingerprintRecognition.ImageOperations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.LinearAlgebra;
using System.Diagnostics;

namespace FingerprintRecognition.Matching
{
    public class MatchingFingerprints
    {

        private static double[] anglesArray;

        private static double theta_base = 300;
        
        private static void InitAnglesArray(int step)
        {
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

            List<double> angles = anglesArray.ToList();
            //Dictionary<Minutiae, int> votes = new Dictionary<Minutiae, int>();
            Votes votes = new Votes();

            for(int mi = 0; mi < minutiaesOriginal.Count; mi++)
            {
                for(int mj = 0; mj < minutiaesScann.Count; mj++)
                {
                    Minutiae m1 = minutiaesOriginal[mi];
                    Minutiae m2 = minutiaesScann[mj];
                    for(int t = 0; t < angles.Count; t++)
                    {
                        double theta = angles[t];

                        if (dd(m2.Angle + theta, m1.Angle) < theta_base)
                        {
                            Vector2 position = CalculateNewPosition(m1, m2, theta);
                            votes.Check(Convert.ToInt32(position.x), Convert.ToInt32(position.y), theta);
                        }
                    }
                }
            }

            votes.Print();

            return votes.GetTranslationVotesByMaxVotes();
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

            matrixI[0, 0] = m1.X;
            matrixI[1, 0] = m1.Y;
            matrixJ[0, 0] = m2.X;
            matrixJ[1, 0] = m2.Y;

            matrixAngle[0,0] = Math.Cos(t);
            matrixAngle[1,0] = -Math.Sin(t);
            matrixAngle[0,1] = Math.Sin(t);  
            matrixAngle[1,1] = Math.Cos(t);

            Matrix<double> multiplyMatrix = matrixAngle.Multiply(matrixJ);
            Matrix<double> result = matrixI.Subtract(multiplyMatrix);
            //Debug.Print(result[0,0] + " " + result[1, 0]);
            return new Vector2(Math.Round(result[0, 0]), Math.Round(result[1, 0]));
        }

        private static double dd(double d1, double d2)
        {
            //return d1 - d2;
            return Math.Min(Math.Abs(d1 - d2), Math.Abs(d1 + Math.PI - d2));
        }
    }
}
