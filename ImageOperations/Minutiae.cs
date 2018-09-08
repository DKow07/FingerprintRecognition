using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FingerprintRecognition.ImageOperations
{
    public class Minutiae
    {
        public int X { get; set; }
        public int Y { get; set; }
        public double Angle { get; set; }
        public MinutiaeType Type { get; set; }

        public Minutiae(int x, int y, double angle, MinutiaeType type)
        {
            this.X = x;
            this.Y = y;
            this.Angle = angle;
            this.Type = type;
        }

        public override string ToString()
        {
            return "Minucja x = " + X + ", y = " + Y + ", kąt = " + Angle + ", typ = " + Type;
        }
    }

    public enum MinutiaeType
    {
        RIDGE_ENDING = 1,
        BIFURCATION,
        CROSSOVER
    }
}
