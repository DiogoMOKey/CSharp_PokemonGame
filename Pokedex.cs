using Newtonsoft.Json;

namespace PokemonGame
{
    class Pokedex
    {
        public class MigrationPokemon : Pokemon
        {
            public string? jsonType { get; set; }
        }
        public List<MigrationPokemon>? Pokemons { get; set; }

        public void PrintAll() {
            Console.WriteLine("This is your Pokedex!!");
            if (this.Pokemons != null) {
                foreach (Pokemon p in this.Pokemons) {
                    p.PrintInfo();
                }
            }
        }

        public Pokedex(string json){
            Pokemons = JsonConvert.DeserializeObject<List<MigrationPokemon>>(json);
            if (Pokemons != null) {
                foreach (MigrationPokemon p in this.Pokemons)
                {
                    if (p.jsonType == "Electric") p.type = PokemonTypes.Electric;
                    else if (p.jsonType == "Fire") p.type = PokemonTypes.Fire;
                    else if (p.jsonType == "Water") p.type = PokemonTypes.Water;
                    else if (p.jsonType == "Earth") p.type = PokemonTypes.Earth;
                };
            }
        }
    }
}
