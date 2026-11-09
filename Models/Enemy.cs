namespace sharp_wave_survival.Models
{
    public abstract class Enemy : Character
    {
        public string Tier { get; set; } //3 tier levels: "Common", "Epic", "Master"
        public int GoldReward { get; set; }

        public Enemy(string name, string tier, int maxHealth, int attackPower, int defense, int goldReward) : base($"{tier} {name}", maxHealth, maxHealth, attackPower, defense)
        {
            Tier = tier;
            GoldReward = goldReward;
        }
    }
}