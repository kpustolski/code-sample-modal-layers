using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using UnityEngine.UI;

namespace CodeSampleModalLayer
{
    public class BackpackButton : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI countText = default;

        private Button button = default;

        private const string kCountString = "{0}/{1}";

        public void Setup()
        {
            button = GetComponent<Button>();
            button.onClick.AddListener(OpenBackpackModal);
        }

        public void OpenBackpackModal()
        {
            MessageBox.CreateBackpackModal();
        }

        public void UpdateCountText(int amount, int maxAmount)
        {
            countText.text = string.Format(kCountString, amount, maxAmount);
        }

        public void Shutdown()
        {
            button.onClick.RemoveAllListeners();
        }
    }
}