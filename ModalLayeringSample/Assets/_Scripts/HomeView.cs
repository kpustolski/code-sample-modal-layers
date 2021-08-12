﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

namespace CodeSampleModalLayer
{
    [RequireComponent(typeof(ScrollingBackground))]
    public class HomeView : MonoBehaviour
    {
        // [SerializeField]
        // private Button button;
        [SerializeField]
        private RectTransform itemParentRectTransform = default;
        [SerializeField]
        private RectTransform buttonParentRectTransform = default;
        [SerializeField]
        private BackpackButton backpackButton = default;
        [SerializeField]
        private TextMeshProUGUI viewTitleText = default;

        private AppManager appMan = default;
        private ScrollingBackground scrollBackground = default;

        private List<SquareItemParent> squareItemParentList = new List<SquareItemParent>();
        private List<NavButton> navButtonList = new List<NavButton>();
        private const string kViewTitle = "Inventory";

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

            // CreateItemGrid();
            CreateNavButtons();
        }

        // public void CreateItemGrid()
        // {
        //     foreach (Item i in appMan.ItemDataList.data)
        //     {
        //         // Only instantiate an object if there is one owned.
        //         //? Could gray out the items that we don't have?
        //         if (i.totalOwned != 0)
        //         {
        //             SquareItem si = Instantiate(appMan.SquareItemPrefab, itemParentRectTransform);
        //             si.Setup(item: i, locationCreated: SquareItem.LocationCreated.homeView);
        //             squareItemParentList.Add(si);
        //         }
        //     }
        // }

        public void CreateNavButtons()
        {
            //TODO: Add 'All' button
            //TODO: When a category is selected, store instantiated squares in a parent prefab and enable / disable when the player
            //TODO: goes back and forth between categories. This way we can avoid continuously destroying and instantiating objects.
            foreach (Utilities.InventoryCategories category in Enum.GetValues(typeof(Utilities.InventoryCategories)))
            {
                // if (category.Equals(Utilities.InventoryCategories.None))
                // {
                //     continue;
                // }

                NavButton navBtn = Instantiate(appMan.NavButtonPrefab, buttonParentRectTransform);
                // navBtn.Setup() //TODO: Button Callback
                navButtonList.Add(navBtn);
            }
        }

        public void UpdateBackpackItemCount(int count, int maxNumber)
        {
            backpackButton.UpdateCountText(count, maxNumber);
        }

        public void Shutdown()
        {
            scrollBackground.Shutdown();

            foreach (var i in squareItemParentList)
            {
                i.Shutdown();
            }
            squareItemParentList.Clear();

            foreach (NavButton i in navButtonList)
            {
                Destroy(i.gameObject);
            }
            navButtonList.Clear();
        }
    }
}