using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CodeSampleModalLayer
{
    //TODO: Change name to 'TabContentParent'?
    public class SquareItemParent : MonoBehaviour
    {
        private Utilities.InventoryCategories category = default;
        private AppManager appMan = default;
        public Utilities.InventoryCategories Category { get { return category; } private set { category = value; } }
        private List<SquareItem> squareItemList = new List<SquareItem>();

        public void Setup(Utilities.InventoryCategories inventoryCategory, List<Item> itemList)
        {
            appMan = AppManager.Instance;
            Category = inventoryCategory;

            foreach (var i in itemList)
            {
                // Don't create an item if we don't own any
                if (i.totalOwned != 0)
                {
                    CreateSquareItem(item: i);
                }
            }
        }

        private void CreateSquareItem(Item item)
        {
            SquareItem si = Instantiate(appMan.SquareItemPrefab, this.gameObject.transform);
            si.Setup(item: item, locationCreated: SquareItem.LocationCreated.homeView);
            squareItemList.Add(si);
        }

        public void Show()
        {
            this.gameObject.SetActive(true);
        }

        public void Hide()
        {
            this.gameObject.SetActive(false);
        }

        public void Shutdown()
        {
            foreach (var si in squareItemList)
            {
                si.Shutdown();
            }
            squareItemList.Clear();
        }
    }
}