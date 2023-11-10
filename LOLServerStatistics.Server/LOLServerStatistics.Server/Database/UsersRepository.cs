using LOLServerStatistics.Server.Application.Helpers;
using LOLServerStatistics.Server.Application.Riot.Models;
using LOLServerStatistics.Server.Database.Base;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace LOLServerStatistics.Server.Database
{
    public class UsersRepository : BaseRepository, IUserRepository
    {
        public UsersRepository(IConfiguration configuration) : base(configuration) { }

        public async Task AddOrUpdateUser(ulong discordGuildId, 
            ulong discordUserId, 
            string riotSummonerId, 
            List<SummonerSQLTableModel> summonerModel)
        {
            try
            {
                using var connection = UsingConnection(ConnectionScopes.LOLServerStat);
                await connection.OpenAsync();

                var command = new SqlCommand
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "[dbo].[spUsersAddOrUpdate]",
                    Connection = connection,
                };

                command.Parameters.AddRange(new[]
                {
                    new SqlParameter("@discord_guild_id", discordGuildId.ToString()),
                    new SqlParameter("@discord_user_id", discordUserId.ToString()),
                    new SqlParameter("@riot_summoner_id", riotSummonerId),
                });

                if (summonerModel != null && summonerModel.Any()) 
                {
                    command.Parameters.Add(
                        new SqlParameter("@summoner_models", DBHelpers.ConvertListModelsToDataTable(summonerModel))
                    );
                }

                await command.ExecuteNonQueryAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Ошибка добавления/обновления пользователя", ex);
            }
        }

        public async Task DeleteUser(ulong discordGuildId, ulong discordUserId)
        {
            try
            {
                using var connection = UsingConnection(ConnectionScopes.LOLServerStat);
                await connection.OpenAsync();

                var command = new SqlCommand
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "[dbo].[spUsersDelete]",
                    Connection = connection,
                };

                command.Parameters.AddRange(new[]
                {
                    new SqlParameter("@discord_guild_id", discordGuildId.ToString()),
                    new SqlParameter("@discord_user_id", discordUserId.ToString())
                });

                await command.ExecuteNonQueryAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Ошибка удалеиня пользователя", ex);
            }
        }

        public async Task<bool> CheckExistsUser(ulong discordGuildId, ulong discordUserId)
        {
            try
            {
                using var connection = UsingConnection(ConnectionScopes.LOLServerStat);
                await connection.OpenAsync();

                var command = new SqlCommand
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "[dbo].[spUsersExists]",
                    Connection = connection,
                };

                command.Parameters.AddRange(new[]
                {
                    new SqlParameter("@discord_guild_id", discordGuildId.ToString()),
                    new SqlParameter("@discord_user_id", discordUserId.ToString())
                });

                var result = await command.ExecuteScalarAsync();

                return result != null && (bool)result;
            }
            catch (Exception ex)
            {
                throw new Exception($"Ошибка проверки существования пользователя", ex);
            }
        }
    }
}
