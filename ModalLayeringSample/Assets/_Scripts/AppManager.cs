using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

namespace CodeSampleModalLayer
{
    //TODO: Create remove all button on the backpack modal.
    //TODO: Create highlight state for the tab buttons
    //TODO: Add a way to show the bag space in the info modals. Add a counter?
    //TODO: Button press effect on items in the inventory
    //TODO: Assign a gradient to each item category
    //TODO: Add an info button for credits
    //TODO: Create a credits popup
    //TODO: Write up README documentation
    //TODO: Alternative design for the scroll bar
    //TODO: Rename the infoModalTemplate to something like ItemInfoModal
    //TODO: Cleanup
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
        public DataManager DataMan { get { return dataManager; } private set { dataManager = value; } }
        public AppData AppDataObject { get { return appDataObject; } }
        // Global Static Variable
        public static AppManager Instance { get; private set; }

        private DataManager dataManager = default;
        private Backpack playerBackpack = default;
        // Amount to decrease or increase in the inventory when we add or remove an item to the backpack
        private const int itemAmountDifference = 1;

        // App Starts here. Ie. the "main" function
        void Start()
        {
            DataMan = new DataManager();
            playerBackpack = new Backpack();
            UIMan.Initialize();
            DataMan.Initialize(jsonFile: AppDataObject.ItemJSONFile);

            Instance = this;
            homeView.Setup();
        }

        public void AddItemToBackpack(Item item)
        {
            if (playerBackpack.IsBackpackFull())
            {
                Debug.Log("Backpack is full! Can't fit anymore items!");
                return;
            }

            playerBackpack.AddItem(item: item);
            item.IncreaseBackpackItemAmount(itemAmountDifference);
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

            item.DecreaseBackpackItemAmount(itemAmountDifference);
            playerBackpack.RemoveItem(item: item);
            homeView.UpdateBackpackItemCount(count: playerBackpack.GetTotalItemsInBackpack(), maxNumber: playerBackpack.MaxTotalItems);
            homeView.UpdateInventoryItem(item: item);
        }

#region Backpack Helpers
        public int GetTotalItemsInBackpack()
        {
            return playerBackpack.GetTotalItemsInBackpack();
        }

        public bool IsBackpackFull()
        {
            return playerBackpack.IsBackpackFull();
        }
        public bool IsBackpackEmpty()
        {
            return playerBackpack.IsBackpackEmpty();
        }
        public int GetBackpackMaxItemCount()
        {
            return playerBackpack.MaxTotalItems;
        }

        public List<Item> GetBackpackItemList()
        {
            return playerBackpack.ItemList;
        }
#endregion
    }
}