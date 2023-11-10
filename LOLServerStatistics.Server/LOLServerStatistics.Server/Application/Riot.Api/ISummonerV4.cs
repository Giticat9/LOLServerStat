using LeagueOfLegendsServerStatistics.Application.Riot.Models;
using LOLServerStatistics.Server.Application.Riot.Models;

namespace LeagueOfLegendsServerStatistics.Application.Riot.Api
{
    public interface ISummonerV4
    {
        Task<SummonerModel?> GetSummonerByName(string summonerName);
        Task<List<SummonerInfoModel?>> GetSummonerInfoById(string summonerId);
    }
}
