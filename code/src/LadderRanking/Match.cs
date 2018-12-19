using System;

namespace LadderRanking
{
    public class Match
    {
        public Match()
        {
        }


        public Match(string winner, string looser):
            this(winner, looser, DateTime.UtcNow)
        {
        }

        public Match(string winner, string looser, DateTime timeStamp)
        {
            Winner = winner;
            Looser = looser;
            TimeStamp = timeStamp;
        }

        public string Winner { get; set; }
        public string Looser { get; set; }

        public DateTime TimeStamp { get; set; }
    }
}