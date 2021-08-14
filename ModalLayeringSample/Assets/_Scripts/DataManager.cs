using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace CodeSampleModalLayer
{
    public class DataManager
    {
        // Holds unsorted item data
        private ItemData itemData = default;
        // Holds sorted item data by category
        public Dictionary<Utilities.InventoryCategories, List<Item>> sortedItemData = new Dictionary<Utilities.InventoryCategories, List<Item>>();
        private TextAsset mDataFile = default;

        public void Initialize(TextAsset jsonFile)
        {
            itemData = new ItemData();
            mDataFile = jsonFile;

            GetItemData();
            SortItemListByCategory();
        }

        public void GetItemData()
        {
            TextAsset file = mDataFile;
            if (file == null)
            {
                Debug.LogError($"AppManager.cs GetItemData() :: Unable to load the Item Data File.");
                return;
            }

            // Deserialize the JSON from ItemData.JSON and store it in ItemData
            JsonUtility.FromJsonOverwrite(file.text, itemData);

            // Make sure to initialize each item.
            foreach (var i in itemData.data)
            {
                i.Initialize();
            }
        }

        private void SortItemListByCategory()
        {
            // Loop through each entry in the Utilities.InventoryCategories enum and sort the items
            foreach (Utilities.InventoryCategories category in Enum.GetValues(typeof(Utilities.InventoryCategories)))
            {
                List<Item> tempList = new List<Item>();

                if (category == Utilities.InventoryCategories.None)
                {
                    continue;
                }

                if (category == Utilities.InventoryCategories.All)
                {
                    sortedItemData.Add(category, itemData.data);
                }
                else
                {
                    foreach (Item i in itemData.data)
                    {
                        if (i.category != category)
                        {
                            continue;
                        }

                        tempList.Add(i);
                    }
                }

                if (tempList.Count != 0)
                {
                    sortedItemData.Add(category, tempList);
                }
                else
                {
                    Debug.Log($"AppManager.cs SortItemListByCategory() :: No item of category {category} found. Did not add new object to sortedItemData.");
                }
            }
        }
    }
}