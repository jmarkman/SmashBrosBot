using Discord;
using Discord.WebSocket;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace DiscordSmashBot
{
    class Program
    {
        private DiscordSocketClient _client;
        private static IConfigurationRoot _config;

        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "Configuration"))
                .AddJsonFile("appSettings.json", optional: false, reloadOnChange: true);

            _config = builder.Build();

            new Program().MainAsync().GetAwaiter().GetResult();
        }

        public async Task MainAsync()
        {
            await Task.Delay(-1);
        }
    }
}
