using System.Collections.Generic;
using UnityEngine;

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

        private DataManager dataManager = default;
        private Backpack playerBackpack = default;

        // App Starts here. Ie. the "main" function
        void Start()
        {
            Instance = this;
            DataMan = new DataManager();
            playerBackpack = new Backpack();
            UIMan.Initialize();
            DataMan.Initialize();

            homeView.Setup();
        }

        public void AddItemToBackpack(Item item, int itemAmountDifference)
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
            
            Debug.Log($"{item.name} has been added to the backpack. Total items in backpack: {GetTotalItemsInBackpack()}");
        }

        public void RemoveItemFromBackpack(Item item, int itemAmountDifference)
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

            Debug.Log($"{item.name} has been removed from the backpack. Total items in backpack: {GetTotalItemsInBackpack()}");
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

        public void EmptyBackpack()
        {
            foreach(Item i in playerBackpack.ItemList)
            {
                i.DecreaseBackpackItemAmount(i.AmountInBackpack);
                homeView.UpdateInventoryItem(item: i);
            }

            homeView.UpdateBackpackItemCount(count: playerBackpack.GetTotalItemsInBackpack(), maxNumber: playerBackpack.MaxTotalItems);
            playerBackpack.RemoveAllItems();
        }
#endregion
    }
}