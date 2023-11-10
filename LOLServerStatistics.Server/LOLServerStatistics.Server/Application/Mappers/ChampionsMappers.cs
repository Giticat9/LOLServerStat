using LOLServerStatistics.Server.Application.Riot.Models;

namespace LOLServerStatistics.Server.Application.Mappers
{
    public class ChampionsMappers : IChampionsMappers
    {
        public ChampionModel MapReaderToChampionModel(dynamic reader)
        {
            return new ChampionModel
            {
                Code = reader.code,
                Key = reader.key,
                Name = reader.name,
                OriginalName = reader.original_name,
                UpdatedDate = reader.updated_date != null ? (DateTime)reader.updated_date : null,
            };
        }
    }
}
