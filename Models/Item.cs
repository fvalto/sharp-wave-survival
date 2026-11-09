namespace sharp_wave_survival.Models
{
    //BASE CLASS TO SHARE ATTRIBUTES BETWEEN DIFFERENT ITEMS
    public class Item
    {
        //Attributes
        public string ItemName { get; set; }
        public string Description { get; set; }
        public int Value { get; set; }

        //Constructor
        public Item(string itemName, string description, int value)        {
            ItemName = itemName;
            Description = description;
            Value = value;
        }
    }
}