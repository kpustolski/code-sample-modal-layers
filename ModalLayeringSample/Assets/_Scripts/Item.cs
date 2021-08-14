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
        public int totalOwned; //TODO: Rename?
        private bool bIsItemInBackPack = default;
        public bool IsItemInBackPack { get { return bIsItemInBackPack; } set { bIsItemInBackPack = value; } }

        public Item(string uniqueId, Utilities.ItemType itemType, Utilities.InventoryCategories inventoryCategory, int numOwned)
        {
            id = uniqueId;
            type = itemType;
            category = inventoryCategory;
            totalOwned = numOwned;
        }

        public void DecreaseItemAmount(int subtraheand)
        {
            totalOwned -= subtraheand;
        }

        public void IncreaseItemAmount(int addend)
        {
            totalOwned += addend;
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