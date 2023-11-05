namespace LeagueOfLegendsServerStatistics.Application.Riot.Models
{
    public class SummonerModal
    {
        public string Id { get; set; } = "";
        public string AccountId { get; set; } = "";
        public string Puuid { get; set; } = "";
        public string Name { get; set; } = "";
        public int ProfileIconId { get; set; }
        public long SummonerLevel { get; set; }
        public long RevisionDate { get; set; }
    }
}
