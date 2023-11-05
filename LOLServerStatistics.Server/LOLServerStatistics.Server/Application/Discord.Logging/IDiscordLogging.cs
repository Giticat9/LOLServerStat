using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeagueOfLegendsServerStatistics.Application.Discord.Logging
{
    public interface IDiscordLogging
    {
        void SetLogger(DiscordSocketClient client, CommandService? command = null);
    }
}
