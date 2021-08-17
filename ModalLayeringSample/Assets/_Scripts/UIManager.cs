using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace CodeSampleModalLayer
{
    public class UIManager : MonoBehaviour
    {
        [Header("Prefabs")]
        [Space(5)]
        [SerializeField]
        private ItemInfoModal itemInfoModalPrefab = default;
        [SerializeField]
        private BackpackModal backpackModalPrefab = default;
        [SerializeField]
        private SquareItem squareItemPrefab = default;
        [SerializeField]
        private NavButton navButtonPrefab = default;
        [SerializeField]
        private TabContentParent tabContentParentPrefab = default;

        [Header("RectTransform")]
        [Space(5)]
        [SerializeField]
        private RectTransform dialogParent = default;
        [SerializeField]
        private CanvasGroup dialogOverlayCanvasGroup = default;

        public ItemInfoModal ItemInfoModalPrefab { get { return itemInfoModalPrefab; } }
        public BackpackModal BackpackModalPrefab { get { return backpackModalPrefab; } }
        public SquareItem SquareItemPrefab { get { return squareItemPrefab; } }
        public TabContentParent TabContentParentPrefab { get { return tabContentParentPrefab; } }
        public NavButton NavButtonPrefab { get { return navButtonPrefab; } }
        public RectTransform DialogParent { get { return dialogParent; } }

        private List<IModalLayer> modalLayerList = new List<IModalLayer>();

        // Animation Variablea
        private const float overlayFadeDuration = 0.2f;

        public void Initialize()
        {
            // init code here
            dialogOverlayCanvasGroup.alpha = 0;
            dialogOverlayCanvasGroup.gameObject.SetActive(false);
        }

        public void AddToModalLayerList(IModalLayer layer)
        {
            if (layer == null)
            {
                //ERROR
                Debug.Log("UIManager.cs AddToModalLayerList():: Layer passed in is null");
                return;
            }

            // If a modal is already in the list, hide that one before showing the new one.
            if (modalLayerList.Count >= 1)
            {
                modalLayerList[modalLayerList.Count - 1].HideLayer();
            }

            // layer.ShowLayer();
            if (!modalLayerList.Contains(layer))
            {
                modalLayerList.Add(layer);
                int layerIndex = modalLayerList.IndexOf(layer);
                layer.AssignId(layerIndex);
            }

            //Show the dialog overlay
            dialogOverlayCanvasGroup.gameObject.SetActive(true);
            dialogOverlayCanvasGroup.DOFade(1f, overlayFadeDuration);

            // PrintModalLayerList();
        }

        public void RemoveFromModalLayerList(IModalLayer layer)
        {
            if (layer == null)
            {
                //ERROR
                Debug.Log("UIManager.cs AddToModalLayerList():: Layer passed in is null");
                return;
            }

            layer.HideLayer();
            if (modalLayerList.Contains(layer))
            {
                modalLayerList.Remove(layer);
            }

            // Show the next modal in the list (ie. the last element) if there are anymore in the list
            if (modalLayerList.Count >= 1)
            {
                modalLayerList[modalLayerList.Count - 1].ShowLayer();
            }

            // Hide the dialog overlay if there are no more modals in the list.
            if(modalLayerList.Count == 0)
            {
                dialogOverlayCanvasGroup.DOFade(0f, overlayFadeDuration);
                dialogOverlayCanvasGroup.gameObject.SetActive(false);
            }
            

            // PrintModalLayerList("cyan");
        }

        #region Debug Methods
        private void PrintModalLayerList(string color = "red")
        {
            Debug.Log("------");
            for (int i = 0; i < modalLayerList.Count; i++)
            {
                Debug.Log($"<color={color}>{modalLayerList[i].GetId()}_{i}</color>");
            }
            Debug.Log("------");
        }

        private IModalLayer GetModalLayerById(string id)
        {
            foreach (IModalLayer modal in modalLayerList)
            {
                if (modal.GetId().Equals(id))
                {
                    return modal;
                }
            }
            return null; // Modal not found
        }
    }
    #endregion
}