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
        // [SerializeField]
        // private Button button;
        [SerializeField]
        private RectTransform itemParentRectTransform = default; //TODO: Change name
        [SerializeField]
        private RectTransform buttonParentRectTransform = default;
        [SerializeField]
        private BackpackButton backpackButton = default;
        [SerializeField]
        private TextMeshProUGUI viewTitleText = default;

        private AppManager appMan = default;
        private ScrollingBackground scrollBackground = default;

        // TODO: Figure out better name. Should I use a dictionary to store the NavButtons and SquareItemParents instead?
        // private Dictionary<NavButton, SquareItemParent> foo = new Dictionary<NavButton, SquareItemParent>();
        private List<SquareItemParent> squareItemParentList = new List<SquareItemParent>();
        private List<NavButton> navButtonList = new List<NavButton>();

        private const string kViewTitle = "Inventory";
        private SquareItemParent currentTabOpen = default;

        public void Setup()
        {
            appMan = AppManager.Instance;
            scrollBackground = GetComponent<ScrollingBackground>();
            viewTitleText.text = kViewTitle;

            if (scrollBackground != null)
            {
                scrollBackground.Initialize();
            }

            backpackButton.Setup();
            SetupNavigation();
            SwitchInventoryTabs(GetSquareItemParentButton(Utilities.InventoryCategories.All));
        }

        private void SetupNavigation()
        {
            // Key: Category, Value: ItemList
            foreach (var data in appMan.sortedItemData)
            {
                // TODO: Handle if there are no items set to a category. Still show button?
                SquareItemParent sip = CreateSquareItemParent(category: data.Key, itemList: data.Value);
                NavButton navButton = CreateNavButton(
                    category: data.Key,
                    cbOnClick: () =>
                    {
                        SwitchInventoryTabs(sip);
                    });

                // Hide the SquareItemParent by default so it doesn't visually conflict with the other tabs.
                sip.Hide();
                navButtonList.Add(navButton);
                squareItemParentList.Add(sip);
            }
        }

        private void SwitchInventoryTabs(SquareItemParent sip)
        {
            if (currentTabOpen != null)
            {
                currentTabOpen.Hide();
            }
            currentTabOpen = sip;
            currentTabOpen.Show();
        }

        private NavButton CreateNavButton(Utilities.InventoryCategories category, UnityAction cbOnClick)
        {
            NavButton navBtn = Instantiate(appMan.NavButtonPrefab, buttonParentRectTransform);
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

        // TODO: do I need to pass in the category?
        private SquareItemParent CreateSquareItemParent(Utilities.InventoryCategories category, List<Item> itemList)
        {
            SquareItemParent sip = Instantiate(appMan.SquareItemParentPrefab, itemParentRectTransform);
            sip.Setup(category, itemList);
            return sip;
        }

        //TODO: Rename?
        private SquareItemParent GetSquareItemParentButton(Utilities.InventoryCategories category)
        {
            foreach (SquareItemParent sip in squareItemParentList)
            {
                if (sip.Category.Equals(category))
                {
                    return sip;
                }
            }
            Debug.Log($"HomeView.cs GetNavButtonByCategory():: No SquareItemParent of category {category} found.");
            return null;
        }


        public void UpdateBackpackItemCount(int count, int maxNumber)
        {
            backpackButton.UpdateCountText(count, maxNumber);
        }

        public void UpdateInventoryItem(Item item)
        {
            foreach (SquareItemParent sip in squareItemParentList)
            {
                sip.UpdateItem(item: item);
            }
        }

        public void Shutdown()
        {
            scrollBackground.Shutdown();

            // foreach (var i in squareItemParentList)
            // {
            //     i.Shutdown();
            // }
            // squareItemParentList.Clear();

            // foreach (NavButton i in navButtonList)
            // {
            //     Destroy(i.gameObject);
            // }
            // navButtonList.Clear();
        }
    }
}