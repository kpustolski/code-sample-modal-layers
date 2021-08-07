using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace CodeSampleModalLayer
{
    public class SquareItem : MonoBehaviour
    {
        // [SerializeField]
        // private Button button = default;
        [SerializeField]
        private Image itemImage = default;
        [SerializeField]
        private RectTransform amountPanel = default;
        [SerializeField]
        private TextMeshProUGUI amountText = default; // Amount owned

        private AppManager appMan = default;

        public void Setup(Item item)
        {
            appMan = AppManager.Instance;

            itemImage.sprite = appMan.AppDataObject.GetItemIconByItemType(item.type);

            amountPanel.gameObject.SetActive(item.totalOwned > 1);
            if (amountPanel.gameObject.activeSelf)
            {
                amountText.text = item.ToString();
            }

            // button.onClick.AddListener(OpenInfoPopupCallback);
        }

        public void OpenInfoPopupCallback()
        {
            // Do something!
        }

        public void Shutdown()
        {
            // button.onClick.RemoveAllListeners();
        }
    }
}