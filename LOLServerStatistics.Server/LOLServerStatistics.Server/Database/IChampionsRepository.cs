using LOLServerStatistics.Server.Application.Riot.Models;

namespace LOLServerStatistics.Server.Database
{
    public interface IChampionsRepository
    {
        Task<List<ChampionModel>> GetChampions(List<long>? ids = null);
    }
}
