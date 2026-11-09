namespace sharp_wave_survival.Models
{
    public class Zombie : Enemy
    {
        //Constructor will take just the tier, based on tier will assign all its attributes with a ternary operator
        public Zombie(string tier = "Common") 
            : base("Zombie", tier, 
                   maxHealth: tier == "Master" ? 90 : (tier == "Epic" ? 45 : 20), 
                   attackPower: tier == "Master" ? 35 : (tier == "Epic" ? 20 : 10), 
                   defense: tier == "Master" ? 9 : (tier == "Epic" ? 3 : 0), 
                   goldReward: tier == "Master" ? 50 : (tier == "Epic" ? 30 : 20))
        {
        }
    }
}