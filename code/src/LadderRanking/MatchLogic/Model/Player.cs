using System.Collections.Generic;

namespace LadderRanking.MatchLogic.Model
{
    public class Player
    {
        private readonly IList<MatchResult> _matches;

        public Player(string id)
        {
            Id = id;
            _matches = new List<MatchResult>();
        }

        public string Id { get; }

        public int? Ranking { get; set; }

        public IEnumerable<MatchResult> GetMatches()
        {
            return _matches;
        }

        public void AddMatch(MatchResult match)
        {
            _matches.Add(match);
        }
    }
}