using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;

namespace CodeSampleModalLayer
{
    public class BackpackModal : ModalBase, IModalLayer
    {
        [Header("BackpackModal variables")]
        [Space(5)]
        [SerializeField]
        private TextMeshProUGUI countText = default;
        [SerializeField]
        private Button closeButton = default;
        [SerializeField]
        private Button clearAllButton = default;
        [SerializeField]
        private TextMeshProUGUI titleText = default;
        [SerializeField]
        private TextMeshProUGUI emptyText = default;
        [SerializeField]
        private RectTransform itemParentTransform = default;

        private string modalId = default;
        private List<SquareItem> squareItemsList = new List<SquareItem>();
        // Stores the number of items in the backpack. It helps determine if we need to reset the modal UI.
        private int currentItemCount = default;

		public override void Initialize()
		{
			base.Initialize();
		}

        public void Setup()
        {
            Initialize();
            
            // Add to the modal layer list
            appMan.UIMan.AddToModalLayerList(this as IModalLayer);

            titleText.text = appMan.DataMan.GetCopyText("backpackmodal.title");
            emptyText.text = appMan.DataMan.GetCopyText("backpackmodal.isempty");
            closeButton.onClick.AddListener(Shutdown);
            clearAllButton.onClick.AddListener(ClearAllItemsCallback);
            currentItemCount = appMan.GetTotalItemsInBackpack();
            CreateBackpackContents();

            // Empty text appears if there is nothing inside the backpack.
            emptyText.gameObject.SetActive(currentItemCount == 0);
            //Disable the clearAllButton if nothing is in the backpack
            clearAllButton.interactable = (currentItemCount != 0);

            // ShowAnimated is located in the base class
            ShowAnimated();
        }

        public void Shutdown()
        {
            appMan.UIMan.RemoveFromModalLayerList(layer: this as IModalLayer, cbOnRemovalFromList: OnRemovalFromList);
        }

        private void OnRemovalFromList()
        {
            closeButton.onClick.RemoveAllListeners();
            ClearSquareItemsList();

            Destroy(gameObject);
        }

        private void Reset()
        {
            ClearSquareItemsList();
            CreateBackpackContents();
            currentItemCount = appMan.GetTotalItemsInBackpack();
            emptyText.gameObject.SetActive(currentItemCount == 0);
            //Disable the clearAllButton if nothing is in the backpack
            clearAllButton.interactable = (currentItemCount != 0);
        }
        
        private void UpdateCountText(int amount, int maxAmount)
        {
            countText.text = string.Format(appMan.DataMan.GetCopyText("backpackmodal.itemcount"), amount, maxAmount);
            if(amount.Equals(maxAmount))
            {
                countText.text = appMan.DataMan.GetCopyText("backpack.full");
            }
        }
        
        private void CreateBackpackContents()
        {
            foreach (Item i in appMan.GetBackpackItemList())
            {
                SquareItem si = Instantiate(appMan.UIMan.SquareItemPrefab, itemParentTransform);
                si.Setup(item: i, locationCreated: SquareItem.LocationCreated.backpackModal);
                squareItemsList.Add(si);
            }
            UpdateCountText(amount: appMan.GetTotalItemsInBackpack(), maxAmount: appMan.GetBackpackMaxItemCount());
        }

        private void ClearSquareItemsList()
        {
            foreach (var i in squareItemsList)
            {
                i.Shutdown();
            }
            squareItemsList.Clear();
        }

        private void ClearAllItemsCallback()
        {
            MessageBox.CreateInfoModal(
                title: appMan.DataMan.GetCopyText("infomodal.title.areyousure"),
                description: appMan.DataMan.GetCopyText("infomodal.clearbackpack.desc"),
                cbOnActionButtonClick: () =>
                {
                    appMan.EmptyBackpack();
                    Reset();
                });
        }


#region ModalLayer Functions

        public void ShowLayer()
        {
            // If the currentItemCount variable does not match the current number of items in the backpack,
            // then we know that the player removed an item. We need to update the UI to reflect the change.
            if(currentItemCount != appMan.GetTotalItemsInBackpack())
            {
                Reset();
            }
            ShowAnimated();
        }

        public void HideLayer(UnityAction cbOnHideLayer)
        {
            HideAnimated(cbOnAnimationComplete: cbOnHideLayer);
        }

        public string GetId()
        {
            return modalId;
        }

        public void AssignId(int layerIndex)
        {
            modalId = $"BackpackModal_{layerIndex}";
        }

 #endregion
    }
}