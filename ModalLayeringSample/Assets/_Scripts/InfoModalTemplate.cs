using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace CodeSampleModalLayer
{
    public class InfoModalTemplate : MonoBehaviour
    {
        [SerializeField]
        private Button closeButton = default;
        [SerializeField]
        private TextMeshProUGUI descriptionText = default;

        public void Setup()
        {
            descriptionText.text = "This is some text";
            closeButton.onClick.AddListener(Shutdown);
        }

        public void Shutdown()
        {
            closeButton.onClick.RemoveAllListeners();
        }
    }
}