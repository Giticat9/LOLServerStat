using LeagueOfLegendsServerStatistics.Application.Riot.Models;
using LOLServerStatistics.Server.Application.Riot.Models;

namespace LOLServerStatistics.Server.Application.Mappers
{
    public class SummonerMappers : ISummonerMappers
    {
        public SummonerSQLTableModel MapSummonerToSummonerSQLTableModel(SummonerModel summoner, SummonerInfoModel summonerInfo)
            => new()
            {
                RiotSummonerId = summoner.Id,
                Name = summoner.Name,
                Level = summoner.SummonerLevel,
                QueueType = summonerInfo.QueueType,
                Tier = summonerInfo.Tier,
                Rank = summonerInfo.Rank,
                LeaguePoints = summonerInfo.LeaguePoints,
                Wins = summonerInfo.Wins,
                Losses = summonerInfo.Losses,
                Veteran = summonerInfo.Veteran,
                Inactive = summonerInfo.Inactive,
                FreshBlood = summonerInfo.FreshBlood,
                HotStreak = summonerInfo.HotStreak
            };
    }
}
