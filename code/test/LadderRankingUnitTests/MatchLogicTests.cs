using System;
using System.Collections.Generic;
using System.Linq;
using LadderRanking;
using LadderRanking.MatchLogic.Extensions;
using LadderRanking.MatchLogic.Model;
using Xunit;
using LadderRanking.MatchLogic;

namespace LadderRankingUnitTests
{

    public class MatchLogicTests
    {
        [Fact]
        public void GetPlayers()
        {
            var player1 = new Player("1");
            var player2 = new Player("2");
            var player3 = new Player("3");
            var matches =
                new[]
                {
                    new MatchResult(player1, player2),
                    new MatchResult(player1, player3),
                    new MatchResult(player1, player2),
                    new MatchResult(player3, player2)
                };

            var actual = matches.GetPlayers().ToArray();
            Assert.Equal(3, actual.Length);
            Assert.Contains(actual, p => p == player1);
            Assert.Contains(actual, p => p == player2);
            Assert.Contains(actual, p => p == player3);
        }

        [Fact]
        public void GetGroups()
        {
            var player1 = new Player("1");
            var player2 = new Player("2");
            var player3 = new Player("3");

            var player4 = new Player("4");
            var player5 = new Player("5");
            var player6 = new Player("6");
            var player7 = new Player("7");

            var player8 = new Player("8");
            var player9 = new Player("9");

            var matches =
                new[]
                {
                    new MatchResult(player1, player2),
                    new MatchResult(player1, player3),
                    new MatchResult(player1, player2),
                    new MatchResult(player3, player2),
                    new MatchResult(player4, player5),
                    new MatchResult(player5, player6),
                    new MatchResult(player6, player7),
                    new MatchResult(player8, player9)
                };

            Assert.Equal(3, matches.GetGroups().Count());
        }

        [Fact]
        public void GroupsShouldBeOrderedBySize()
        {
            var player1 = new Player("1");
            var player2 = new Player("2");
            var player3 = new Player("3");

            var player4 = new Player("4");
            var player5 = new Player("5");
            var player6 = new Player("6");
            var player7 = new Player("7");

            var player8 = new Player("8");
            var player9 = new Player("9");

            var matches =
                new[]
                {
                    new MatchResult(player1, player2),
                    new MatchResult(player1, player3),
                    new MatchResult(player1, player2),
                    new MatchResult(player3, player2),
                    new MatchResult(player4, player5),
                    new MatchResult(player5, player6),
                    new MatchResult(player6, player7),
                    new MatchResult(player8, player9)
                };

            var actual = matches.GetGroups().GetGroupsBySize().ToList();
            Assert.Equal(4, actual.First().Players.Count);
            Assert.Equal(3, actual.Skip(1).First().Players.Count);
            Assert.Equal(2, actual.Skip(2).First().Players.Count);

            IEnumerable<string> actual1 = actual.First().Players.Select(p => p.Id).ToArray();
            Assert.Equal(new[] { "4", "5", "6", "7" }, actual1);
            IEnumerable<string> actual2 = actual.Skip(1).First().Players.Select(p => p.Id).ToArray();
            Assert.Equal(new[] { "1", "2", "3" }, actual2);
            IEnumerable<string> actual3 = actual.Skip(2).First().Players.Select(p => p.Id).ToArray();
            Assert.Equal(new[] { "8", "9" }, actual3);
        }

        [Fact]
        public void WhenRankingTwoGamesBeatsOne()
        {
            var player1 = new Player("1");
            var player2 = new Player("2");

            var matches =
                new[]
                {
                    new MatchResult(player1, player2),
                    new MatchResult(player1, player2),
                    new MatchResult(player2, player1)
                };

            var actual = RankingAlgorithm.GetPlayersByRanking(matches).Select(p => p.Id).ToList();
            IEnumerable<string> actual1 = actual.ToArray();
            Assert.Equal(new[] { "1", "2" }, actual1);
        }

        [Fact]
        public void WhenRankingTwoGamesBeatsTwo()
        {
            var player1 = new Player("1");
            var player2 = new Player("2");

            var matches =
                new[]
                {
                    new MatchResult(player1, player2),
                    new MatchResult(player1, player2),
                    new MatchResult(player2, player1),
                    new MatchResult(player2, player1)
                };

            var actual = RankingAlgorithm.GetPlayersByRanking(matches).Select(p => p.Id).ToList();
            IEnumerable<string> actual1 = actual.ToArray();
            Assert.Equal(new[] { "2", "1" }, actual1);
        }

        [Fact]
        public void WhenRankingThreeGamesBeatTwo()
        {
            var player1 = new Player("1");
            var player2 = new Player("2");

            var matches =
                new[]
                {
                    new MatchResult(player1, player2),
                    new MatchResult(player1, player2),
                    new MatchResult(player1, player2),
                    new MatchResult(player2, player1),
                    new MatchResult(player2, player1)
                };

            var actual = RankingAlgorithm.GetPlayersByRanking(matches).Select(p => p.Id).ToList();
            IEnumerable<string> actual1 = actual.ToArray();
            Assert.Equal(new[] { "1", "2" }, actual1);
        }

        [Fact]
        public void WhenRankingThreeGamesBeatThree()
        {
            var player1 = new Player("1");
            var player2 = new Player("2");

            var matches =
                new[]
                {
                    new MatchResult(player1, player2),
                    new MatchResult(player1, player2),
                    new MatchResult(player1, player2),
                    new MatchResult(player2, player1),
                    new MatchResult(player2, player1),
                    new MatchResult(player2, player1)
                };

            var actual = RankingAlgorithm.GetPlayersByRanking(matches).Select(p => p.Id).ToList();
            IEnumerable<string> actual1 = actual.ToArray();
            Assert.Equal(new[] { "2", "1" }, actual1);
        }

        [Fact]
        public void PlayersShouldBeRankedByGroupSizeAndResults()
        {
            var player1 = new Player("1");
            var player2 = new Player("2");
            var player3 = new Player("3");

            var player4 = new Player("4");
            var player5 = new Player("5");
            var player6 = new Player("6");
            var player7 = new Player("7");

            var player8 = new Player("8");
            var player9 = new Player("9");

            var matches =
                new[]
                {
                    new MatchResult(player1, player2),
                    new MatchResult(player1, player3),
                    new MatchResult(player1, player2),
                    new MatchResult(player3, player2),
                    new MatchResult(player4, player5),
                    new MatchResult(player5, player6),
                    new MatchResult(player6, player7),
                    new MatchResult(player8, player9)
                };


            var actual = RankingAlgorithm.GetPlayersByRanking(matches).Select(p => p.Id).ToList();

            IEnumerable<string> actual1 = actual.ToArray();
            Assert.Equal(new[] { "4", "5", "6", "7", "1", "3", "2", "8", "9" }, actual1);
        }

        [Fact]
        public void GroupsShouldBeOrderedByLastMatchPlayed()
        {
            var player1 = new Player("1");
            var player2 = new Player("2");

            var player4 = new Player("4");
            var player5 = new Player("5");
            var player6 = new Player("6");
            var player7 = new Player("7");

            var player8 = new Player("8");
            var player9 = new Player("9");

            var matches =
                new[]
                {
                    new MatchResult(player1, player2),
                    new MatchResult(player1, player2),
                    new MatchResult(player4, player5),
                    new MatchResult(player5, player6),
                    new MatchResult(player6, player7),
                    new MatchResult(player8, player9, DateTime.Now.AddDays(1))
                };


            var actual = matches.GetGroupsBySize().ToList();
            Assert.Equal(4, actual.First().Players.Count);
            Assert.Equal(2, actual.Skip(1).First().Players.Count);
            Assert.Equal(2, actual.Skip(2).First().Players.Count);

            IEnumerable<string> actual1 = actual.First().Players.Select(p => p.Id).ToArray();
            Assert.Equal(new[] { "4", "5", "6", "7" }, actual1);
            IEnumerable<string> actual2 = actual.Skip(1).First().Players.Select(p => p.Id).ToArray();
            Assert.Equal(new[] { "8", "9" }, actual2);
            IEnumerable<string> actual3 = actual.Skip(2).First().Players.Select(p => p.Id).ToArray();
            Assert.Equal(new[] { "1", "2" }, actual3);
        }

        [Fact]
        public void PerfTest_Rank20000Matches()
        {
            var players = new List<Player>();
            for (var i = 0; i < 30; i++)
                players.Add(new Player(i.ToString()));

            var matches = new List<MatchResult>();

            for (var i = 0; i < 28; i++)
                matches.Add(new MatchResult(players[i], players[i + 1]));
            matches.Add(new MatchResult(players[0], players[29]));

            var random = new Random();
            for (var i = 0; i < 20000; i++)
                matches.Add(new MatchResult(players[random.Next(0, 15)], players[new Random().Next(16, 29)]));

            var actual = RankingAlgorithm.GetPlayersByRanking(matches).Select(p => p.Id).ToList();
            Assert.Equal(30, actual.Count);
        }

        [Fact]
        public void MatchesOnlyAffectRankingWhenLooserIsRankedBetterThanWinnerByMax2()
        {
            var player1 = new Player("1");
            var player2 = new Player("2");
            var player3 = new Player("3");
            var player4 = new Player("4");
            var player5 = new Player("5");

            //Ranking 1,2,3,4,5
            var matches =
                new[]
                {
                    new MatchResult(player1, player2),
                    new MatchResult(player2, player3),
                    new MatchResult(player3, player4),
                    new MatchResult(player4, player5),
                    new MatchResult(player5, player1) //This match has no effect
                };

            var actual = RankingAlgorithm.GetPlayersByRanking(matches).Select(p => p.Id).ToList();

            IEnumerable<string> actual1 = actual.ToArray();
            Assert.Equal(new[] { "1", "2", "3", "4", "5" }, actual1);
        }


        [Fact]
        public void LooserShouldBeMoveDownByOne()
        {
            var player1 = new Player("1");
            var player2 = new Player("2");
            var player3 = new Player("3");
            var player4 = new Player("4");
            var player5 = new Player("5");

            //Ranking 1,2,3,4,5
            var matches =
                new[]
                {
                    new MatchResult(player1, player2),
                    new MatchResult(player2, player3),
                    new MatchResult(player3, player4),
                    new MatchResult(player4, player5),
                    new MatchResult(player5, player1), //This match has no effect
                    new MatchResult(player3, player1)
                };

            var actual = RankingAlgorithm.GetPlayersByRanking(matches).Select(p => p.Id).ToList();

            IEnumerable<string> actual1 = actual.ToArray();
            Assert.Equal(new[] { "3", "1", "2", "4", "5" }, actual1);
        }

        [Fact]
        public void WhenPlayersFromDifferentGroupsPlay()
        {
            var player1 = new Player("1");
            var player2 = new Player("2");
            var player3 = new Player("3");

            var player4 = new Player("4");
            var player5 = new Player("5");
            var player6 = new Player("6");
            var player7 = new Player("7");

            var player8 = new Player("8");
            var player9 = new Player("9");

            var matches =
                new[]
                {
                    new MatchResult(player1, player2),
                    new MatchResult(player2, player3),
                    new MatchResult(player4, player5),
                    new MatchResult(player5, player6),
                    new MatchResult(player6, player7),
                    new MatchResult(player8, player9),
                    new MatchResult(player4, player2),
                    new MatchResult(player8, player6)
                };


            var actual = RankingAlgorithm.GetPlayersByRanking(matches).Select(p => p.Id).ToList();

            IEnumerable<string> actual1 = actual.ToArray();
            Assert.Equal(new[] { "1", "4", "2", "3", "5", "8", "6", "7", "9" }, actual1);
        }

        [Fact]
        public void PlayersShouldBeRankedByResults()
        {
            var player1 = new Player("1");
            var player2 = new Player("2");
            var player3 = new Player("3");


            var matches =
                new[]
                {
                    new MatchResult(player1, player2),
                    new MatchResult(player1, player3),
                    new MatchResult(player1, player2),
                    new MatchResult(player3, player2)
                };

            Assert.Equal(new[] { "1", "3", "2" }, (IEnumerable<string>) 
                RankingAlgorithm.GetPlayersByRanking(matches).Select(p => p.Id));
        }

        [Fact]
        public void PlayersShouldBeRankedByResultsWhenPlayersAreDuplicated()
        {
            var matches =
                new[]
                {
                    new MatchResult(new Player("1"), new Player("2")),
                    new MatchResult(new Player("1"), new Player("3")),
                    new MatchResult(new Player("1"), new Player("2")),
                    new MatchResult(new Player("3"), new Player("2"))
                };

            Assert.Equal(new[] { "1", "3", "2" }, (IEnumerable<string>)
                RankingAlgorithm.GetPlayersByRanking(matches).Select(p => p.Id));
        }

        [Fact]
        public void RankPlayers()
        {
            var player1 = new Player("1");
            var player2 = new Player("2");
            var player3 = new Player("3");

            var matches =
                new[]
                {
                    new Match(player1.Id, player2.Id),
                    new Match(player1.Id, player3.Id),
                    new Match(player1.Id, player2.Id),
                    new Match(player3.Id, player2.Id)
                };

            Assert.Equal(new[] { "1", "3", "2" },
                 Ranking.RankPlayers(matches).Select(p => p.Id));
        }

        [Fact]
        public void RankPlayers2()
        {
            var player1 = new Player("1");
            var player2 = new Player("2");
            var player3 = new Player("3");

            var matches =
                new[]
                {
                    new Match(player1.Id, player2.Id),
                    new Match(player1.Id, player3.Id),
                    new Match(player1.Id, player2.Id),
                    new Match(player3.Id, player2.Id)
                };

            var matches2 =
                new[]
                {
                    new MatchResult(player1, player2),
                    new MatchResult(player1, player3),
                    new MatchResult(player1, player2),
                    new MatchResult(player3, player2)
                };
            var playersByRanking2 = RankingAlgorithm.GetPlayersByRanking( matches2);

            Assert.Equal(new[] { "1", "3", "2" },
                playersByRanking2.Select(p => p.Id));


            var playersByRanking = RankingAlgorithm.GetPlayersByRanking(
                matches.Select(m => new MatchResult(new Player(m.Winner), new Player(m.Looser), m.TimeStamp)));

            Assert.Equal(new[] { "1", "3", "2" },
                playersByRanking.Select(p => p.Id));

            Assert.Equal(new[] { "1", "3", "2" },
                playersByRanking
                    .Select(p => new RankedPlayer(p.Id, p.Ranking)).OrderBy(p => p.Rank).Select(p => p.Id));
        }
    }
}