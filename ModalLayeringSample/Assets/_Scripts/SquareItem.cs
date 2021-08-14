using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace CodeSampleModalLayer
{
    public class SquareItem : MonoBehaviour
    {
        public enum LocationCreated //TODO: Better name?
        {
            homeView = 0,
            backpackModal = 1
        }

        [SerializeField]
        private Button button = default;
        [SerializeField]
        private Image itemImage = default;
        [SerializeField]
        private RectTransform amountPanel = default;
        [SerializeField]
        private TextMeshProUGUI amountText = default; // Amount owned

        private AppManager appMan = default;
        private Item mItem = default;
        private LocationCreated mLocationCreated = default;

        public Item ItemAssigned { get { return mItem; } }

        public void Setup(Item item, LocationCreated locationCreated)
        {
            appMan = AppManager.Instance;
            mItem = item;
            mLocationCreated = locationCreated;

            itemImage.sprite = appMan.AppDataObject.GetItemIconByItemType(mItem.type);

            UpdateAmountText();

            button.onClick.AddListener(OpenInfoPopupCallback);
        }

        public void OpenInfoPopupCallback()
        {
            //Create an info modal for the item
            MessageBox.CreateInfoModal(item: mItem, locationCreated: mLocationCreated);
        }

        //TODO: Remove the isBtnInteractive parameter
        public void UpdateState(bool isBtnInteractable)
        {
            UpdateAmountText();
            button.interactable = isBtnInteractable;
        }

        public void UpdateAmountText()
        {
            // Use a specific amount variable based on where the SquareItem was created.
            // If instantiated in the backpack modal, the amount text represents the number of the item in the backpack
            // If instantiated in the inventory, the amount text represents the number of the item in the inventory
            int amountInView = (mLocationCreated == LocationCreated.backpackModal) ? mItem.AmountInBackpack : mItem.AmountInInventory;

            if (amountInView <= 1)
            {
                amountPanel.gameObject.SetActive(false);
            }
            else
            {
                amountPanel.gameObject.SetActive(true);
                amountText.text = amountInView.ToString();
            }
        }

        public void Shutdown()
        {
            button.onClick.RemoveAllListeners();
            Destroy(gameObject);
        }
    }
}