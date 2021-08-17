using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CodeSampleModalLayer
{
    public static class Utilities
    {
        public enum InventoryCategories
        {
            All = 0, // No item should have this set as its category. The code will throw an error.
            None = 1, // This will make the item not show up.
            Clothing = 2,
            Tools = 3,
            Gear = 4,
            Other = 5,
        }

        public enum ItemType
        {
            None = 0,
            BaseballTShirt = 1,
            Binoculars = 2,
            CargoShorts = 3,
            FirstAidKit = 4,
            Guitar = 5,
            Hat = 6,
            Map = 7,
            MarshmellowStick = 8,
            Pickaxe = 9,
            Rope = 10,
            Tent = 11
        }
    }
}