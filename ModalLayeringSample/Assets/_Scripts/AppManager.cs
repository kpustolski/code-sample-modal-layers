using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

namespace CodeSampleModalLayer
{
    public class AppManager : MonoBehaviour
    {
        [Header("Views")]
        [Space(5)]
        [SerializeField]
        private HomeView homeView = default;

        [Header("Data")]
        [Space(5)]
        [SerializeField]
        private AppData appDataObject = default;

        [Header("Managers")]
        [Space(5)]
        [SerializeField]
        private UIManager uIManager = default;

        public UIManager UIMan { get { return uIManager; } }
        public AppData AppDataObject { get { return appDataObject; } }
        // Global Static Variable
        public static AppManager Instance { get; private set; }
        public Backpack PlayerBackpack { get { return playerBackpack; } }
        // Holds sorted item data by category
        public Dictionary<Utilities.InventoryCategories, List<Item>> sortedItemData = new Dictionary<Utilities.InventoryCategories, List<Item>>();

        // Holds unsorted item data
        private ItemData itemData = default;
        private Backpack playerBackpack = default;
        // Amount to decrease or increase in the inventory when we add or remove an item to the backpack
        private const int itemAmountDifference = 1;

        // App Starts here. Ie. the "main" function
        void Start()
        {
            itemData = new ItemData();
            playerBackpack = new Backpack();
            UIMan.Initialize();

            Instance = this;
            GetItemData();
            SortItemListByCategory();
            homeView.Setup();
        }

        public void GetItemData()
        {
            TextAsset file = AppDataObject.ItemJSONFile;
            if (file == null)
            {
                Debug.LogError($"AppManager.cs GetItemData() :: Unable to load the Item Data File.");
                return;
            }

            // Deserialize the JSON from ItemData.JSON and store it in ItemData
            Debug.Log($"File text: {file.text}");
            JsonUtility.FromJsonOverwrite(file.text, itemData);
            Debug.Log($"ItemData: {itemData.data.Count}");
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

        public void AddItemToBackpack(Item item)
        {
            Debug.Log("Adding item to backpack");
            if (playerBackpack.IsBackpackFull())
            {
                Debug.Log("Backpack is full! Can't fit anymore items!");
                return;
            }

            playerBackpack.AddItem(item);
            item.DecreaseItemAmount(itemAmountDifference);
            homeView.UpdateBackpackItemCount(count: playerBackpack.GetTotalItemsInBackpack(), maxNumber: playerBackpack.MaxTotalItems);
            homeView.UpdateInventoryItem(item: item);
        }
        public void RemoveItemFromBackpack(Item item)
        {
            if (playerBackpack.IsBackpackEmpty())
            {
                Debug.Log("Bag is empty. Nothing to remove.");
                return;
            }

            playerBackpack.RemoveItemById(item.id);
            item.IncreaseItemAmount(itemAmountDifference);
            homeView.UpdateBackpackItemCount(count: playerBackpack.GetTotalItemsInBackpack(), maxNumber: playerBackpack.MaxTotalItems);
            homeView.UpdateInventoryItem(item: item);
        }

        public int GetTotalItemsInBackpack()
        {
            return playerBackpack.GetTotalItemsInBackpack();
        }
    }
}