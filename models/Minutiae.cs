using System;

namespace FingerprintRecognition.models
{
    [Serializable]
    public class Minutiae
    {
        public int X { get; set; }
        public int Y { get; set; }
        public double Angle { get; set; }
        public MinutiaeType Type { get; set; }
        public bool IsMatching { get; set; }

        public Minutiae(int x, int y, double angle, MinutiaeType type)
        {
            this.X = x;
            this.Y = y;
            this.Angle = angle;
            this.Type = type;
            this.IsMatching = false;
        }

        public override string ToString()
        {
            return "Minucja x = " + X + ", y = " + Y + ", k¹t = " + Angle + ", typ = " + Type;
        }

        public bool Compare(Minutiae other)
        {
            int toleranceDistance = 70; //70
            double toleranceAngle = 60; //60

            //Debug.Print(this.X + " " + other.X + " " + this.Y + " " + other.Y + " " + this.Angle + " " + other.Angle);

            if(Math.Abs(this.X - other.X) <= toleranceDistance 
                && Math.Abs(this.Y - other.Y) <= toleranceDistance 
                && areAnglesMatch(this.Angle, other.Angle, toleranceAngle))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool areAnglesMatch(double a, double b, double toleranceAngle)
        {
            return Math.Abs(a - b) <= toleranceAngle ||
                    Math.Abs(a + Math.PI - b) <= toleranceAngle;
        }

    }

    public enum MinutiaeType
    {
        RIDGE_ENDING = 1,
        BIFURCATION,
        CROSSOVER
    }
    
}