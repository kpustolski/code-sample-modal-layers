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
        [SerializeField]
        private RectTransform itemParentTransform = default;

        private string modalId = default;
        private AppManager appMan = default;
        private List<SquareItem> squareItemsList = new List<SquareItem>();

        public void Setup()
        {
            appMan = AppManager.Instance;
            // Add to the modal layer list
            appMan.AddToModalLayerList(this as IModalLayer);

            descriptionText.text = "This is a description";
            closeButton.onClick.AddListener(Shutdown);
            CreateBackpackContents();
        }

        public void Shutdown()
        {
            closeButton.onClick.RemoveAllListeners();

            foreach (var i in squareItemsList)
            {
                i.Shutdown();
            }
            squareItemsList.Clear();

            // Remove from modal layer list
            appMan.RemoveFromModalLayerList(this as IModalLayer);

            Destroy(gameObject);
        }

        public void CreateBackpackContents()
        {
            foreach (Item i in appMan.PlayerBackpack.ItemList)
            {
                if (i.totalOwned != 0)
                {
                    SquareItem si = Instantiate(appMan.SquareItemPrefab, itemParentTransform);
                    si.Setup(item: i, locationCreated: SquareItem.LocationCreated.backpackModal);
                    squareItemsList.Add(si);
                }
            }
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