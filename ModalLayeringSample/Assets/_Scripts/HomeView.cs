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
        private NavButton currentSelectedNavButton = default;

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
            SwitchInventoryTabs(GetNavButton(Utilities.InventoryCategories.All));
        }

        private void SetupNavigation()
        {
            // Key: Category, Value: ItemList
            foreach (var data in appMan.DataMan.sortedItemData)
            {
                // TODO: Handle if there are no items set to a category. Still show button?
                TabContentParent tcp = CreateTabContentParent(category: data.Key, itemList: data.Value);
                NavButton navButton = CreateNavButton(category: data.Key, tabContentParent: tcp);

                // Hide the TabContentParent by default so it doesn't visually conflict with the other tabs.
                tcp.Hide();
                navButtonList.Add(navButton);
            }
        }

        private void SwitchInventoryTabs(NavButton nb)
        {
            // Reset the scrollRect to the top of the list
            scrollRect.verticalNormalizedPosition = 1;
            nb.ChangeState(isSelected: true);
            if (currentSelectedNavButton != null)
            {
                currentSelectedNavButton.ChangeState(isSelected: false);
                currentSelectedNavButton.TabContent.Hide();
            }
            currentSelectedNavButton = nb;
            currentSelectedNavButton.TabContent.Show();
        }

        private NavButton CreateNavButton(Utilities.InventoryCategories category, TabContentParent tabContentParent)
        {
            NavButton navBtn = Instantiate(appMan.UIMan.NavButtonPrefab, buttonParentRectTransform);
            navBtn.Setup(
                label: category.ToString(),
                inventoryCategory: category,
                tabContentParent: tabContentParent,
                cbOnClick: () =>
                {
                    Debug.Log($"nav button is null: {(navBtn == null)}");
                    SwitchInventoryTabs(navBtn);
                }
            );
            return navBtn;
        }

        private TabContentParent CreateTabContentParent(Utilities.InventoryCategories category, List<Item> itemList)
        {
            TabContentParent tcp = Instantiate(appMan.UIMan.TabContentParentPrefab, scrollContentRectTransform);
            tcp.Setup(category, itemList);
            return tcp;
        }


        private NavButton GetNavButton(Utilities.InventoryCategories category)
        {
            foreach (NavButton nb in navButtonList)
            {
                if (nb.Category.Equals(category))
                {
                    return nb;
                }
            }
            Debug.Log($"HomeView.cs GetNavButton():: No nav button of category {category} found.");
            return null;
        }


        public void UpdateBackpackItemCount(int count, int maxNumber)
        {
            backpackButton.UpdateCountText(count, maxNumber);
        }

        public void UpdateInventoryItem(Item item)
        {
            foreach (NavButton nb in navButtonList)
            {
                nb.TabContent.UpdateItem(item: item);
            }
        }

        public void Shutdown()
        {
            scrollBackground.Shutdown();

            foreach (NavButton i in navButtonList)
            {
                i.Shutdown();
            }
            navButtonList.Clear();
        }
    }
}