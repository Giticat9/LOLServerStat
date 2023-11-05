using Discord.WebSocket;

namespace LeagueOfLegendsServerStatistics.Application.Discord.Commands
{
    public class DiscordCommandService
    {
        private readonly IEnumerable<IDiscordCommand> _commands;

        public DiscordCommandService(IEnumerable<IDiscordCommand> commands)
        {
            _commands = commands;
        }

        public async Task HandlerCommandExecuted(SocketSlashCommand command)
        {
            var commandHandler = _commands
                .Where(x => x.CommandName == command.CommandName)
                .FirstOrDefault();

            if (commandHandler != null)
            {
                await commandHandler.CommandHandler(command);
            }
        }
    }
}
