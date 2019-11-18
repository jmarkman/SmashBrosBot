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

        [Command("pokedex")]
        public async Task GetPokedexEntryFor(string pokemon)
        {
            var entry = await pokemonApi.GetPokedexEntryAsyncFor(pokemon);
            await ReplyAsync(entry.ToString());
        }
    }
}
