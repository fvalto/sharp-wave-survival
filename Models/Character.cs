namespace sharp_wave_survival.Models
{
    //BASE CLASS TO SHARE ATTRIBUTES BETWEEN PLAYER AND ENEMIES
    public abstract class Character{
        // Attributes with getters and setters
        public string Name { get; set; }
        public int Health { get; set; }
        public int MaxHealth { get; set; }
        public int AttackPower { get; set; }
        public int Defense { get; set; }
        //Helper to check if character is alive
        public bool IsAlive
        {
            get
            {
                if (Health > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        
        // Constructor
        public Character(string name, int health, int maxHealth, int attackPower, int defense)
        {
            this.Name = name;
            this.Health = maxHealth;
            this.MaxHealth = maxHealth;
            this.AttackPower = attackPower;
            this.Defense = defense;
        }

        public virtual void TakeDamage(int incomingDamage)
        {
            int netDamage = incomingDamage - Defense;
            if (netDamage < 1)
            {
                netDamage = 1; //at least take 1 damage
            }
            Health -= netDamage;
        }
    }
}