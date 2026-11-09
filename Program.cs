using System;
using sharp_wave_survival.Models;
using sharp_wave_survival.Systems;

namespace sharp_wave_survival
{
    class Program
    {
        static void Main(string[] args)
        {
            //1. Display Title Screen
            DisplayTitle();

            // 2. Initialize Player
            Console.Write("Enter your hero's name: ");
            string playerName = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(playerName))
            {
                playerName = "Hero";
            }

            Player hero = new Player(playerName);
            Console.WriteLine($"\nWelcome, {hero.Name}! Prepare for survival.");
            Console.WriteLine("Press any key to enter the shop and begin...");
            Console.ReadKey();

            //3. Initialize Game Systems
            ShopManager shopManager = new ShopManager();
            WaveManager waveManager = new WaveManager();
            GameEngine gameEngine = new GameEngine();

            //4. Main Game Loop (10 waves max)
            int currentWave = 1;
            int maxWaves = 10;
            bool gameRunning = true;

            while (gameRunning && hero.IsAlive)
            {
                // Step A: Visit Shop before wave combat
                shopManager.OpenShop(hero, currentWave);

                // Step B: Get enemies for current wave & enter combat
                var enemies = waveManager.GetEnemiesForWave(currentWave);
                bool waveCleared = gameEngine.StartCombatPhase(hero, enemies);

                // Step C: Handle wave outcome
                if (!hero.IsAlive)
                {
                    Console.Clear();
                    Console.WriteLine("\n=================================");
                    Console.WriteLine("   GAME OVER - YOU WERE SLAIN !   ");
                    Console.WriteLine("=================================");
                    gameRunning = false;
                }
                else if (waveCleared)
                {
                    if (currentWave == maxWaves)
                    {
                        // Victory condition after Wave 10
                        gameEngine.DisplayVictoryScreen(hero);
                        gameRunning = false;
                    }
                    else
                    {
                        currentWave++;
                        Console.Clear();
                        Console.WriteLine($"\n*** WAVE CLEARED! Get ready for Wave {currentWave} ***");
                        Console.WriteLine("Press any key to enter the shop...");
                        Console.ReadKey();
                    }
                }
            }

            Console.WriteLine("\nThanks for playing Sharp Wave Survival!");
        }

        private static void DisplayTitle()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write(@"

            Welcome to: 
            ╔═══════════════════════════════════════════════╗
            ║                                               ║
            ║             -SHARP WAVE SURVIVAL-             ║
            ║                                               ║
            ╚═══════════════════════════════════════════════╝

            ");
            Console.ResetColor();
        }
    }
}