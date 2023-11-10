using LOLServerStatistics.Server.Application.Helpers;
using LOLServerStatistics.Server.Application.Mappers;
using LOLServerStatistics.Server.Application.Riot.Models;
using LOLServerStatistics.Server.Database.Base;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace LOLServerStatistics.Server.Database
{
    public class ChampionsRepository : BaseRepository, IChampionsRepository
    {
        private readonly IChampionsMappers _championsMappers;

        public ChampionsRepository(IConfiguration configuration, IChampionsMappers championsMappers) : base(configuration) 
        { 
            _championsMappers = championsMappers;
        }

        public async Task<List<ChampionModel>> GetChampions(List<long>? ids = null)
        {
            try
            {
                using var connection = UsingConnection(ConnectionScopes.LOLServerStat);
                await connection.OpenAsync();

                var command = new SqlCommand
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "[dbo].[spChampionsGet]",
                    Connection = connection
                };

                if (ids != null )
                {
                    var dataTable = DBHelpers.ConvertModelToDataTable(ids);
                    command.Parameters.Add(
                        new SqlParameter("@champions_ids", dataTable)
                    );
                }

                using var reader = await command.ExecuteReaderAsync();

                if (reader.HasRows)
                {
                    var result = new List<ChampionModel>();

                    while (await reader.ReadAsync())
                    {
                        var champion = _championsMappers.MapReaderToChampionModel(reader);
                        result.Add(champion);
                    }

                    return result;
                }

                return new List<ChampionModel> { };
            }
            catch (Exception ex)
            {
                throw new Exception("Ошибка получения списка чемпионов", ex);
            }
        }
    }
}
