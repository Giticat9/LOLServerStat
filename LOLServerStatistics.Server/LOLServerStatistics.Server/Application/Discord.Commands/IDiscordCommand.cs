using Discord.WebSocket;

namespace LeagueOfLegendsServerStatistics.Application.Discord.Commands
{
    public interface IDiscordCommand
    {
        string CommandName { get; }
        Task<bool> Registry();
        Task CommandHandler(SocketSlashCommand command);
    }
}
