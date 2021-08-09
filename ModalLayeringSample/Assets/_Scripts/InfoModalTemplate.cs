using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace CodeSampleModalLayer
{
    public class InfoModalTemplate : MonoBehaviour, IModalLayer
    {
        [SerializeField]
        private Button closeButton = default;
        [SerializeField]
        private Button addToBagButton = default;
        [SerializeField]
        private TextMeshProUGUI descriptionText = default;

        private string modalId = default;
        private AppManager appMan = default;
        private Item mItem = default;

        public void Setup(Item item)
        {
            appMan = AppManager.Instance;
            mItem = item;
            // Add to the modal layer list
            appMan.AddToModalLayerList(this as IModalLayer);

            descriptionText.text = string.Format(mItem.id, modalId);
            closeButton.onClick.AddListener(Shutdown);
            addToBagButton.onClick.AddListener(AddToBagCallback);

        }

        public void Shutdown()
        {
            closeButton.onClick.RemoveAllListeners();
            addToBagButton.onClick.RemoveAllListeners();

            // Remove from modal layer list
            appMan.RemoveFromModalLayerList(this as IModalLayer);
            Destroy(gameObject);
        }

        public void AddToBagCallback()
        {
            appMan.AddItemToBackpack(mItem);
            Shutdown();
        }

        #region ModalLayer Functions

        public void ShowLayer()
        {
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
            modalId = $"InfoModal_{layerIndex}";
        }

        #endregion
    }
}