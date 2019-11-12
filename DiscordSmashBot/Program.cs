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
        public static Task Main(string[] args)
        {
            return BotStartup.RunAsync(args);
        }
    }
}
