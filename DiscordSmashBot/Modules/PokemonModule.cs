using Discord;
using Discord.Commands;
using DiscordSmashBot.ApiService;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DiscordSmashBot.Modules
{
    [Name("Pokemon")]
    public class PokemonModule : ModuleBase<SocketCommandContext>
    {
        private readonly PokemonApiService pokemonApi;

        public PokemonModule(PokemonApiService pkmnApiService)
        {
            pokemonApi = pkmnApiService;
        }

        [Command("pokedex")]
        public async Task GetPokedexEntryFor(string pokemon)
        {
            var entry = await pokemonApi.GetPokedexEntryAsyncFor(pokemon);

            var pokedexEntry = new EmbedBuilder
            {
                ThumbnailUrl = entry.SpriteUrl,
                Title = $"Pokedex Entry for: {entry.PokemonName}",
                Color = Color.Red,
                Description = entry.FlavorText
            }
            .WithFooter(footer => footer.Text = $"Game Version: Pokemon {entry.CameFrom}")
            .WithCurrentTimestamp()
            .Build();

            await ReplyAsync(embed: pokedexEntry);
        }
    }
}
