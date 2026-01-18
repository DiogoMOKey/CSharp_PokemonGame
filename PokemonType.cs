
namespace PokemonGame
{
    class PokemonTypes{
        public string? name;
        public PokemonTypes? pokemonTypeWeaknesses { get; private set; }

        public PokemonTypes(string typeName)
        {
            this.name = typeName;
        }

        // Static predefined instances
        public static PokemonTypes Electric = new PokemonTypes("Electric");
        public static PokemonTypes Fire = new PokemonTypes("Fire");
        public static PokemonTypes Water = new PokemonTypes("Water");
        public static PokemonTypes Earth = new PokemonTypes("Earth");

        // Static constructor to assign weaknesses
        static PokemonTypes()
        {
            Electric.pokemonTypeWeaknesses = Earth;
            Fire.pokemonTypeWeaknesses = Water;
            Water.pokemonTypeWeaknesses = Electric;
            Earth.pokemonTypeWeaknesses = Fire;
        }
    }

}
