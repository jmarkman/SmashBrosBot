using Discord;
using Discord.Commands;
using Discord.WebSocket;
using DiscordSmashBot.ExceptionTypes;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DiscordSmashBot.Services
{
    public class BotStartupService
    {
        private readonly IServiceProvider _provider;
        private readonly DiscordSocketClient _discordClient;
        private readonly CommandService _commands;
        private readonly IConfiguration _configuration;

        public BotStartupService(IServiceProvider provider, DiscordSocketClient discordClient, CommandService cmdService, IConfigurationRoot cfg)
        {
            _provider = provider;
            _discordClient = discordClient;
            _commands = cmdService;
            _configuration = cfg;
        }

        /// <summary>
        /// Starts the bot by logging into the bot user and starting communication with the bot
        /// </summary>
        /// <returns></returns>
        public async Task StartBotAsync()
        {
            string discordToken = _configuration["ApiToken"];

            // Check that the API token is present in the configuration file; if it isn't, don't allow execution to continue
            if (string.IsNullOrWhiteSpace(discordToken))
            {
                throw new BotConfigurationException("The API token in the bot configuration was not detected; ensure that the token is present and correctly placed");
            }

            // LoginAsync is how our bot will log into the user we created
            await _discordClient.LoginAsync(TokenType.Bot, discordToken);
            // StartAsync simply connects to the websocket that allows our program to communicate with the Discord API
            await _discordClient.StartAsync();

            // Once our bot is online, load all of the various commands and modules into the bot
            await _commands.AddModulesAsync(Assembly.GetEntryAssembly(), _provider);
        }
    }
}
