using Discord.Commands;
using DiscordSmashBot.TextService;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DiscordSmashBot.Modules
{
    [Name("TextMutation")]
    public class TextMutationModule : ModuleBase<SocketCommandContext>
    {
        private readonly TextMutationService _textMutationService;

        public TextMutationModule(TextMutationService txtMutationService)
        {
            _textMutationService = txtMutationService;
        }

        [Command("regionalify")]
        public async Task ConvertToRegionalIndicator([Remainder]string text)
        {
            var regionalIndicatorResult = _textMutationService.ConvertInputToRegionalIndicator(text);

            await ReplyAsync(regionalIndicatorResult);
        }
    }
}
