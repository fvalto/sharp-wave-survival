namespace sharp_wave_survival.Models
{
    public class Player : Character //Player class inherits from class Character
    {
        public int Gold { get; set; }
        public List<Item> Inventory { get; set; }
        public Weapon EquippedWeapon { get; set; }
        //To manage armor, a dictonary is implemented as => Key: ArmorSlot (e.g. Helmet), Value: Equipped Armor item
        public Dictionary<ArmorSlot, Armor> EquippedArmor { get; set; }

        // The constructor takes a name, and passes default values up to Character (base)
        public Player(string name) : base(name, health: 100, maxHealth: 100, attackPower: 10, defense: 2)
        {
            Gold = 50;
            Inventory = new List<Item>();
            EquippedArmor = new Dictionary<ArmorSlot, Armor>();
        }
        // Calculate total attack: Base attack + Equipped Weapon
        public int GetTotalAttack()
        {
            if (EquippedWeapon != null)
            {
                return AttackPower + EquippedWeapon.BonusDamage; //if equipped weappong is not null, the add the bonusdamage from the weapon
            }
            else
            {
                return AttackPower; //if no weapons equipped, just return attack power
            }
        }
        // Calculate total defense: Base Defense + All Equipped Armor Bonuses
        public int GetTotalDefense()
        {
            int extraDefense = 0;
            foreach (var armor in EquippedArmor.Values)
            {
                extraDefense += armor.BonusDefense;
            }
            return Defense + extraDefense;
        }
    }
}