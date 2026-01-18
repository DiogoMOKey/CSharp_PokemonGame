
namespace PokemonGame
{
    class GameControls{
        private string option = "0";

        public void questionGame()
        {
            Console.WriteLine("\n" + "You have 4 available choices:");
            Console.WriteLine("1 - Continue adventuring");
            Console.WriteLine("2 - Check your Pokemons Team");
            Console.WriteLine("3 - Check your Pokedex");
            Console.WriteLine("4 - Exit Game \n");

            option = Console.ReadLine() ?? string.Empty;
            if (option != null && option.Length > 0)
            {
                switch (option)
                {
                    case "1": Console.Clear(); new Adventuring().ContinueStory(); break;
                    case "2": Console.Clear(); GameProgram.teamPlayer.PrintAll(); break;  // Player's team
                    case "3": Console.Clear(); GameProgram.pokedex.PrintAll(); break;  // Pokedex mode
                    case "4": Console.Clear(); ExitGame(); break;
                    default: Console.WriteLine("Invalid option."); break;
                }
            }
        }

        private void ExitGame()
        {
            Console.WriteLine("\n" + "Exiting the game...");
            GameProgram.running = false;
        }
    }
}
