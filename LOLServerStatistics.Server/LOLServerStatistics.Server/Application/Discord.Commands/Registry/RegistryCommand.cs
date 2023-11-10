using Discord;
using Discord.WebSocket;
using LeagueOfLegendsServerStatistics.Application.Discord.Bot;
using LeagueOfLegendsServerStatistics.Application.Riot.Api;
using LOLServerStatistics.Server.Application.Mappers;
using LOLServerStatistics.Server.Application.Riot.Models;
using LOLServerStatistics.Server.Database;
using Newtonsoft.Json;

namespace LeagueOfLegendsServerStatistics.Application.Discord.Commands.Registry
{
    public class RegistryCommand : IRegistryCommand
    {
        private readonly IDiscordBot _discordBot;
        private readonly ISummonerV4 _summonerV4;
        private readonly IUserRepository _userRepository;
        private readonly ISummonerMappers _summonerMappers;
        private readonly string _commandName = "registry";

        public RegistryCommand(IDiscordBot discordBot, 
            ISummonerV4 summonerV4, 
            IUserRepository userRepository,
            ISummonerMappers summonerMappers)
        {
            _discordBot = discordBot;
            _summonerV4 = summonerV4;
            _userRepository = userRepository;
            _summonerMappers = summonerMappers;
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
                var discordGuildId = command?.GuildId ?? 0;
                var discordUserId = command?.User.Id ?? 0;

                var isUserExists = await _userRepository.CheckExistsUser(discordGuildId, discordUserId);
                if (isUserExists)
                {
                    await command.RespondAsync($"Пользователь с никнеймом {summonerName} уже добавлен");
                    return;
                }

                var summonerInfo = await _summonerV4.GetSummonerInfoById(summoner?.Id ?? "");

                var summonerSQLTableModelList = new List<SummonerSQLTableModel>(summonerInfo.Count);

                foreach(var info in summonerInfo)
                {
                    var mapped = _summonerMappers.MapSummonerToSummonerSQLTableModel(summoner, info);
                    summonerSQLTableModelList.Add(mapped);
                }

                await _userRepository.AddOrUpdateUser(discordGuildId, discordUserId, summoner?.Id ?? "", summonerSQLTableModelList);
                await command.RespondAsync($"Пользователь с никнеймом {summonerName} добавлен!", ephemeral: true);
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
