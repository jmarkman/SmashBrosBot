using Discord;
using Discord.Commands;
using Discord.WebSocket;
using DiscordSmashBot.ApiService;
using DiscordSmashBot.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
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

        /// <summary>
        /// Run the bot
        /// </summary>
        /// <param name="args">Arguments from the command line</param>
        /// <returns></returns>
        public static async Task RunAsync(string[] args)
        {
            var botStart = new BotStartup(args);
            await botStart.RunAsync();
        }

        /// <summary>
        /// Gather the various services associated with the bot and use the startup service to run the bot
        /// </summary>
        /// <returns></returns>
        public async Task RunAsync()
        {
            var services = new ServiceCollection();
            ConfigureBotServices(services);

            // Construct a provider from the services collection so that the various services can be used
            var provider = services.BuildServiceProvider();
            // Start the logging service and command handling service; other services that aren't calling
            // methods immediately after construction will follow these GetRequiredService calls
            provider.GetRequiredService<LoggingService>();
            provider.GetRequiredService<PokemonApiService>();
            provider.GetRequiredService<CommandHandlerService>();

            // Start the bot: get API token, log in to bot account, begin websocket communication
            await provider.GetRequiredService<BotStartupService>().StartBotAsync();

            // Block RunAsync from "completing" until the program is closed,
            // i.e., the bot's existence is a task and once the task is done,
            // the bot is subsequently deactivated
            await Task.Delay(-1);
        }

        /// <summary>
        /// Collects and performs rudimentary initialization for the various services
        /// associated with the bot
        /// </summary>
        /// <param name="services"></param>
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
            }))
            .AddSingleton<CommandHandlerService>()
            .AddSingleton<BotStartupService>()
            .AddSingleton<LoggingService>()
            .AddSingleton<PokemonApiService>()
            .AddSingleton(Configuration);
        }
    }
}
