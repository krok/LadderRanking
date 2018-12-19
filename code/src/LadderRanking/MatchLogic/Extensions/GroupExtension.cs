using System;
using System.Collections.Generic;
using System.Linq;
using LadderRanking.MatchLogic.Model;

namespace LadderRanking.MatchLogic.Extensions
{
    public static class GroupExtension
    {
        public static IOrderedEnumerable<Group> GetGroupsBySize(this IEnumerable<Group> groups)
        {
            return groups.OrderByDescending(g => g.Players.Count)
                .ThenBy(g => g.GetMatches().Select(m => m.Registered).Max(), Comparer<DateTime>.Default.Reverse());
        }

        internal static Group GetGroupByPlayer(this IEnumerable<Group> groups, Player player)
        {
            return groups.FirstOrDefault(g => g.Players.Any(p => p.Id == player.Id));
        }

        public static void JoinGroups(this ICollection<Group> groups, Group g1, Group g2)
        {
            foreach (var player in g2.Players)
                g1.Players.Add(player);
            groups.Remove(g2);
        }
    }
}