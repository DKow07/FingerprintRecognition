using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FingerprintRecognition.Matching
{
    public class Votes
    {
        private List<TranslationVotes> translation = new List<TranslationVotes>();
            
        public void Check(int x, int y, double theta)
        {
            TranslationVotes translationVotes = Search(x, y, theta);
            if(translationVotes != null)
            {
                translationVotes.Votes++;
                //translationVotes.Votes++;
                CheckNeighberhood(x, y, theta);
            }
            else
            {
                translation.Add(new TranslationVotes(x, y, theta));
            }
        }

        private void CheckNeighberhood(int x, int y, double theta)
        {
            for(int i = -1; i <= 1; i++)
            {
                for(int j = -1; j <= 1; j++)
                {
                    for(int k = -1; k <= 1; k++)
                    {
                        IncrementVotes(x + i, y + j, theta + k);
                    }
                }
            }
        }

        private void IncrementVotes(int x, int y, double theta)
        {
            TranslationVotes translationVotes = Search(x, y, theta);
            if (translationVotes != null)
            {
                translationVotes.Votes++;
            }
            else
            {
                translation.Add(new TranslationVotes(x, y, theta));
            }
        }

        public TranslationVotes GetTranslationVotesByMaxVotes()
        {
            TranslationVotes translationVotes = translation.OrderByDescending(tv => tv.Votes).First();
            return translationVotes;
        }

        private TranslationVotes Search(int x, int y, double theta)
        {
            foreach(TranslationVotes tv in translation)
            {
                if(tv.DeltaX == x && tv.DeltaY == y && tv.DeltaTheta == theta)
                {
                    return tv;
                }
            }
            return null;
        }

        public void Print()
        {
            translation.ForEach(t => 
            {
                if(t.Votes > 1) 
                    Debug.Print(t.ToString());
            }
            );
        }
    }

    public class TranslationVotes
    {
        public int DeltaX { get; set; }
        public int DeltaY { get; set; }
        public double DeltaTheta { get; set; }
        public int Votes { get; set; }

        public TranslationVotes(int x, int y, double theta)
        {
            this.DeltaX = x;
            this.DeltaY = y;
            this.DeltaTheta = theta;
            this.Votes = 1;
        }

        public override string ToString()
        {
            return "x = " + this.DeltaX + " y =  " + this.DeltaY + " theta = " + this.DeltaTheta;
        }
    }
}
