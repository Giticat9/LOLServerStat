using LeagueOfLegendsServerStatistics.Application.Discord.Bot;
using LeagueOfLegendsServerStatistics.Application.Discord.Logging;
using LOLServerStatistics.Server.Application.Mappers;
using LOLServerStatistics.Server.Database;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LeagueOfLegendsServerStatistics.Application.Setup
{
    public class BaseModuleRegistry
    {
        public static IServiceCollection ConfigureServices()
        {
            IServiceCollection services = new ServiceCollection();

            var config = LoadConfiguration();

            services.AddSingleton(config);

            services.AddSingleton<IDiscordBot, DiscordBot>();
            services.AddSingleton<IDiscordLogging, DiscordLogging>();

            //  registry all discord commands
            services.AddDiscordCommands();

            //  registry all riot api services
            services.AddRiotApiServices();

            services.AddTransient<IUserRepository, UsersRepository>();
            services.AddTransient<IChampionsRepository, ChampionsRepository>();

            services.AddTransient<ISummonerMappers, SummonerMappers>();
            services.AddTransient<IChampionsMappers, ChampionsMappers>();

            return services;
        }

        private static IConfiguration LoadConfiguration()
        {
            var rootPath = AppContext.BaseDirectory;

            var builder = new ConfigurationBuilder()
                .SetBasePath(rootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            return builder.Build();
        }
    }
}
