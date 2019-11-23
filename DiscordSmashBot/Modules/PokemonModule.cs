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
                Title = $"Pokedex Entry for {pokemon}",
                Color = Color.Red,
                Description = entry.ToString()

            }.Build();

            await ReplyAsync(embed: pokedexEntry);
        }
    }
}
