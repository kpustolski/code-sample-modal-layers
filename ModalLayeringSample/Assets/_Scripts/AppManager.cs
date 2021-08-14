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
        public DataManager DataMan { get { return dataManager; } private set { dataManager = value; } }
        public AppData AppDataObject { get { return appDataObject; } }
        // Global Static Variable
        public static AppManager Instance { get; private set; }
        public Backpack PlayerBackpack { get { return playerBackpack; } }

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
            Debug.Log("Adding item to backpack");
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

        public int GetTotalItemsInBackpack()
        {
            return playerBackpack.GetTotalItemsInBackpack();
        }
    }
}