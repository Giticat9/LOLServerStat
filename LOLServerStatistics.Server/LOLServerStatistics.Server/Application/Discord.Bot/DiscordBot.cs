using Discord;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;

namespace LeagueOfLegendsServerStatistics.Application.Discord.Bot
{
    public class DiscordBot : IDiscordBot
    {
        private readonly IConfiguration _configuration;
        private readonly DiscordSocketClient _discordSocketClient;

        public DiscordBot(IConfiguration configuration)
        {
            _configuration = configuration;
            _discordSocketClient = new DiscordSocketClient(new DiscordSocketConfig
            {
                GatewayIntents = GatewayIntents.AllUnprivileged | GatewayIntents.GuildMembers | GatewayIntents.GuildPresences
            });
        }

        public DiscordSocketClient SocketClient
        {
            get => _discordSocketClient;
        }

        public async Task<bool> TryRunAsync()
        {
            var token = _configuration.GetValue<string>("Discord:Bot:AuthToken");

            try
            {
                await _discordSocketClient.LoginAsync(TokenType.Bot, token);
                await _discordSocketClient.StartAsync();

                return true;
            }
            catch (PlatformNotSupportedException exception)
            {
                throw new Exception("WebSocken.NET: Платформа для запуска бота не поддерживается", exception);
            }
            catch (Exception exception)
            {
                throw new Exception($"Ошибка запуска бота:\n{exception.Message}", exception);
            }
        }
    }
}
