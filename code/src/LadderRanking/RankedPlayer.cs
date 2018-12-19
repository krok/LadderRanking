namespace LadderRanking
{
    public class RankedPlayer
    {
        public RankedPlayer(string Id, int? rank)
        {
            this.Id = Id;
            Rank = rank;
        }

        public string Id { get;}
        public int? Rank { get; }
    }
}