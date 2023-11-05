using Discord;
using Discord.WebSocket;
using LeagueOfLegendsServerStatistics.Application.Discord.Bot;

namespace LeagueOfLegendsServerStatistics.Application.Discord.Commands.Help
{
    internal class HelpCommand : IHelpCommand
    {
        private readonly IDiscordBot _discordBot;
        private readonly string _commandName = "help";

        public HelpCommand(IDiscordBot discordBot)
        {
            _discordBot = discordBot;
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

                    foreach (var guild in guilds)
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
            await command.RespondAsync(text: "help responded!", ephemeral: true);
        }

        private SlashCommandProperties _slashCommandProperties
        {
            get => new SlashCommandBuilder()
                .WithName(_commandName)
                .WithDescription("Help using bot commands")
                .Build();
        }
    }
}
