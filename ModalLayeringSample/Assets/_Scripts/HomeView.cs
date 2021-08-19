using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
using UnityEngine.Events;

namespace CodeSampleModalLayer
{
    [RequireComponent(typeof(ScrollingBackground))]
    public class HomeView : MonoBehaviour
    {
        [SerializeField]
        private RectTransform scrollContentRectTransform = default;
        [SerializeField]
        private RectTransform buttonParentRectTransform = default;
        [SerializeField]
        private BackpackButton backpackButton = default;
        [SerializeField]
        private TextMeshProUGUI viewTitleText = default;
        [SerializeField]
        private ScrollRect scrollRect = default;

        private AppManager appMan = default;
        private ScrollingBackground scrollBackground = default;
        private List<TabContentParent> TabContentParentList = new List<TabContentParent>();
        private List<NavButton> navButtonList = new List<NavButton>();
        private TabContentParent currentTabOpen = default;

        public void Setup()
        {
            appMan = AppManager.Instance;
            scrollBackground = GetComponent<ScrollingBackground>();
            viewTitleText.text = appMan.DataMan.GetCopyText("inventory.title");

            if (scrollBackground != null)
            {
                scrollBackground.Initialize();
            }

            backpackButton.Setup();
            SetupNavigation();
            SwitchInventoryTabs(GetTabContentParent(Utilities.InventoryCategories.All));
        }

        private void SetupNavigation()
        {
            // Key: Category, Value: ItemList
            foreach (var data in appMan.DataMan.sortedItemData)
            {
                // TODO: Handle if there are no items set to a category. Still show button?
                TabContentParent sip = CreateTabContentParent(category: data.Key, itemList: data.Value);
                NavButton navButton = CreateNavButton(
                    category: data.Key,
                    cbOnClick: () =>
                    {
                        SwitchInventoryTabs(sip);
                    });

                // Hide the TabContentParent by default so it doesn't visually conflict with the other tabs.
                sip.Hide();
                navButtonList.Add(navButton);
                TabContentParentList.Add(sip);
            }
        }

        private void SwitchInventoryTabs(TabContentParent sip)
        {
            // Reset the scrollRect to the top of the list
            scrollRect.verticalNormalizedPosition = 1;
            if (currentTabOpen != null)
            {
                currentTabOpen.Hide();
            }
            currentTabOpen = sip;
            currentTabOpen.Show();
        }

        private NavButton CreateNavButton(Utilities.InventoryCategories category, UnityAction cbOnClick)
        {
            NavButton navBtn = Instantiate(appMan.UIMan.NavButtonPrefab, buttonParentRectTransform);
            navBtn.Setup(
                label: category.ToString(),
                inventoryCategory: category,
                cbOnClick: () =>
                {
                    if (cbOnClick != null)
                    {
                        cbOnClick();
                    }
                }
            );
            return navBtn;
        }

        private TabContentParent CreateTabContentParent(Utilities.InventoryCategories category, List<Item> itemList)
        {
            TabContentParent sip = Instantiate(appMan.UIMan.TabContentParentPrefab, scrollContentRectTransform);
            sip.Setup(category, itemList);
            return sip;
        }


        private TabContentParent GetTabContentParent(Utilities.InventoryCategories category)
        {
            foreach (TabContentParent sip in TabContentParentList)
            {
                if (sip.Category.Equals(category))
                {
                    return sip;
                }
            }
            Debug.Log($"HomeView.cs GetNavButtonByCategory():: No TabContentParent of category {category} found.");
            return null;
        }


        public void UpdateBackpackItemCount(int count, int maxNumber)
        {
            backpackButton.UpdateCountText(count, maxNumber);
        }

        public void UpdateInventoryItem(Item item)
        {
            foreach (TabContentParent sip in TabContentParentList)
            {
                sip.UpdateItem(item: item);
            }
        }

        public void Shutdown()
        {
            scrollBackground.Shutdown();

            foreach (var i in TabContentParentList)
            {
                i.Shutdown();
            }
            TabContentParentList.Clear();

            foreach (NavButton i in navButtonList)
            {
                Destroy(i.gameObject);
            }
            navButtonList.Clear();
        }
    }
}