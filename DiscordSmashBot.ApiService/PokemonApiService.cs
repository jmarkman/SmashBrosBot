using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DiscordSmashBot.ApiService.DataObjects;
using PokeApiNet;
using PokeApiNet.Models;

namespace DiscordSmashBot.ApiService
{
    public class PokemonApiService
    {
        /// <summary>
        /// This instance of the <see cref="Random"/> class will be used to select some 
        /// pseudorandom entry from the list of pokedex entries
        /// </summary>
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
                var allFlavorTextEnglish = await GetAllEnglishPokedexEntriesAsync(pkmnClient, formattedPokemonName);

                // Select a random flavor text entry to be used
                var flavorText = allFlavorTextEnglish[rnd.Next(allFlavorTextEnglish.Count)];

                var formattedVersionName = await GetFormattedGameVersionNameInEnglishAsync(pkmnClient, flavorText);
                               
                return new PokemonFlavorTextEntry
                {
                    // TODO: Get the name of the pokemon from a property from the Species object
                    PokemonName = pokemonName,
                    PokedexEntry = flavorText.FlavorText,
                    CameFrom = formattedVersionName
                };
            }
        }

        /// <summary>
        /// Retrieve a collection of every pokedex entry for the specified pokemon 
        /// //specifically in english\\ from the PokeAPI endpoint
        /// </summary>
        /// <param name="client">The PokeAPI client currently in use</param>
        /// <param name="pokemonName">This method will get all the english pokedex entries for this pokemon</param>
        /// <returns>A list of <see cref="PokemonSpeciesFlavorTexts"/> wrapped in a task</returns>
        private async Task<List<PokemonSpeciesFlavorTexts>> GetAllEnglishPokedexEntriesAsync(PokeApiClient client, string pokemonName)
        {
            // TODO: if the pokemonName property isn't a valid pokemon (misspelled or nonexistent), an exception gets swallowed here
            var pokemonApiObject = await client.GetResourceAsync<Pokemon>(pokemonName);
            var pokemonSpeciesApiObject = await client.GetResourceAsync(pokemonApiObject.Species);
            var allFlavorTextEnglish = pokemonSpeciesApiObject.FlavorTextEntries.Where(fte => fte.Language.Name == "en").ToList();

            return allFlavorTextEnglish;
        }

        /// <summary>
        /// Fetches the formatted name of the version from which the pokedex entry game 
        /// to display to the user, i.e., instead of "alpha-sapphire", returns "Alpha Sapphire"
        /// </summary>
        /// <param name="client">The PokeAPI client currently in use</param>
        /// <param name="flavorText">The pokedex entry selected in the body of <see cref="GetPokedexEntryAsyncFor(string)"/></param>
        /// <returns></returns>
        private async Task<string> GetFormattedGameVersionNameInEnglishAsync(PokeApiClient client, PokemonSpeciesFlavorTexts flavorText)
        {
            // In order to get the properly formatted name from the API, we need to get the Version object from the resource
            var flavorTextVersion = await client.GetResourceAsync(flavorText.Version);
            return flavorTextVersion.Names.Where(x => x.Language.Name == "en").SingleOrDefault().Name;
        }
    }
}
