using LeagueOfLegendsServerStatistics.Application.Riot.Api;
using Microsoft.Extensions.DependencyInjection;

namespace LeagueOfLegendsServerStatistics.Application.Setup
{
    public static class RegistryRiotApiServices
    {
        public static IServiceCollection AddRiotApiServices(this IServiceCollection services)
        {
            services.AddSingleton<ISummonerV4, SummonerV4>();

            return services;
        }
    }
}
