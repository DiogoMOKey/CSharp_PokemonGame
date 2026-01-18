namespace PokemonGame
{
    class Pokemon
    {
        public int? pokedexId { get; set; }
        public int level { get; set; } = 1;
        public int experience { get; set; } = 0;
        public int beginStats { get; set; } = 40;
        public PokemonTypes? type { get; set; }
        public string? name { get; set; }

        public void PrintInfo() {
            Console.WriteLine($"{this.name} ({this.type?.name}) [Lvl: {this.level} / Exp: {this.experience} / Str: {this.Strength()}]");
        }

        public int Strength() {
            int baseStrength = this.beginStats;
            int strength = baseStrength + (this.level - 1) * 2;
            return strength;
        }

        public bool Challenge(Pokemon opponent){
            int playerStrength = this.Strength();
            int opponentStrength = opponent.Strength();
            if (this.type != null && opponent.type != null && this.type.pokemonTypeWeaknesses == opponent.type) {
                playerStrength = playerStrength - 7;
            } else if (this.type != null && opponent.type != null && opponent.type.pokemonTypeWeaknesses == this.type) {
                playerStrength = playerStrength + 7;
            }
            return playerStrength >= opponentStrength;
        }

        public void WonChallenge() {
            int randomExperience = GameProgram.rnd.Next(20, 41);

            Console.WriteLine($"{this.name} (Exp: {this.experience} ==> +{randomExperience} Exp!)");
            this.experience += randomExperience;
            if (this.experience >= 100){
                this.level += 1;
                this.experience = this.experience - 100;
                Console.WriteLine($"\n{this.name} leveled up! Now is Level {this.level}!");
            }
        }

        public static Pokemon randomizerPokemon() {
            if (GameProgram.pokedex == null || GameProgram.pokedex.Pokemons == null || GameProgram.pokedex.Pokemons.Count == 0) {
                throw new InvalidOperationException("Pokedex or Pokemons list is null or empty.");
            }
            int randomNumber = GameProgram.rnd.Next(1, GameProgram.pokedex.Pokemons.Count);
            Pokemon pokemon = GameProgram.pokedex.Pokemons[randomNumber];
            return pokemon;
        }

    }
}
