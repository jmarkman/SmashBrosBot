using Discord.Commands;
using System.Threading.Tasks;

namespace DiscordSmashBot.Modules
{
    [Name("Example")]
    public class ExampleModule : ModuleBase<SocketCommandContext>
    {
        [Command("lorem")]
        public Task LoremIpsum()
        {
            // TODO: Even if this is an example, this can probably be generated in a cleaner manner
            var lorem = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. In laoreet, turpis eget molestie posuere, tortor ipsum imperdiet ante, non suscipit lacus nisi eu diam. Sed viverra bibendum dui vel ultricies. Pellentesque gravida auctor aliquet. Pellentesque et pretium dolor. Duis malesuada vestibulum mauris, nec iaculis enim eleifend id. Integer tincidunt in velit in semper. Mauris nisi dolor, elementum non arcu vitae, rhoncus elementum enim. Vivamus at purus sit amet mi tincidunt cursus id nec mi. Duis odio arcu, feugiat semper sem vel, mollis sollicitudin dui. Etiam quis dolor tincidunt, bibendum ex vel, dictum dolor. Donec in dui elit.";
            return ReplyAsync(lorem);
        }
    }
}
