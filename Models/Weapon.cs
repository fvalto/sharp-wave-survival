namespace sharp_wave_survival.Models
{
    public class Weapon : Item
    {
        //Attributes
        public int WeaponLevel { get; set; }
        public int BonusDamage { get; set; }

        // Constructor
public Weapon(string itemName, string description, int value, int weaponLevel, int bonusDamage) : base(itemName, description, value)        {
            WeaponLevel = weaponLevel;
            BonusDamage = bonusDamage;
        }
    }
}