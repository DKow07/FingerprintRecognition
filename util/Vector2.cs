using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FingerprintRecognition.util
{
    public class Vector2
    {
        public double x;
        public double y;


        #region CONSTRUCTORS
        public Vector2()
        {
            this.x = 0f;
            this.y = 0f;
        }

        public Vector2(double x, double y)
        {
            this.x = x;
            this.y = y;
        }
        #endregion

        public double Dot(Vector2 vector)
        {
            return this.x * vector.x + this.y * vector.y;
        }

        public double GetAngleBetweenVectorAndOx()
        {
            Vector2 ox = new Vector2(1, 0);
            double dot = ox.Dot(this);
            // Debug.Print("dot " + dot.ToString());
            double denominator = ox.Length() * this.Length();
            double result = dot / denominator;
            double rad = Math.Acos(result);
            double angle = RadianToDegree(rad);
            return angle;
        }

        public void Normalize()
        {
            this.x /= (double)this.Length();
            this.y /= (double)this.Length();
        }

        public double Length() //||A|| 
        {
            return Math.Sqrt(this.LengthSquared());
        }

        public double LengthSquared() //|A| 
        {
            return (Math.Pow(x, 2) + Math.Pow(y, 2));
        }

        private double DegreeToRadian(double angle)
        {
            return Math.PI * angle / 180.0;
        }

        private double RadianToDegree(double angle)
        {
            return angle * (180.0 / Math.PI);
        }

        public void Display()
        {
            Console.WriteLine("Vector2 " + this.x + " " + this.y);
        }
    }
}
