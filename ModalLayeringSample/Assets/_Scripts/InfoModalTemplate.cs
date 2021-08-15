﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace CodeSampleModalLayer
{
	public class InfoModalTemplate : ModalBase, IModalLayer
	{
		[Header("InfoModalTemplate Variables")]
		[Space(5)]
		[SerializeField]
		private Button closeButton = default;
		[SerializeField]
		private Button addToBagButton = default;
		[SerializeField]
		private Button removeFromBagButton = default;
		[SerializeField]
		private TextMeshProUGUI titleText = default;
		[SerializeField]
		private Image itemImage = default;
		[SerializeField]
		private TextMeshProUGUI amountInBackpackText = default;

		private string modalId = default;
		private Item mItem = default;
		private SquareItem.LocationCreated mLocationCreated = default;
		// amount in backback / total amount owned.
		private string amountTestFormat = "{0}/{1}";

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
			//TODO: May need to rethink this logic
			// Add to bag button is enabled when the square item button is selected in the home view
			addToBagButton.gameObject.SetActive(mLocationCreated == SquareItem.LocationCreated.homeView);
			// The remove from bag button is enabled when the square item button is selected in the backpack modal 
			removeFromBagButton.gameObject.SetActive(mLocationCreated == SquareItem.LocationCreated.backpackModal);

			// Add to the modal layer list
			//AppMan is set in base class
			appMan.UIMan.AddToModalLayerList(this as IModalLayer);

			titleText.text = mItem.id;
			itemImage.sprite = appMan.AppDataObject.GetItemIconByItemType(mItem.type);
			amountInBackpackText.text = string.Format(amountTestFormat, mItem.AmountInBackpack, mItem.totalOwned);

			closeButton.onClick.AddListener(Shutdown);
			addToBagButton.onClick.AddListener(AddToBagCallback);
			removeFromBagButton.onClick.AddListener(RemoveFromBagCallback);

			// Disable the add to bag button if there all of the items are already in the backpack
			// AKA the mItem.AmountInBackpack == mItem.totalOwned
			if (mItem.AmountInInventory == 0)
			{
				addToBagButton.interactable = false;
			}
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
			Destroy(gameObject); //TODO: Throws a dotween warning. How can I properly destroy the game object after the sequence is done?
		}

		private void RemoveFromBagCallback()
		{
			appMan.RemoveItemFromBackpack(mItem);
			Shutdown();
		}

		private void AddToBagCallback()
		{
			appMan.AddItemToBackpack(mItem);
			Shutdown();
		}

#region ModalLayer Functions

		public void ShowLayer()
		{
			ShowAnimated(cbBeforeAnimationStart: null);
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
			modalId = $"InfoModal_{layerIndex}";
		}

#endregion
	}
}