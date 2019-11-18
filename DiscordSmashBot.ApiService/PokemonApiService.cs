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
        private Random rnd;

        public PokemonApiService()
        {
            rnd = new Random();
        }

        /// <summary>
        /// Retrieves a Pokedex entry for the specified Pokemon, which contains the pokemon's name,
        /// the blurb associated with their pokedex entry, and what game that blurb came from
        /// </summary>
        /// <param name="pokemonName">The name of the pokemon, which should be formatted as lowercase</param>
        /// <returns></returns>
        public async Task<PokemonFlavorTextEntry> GetPokedexEntryAsyncFor(string pokemonName)
        {
            // Clean any extra space from around the name and make sure it's in lower-case
            // or else the API throws a fit in the form of a 404 Not Found
            var formattedPokemonName = pokemonName.Trim().ToLower();

            using (PokeApiClient pkmnClient = new PokeApiClient())
            {
                var pkmn = await pkmnClient.GetResourceAsync<Pokemon>(pokemonName);
                var pkmnSpecies = await pkmnClient.GetResourceAsync(pkmn.Species);
                var allFlavorTextInEnglish = pkmnSpecies.FlavorTextEntries.Where(fte => fte.Language.Name == "en").ToList();
                // Select a random flavor text entry to be used
                var flavorText = allFlavorTextInEnglish[rnd.Next(allFlavorTextInEnglish.Count)];

                // In order to get the properly formatted name from the API, we need to get the Version object from the resource
                var flavorTextVersion = await pkmnClient.GetResourceAsync(flavorText.Version);
                var formattedVersionName = flavorTextVersion.Names.Where(x => x.Language.Name == "en").SingleOrDefault().Name;
                               
                return new PokemonFlavorTextEntry
                {
                    // TODO: Get the name of the pokemon from a property from the Species object
                    PokemonName = pokemonName,
                    PokedexEntry = flavorText.FlavorText,
                    CameFrom = formattedVersionName
                };
            }
        }
    }
}
