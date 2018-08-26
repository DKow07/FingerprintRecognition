using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FingerprintRecognition.util
{
    public class ProgressEventArgs
    {
        public int Width { get; private set; }
        public int Height { get; private set; }
        public int CurrentWidth { get; private set; }
        public int CurrentHeight { get; private set; }

        public ProgressEventArgs(int width, int height, int currentWidth, int currentHeight)
        {
            Width = width;
            Height = height;
            CurrentHeight = currentHeight;
            CurrentWidth = currentWidth;
        }
    }
}
