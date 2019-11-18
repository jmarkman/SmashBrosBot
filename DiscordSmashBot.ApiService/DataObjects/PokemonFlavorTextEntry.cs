using System;
using System.Collections.Generic;
using System.Text;

namespace DiscordSmashBot.ApiService.DataObjects
{
    public class PokemonFlavorTextEntry
    {
        /// <summary>
        /// The name of the Pokemon
        /// </summary>
        public string PokemonName { get; set; }
        /// <summary>
        /// The associated Pokedex entry that Pokemon has
        /// </summary>
        public string PokedexEntry { get; set; }
        /// <summary>
        /// The game where said Pokedex entry came from
        /// </summary>
        public string CameFrom { get; set; }

        public override string ToString()
        {
            return $"Pokemon: {PokemonName}{Environment.NewLine}Pokedex Entry: {PokedexEntry}{Environment.NewLine}(This entry came from {CameFrom})";
        }
    }
}
