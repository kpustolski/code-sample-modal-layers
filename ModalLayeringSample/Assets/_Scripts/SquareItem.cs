using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace CodeSampleModalLayer
{
    public class SquareItem : MonoBehaviour
    {
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

        public void Setup(Item item)
        {
            appMan = AppManager.Instance;
            mItem = item;

            itemImage.sprite = appMan.AppDataObject.GetItemIconByItemType(mItem.type);

            amountPanel.gameObject.SetActive(mItem.totalOwned > 1);
            if (amountPanel.gameObject.activeSelf)
            {
                amountText.text = mItem.totalOwned.ToString();
            }

            button.onClick.AddListener(OpenInfoPopupCallback);
        }

        public void OpenInfoPopupCallback()
        {
            //Create an info modal for the item
            MessageBox.CreateInfoModal(description: mItem.id);
        }

        public void Shutdown()
        {
            button.onClick.RemoveAllListeners();
            Destroy(gameObject);
        }
    }
}