namespace sharp_wave_survival.Models
{
    public class Potion : Item
    {
        public int HealingPoints { get; set; }

        public Potion(string itemName, string description, int value, int healingPoints) : base(itemName, description, value)
        {
            HealingPoints = healingPoints;
        }
    }
}