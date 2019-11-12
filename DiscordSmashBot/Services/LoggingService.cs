using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace DiscordSmashBot.Services
{
    /// <summary>
    /// This class is a logging placeholder for more advanced logging (i.e., using NLog or Serilog)
    /// It just copies the LoggingService that already exists in the Discord.Net-Example project
    /// </summary>
    public class LoggingService
    {
        private readonly DiscordSocketClient _discordClient;
        private readonly CommandService _commands;

        public string LogDirectory { get; }
        public string LogFile => Path.Combine(LogDirectory, $"SmashBrosBot_Log_{DateTime.UtcNow.ToString("yyyy-MM-dd")}.txt");

        public LoggingService(DiscordSocketClient discordClient, CommandService commands)
        {
            LogDirectory = Path.Combine(AppContext.BaseDirectory, "Logs");

            _discordClient = discordClient;
            _commands = commands;

            _discordClient.Log += OnLogAsync;
            _commands.Log += OnLogAsync;
        }

        private Task OnLogAsync(LogMessage logMsg)
        {

        }
    }
}
