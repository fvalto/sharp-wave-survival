using System;
using System.Collections.Generic;
using sharp_wave_survival.Models;

namespace sharp_wave_survival.Systems
{
    public class WaveManager
    {
        private Random _random = new Random();

        // Helper method to generate a single random enemy of a specific tier
        private Enemy GetRandomEnemy(string tier)
        {
            int roll = _random.Next(1, 4); // Picks 1, 2, or 3
            return roll switch
            {
                1 => new Bandit(tier),
                2 => new Zombie(tier),
                3 => new Orc(tier),
                _ => new Bandit(tier)
            };
        }

        // Returns a list of enemies based on the wave number
        public List<Enemy> GetEnemiesForWave(int waveNumber)
        {
            List<Enemy> waveEnemies = new List<Enemy>();

            switch (waveNumber)
            {
                case 1:
                    // Wave 1: 1 random common enemy
                    waveEnemies.Add(GetRandomEnemy("Common"));
                    break;

                case 2:
                    // Wave 2: 2 random common enemies
                    waveEnemies.Add(GetRandomEnemy("Common"));
                    waveEnemies.Add(GetRandomEnemy("Common"));
                    break;

                case 3:
                    // Wave 3: 2 random common enemies, 1 random epic enemy
                    waveEnemies.Add(GetRandomEnemy("Common"));
                    waveEnemies.Add(GetRandomEnemy("Common"));
                    waveEnemies.Add(GetRandomEnemy("Epic"));
                    break;

                case 4:
                    // Wave 4: 2 random common enemies, 2 random epic enemies
                    waveEnemies.Add(GetRandomEnemy("Common"));
                    waveEnemies.Add(GetRandomEnemy("Common"));
                    waveEnemies.Add(GetRandomEnemy("Epic"));
                    waveEnemies.Add(GetRandomEnemy("Epic"));
                    break;

                case 5:
                    // Wave 5: 4 random common enemies
                    waveEnemies.Add(GetRandomEnemy("Common"));
                    waveEnemies.Add(GetRandomEnemy("Common"));
                    waveEnemies.Add(GetRandomEnemy("Common"));
                    waveEnemies.Add(GetRandomEnemy("Common"));
                    break;

                case 6:
                    // Wave 6: 3 random common enemies, 1 random epic enemy
                    waveEnemies.Add(GetRandomEnemy("Common"));
                    waveEnemies.Add(GetRandomEnemy("Common"));
                    waveEnemies.Add(GetRandomEnemy("Common"));
                    waveEnemies.Add(GetRandomEnemy("Epic"));
                    break;

                case 7:
                    // Wave 7: 2 random common enemies, 2 random epic enemies
                    waveEnemies.Add(GetRandomEnemy("Common"));
                    waveEnemies.Add(GetRandomEnemy("Common"));
                    waveEnemies.Add(GetRandomEnemy("Epic"));
                    waveEnemies.Add(GetRandomEnemy("Epic"));
                    break;

                case 8:
                    // Wave 8: 2 random common enemies, 2 random epic enemies, 1 random master enemy
                    waveEnemies.Add(GetRandomEnemy("Common"));
                    waveEnemies.Add(GetRandomEnemy("Common"));
                    waveEnemies.Add(GetRandomEnemy("Epic"));
                    waveEnemies.Add(GetRandomEnemy("Epic"));
                    waveEnemies.Add(GetRandomEnemy("Master"));
                    break;

                case 9:
                    // Wave 9: 3 random common enemies, 3 random epic enemies, 1 random master enemy
                    waveEnemies.Add(GetRandomEnemy("Common"));
                    waveEnemies.Add(GetRandomEnemy("Common"));
                    waveEnemies.Add(GetRandomEnemy("Common"));
                    waveEnemies.Add(GetRandomEnemy("Epic"));
                    waveEnemies.Add(GetRandomEnemy("Epic"));
                    waveEnemies.Add(GetRandomEnemy("Epic"));
                    waveEnemies.Add(GetRandomEnemy("Master"));
                    break;

                case 10:
                    // Wave 10 (Final): 4 random master enemies
                    waveEnemies.Add(GetRandomEnemy("Master"));
                    waveEnemies.Add(GetRandomEnemy("Master"));
                    waveEnemies.Add(GetRandomEnemy("Master"));
                    waveEnemies.Add(GetRandomEnemy("Master"));
                    break;

                default:
                    // End Game
                    break;
            }

            return waveEnemies;
        }
    }
}