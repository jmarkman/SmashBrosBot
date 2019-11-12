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

        /// <summary>
        /// Writes a log event to both a text file and the console
        /// </summary>
        /// <param name="logMsg"></param>
        /// <returns></returns>
        private Task OnLogAsync(LogMessage logMsg)
        {
            CreateLogDirectoryAndFileIfNotExist();

            // This basic logging will keep track of any action that's logged like any other logging system: if there's an exception, it'll be printed,
            // but if the log message Exception property is null (i.e., nothing's wrong, just logging that [x] was used), it'll log the message
            string logText = $"{DateTime.UtcNow.ToString("hh:mm:ss")} [{logMsg.Severity}] {logMsg.Source}: {logMsg.Exception?.ToString() ?? logMsg.Message}";
            File.AppendAllText(LogFile, logText + Environment.NewLine);

            return Console.Out.WriteLineAsync(logText);
        }

        /// <summary>
        /// If the log directory and/or the log file do not exist, create them
        /// </summary>
        private void CreateLogDirectoryAndFileIfNotExist()
        {
            if (!Directory.Exists(LogDirectory))
            {
                Directory.CreateDirectory(LogDirectory);
                Console.WriteLine($"Created log directory at '{LogDirectory}'");
            }

            if (!File.Exists(LogFile))
            {
                // C# 8.0 syntactic sugar: if a simple operation requires disposing, 
                // the using statement can now be written on one line
                using FileStream fs = File.Create(LogFile);
                Console.WriteLine($"Created log file '{LogFile}' at '{LogDirectory}'");
            }
        }
    }
}
