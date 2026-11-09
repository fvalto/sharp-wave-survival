namespace sharp_wave_survival.Models
{
    public class Bandit : Enemy
    {
        //Constructor will take just the tier, based on tier will assign all its attributes with a ternary operator
        public Bandit(string tier = "Common") 
            : base("Bandit", tier, 
                   maxHealth: tier == "Master" ? 100 : (tier == "Epic" ? 50 : 30), 
                   attackPower: tier == "Master" ? 35 : (tier == "Epic" ? 25 : 15), 
                   defense: tier == "Master" ? 15 : (tier == "Epic" ? 5 : 2), 
                   goldReward: tier == "Master" ? 60 : (tier == "Epic" ? 40 : 25))
        {
        }
    }
}