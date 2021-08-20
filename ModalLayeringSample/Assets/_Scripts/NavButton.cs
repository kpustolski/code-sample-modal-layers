using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace CodeSampleModalLayer
{
    [RequireComponent(typeof(Button))]
    public class NavButton : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI labelText = default;
        [SerializeField]
        private Color32 defaultColor = new Color32(255, 255, 255, 178);
        [SerializeField]
        private Color32 greenColor = new Color32(32, 101, 113, 255);
        private Button buttonScript = default;
        private TabContentParent tabContent = default;
        private Utilities.InventoryCategories invCategory = default;
        public Utilities.InventoryCategories Category { get { return invCategory; } private set { invCategory = value; } }
        public TabContentParent TabContent { get { return tabContent; } private set { tabContent = value; } }

        public void Setup(string label, Utilities.InventoryCategories inventoryCategory, TabContentParent tabContentParent, UnityAction cbOnClick)
        {
            Category = inventoryCategory;
            TabContent = tabContentParent;
            labelText.text = label;

            buttonScript = GetComponent<Button>();

            if (cbOnClick != null)
            {
                buttonScript.onClick.AddListener(cbOnClick);
            }
        }

        public void ChangeState(bool isSelected)
        {
            if(isSelected)
            {
                labelText.color = Color.white;
                buttonScript.image.color = greenColor;
            }
            else
            {
                labelText.color = greenColor;
                buttonScript.image.color = default;
            }
        }

        public void Shutdown()
        {
            buttonScript.onClick.RemoveAllListeners();
            TabContent.Shutdown();
            Destroy(gameObject);
        }
    }
}