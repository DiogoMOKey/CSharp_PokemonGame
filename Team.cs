namespace PokemonGame
{
    class Team
    {
        public List<Pokemon> Members { get; set; } = new();

        public void PrintAll() {
            if (this.Members.Count == 0) {
                Console.WriteLine("No pokemons available.");
            } else {
                foreach (var p in this.Members) {
                    p.PrintInfo();
                }
            }
        }

        public static Team GenerateFullTeam(int teamSize = 2) {
            Team fullTeam = new Team();  // Player's team
            for (int i = 0; i < teamSize; i++) {
                Pokemon starter = Pokemon.randomizerPokemon();
                fullTeam.Members.Add(starter);
            }
            return fullTeam;
        }
    }
}
