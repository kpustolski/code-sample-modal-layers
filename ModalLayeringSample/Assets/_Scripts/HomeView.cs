using System.Collections;
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
        private RectTransform itemParentRectTransform;
        [SerializeField]
        private RectTransform buttonParentRectTransform;
        [SerializeField]
        private TextMeshProUGUI backpackCountText; //TODO: Create backpack button class and put this in there.


        private AppManager appMan = default;
        private ScrollingBackground scrollBackground = default;

        private List<SquareItem> squareItemList = new List<SquareItem>();
        private List<Button> navButtonList = new List<Button>();

        public void Setup()
        {
            appMan = AppManager.Instance;
            scrollBackground = GetComponent<ScrollingBackground>();

            if (scrollBackground != null)
            {
                scrollBackground.Initialize();
            }

            // button.onClick.AddListener(CreateInfoModal);

            CreateItemGrid();
            CreateNavButtons();
        }

        public void CreateItemGrid()
        {
            foreach (Item i in appMan.ItemDataList.data)
            {
                // Only instantiate an object if there is one owned.
                //? Could gray out the items that we don't have?
                if (i.totalOwned != 0)
                {
                    SquareItem si = Instantiate(appMan.SquareItemPrefab, itemParentRectTransform);
                    si.Setup(item: i);
                    squareItemList.Add(si);
                }
            }
        }

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

                Button navBtn = Instantiate(appMan.NavButtonPrefab, buttonParentRectTransform);
                // navBtn.Setup() //TODO: Button Callback
                navButtonList.Add(navBtn);
            }
        }

        public void UpdateBackpackItemCount(int count, int maxNumber)
        {
            backpackCountText.text = $"{count}/{maxNumber}";
        }

        public void Shutdown()
        {
            //button.onClick.RemoveAllListeners();
            scrollBackground.Shutdown();

            foreach (SquareItem i in squareItemList)
            {
                i.Shutdown();
            }
            squareItemList.Clear();

            foreach (Button i in navButtonList)
            {
                // i.Shutdown();
                Destroy(i.gameObject);
            }
            navButtonList.Clear();
        }
    }
}