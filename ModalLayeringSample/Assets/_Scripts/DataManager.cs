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
                        if(i.category  == Utilities.InventoryCategories.All)
                        {
                            Debug.LogError($"DataManager.cs SortItemListByCategory() :: item with id {i.id} should not have its category set to All. Please update.");
                        }
                        
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
                    Debug.Log($"DataManager.cs SortItemListByCategory() :: No item of category {category} found. Did not add new object to sortedItemData.");
                }
            }
        }
    }
}