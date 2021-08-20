using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

namespace CodeSampleModalLayer
{
    public class SquareItem : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        public enum LocationCreated
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
        private RectTransform selectOutline = default;
        [SerializeField]
        private TextMeshProUGUI amountText = default;
        [SerializeField]
        private RectTransform backpackIconPanel = default;
        [SerializeField]
        private UnityEngine.UI.Extensions.Gradient2 gradientScript = default;

        private AppManager appMan = default;
        private Item mItem = default;
        private LocationCreated mLocationCreated = default;

        public Item ItemAssigned { get { return mItem; } }

        public void Setup(Item item, LocationCreated locationCreated)
        {
            appMan = AppManager.Instance;
            mItem = item;
            mLocationCreated = locationCreated;
            backpackIconPanel.gameObject.SetActive(false); //Off by default
            itemImage.preserveAspect = true;

            if(gradientScript != null)
            {
                gradientScript.EffectGradient = appMan.AppDataObject.GetCategoryGradient(item.category);
            }

            itemImage.sprite = appMan.AppDataObject.GetItemIcon(mItem.id);

            UpdateState();
            selectOutline.gameObject.SetActive(false);
            //button.onClick.AddListener(OpenInfoPopupCallback);
        
        }

        public void OpenInfoPopupCallback()
        {
            //Create an info modal for the item
            MessageBox.CreateItemInfoModal(item: mItem, locationCreated: mLocationCreated);
        }

        public void UpdateState()
        {
            if (mLocationCreated == LocationCreated.homeView)
            {
                backpackIconPanel.gameObject.SetActive((mItem.AmountInBackpack > 0));
            }
            UpdateAmountText();
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

        public void OnPointerDown(PointerEventData pointerEventData)
        {
            OpenInfoPopupCallback();
            selectOutline.gameObject.SetActive(true);
        }
        public void OnPointerUp(PointerEventData pointerEventData)
        {
            selectOutline.gameObject.SetActive(false);
        }

        public void Shutdown()
        {
            button.onClick.RemoveAllListeners();
            Destroy(gameObject);
        }
    }
}