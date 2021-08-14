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
        private Button removeFromBagButton = default;
        [SerializeField]
        private TextMeshProUGUI descriptionText = default;

        private string modalId = default;
        private AppManager appMan = default;
        private Item mItem = default;


        public void Setup(Item item, SquareItem.LocationCreated locationCreated)
        {
            appMan = AppManager.Instance;
            mItem = item;

            //TODO: May need to rethink this logic
            // Add to bag button is enabled when the square item button is selected in the home view
            addToBagButton.gameObject.SetActive(locationCreated == SquareItem.LocationCreated.homeView);
            // The remove from bag button is enabled when the square item button is selected in the backpack modal 
            removeFromBagButton.gameObject.SetActive(locationCreated == SquareItem.LocationCreated.backpackModal);

            // Add to the modal layer list
            appMan.UIMan.AddToModalLayerList(this as IModalLayer);

            descriptionText.text = string.Format(mItem.id, modalId);
            closeButton.onClick.AddListener(Shutdown);
            addToBagButton.onClick.AddListener(AddToBagCallback);
            removeFromBagButton.onClick.AddListener(RemoveFromBagCallback);
        }

        public void Shutdown()
        {
            closeButton.onClick.RemoveAllListeners();
            addToBagButton.onClick.RemoveAllListeners();
            removeFromBagButton.onClick.RemoveAllListeners();

            // Remove from modal layer list
            appMan.UIMan.RemoveFromModalLayerList(this as IModalLayer);
            Destroy(gameObject);
        }

        public void RemoveFromBagCallback()
        {
            appMan.RemoveItemFromBackpack(mItem);
            Debug.Log("Item removed from backpack");
            Shutdown();
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