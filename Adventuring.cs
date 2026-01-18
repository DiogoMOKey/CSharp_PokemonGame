
namespace PokemonGame
{
    interface IBattleAction {
        void Execute(Adventuring context);
    }

    class AttackAction : IBattleAction {
        public void Execute(Adventuring context) {
            var player = context.CurrentPlayerPokemon;
            var opponent = context.CurrentOpponentPokemon;
            Console.WriteLine($"\n{player.name} (Lvl: {player.level} / Str: {player.Strength()}) attacks {opponent.name} (Lvl: {opponent.level} / Str: {opponent.Strength()})");

            if (player.Challenge(opponent)) {
                Console.WriteLine($"{opponent.name} takes heavy damage!");
                Console.WriteLine($"\nYou Won!! {player.name} defeated {opponent.name}!");
                player.WonChallenge(); // Grant experience to the player's Pokemon

                if (context.CurrentOpponentTeam.Members.Count > 1) {
                    context.CurrentOpponentTeam.Members.RemoveAt(0);
                    context.CurrentOpponentPokemon = context.CurrentOpponentTeam.Members[0];
                    Console.WriteLine($"\nOpponent sends out {context.CurrentOpponentPokemon.name}!");
                    context.battleGame();
                }
            }
            else if (!player.Challenge(opponent)) {
                Console.WriteLine($"{player.name} barely scratches {opponent.name}...");
                Console.WriteLine($"{opponent.name} counter-attacks and {player.name} takes heavy damage!");
                Console.WriteLine($"\nYou Lost!! {opponent.name} defeated {player.name}!");

                if (context.CurrentPlayerTeam.Members.Count > 1) {
                    context.CurrentPlayerTeam.Members.RemoveAt(0);
                    context.CurrentPlayerPokemon = context.CurrentPlayerTeam.Members[0];
                    Console.WriteLine($"\nWith determination, you throw a Pokéball! 'It's your turn, {context.CurrentPlayerPokemon.name}! I believe in you!'\n");
                    context.battleGame();
                }

                context.defeatCounter += 1;
                if (context.defeatCounter >= 3) {
                    Console.WriteLine($"\n{context.CurrentOpponentPokemon.name} suddenly leaps past your Pokémon and charges at you!");
                    Console.WriteLine("\nYou feel a sharp pain and collapse to the ground...");
                    Console.WriteLine("Your vision fades as you hear distant voices calling for help.");
                    Console.WriteLine("But potions only work on Pokémon... not on trainers.");
                    Console.WriteLine("\nYou black out! The world goes dark...");
                    Console.WriteLine("\nThank you for playing this little demo. Rest up, and try again soon!");
                    GameProgram.running = false;
                    return;
                }
            }
        }
    }

    class ChangePokemonAction : IBattleAction {
        public void Execute(Adventuring context) {
            var team = context.CurrentPlayerTeam;
            var player = context.CurrentPlayerPokemon;
            Console.WriteLine("\nAvailable Pokemons in your team:");
            team.PrintAll();

            Console.WriteLine($"\nChoose a number from 1 to {context.CurrentPlayerTeam.Members.Count} for the Pokemon you want to switch to:");
            string input = Console.ReadLine() ?? string.Empty;
            if (int.TryParse(input, out int opt) && opt >= 1 && opt <= context.CurrentPlayerTeam.Members.Count) {
                context.CurrentPlayerPokemon = context.CurrentPlayerTeam.Members[opt - 1];
            }

            Console.WriteLine($"\nYou call back {player.name} to your side. 'You did amazing, {player.name}! Take a good rest!'");
            Console.WriteLine($"With determination, you throw a Pokéball! 'It's your turn, {context.CurrentPlayerPokemon.name}! I believe in you!'\n");
            context.CurrentPlayerPokemon.PrintInfo();
        }
    }

    class CatchPokemonAction : IBattleAction {
        public void Execute(Adventuring context) {
            Console.WriteLine($"\nYou saw {context.CurrentOpponentPokemon.name} and fell in love!");
            Console.WriteLine($"You understood that you had to catch {context.CurrentOpponentPokemon.name} no matter what!");
            Console.WriteLine("You throw a Pokéball with all your might!");
            Console.WriteLine("\n♪... wiggle... wiggle... wiggle... ♪");
            Console.WriteLine("\nWith amazement pokeball shines one time!");

            int fakeBool = GameProgram.rnd.Next(1, 3);
            if (fakeBool == 2) {
                Console.WriteLine("\nBut suddenly the Pokéball shakes violently!");
                Console.WriteLine("Oh no! The Pokémon broke free from the Pokéball!");

                context.catchCounter += 1;
                if (context.catchCounter >= 3) {
                    Console.WriteLine($"\n{context.CurrentOpponentPokemon.name} suddenly leaps past your Pokémon and charges at you!");
                    Console.WriteLine("\nYou feel a sharp pain and collapse to the ground...");
                    Console.WriteLine("Your vision fades as you hear distant voices calling for help.");
                    Console.WriteLine("But potions only work on Pokémon... not on trainers.");
                    Console.WriteLine("\nYou black out! The world goes dark...");
                    Console.WriteLine("\nThank you for playing this little demo. Rest up, and try again soon!");
                    GameProgram.running = false;
                    return;
                }
                context.battleGame();
                return;
            } 
            
            Console.WriteLine("\n♪... wiggle... wiggle... wiggle... ♪");
            Console.WriteLine("\nWith amazement pokeball shines two times!");
            fakeBool = GameProgram.rnd.Next(1, 3);
            if (fakeBool == 2) {
                Console.WriteLine("\nBut suddenly the Pokéball shakes violently!");
                Console.WriteLine("Oh no! The Pokémon broke free from the Pokéball!");
                
                context.catchCounter += 1;
                if (context.catchCounter >= 3) {
                    Console.WriteLine($"\n{context.CurrentOpponentPokemon.name} suddenly leaps past your Pokémon and charges at you!");
                    Console.WriteLine("\nYou feel a sharp pain and collapse to the ground...");
                    Console.WriteLine("Your vision fades as you hear distant voices calling for help.");
                    Console.WriteLine("But potions only work on Pokémon... not on trainers.");
                    Console.WriteLine("\nYou black out! The world goes dark...");
                    Console.WriteLine("\nThank you for playing this little demo. Rest up, and try again soon!");
                    GameProgram.running = false;
                    return;
                }
                context.battleGame();
                return;
            }

            Console.WriteLine("\n♪... wiggle... wiggle... wiggle... ♪");
            Console.WriteLine("The Pokéball shines brightly three times!");
            Console.WriteLine("\n♪... click! ...");
            Console.WriteLine("♪ TA-DA-DA-DAAA! ♪");
            Console.WriteLine($"\nCongratulations! You caught {context.CurrentOpponentPokemon.name}!");
            GameProgram.teamPlayer.Members.Add(context.CurrentOpponentPokemon);
        }
    }

    class RunAction : IBattleAction {
        public void Execute(Adventuring context) {
            Console.WriteLine("\nYou attempt to run away...");
            int fakeBool = GameProgram.rnd.Next(1, 3);
            if (fakeBool == 2) {
                context.runCounter += 1;
                Console.WriteLine("\nBut the opponent blocks your path!");
                Console.WriteLine("You couldn't escape the battle!");
                
                if (context.runCounter >= 3) {
                    Console.WriteLine($"\n{context.CurrentOpponentPokemon.name} suddenly leaps past your Pokémon and charges at you!");
                    Console.WriteLine("\nYou feel a sharp pain and collapse to the ground...");
                    Console.WriteLine("Your vision fades as you hear distant voices calling for help.");
                    Console.WriteLine("But potions only work on Pokémon... not on trainers.");
                    Console.WriteLine("\nYou black out! The world goes dark...");
                    Console.WriteLine("\nThank you for playing this little demo. Rest up, and try again soon!");
                    GameProgram.running = false;
                    return;
                }

                context.battleGame();
                return;
            } 
            else
            {
                Console.WriteLine("\nYou ran away safely!");
            }
        }
    }


    class Adventuring {
        private string option = "0";
        private readonly IBattleAction[] actions;

        public Team CurrentPlayerTeam { get; set; }
        public Pokemon CurrentPlayerPokemon { get; set; }
        public Team CurrentOpponentTeam { get; set; }
        public Pokemon CurrentOpponentPokemon { get; set; }

        public int catchCounter = 1;
        public int runCounter = 1;
        public int defeatCounter = 1;

        public Adventuring() {
            CurrentPlayerTeam = GameProgram.teamPlayer;
            CurrentPlayerPokemon = this.CurrentPlayerTeam.Members[0];
            CurrentOpponentTeam = Team.GenerateFullTeam();
            CurrentOpponentPokemon = this.CurrentOpponentTeam.Members[0];

            actions = new IBattleAction[] {
                new AttackAction(),
                new ChangePokemonAction(),
                new CatchPokemonAction(),
                new RunAction()
            };
        }

        public void battleGame() {
            Console.WriteLine("\n" + "You have 4 available choices:");
            Console.WriteLine("1 - Attack");
            Console.WriteLine("2 - Change Pokemon");
            Console.WriteLine("3 - Catch Pokemon");
            Console.WriteLine("4 - Run\n");

            option = Console.ReadLine() ?? string.Empty;
            if (int.TryParse(option, out int opt) && opt >= 1 && opt <= 4) {
                Console.Clear();
                actions[opt - 1].Execute(this);
            } else {
                Console.WriteLine("Invalid option.");
            }
        }

        private void RandomEncounter() {
            Console.WriteLine("\nYou wander deeper into the tall grass, searching for pokemons... or maybe just a way out.");
            Console.WriteLine("\nSuddenly, you hear a strange rustling from the bushes nearby.");
            Console.WriteLine("Leaves tremble, branches snap, and the bushes start shaking violently!!");
            Console.WriteLine("Your heart races as the noise grows louder...");
            Console.WriteLine("\nThe bushes rustle loudly... STS STS SSS ST! Something is about to jump out!");

            CurrentOpponentPokemon = Pokemon.randomizerPokemon();
            Console.WriteLine($"\nA wild {CurrentOpponentPokemon.name} (Lvl: {CurrentOpponentPokemon.level}) appeared!");

            Console.WriteLine("\n" + "Battle music intensifies...");
            Console.WriteLine("\n" + "♪ TUTUTUTUTUUUU TUUUUU TUTUUUUUUUUUU TUUUUUUUUUUUUUUUUUUUU");
            Console.WriteLine("\n" + "♪ TU TU TU TU TU TU TUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUU");
            Console.WriteLine($"\nYou throw a Pokéball !!  'Go, {CurrentPlayerPokemon.name} (Lvl: {CurrentPlayerPokemon.level})!'");
            Console.WriteLine("\nThe battle begins!");
            battleGame();
        }

        private void ChallengePlayer() {
            Console.Clear();
            Console.WriteLine("\n" + "Challenging another player...\n");
            Console.WriteLine("A wild battle is about to begin!");
            Console.WriteLine("Opponent trainer locks eyes with you!");
            Console.WriteLine("\n" + "Battle music intensifies...");
            Console.WriteLine("\n" + "♪ TUTUTUTUTUUUU TUUUUU TUTUUUUUUUUUU TUUUUUUUUUUUUUUUUUUUU");
            Console.WriteLine("\n" + "♪ TU TU TU TU TU TU TUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUU");

            Console.WriteLine("\n" + "Opponent Team");
            CurrentOpponentTeam.PrintAll();

            Console.WriteLine($"\nYou throw a Pokéball !!  'Go, {CurrentPlayerPokemon.name} (Lvl: {CurrentPlayerPokemon.level})!'");
            Console.WriteLine($"Opponent throws a Pokéball !!  'Go, {CurrentOpponentPokemon.name} (Lvl: {CurrentOpponentPokemon.level})!'");
            Console.WriteLine("\nThe battle begins!");
            battleGame();
        }

        public void ContinueStory() {
            switch (GameProgram.rnd.Next(3)) {
                case 1: ChallengePlayer(); break;
                case 2: RandomEncounter(); break;
                default: Console.WriteLine("\n" + "You continue your journey peacefully..."); break;
            }
        }
    }
}
