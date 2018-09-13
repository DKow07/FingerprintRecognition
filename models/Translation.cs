namespace FingerprintRecognition.models
{
    public class Translation
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int T { get; set; }

        public Translation(int x, int y, int t)
        {
            this.X = x;
            this.Y = y;
            this.T = t;
        }

        public override string ToString()
        {
            return "x=" + X + " y=" + Y + " t=" + T;
        }
    }
}