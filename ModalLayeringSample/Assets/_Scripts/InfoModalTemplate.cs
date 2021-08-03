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
        private TextMeshProUGUI descriptionText = default;

        private string modalId = default;
        private AppManager appMan = default;

        public void Setup()
        {
            appMan = AppManager.Instance;
            descriptionText.text = "This is some text";
            closeButton.onClick.AddListener(Shutdown);

            // Add to the modal layer list
            appMan.AddToModalLayerList(this as IModalLayer);
        }

        public void Shutdown()
        {
            closeButton.onClick.RemoveAllListeners();
            appMan.RemoveFromModalLayerList(this as IModalLayer);
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