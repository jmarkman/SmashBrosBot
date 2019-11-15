using System;
using System.Linq;
using System.Threading.Tasks;
using DiscordSmashBot.ApiService.DataObjects;
using PokeApiNet;
using PokeApiNet.Models;

namespace DiscordSmashBot.ApiService
{
    public class PokemonApiService
    { 

        public async Task<PokemonFlavorTextEntry> GetPokedexEntryAsyncFor(string pokemonName)
        {
            using (PokeApiClient pkmnClient = new PokeApiClient())
            {
                var pkmn = await pkmnClient.GetResourceAsync<Pokemon>(pokemonName);
                var pkmnSpecies = await pkmnClient.GetResourceAsync(pkmn.Species);
                var flavorTextEnglish = pkmnSpecies.FlavorTextEntries.Where(fte => fte.Language.Name == "en");

                return new PokemonFlavorTextEntry
                {
                    PokemonName = pokemonName
                };
            }
        }
    }
}
