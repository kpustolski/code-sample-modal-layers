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
        // Holds copy data
        private CopyData copyData = default;

        public void Initialize()
        {
            copyData = new CopyData();
            itemData = new ItemData();

            GetItemData();
            GetCopyData();
            SortItemListByCategory();
        }

        public string GetCopyText(string copyId)
        {
            foreach(Copy c in copyData.data)
            {
                if(c.copyKey.Equals(copyId))
                {
                    return c.copyValue;
                }
            }
            return "[Missing Copy Value]";
        }

        private void GetCopyData()
        {
            TextAsset file = AppManager.Instance.AppDataObject.CopyJSONFile;
            if (file == null)
            {
                Debug.LogError($"DataManager.cs GetCopyData() :: Unable to load the Copy Data File.");
                return;
            }

            JsonUtility.FromJsonOverwrite(file.text, copyData);
        }

        private void GetItemData()
        {
            TextAsset file = AppManager.Instance.AppDataObject.ItemJSONFile;
            if (file == null)
            {
                Debug.LogError($"DataManager.cs GetItemData() :: Unable to load the Item Data File.");
                return;
            }

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

                if (category == Utilities.InventoryCategories.All)
                {
                    sortedItemData.Add(category, itemData.data);
                }
                else
                {
                    foreach (Item i in itemData.data)
                    {
                        // Items marked as None should be placed in the 'other' category
                        if (i.category.Equals(Utilities.InventoryCategories.None))
                        {
                            i.category = Utilities.InventoryCategories.Other;
                        }

                        if(i.category == Utilities.InventoryCategories.All)
                        {
                            Debug.LogError($"DataManager.cs SortItemListByCategory() :: item with id {i.id} should not have its category set to All. Please update.");
                        }
                        
                        if (i.category != category)
                        {
                            continue;
                        }

                        tempList.Add(i);
                    }

                    sortedItemData.Add(category, tempList);
                }
            }
        }
    }
}