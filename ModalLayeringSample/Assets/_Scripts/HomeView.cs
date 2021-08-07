using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CodeSampleModalLayer
{
    [RequireComponent(typeof(ScrollingBackground))]
    public class HomeView : MonoBehaviour
    {
        [SerializeField]
        private Button button;
        [SerializeField]
        private RectTransform itemParentRectTransform;
        [SerializeField]
        private RectTransform buttonParentRectTransform;

        private AppManager appMan = default;
        private ScrollingBackground scrollBackground = default;

        private List<SquareItem> squareItemList = new List<SquareItem>();

        public void Setup()
        {
            appMan = AppManager.Instance;
            scrollBackground = GetComponent<ScrollingBackground>();

            if (scrollBackground != null)
            {
                scrollBackground.Initialize();
            }

            button.onClick.AddListener(CreateInfoModal);

            CreateItemGrid();
        }

        public void CreateItemGrid()
        {
            foreach (Item i in appMan.ItemDataList.data)
            {
                SquareItem si = Instantiate(appMan.SquareItemPrefab, itemParentRectTransform);
                si.Setup(item: i);
                squareItemList.Add(si);
            }
        }

        public void CreateNavButtons()
        {

        }

        public void CreateInfoModal()
        {
            InfoModalTemplate m = Instantiate(appMan.InfoModalTemplatePrefab, appMan.DialogParent);
            m.Setup(descText: "This is modal layer: {0}");
        }

        public void Shutdown()
        {
            button.onClick.RemoveAllListeners();
            scrollBackground.Shutdown();

            foreach (SquareItem i in squareItemList)
            {
                i.Shutdown();
            }
            squareItemList.Clear();
        }
    }
}