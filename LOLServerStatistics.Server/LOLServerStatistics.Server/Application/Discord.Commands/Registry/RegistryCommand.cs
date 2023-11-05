using Discord;
using Discord.WebSocket;
using LeagueOfLegendsServerStatistics.Application.Discord.Bot;
using LeagueOfLegendsServerStatistics.Application.Riot.Api;
using Newtonsoft.Json;

namespace LeagueOfLegendsServerStatistics.Application.Discord.Commands.Registry
{
    public class RegistryCommand : IRegistryCommand
    {
        private readonly IDiscordBot _discordBot;
        private readonly ISummonerV4 _summonerV4;
        private readonly string _commandName = "registry";

        public RegistryCommand(IDiscordBot discordBot, ISummonerV4 summonerV4)
        {
            _discordBot = discordBot;
            _summonerV4 = summonerV4;
        }

        public string CommandName
        {
            get => _commandName;
        }

        public async Task<bool> Registry()
        {
            try
            {
                var client = _discordBot.SocketClient;
                var user = await client.Rest.GetCurrentUserAsync();

                if (user != null)
                {
                    var guilds = client.Guilds.ToList();

                    foreach ( var guild in guilds )
                    {
                        await client.Rest.CreateGuildCommand(_slashCommandProperties, guild.Id);
                    }

                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task CommandHandler(SocketSlashCommand command)
        {
            try
            {
                var summonerName = command?.Data?.Options
                    ?.ToList()
                    ?.FirstOrDefault(x => x.Name == "riotid")
                    ?.Value as string;

                if (string.IsNullOrEmpty(summonerName))
                {
                    await command.RespondAsync("Не удалось получить RiotId из запроса", ephemeral: true);
                    return;
                }

                var summoner = await _summonerV4.GetSummonerByName(summonerName);

                await command.RespondAsync(JsonConvert.SerializeObject(summoner, Formatting.Indented), ephemeral: true);
            }
            catch(Exception ex)
            {
                await command.RespondAsync($"Ошибка выполнения запроса\n{ex.Message}", ephemeral: true);
            }
        }

        private SlashCommandProperties _slashCommandProperties
        {
            get => new SlashCommandBuilder()
                .WithName(_commandName)
                .WithDescription("Add your profile to the list of participants")
                .AddOption("riotid", ApplicationCommandOptionType.String, "Your RiotID to add a profile", isRequired: true)
                .Build();
        }
    }
}
