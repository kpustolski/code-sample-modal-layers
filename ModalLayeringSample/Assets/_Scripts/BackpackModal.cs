using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

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
        private TextMeshProUGUI titleText = default;
        [SerializeField]
        private RectTransform itemParentTransform = default;

        private string modalId = default;
        private List<SquareItem> squareItemsList = new List<SquareItem>();
        private const string kModalTitle = "My Backpack";
        private const string kCountString = "Space Left: {0}/{1}";
        // Stores the number of items in the backpack. It helps determine if we need to reset the modal UI.
        private int currentItemCount = default;

		public override void Initialize()
		{
			base.Initialize();
		}

        public void Setup()
        {
            Initialize();
            // ShowAnimated is located in the base class
            ShowAnimated(cbBeforeAnimationStart: SetupBeforeAnimationCallback);
        }

        public void Shutdown()
        {
            // HideAnimated is located in the base class
            HideAnimated(cbOnAnimationComplete: ShutdownOnAnimationCompleteCallback);
        }

        private void SetupBeforeAnimationCallback()
        {
            // Add to the modal layer list
            appMan.UIMan.AddToModalLayerList(this as IModalLayer);

            titleText.text = kModalTitle;
            closeButton.onClick.AddListener(Shutdown);
            currentItemCount = appMan.GetTotalItemsInBackpack();
            CreateBackpackContents();
        }

        private void ShutdownOnAnimationCompleteCallback()
        {
            closeButton.onClick.RemoveAllListeners();
            ClearSquareItemsList();

            // Remove from modal layer list
            appMan.UIMan.RemoveFromModalLayerList(this as IModalLayer);

            Destroy(gameObject);
        }

        private void Reset()
        {
            ClearSquareItemsList();
            CreateBackpackContents();
            currentItemCount = appMan.GetTotalItemsInBackpack();
        }
        
        //TODO: This is also used on the backpack button. Should I make the count text its own prefab and class?
        public void UpdateCountText(int amount, int maxAmount)
        {
            countText.text = string.Format(kCountString, amount, maxAmount);
            if(amount.Equals(maxAmount))
            {
                countText.text = "Full!";
            }
        }
        
        private void CreateBackpackContents()
        {
            foreach (Item i in appMan.PlayerBackpack.ItemList)
            {
                SquareItem si = Instantiate(appMan.UIMan.SquareItemPrefab, itemParentTransform);
                si.Setup(item: i, locationCreated: SquareItem.LocationCreated.backpackModal);
                squareItemsList.Add(si);
            }
            UpdateCountText(amount: appMan.PlayerBackpack.GetTotalItemsInBackpack(), maxAmount: appMan.PlayerBackpack.MaxTotalItems);
        }

        private void ClearSquareItemsList()
        {
            foreach (var i in squareItemsList)
            {
                i.Shutdown();
            }
            squareItemsList.Clear();
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
            this.gameObject.SetActive(true);
        }
        public void HideLayer()
        {
            this.gameObject.SetActive(false);
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