using LeagueOfLegendsServerStatistics.Application.Riot.Models;
using LOLServerStatistics.Server.Application.Riot.Models;

namespace LOLServerStatistics.Server.Application.Mappers
{
    public interface ISummonerMappers
    {
        public SummonerSQLTableModel MapSummonerToSummonerSQLTableModel(SummonerModel summoner, SummonerInfoModel summonerInfo);
    }
}
