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

        public void UpdateState(bool isBtnInteractable)
        {
            UpdateAmountText();
            button.interactable = isBtnInteractable;
        }

        public void UpdateAmountText()
        {
            if (mItem.totalOwned <= 1)
            {
                amountPanel.gameObject.SetActive(false);
            }
            else
            {
                amountPanel.gameObject.SetActive(true);
                amountText.text = mItem.AmountInInventory.ToString();
            }
        }

        public void Shutdown()
        {
            button.onClick.RemoveAllListeners();
            Destroy(gameObject);
        }
    }
}