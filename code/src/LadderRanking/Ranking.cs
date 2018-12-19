using System.Collections.Generic;
using System.Linq;
using LadderRanking.MatchLogic;
using LadderRanking.MatchLogic.Model;

namespace LadderRanking
{
    public static class Ranking
    {
        public static IOrderedEnumerable<RankedPlayer> RankPlayers(IEnumerable<Match> matches)
        {
            return RankingAlgorithm.GetPlayersByRanking(
                matches.Select(m => new MatchResult(new Player(m.Winner), new Player(m.Looser), m.TimeStamp)))
                    .Select(p => new RankedPlayer(p.Id, p.Ranking)).OrderBy(p => p.Rank);
        }
    }
}