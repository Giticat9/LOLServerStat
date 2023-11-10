namespace LOLServerStatistics.Server.Application.Riot.Models
{
    public class SummonerInfoModel
    {
        public string LeagueId { get; set; } = string.Empty;
        public string QueueType { get;set; } = string.Empty;
        public string Tier { get; set; } = string.Empty;
        public string Rank { get; set; } = string.Empty;
        public string SummonerId { get; set; } = string.Empty;
        public string SummonerName { get; set; } = string.Empty;
        public long LeaguePoints { get; set; }
        public long Wins { get; set; }
        public long Losses { get; set; }
        public bool Veteran { get; set; }
        public bool Inactive { get; set; }
        public bool FreshBlood { get; set; }
        public bool HotStreak { get; set; }
    }
}
