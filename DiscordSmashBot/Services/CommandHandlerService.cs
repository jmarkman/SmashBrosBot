using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DiscordSmashBot.Services
{
    /// <summary>
    /// This class is in charge of processing incoming commands and associating
    /// the user prefix:command combo with the associated command so said command
    /// can be executed
    /// </summary>
    public class CommandHandlerService
    {
        private readonly DiscordSocketClient _discordClient;
        private readonly CommandService _commands;
        private readonly IConfigurationRoot _configuration;
        private readonly IServiceProvider _provider;

        public CommandHandlerService(DiscordSocketClient discordClient, CommandService cmdService, IConfigurationRoot cfg, IServiceProvider provider)
        {
            _discordClient = discordClient;
            _commands = cmdService;
            _configuration = cfg;
            _provider = provider;

            _discordClient.MessageReceived += OnMessageReceivedAsync;
        }

        private async Task OnMessageReceivedAsync(SocketMessage arg)
        {
            throw new NotImplementedException();
        }
    }
}
