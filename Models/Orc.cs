namespace sharp_wave_survival.Models
{
    public class Orc : Enemy
    {
        //Constructor will take just the tier, based on tier will assign all its attributes with a ternary operator
        public Orc(string tier = "Common") 
            : base("Orc", tier, 
                   maxHealth: tier == "Master" ? 120 : (tier == "Epic" ? 80 : 40), 
                   attackPower: tier == "Master" ? 40 : (tier == "Epic" ? 32 : 20), 
                   defense: tier == "Master" ? 22 : (tier == "Epic" ? 11 : 4), 
                   goldReward: tier == "Master" ? 80 : (tier == "Epic" ? 55 : 35))
        {
        }
    }
}