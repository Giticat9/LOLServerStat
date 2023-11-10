using LeagueOfLegendsServerStatistics.Application.Discord.Bot;
using LeagueOfLegendsServerStatistics.Application.Discord.Commands;
using LeagueOfLegendsServerStatistics.Application.Discord.Logging;
using LeagueOfLegendsServerStatistics.Application.Setup;
using Microsoft.Extensions.DependencyInjection;

namespace LeagueOfLegendsServerStatistics
{
    internal class Programm
    {
        public static Task Main(string[] args)
            => new Programm().MainAsync(args);
        
        public async Task MainAsync(string[] args)
         {
            var services = BaseModuleRegistry
                .ConfigureServices();

            var serviceProvider = services
                .BuildServiceProvider();

            var discordBotService = serviceProvider
                .GetService<IDiscordBot>();

            try
            {
                if (discordBotService != null)
                {
                    var client = discordBotService.SocketClient;

                    var discordBotLogging = serviceProvider
                        .GetService<IDiscordLogging>();

                    discordBotLogging?.SetLogger(client);

                    IEnumerable<IDiscordCommand>? commandServices = null;
                    client.Connected += async () =>
                    {
                        services.AddDiscordCommands();

                        commandServices = serviceProvider.GetServices<IDiscordCommand>();

                        if (commandServices.Any())
                        {
                            foreach (var command in commandServices)
                            {
                                await command.Registry();
                            }
                        }

                        if (commandServices != null && commandServices.Any())
                        {
                            var discordCommandService = new DiscordCommandService(commandServices);

                            client.SlashCommandExecuted += discordCommandService.HandlerCommandExecuted;
                        }

                        await Task.CompletedTask;
                    };

                    await discordBotService.TryRunAsync();
                }
                else
                {
                    throw new Exception("Ошибка запуска бота: экземпляр недоступен");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            await Task.Delay(-1);
        }
    }
}