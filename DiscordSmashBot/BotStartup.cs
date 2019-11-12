using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace DiscordSmashBot
{
    public class BotStartup
    {
        public IConfigurationRoot Configuration { get; }

        public BotStartup(string[] args)
        {
            var cfgBuilder = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "Configuration"))
                .AddJsonFile("appSettings.json", optional: false, reloadOnChange: true);

            Configuration = cfgBuilder.Build();
        }

        public static async Task RunAsync(string[] args)
        {
            var botStart = new BotStartup(args);
            await botStart.RunAsync();
        }

        public async Task RunAsync()
        {
            var services = new ServiceCollection();
            ConfigureBotServices(services);

            var provider = services.BuildServiceProvider();
        }

        private void ConfigureBotServices(IServiceCollection services)
        {
            services.AddSingleton(new DiscordSocketClient(new DiscordSocketConfig
            {
                LogLevel = LogSeverity.Verbose,
                MessageCacheSize = 1000
            }))
            .AddSingleton(new CommandService(new CommandServiceConfig
            {
                LogLevel = LogSeverity.Verbose,
                DefaultRunMode = RunMode.Async
            }));
        }
    }
}
