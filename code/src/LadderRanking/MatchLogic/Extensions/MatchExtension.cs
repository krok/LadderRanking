using System.Collections.Generic;
using System.Linq;
using LadderRanking.MatchLogic.Model;

namespace LadderRanking.MatchLogic.Extensions
{
    public static class MatchExtension
    {
        public static IEnumerable<Group> GetGroups(this IEnumerable<MatchResult> matches)
        {
            var groups = new List<Group>();
            foreach (var match in matches)
            {
                var g1 = groups.GetGroupByPlayer(match.Winner);
                var g2 = groups.GetGroupByPlayer(match.Looser);

                if (g1 == null && g2 == null)
                {
                    groups.Add(new Group {Players = {match.Winner, match.Looser}});
                    continue;
                }

                if (g1 != null && g2 == null)
                {
                    g1.Players.Add(match.Looser);
                    continue;
                }

                if (g1 == null)
                {
                    g2.Players.Add(match.Winner);
                    continue;
                }

                if (g1.Id != g2.Id)
                    groups.JoinGroups(g1, g2);
            }
            return groups;
        }

        public static IEnumerable<Group> GetGroupsBySize(this IEnumerable<MatchResult> matches)
        {
            return matches.GetGroups().GetGroupsBySize();
        }

        public static IEnumerable<Player> GetPlayers(this IEnumerable<MatchResult> matches)
        {
            var list = matches.ToList();
            return
                list.Select(m => m.Winner)
                    .Union(list.Select(m => m.Looser))
                    .Distinct()
                    .ToList();
        }
    }
}