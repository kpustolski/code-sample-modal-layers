using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;

namespace CodeSampleModalLayer
{
    [RequireComponent(typeof(Button))]
    public class NavButton : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI labelText = default;
        private Button buttonScript = default;
        private Utilities.InventoryCategories invCategory = default;
        public Utilities.InventoryCategories Category { get { return invCategory; } private set { invCategory = value; } }

        public void Setup(string label, Utilities.InventoryCategories inventoryCategory, UnityAction cbOnClick)
        {
            buttonScript = GetComponent<Button>();
            Category = inventoryCategory;

            labelText.text = label;

            if (cbOnClick != null)
            {
                buttonScript.onClick.AddListener(cbOnClick);
            }
        }

        public void Shutdown()
        {
            buttonScript.onClick.RemoveAllListeners();
        }
    }
}