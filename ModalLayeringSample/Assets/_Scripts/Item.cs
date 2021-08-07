using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CodeSampleModalLayer
{
    public class Item : MonoBehaviour
    {
        public string id;
        public Utilities.ItemType type;
        public Utilities.InventoryCategories category;
        public int totalOwned;

        public Item(string uniqueId, Utilities.ItemType itemType, Utilities.InventoryCategories inventoryCategory, int numOwned)
        {
            id = uniqueId;
            type = itemType;
            category = inventoryCategory;
            totalOwned = numOwned;
        }
    }
}