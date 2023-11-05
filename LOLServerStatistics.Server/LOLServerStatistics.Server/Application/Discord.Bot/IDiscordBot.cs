using Discord.WebSocket;

namespace LeagueOfLegendsServerStatistics.Application.Discord.Bot
{
    public interface IDiscordBot
    {
        DiscordSocketClient SocketClient { get; }

        Task<bool> TryRunAsync();
    }
}
