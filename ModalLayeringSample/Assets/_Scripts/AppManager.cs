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

        [Header("RectTransform")]
        [Space(5)]
        [SerializeField]
        private RectTransform dialogParent = default;

        [Header("Data")]
        [Space(5)]
        [SerializeField]
        private AppData appDataObject = default;

        //TODO: May place these in a UIManager.cs class
        [Header("Prefabs")]
        [Space(5)]
        [SerializeField]
        private InfoModalTemplate infoModalTemplatePrefab = default;
        [SerializeField]
        private BackpackModal backpackModalPrefab = default;
        [SerializeField]
        private SquareItem squareItemPrefab = default;
        [SerializeField]
        private NavButton navButtonPrefab = default;
        [SerializeField]
        private SquareItemParent squareItemParentPrefab = default;

        public InfoModalTemplate InfoModalTemplatePrefab { get { return infoModalTemplatePrefab; } }
        public BackpackModal BackpackModalPrefab { get { return backpackModalPrefab; } }
        public SquareItem SquareItemPrefab { get { return squareItemPrefab; } }
        public SquareItemParent SquareItemParentPrefab { get { return squareItemParentPrefab; } }
        public NavButton NavButtonPrefab { get { return navButtonPrefab; } }
        public RectTransform DialogParent { get { return dialogParent; } }
        public AppData AppDataObject { get { return appDataObject; } }


        private List<IModalLayer> modalLayerList = new List<IModalLayer>();
        private Backpack playerBackpack = default;
        public Backpack PlayerBackpack { get { return playerBackpack; } }
        // Global Static Variable
        public static AppManager Instance { get; private set; }
        // Holds unsorted item data
        private ItemData itemData = default;
        // Holds sorted item data by category
        public Dictionary<Utilities.InventoryCategories, List<Item>> sortedItemData = new Dictionary<Utilities.InventoryCategories, List<Item>>();
        // Amount to decrease or increase in the inventory when we add or remove an item to the backpack
        private const int itemAmountDifference = 1;

        // App Starts here. Ie. the "main" function
        void Start()
        {
            itemData = new ItemData();
            playerBackpack = new Backpack();

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

        public void AddToModalLayerList(IModalLayer layer)
        {
            if (layer == null)
            {
                //ERROR
                Debug.Log("AppManager.cs AddToModalLayerList():: Layer passed in is null");
                return;
            }

            // If a modal is already in the list, hide that one before showing the new one.
            if (modalLayerList.Count >= 1)
            {
                modalLayerList[modalLayerList.Count - 1].HideLayer();
            }

            // layer.ShowLayer();
            if (!modalLayerList.Contains(layer))
            {
                modalLayerList.Add(layer);
                int layerIndex = modalLayerList.IndexOf(layer);
                layer.AssignId(layerIndex);
            }

            // PrintModalLayerList();
        }

        public void RemoveFromModalLayerList(IModalLayer layer)
        {
            if (layer == null)
            {
                //ERROR
                Debug.Log("AppManager.cs AddToModalLayerList():: Layer passed in is null");
                return;
            }

            layer.HideLayer();
            if (modalLayerList.Contains(layer))
            {
                modalLayerList.Remove(layer);
            }

            // Show the next modal in the list (ie. the last element) if there are anymore in the list
            if (modalLayerList.Count >= 1)
            {
                modalLayerList[modalLayerList.Count - 1].ShowLayer();
            }

            // PrintModalLayerList("cyan");
        }

        #region Debug Methods
        private void PrintModalLayerList(string color = "red")
        {
            Debug.Log("------");
            for (int i = 0; i < modalLayerList.Count; i++)
            {
                Debug.Log($"<color={color}>{modalLayerList[i].GetId()}_{i}</color>");
            }
            Debug.Log("------");
        }

        private IModalLayer GetModalLayerById(string id)
        {
            foreach (IModalLayer modal in modalLayerList)
            {
                if (modal.GetId().Equals(id))
                {
                    return modal;
                }
            }
            return null; // Modal not found
        }
    }
    #endregion
}