namespace sharp_wave_survival.Models
{
    public enum ArmorSlot { Helmet, Coat, Pants }

    public class Armor : Item
    {
        //Attributes
        public int ArmorLevel { get; set; }
        public int BonusDefense { get; set; }
        public ArmorSlot Slot { get; set; }

        //Constructor
public Armor(string itemName, string description, int value, int armorLevel, int bonusDefense, ArmorSlot slot) : base(itemName, description, value)        {
            ArmorLevel = armorLevel;
            BonusDefense = bonusDefense;
            Slot = slot;
        }
    }
}