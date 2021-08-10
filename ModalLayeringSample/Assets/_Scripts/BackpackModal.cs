using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace CodeSampleModalLayer
{
    public class BackpackModal : MonoBehaviour, IModalLayer
    {
        [SerializeField]
        private Button closeButton = default;
        [SerializeField]
        private TextMeshProUGUI descriptionText = default;

        private string modalId = default;
        private AppManager appMan = default;

        public void Setup()
        {
            appMan = AppManager.Instance;
            // Add to the modal layer list
            appMan.AddToModalLayerList(this as IModalLayer);

            descriptionText.text = "This is a description";
            closeButton.onClick.AddListener(Shutdown);
        }

        public void Shutdown()
        {
            closeButton.onClick.RemoveAllListeners();

            // Remove from modal layer list
            appMan.RemoveFromModalLayerList(this as IModalLayer);
            Destroy(gameObject);
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
            modalId = $"BackpackModal_{layerIndex}";
        }

        #endregion
    }
}