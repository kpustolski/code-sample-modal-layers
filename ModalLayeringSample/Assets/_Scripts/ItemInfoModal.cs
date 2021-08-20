using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace CodeSampleModalLayer
{
	public class ItemInfoModal : ModalBase, IModalLayer
	{
		[Header("InfoModalTemplate Variables")]
		[Space(5)]
		[SerializeField]
		private Button closeButton = default;
		[SerializeField]
		private Button addToBagButton = default;
		[SerializeField]
		private TextMeshProUGUI addToBagButtonText = default;
		[SerializeField]
		private Button removeFromBagButton = default;
		[SerializeField]
		private TextMeshProUGUI titleText = default;
		[SerializeField]
		private Image itemImage = default;
		[SerializeField]
		private TextMeshProUGUI amountInBackpackText = default;
		[SerializeField]
        private UnityEngine.UI.Extensions.Gradient2 gradientScript = default;

		private string modalId = default;
		private Item mItem = default;
		private SquareItem.LocationCreated mLocationCreated = default;
		// amount in backback / total amount owned.
		private string amountTestFormat = default;
		private string bagIsFullString = default;

		public override void Initialize()
		{
			base.Initialize();
            itemImage.preserveAspect = true;
		}

		public void Setup(Item item, SquareItem.LocationCreated locationCreated)
		{
			Initialize();
			mItem = item;
			mLocationCreated = locationCreated;
			amountTestFormat = appMan.DataMan.GetCopyText("itemcount");
			bagIsFullString = appMan.DataMan.GetCopyText("backpack.full");

			// Add to bag button is enabled when the square item button is selected in the home view
			addToBagButton.gameObject.SetActive(mLocationCreated == SquareItem.LocationCreated.homeView);
			// The remove from bag button is enabled when the square item button is selected in the backpack modal 
			removeFromBagButton.gameObject.SetActive(mLocationCreated == SquareItem.LocationCreated.backpackModal);

			// Add to the modal layer list
			//AppMan is set in base class
			appMan.UIMan.AddToModalLayerList(this as IModalLayer);

			titleText.text = mItem.name;
			itemImage.sprite = appMan.AppDataObject.GetItemIcon(mItem.id);
			amountInBackpackText.text = string.Format(amountTestFormat, mItem.AmountInBackpack, mItem.totalOwned);
			addToBagButtonText.text = appMan.DataMan.GetCopyText("action.addtobag");

			if(gradientScript != null)
            {
                gradientScript.EffectGradient = appMan.AppDataObject.GetCategoryGradient(item.category);
            }

			closeButton.onClick.AddListener(Shutdown);
			addToBagButton.onClick.AddListener(AddToBagCallback);
			removeFromBagButton.onClick.AddListener(RemoveFromBagCallback);

			// Disable the add to bag button if all of the items are already in the backpack
			addToBagButton.interactable = (mItem.AmountInInventory != 0);

			if(appMan.IsBackpackFull())
			{
				addToBagButtonText.text = bagIsFullString;
				addToBagButton.interactable = false;
			}

			// ShowAnimated is located in the base class
			ShowAnimated();
		}
		public void Shutdown()
		{
            // HideAnimated is located in the base class
            HideAnimated(cbOnAnimationComplete: ShutdownOnAnimationCompleteCallback);
		}

		private void ShutdownOnAnimationCompleteCallback()
		{
			// Turn off button interactables to avoid double clicks
			closeButton.interactable = false;
			addToBagButton.interactable = false;
			removeFromBagButton.interactable = false;

			closeButton.onClick.RemoveAllListeners();
			addToBagButton.onClick.RemoveAllListeners();
			removeFromBagButton.onClick.RemoveAllListeners();

			// Remove from modal layer list
			appMan.UIMan.RemoveFromModalLayerList(this as IModalLayer);
			Destroy(gameObject);
		}

		private void RemoveFromBagCallback()
		{
			appMan.RemoveItemFromBackpack(item: mItem, itemAmountDifference: 1);
            amountInBackpackText.text = string.Format(amountTestFormat, mItem.AmountInBackpack, mItem.totalOwned);
			if(mItem.AmountInBackpack == 0)
			{
				Shutdown();
			}
		}

        private void AddToBagCallback()
        {
            appMan.AddItemToBackpack(item: mItem, itemAmountDifference: 1);

            //Are all instances of this item in our backpack? If so, disable the add to bag button.
            addToBagButton.interactable = (mItem.AmountInInventory != 0);
            amountInBackpackText.text = string.Format(amountTestFormat, mItem.AmountInBackpack, mItem.totalOwned);

            if (appMan.IsBackpackFull())
            {
                addToBagButtonText.text = bagIsFullString;
                addToBagButton.interactable = false;
            }
        }

#region ModalLayer Functions

		public void ShowLayer()
		{
			ShowAnimated();
		}
		public void HideLayer()
		{
			HideAnimated(cbOnAnimationComplete: null);
		}
		public string GetId()
		{
			return modalId;
		}

		public void AssignId(int layerIndex)
		{
			modalId = $"ItemInfoModal_{layerIndex}";
		}

#endregion
	}
}