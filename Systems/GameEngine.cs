using System;
using System.Collections.Generic;
using sharp_wave_survival.Models;

namespace sharp_wave_survival.Systems
{
    public class GameEngine
    {
        private Random _random = new Random();

        public bool StartCombatPhase(Player player, List<Enemy> enemies)
        {
            Console.Clear();
            Console.WriteLine($"=== COMBAT STARTED! ({enemies.Count} enemies appeared) ===");

            //Combat continues until player dies OR all enemies are defeated
            while (player.IsAlive && enemies.Count > 0)
            {
                // --------------------------------------------------
                // 1. PLAYER'S TURN
                // --------------------------------------------------
                bool actionCompleted = false;

                while (!actionCompleted)
                {
                    Console.WriteLine($"\n--- YOUR TURN | HP: {player.Health}/{player.MaxHealth} ---");
                    Console.WriteLine("Enemies in front of you:");
                    for (int i = 0; i < enemies.Count; i++)
                    {
                        Console.WriteLine($"  {i + 1}. {enemies[i].Name} (HP: {enemies[i].Health}/{enemies[i].MaxHealth})");
                    }

                    Console.WriteLine("\nActions:");
                    Console.WriteLine("1. Attack Enemy");
                    Console.WriteLine("2. Use Potion");
                    Console.Write("Choose your action (1-2): ");

                    string actionChoice = Console.ReadLine();

                    // Attack Logic
                    if (actionChoice == "1")
                    {
                        Console.Write($"Select target enemy (1-{enemies.Count}): ");
                        if (int.TryParse(Console.ReadLine(), out int targetIndex) && targetIndex > 0 && targetIndex <= enemies.Count) // Check valid target
                        {
                            Enemy target = enemies[targetIndex - 1];

                            int totalAttack = player.GetTotalAttack();
                            // Calculate actual net damage (Damage - Enemy Defense, minimum 1)
                            int netDamage = totalAttack - target.Defense;
                            if (netDamage < 1)
                            {
                                netDamage = 1;
                            }

                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.WriteLine($"\nYou strike {target.Name} and dealt {netDamage} damage to him!");
                            Console.ResetColor();

                            // Pass totalAttack to TakeDamage
                            target.TakeDamage(totalAttack);
                            if (!target.IsAlive) // Check if enemy is alove
                            {
                                Console.WriteLine($"--> You defeated {target.Name}! Earned {target.GoldReward}¢");
                                player.Gold += target.GoldReward;
                                enemies.RemoveAt(targetIndex - 1);
                            }

                            actionCompleted = true; // Turn action done
                        }
                        else // Selected target not valid
                        {
                            Console.WriteLine("\n[!] Invalid target selection! Choose again.");
                        }
                    }
                    else if (actionChoice == "2")
                    {
                        // Check for potions BEFORE using the turn
                        Potion potionToUse = null;
                        foreach (Item item in player.Inventory) // simple logic: get the first potion in inventory
                        {
                            if (item is Potion potion)
                            {
                                potionToUse = potion;
                                break;
                            }
                        }

                        if (potionToUse != null)
                        {
                            player.Health += potionToUse.HealingPoints;
                            if (player.Health > player.MaxHealth)
                            {
                                player.Health = player.MaxHealth;
                            }

                            player.Inventory.Remove(potionToUse);
                            Console.WriteLine($"\nYou used {potionToUse.ItemName} and restored {potionToUse.HealingPoints} HP!");
                            Console.WriteLine($"Current Health: {player.Health}/{player.MaxHealth}");

                            actionCompleted = true; // Turn action done
                        }
                        else
                        {
                            // Player has no potions, don't end the turn
                            Console.WriteLine("\n[!] You have no potions in your inventory! Pick another action.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("\n[!] Invalid input! Please select option 1 or 2.");
                    }
                }

                // If all enemies are defeated after player action, combat ends immediately
                if (enemies.Count == 0)
                {
                    Console.WriteLine("\n*** VICTORY! All enemies in this wave have been defeated! ***");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    return true;
                }

                // --------------------------------------------------
                // 2. ENEMIES' TURN
                // --------------------------------------------------
                Console.WriteLine("\n--- ENEMIES' TURN ---");
                foreach (Enemy enemy in enemies)
                {
                    if (enemy.IsAlive)
                    {
                        Console.WriteLine($"{enemy.Name} attacks you!");
                        
                        // Player takes damage (factoring in equipped armor bonuses)
                        int initialHealth = player.Health;
                        
                        // Apply defense reduction
                        int totalDefense = player.GetTotalDefense();
                        int netDamage = enemy.AttackPower - totalDefense;
                        if (netDamage < 1) netDamage = 1;

                        player.Health -= netDamage;
                        if (player.Health < 0) player.Health = 0;
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"--> {enemy.Name} dealt {netDamage} damage to you! (HP: {player.Health}/{player.MaxHealth})");
                        Console.ResetColor();
                        // Check if player died during enemy turn
                        if (!player.IsAlive)
                        {
                            Console.WriteLine("\nYou have been slain in battle...");
                            Console.WriteLine("Press any key to continue...");
                            Console.ReadKey();
                            return false; // Player failed wave
                        }
                    }
                }

                Console.WriteLine("\nPress any key for the next turn...");
                Console.ReadKey();
                Console.Clear();
            }

            return player.IsAlive;
        }

        public void DisplayVictoryScreen(Player player)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(@"

            YOU BEATED ALL 10 WAVES!!!
            ╔═══════════════════════════════════════════════╗
            ║                                               ║
            ║               --- GAME OVER ---               ║
            ║                                               ║
            ╚═══════════════════════════════════════════════╝

            ");
            Console.ResetColor();

            Console.WriteLine($"\nCongratulations, {player.Name}!");
            Console.WriteLine("You have survived all 10 brutal waves of Sharp Wave Survival!");
            Console.WriteLine($"Final Stats: Gold Remaining: {player.Gold}¢ | Health: {player.Health}/{player.MaxHealth}");
            Console.WriteLine("\nPress any key to finish the game...");
            Console.ReadKey();
        }
    }
}