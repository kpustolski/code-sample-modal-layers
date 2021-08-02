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

        private const string modalId = "InfoModal";

        public void Setup()
        {
            descriptionText.text = "This is some text";
            closeButton.onClick.AddListener(Shutdown);
        }

        public void Shutdown()
        {
            closeButton.onClick.RemoveAllListeners();
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

        #endregion
    }
}