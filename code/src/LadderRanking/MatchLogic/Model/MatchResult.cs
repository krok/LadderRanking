using System;

namespace LadderRanking.MatchLogic.Model
{
    public class MatchResult
    {
        private static int _idCounter = 1;

        public MatchResult(Player winner, Player looser)
            : this(winner, looser, DateTime.UtcNow)
        {
        }

        public MatchResult(Player winner, Player looser, DateTime registered)
        {
            Winner = winner;
            Looser = looser;
            Registered = registered;
            Id = _idCounter++;
            winner.AddMatch(this);
            looser.AddMatch(this);
        }

        public int Id { get; }

        public DateTime Registered { get; }
        public Player Winner { get; set; }
        public Player Looser { get; set; }
    }
}