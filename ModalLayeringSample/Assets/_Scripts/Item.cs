using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace CodeSampleModalLayer
{
    [Serializable]
    public class Item
    {
        public string id;
        public Utilities.ItemType type;
        public Utilities.InventoryCategories category;
        public int totalOwned;
        private int amountInBackpack = default;
        private int amountInInventory = default;
        public int AmountInBackpack { get { return amountInBackpack; } set { amountInBackpack = value; } }
        public int AmountInInventory { get { return amountInInventory; } set { amountInInventory = value; } }

        public Item(string uniqueId, Utilities.ItemType itemType, Utilities.InventoryCategories inventoryCategory, int numOwned)
        {
            id = uniqueId;
            type = itemType;
            category = inventoryCategory;
            totalOwned = numOwned;
        }

        public void Initialize()
        {
            AmountInBackpack = 0;
            AmountInInventory = totalOwned;
        }

        //TODO: Add tostring override for debugging

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