using System.Collections.Generic;
using System;

namespace CodeSampleModalLayer
{
    [Serializable]
    public class Item
    {
        public string id;
        public string name;
        public Utilities.InventoryCategories category;
        public int totalOwned;
        private int amountInBackpack = default;
        private int amountInInventory = default;
        public int AmountInBackpack { get { return amountInBackpack; } set { amountInBackpack = value; } }
        public int AmountInInventory { get { return amountInInventory; } set { amountInInventory = value; } }

        public Item(string uniqueId, string nameString, Utilities.InventoryCategories inventoryCategory, int numOwned)
        {
            id = uniqueId;
            category = inventoryCategory;
            totalOwned = numOwned;
            name = nameString;
        }

        public void Initialize()
        {
            AmountInBackpack = 0;
            AmountInInventory = totalOwned;
        }

        public override string ToString()
        {
            return $"id: {id} name: {name} category {category} totalOwned {totalOwned} amountInBackpack {amountInBackpack} amountInInventory {amountInInventory}";
        }

        public void DecreaseBackpackItemAmount(int subtraheand)
        {
            AmountInBackpack -= subtraheand;
            AmountInInventory += subtraheand;
        }

        public void IncreaseBackpackItemAmount(int addend)
        {
            AmountInBackpack += addend;
            AmountInInventory -= addend;
        }

    }

    // This class helps store the deserialized JSON data.
    [Serializable]
    public class ItemData
    {
        public List<Item> data = new List<Item>();

        public override string ToString()
        {
            foreach (var d in data)
            {
                return $"{d.ToString()}";
            }
            return "";
        }
    }
}