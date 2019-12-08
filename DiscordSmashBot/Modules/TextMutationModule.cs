using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DiscordSmashBot.Modules
{
    [Name("TextMutation")]
    public class TextMutationModule : ModuleBase<SocketCommandContext>
    {
        public TextMutationModule()
        {

        }

        [Command("regionalify")]
        public async Task ConvertToRegionalIndicator(string text)
        {
            
        }
    }
}
