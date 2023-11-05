using LeagueOfLegendsServerStatistics.Application.Riot.Models;

namespace LeagueOfLegendsServerStatistics.Application.Riot.Api
{
    public interface ISummonerV4
    {
        Task<SummonerModal?> GetSummonerByName(string summonerName);
    }
}
