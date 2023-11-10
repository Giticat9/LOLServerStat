using LOLServerStatistics.Server.Application.Riot.Models;

namespace LOLServerStatistics.Server.Application.Mappers
{
    public interface IChampionsMappers
    {
        ChampionModel MapReaderToChampionModel(dynamic reader);
    }
}
