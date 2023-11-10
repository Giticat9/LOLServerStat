namespace LOLServerStatistics.Server.Application.Riot.Models
{
    public class ChampionModel
    {
        public long Code { get; set; }
        public string Key { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string OriginalName { get; set; } = string.Empty;
        public DateTime? UpdatedDate { get; set; } = null;
    }
}
