using System;

namespace PokemonGame
{
    class GameProgram
    {
        public static bool running = true;  // Game loop control
        public static Random rnd = new Random();  // Randomizer
        public static readonly Pokedex pokedex = new Pokedex(File.ReadAllText("pokedex.json"));  // Pokemons Availalble
        public static readonly Team teamPlayer = new Team();  // Player's team
        public static readonly GameControls gameControls = new GameControls();  // Game controls

        static void Main()
        {
            Console.Clear();
            if (pokedex == null || pokedex.Pokemons == null || pokedex.Pokemons.Count == 0) {
                Console.WriteLine("\nNo Pokemons available in the Pokedex!!! Exiting game.");
                running = false;
                return;
            }

            Console.WriteLine("\nWelcome to the Game!");
            Pokemon starter = Pokemon.randomizerPokemon();
            teamPlayer.Members.Add(starter);
            Console.WriteLine($"\nCongratulationsss, Professor gave you {starter?.name} as a starter Pokemon!");


            while (running) { gameControls.questionGame(); }
            Console.WriteLine("\n");
        }
    }
}
