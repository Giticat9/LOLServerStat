using LeagueOfLegendsServerStatistics.Application.Discord.Commands;
using LeagueOfLegendsServerStatistics.Application.Discord.Commands.Help;
using LeagueOfLegendsServerStatistics.Application.Discord.Commands.Registry;
using Microsoft.Extensions.DependencyInjection;

namespace LeagueOfLegendsServerStatistics.Application.Setup
{
    public static class RegistryDiscordCommandsExtension
    {
        public static IServiceCollection AddDiscordCommands(this IServiceCollection services)
        {
            services.AddTransient<IDiscordCommand, RegistryCommand>();
            services.AddTransient<IDiscordCommand, HelpCommand>();

            return services;
        }
    }
}
