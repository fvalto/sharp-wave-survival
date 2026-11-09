using System;
using System.Collections.Generic;
using sharp_wave_survival.Models;

namespace sharp_wave_survival.Systems
{
    public class ShopManager
    {
        public List<Item> GetCatalogForWave(int currentWave)
        {
            if (currentWave <= 3)
            {
                // TIER 1 CATALOG (Waves 1-3)
                return new List<Item>
                {
                    new Weapon("Wood Sword", "It's better than nothing", 25, 1, 5),
                    new Weapon("Rusty Axe", "Heavy but packs a punch", 35, 1, 8),
                    new Weapon("Hunting Bow", "Light and reliable", 30, 1, 6),
                    new Armor("Leather Coat", "Basic torso protection (+3 Def)", 25, 1, 3, ArmorSlot.Coat),
                    new Armor("Leather Pants", "Better than running bare-legged (+2 Def)", 20, 1, 2, ArmorSlot.Pants),
                    new Potion("Minor Healing Potion", "Restores 20 HP", 15, 20)
                };
            }
            else if (currentWave <= 6)
            {
                // TIER 2 CATALOG (Waves 4-6)
                return new List<Item>
                {
                    new Weapon("Iron Broadsword", "Balanced steel blade (+12 Dmg)", 60, 2, 12),
                    new Weapon("Battle Axe", "Brutal force (+18 Dmg)", 80, 2, 18),
                    new Weapon("Recurve Bow", "Reinforced grip (+14 Dmg)", 70, 2, 14),
                    new Armor("Iron Helmet", "Nice head protection (+5 Def)", 45, 2, 5, ArmorSlot.Helmet),
                    new Armor("Iron Vest", "Solid iron vest (+8 Def)", 55, 2, 8, ArmorSlot.Coat),
                    new Armor("Iron Greaves", "Heavy leg guards (+6 Def)", 50, 2, 6, ArmorSlot.Pants),
                    new Potion("Regular Healing Potion", "Restores 50 HP", 30, 50)
                };
            }
            else
            {
                // TIER 3 CATALOG (Waves 7-10)
                return new List<Item>
                {
                    new Weapon("Mythril Greatsword", "Legendary edge (+25 Dmg)", 130, 3, 25),
                    new Weapon("Warhammer", "Crushes high armor (+32 Dmg)", 160, 3, 32),
                    new Weapon("Composite Bow", "Devastating range (+28 Dmg)", 145, 3, 28),
                    new Armor("Knight Helmet", "Full-visor helmet (+10 Def)", 90, 3, 10, ArmorSlot.Helmet),
                    new Armor("Knight Plate Armor", "Top-tier chest plate (+16 Def)", 120, 3, 16, ArmorSlot.Coat),
                    new Armor("Knight Greaves", "Armored greaves (+8 Def)", 80, 3, 8, ArmorSlot.Pants),
                    new Potion("Greater Healing Potion", "Restores full health", 50, 100)
                };
            }
        }

        public void OpenShop(Player player, int currentWave) //OpenSHop Method to start shopping
        {
            List<Item> catalog = GetCatalogForWave(currentWave);
            bool shopping = true;            while (shopping)
            {
                Console.Clear();
                Console.WriteLine($""" 
                Welcome {player.Name} to
                ╔═══════════════════════════════════════════════╗
                ║                                               ║
                ║                   -SHOP MENU-                 ║
                ║                                               ║
                ╚═══════════════════════════════════════════════╝
                Your Gold: {player.Gold}¢ - Life: {player.Health}
                
                """);

                for (int i = 0; i < catalog.Count; i++)
                {
                    Item item = catalog[i];
                    Console.WriteLine($"{i + 1}. {item.ItemName} => [{item.Value}¢] - '{item.Description}'");
                }
                Console.WriteLine($"{catalog.Count + 1}. Exit Shop");

                Console.Write("\nSelect an item to buy: ");
                if (int.TryParse(Console.ReadLine(), out int choice) && choice > 0 && choice <= catalog.Count + 1)
                {
                    if (choice == catalog.Count + 1)
                    {
                        shopping = false;
                    }
                    else
                    {
                        Item selectedItem = catalog[choice - 1];
                        ProcessPurchase(player, selectedItem);
                    }
                }
            }
        }

        private void ProcessPurchase(Player player, Item item)
        {
            // First check if the player has enough gold
            if (player.Gold < item.Value)
            {
                Console.WriteLine("\nNot enough gold to buy this item!");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                return;
            }

            // Handle Weapon replacement logic
            if (item is Weapon newWeapon)
            {
                if (player.EquippedWeapon != null)
                {
                    Console.WriteLine($"\nTHIS PURCHASE WILL REPLACE YOUR CURRENT WEAPON ({player.EquippedWeapon.ItemName})!");
                    Console.Write("Do you want to buy it anyway? (y/n): ");
                    string choice = Console.ReadLine()?.ToLower();

                    if (choice != "y")
                    {
                        Console.WriteLine("Purchase canceled.");
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();
                        return; // Exit purchase flow
                    }
                }

                // Deduct gold, assign weapon, and store in inventory
                player.Gold -= item.Value;
                player.EquippedWeapon = newWeapon;
                player.Inventory.Add(item);
                Console.WriteLine($"\nYou purchased and equipped the {newWeapon.ItemName}!");
            }

            // Handle Armor replacement logic
            else if (item is Armor newArmor)
            {
                // Check if the player already has armor equipped in THIS specific slot
                if (player.EquippedArmor.ContainsKey(newArmor.Slot))
                {
                    Armor currentArmor = player.EquippedArmor[newArmor.Slot];
                    Console.WriteLine($"\nTHIS PURCHASE WILL REPLACE YOUR CURRENT {newArmor.Slot.ToString().ToUpper()} ({currentArmor.ItemName})!");
                    Console.Write("Do you want to buy it anyway? (y/n): ");
                    string choice = Console.ReadLine()?.ToLower();

                    if (choice != "y")
                    {
                        Console.WriteLine("Purchase canceled.");
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();
                        return;
                    }
                }
                // Deduct gold
                player.Gold -= item.Value;
                // Equip/override the item in that specific slot
                player.EquippedArmor[newArmor.Slot] = newArmor;
                player.Inventory.Add(item);
                Console.WriteLine($"\nYou purchased and equipped the {newArmor.ItemName} ({newArmor.Slot})!");
            }
            else
            {
                player.Gold -= item.Value;
                player.Inventory.Add(item);
                Console.WriteLine($"\nYou purchased the {item.ItemName}!");
            }

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            }
    }
}