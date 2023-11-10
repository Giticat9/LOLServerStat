using LOLServerStatistics.Server.Application.Riot.Models;

namespace LOLServerStatistics.Server.Database
{
    public interface IUserRepository
    {
        Task AddOrUpdateUser(ulong discordGuildId, ulong discordUserId, string riotSummonerId, List<SummonerSQLTableModel> summonerModel);
        Task DeleteUser(ulong discordGuildId, ulong discordUserId);
        Task<bool> CheckExistsUser(ulong discordGuildId, ulong discordUserId);
    }
}
