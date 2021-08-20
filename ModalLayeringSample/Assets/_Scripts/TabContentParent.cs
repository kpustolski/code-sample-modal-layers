using System.Collections.Generic;
using UnityEngine;

namespace CodeSampleModalLayer
{
    public class TabContentParent : MonoBehaviour
    {
        private Utilities.InventoryCategories category = default;
        private AppManager appMan = default;
        public Utilities.InventoryCategories Category { get { return category; } private set { category = value; } }
        private List<SquareItem> squareItemList = new List<SquareItem>();
        private RectTransform EmptyTabPanel = default;

        public void Setup(Utilities.InventoryCategories inventoryCategory, List<Item> itemList, RectTransform emptyTabPanel)
        {
            appMan = AppManager.Instance;
            Category = inventoryCategory;
            EmptyTabPanel = emptyTabPanel;

            foreach (var i in itemList)
            {
                CreateSquareItem(item: i);
            }
        }

        private void CreateSquareItem(Item item)
        {
            SquareItem si = Instantiate(appMan.UIMan.SquareItemPrefab, this.gameObject.transform);
            si.Setup(item: item, locationCreated: SquareItem.LocationCreated.homeView);
            squareItemList.Add(si);
        }

        public void UpdateItem(Item item)
        {
            SquareItem sqItem = GetSquareItem(item.id);
            if (sqItem != null)
            {
                sqItem.UpdateState();
            }
        }

        public SquareItem GetSquareItem(string itemId)
        {
            foreach (SquareItem si in squareItemList)
            {
                if (si.ItemAssigned.id.Equals(itemId))
                {
                    return si;
                }
            }
            return null;
        }

        public void Show()
        {
            EmptyTabPanel.gameObject.SetActive(squareItemList.Count == 0);
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
            Destroy(gameObject);
        }
    }
}