﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CodeSampleModalLayer
{
    public class AppManager : MonoBehaviour
    {
        [Header("Views")]
        [Space(5)]
        [SerializeField]
        private HomeView homeView = default;

        [Header("RectTransform")]
        [Space(5)]
        [SerializeField]
        private RectTransform dialogParent = default;

        [Header("Data")]
        [Space(5)]
        [SerializeField]
        private AppData appDataObject = default;

        //TODO: May place these in a UIManager.cs class
        [Header("Prefabs")]
        [Space(5)]
        [SerializeField]
        private InfoModalTemplate infoModalTemplatePrefab = default;
        [SerializeField]
        private SquareItem squareItemPrefab = default;
        [SerializeField]
        private Button navButtonPrefab = default;

        public InfoModalTemplate InfoModalTemplatePrefab { get { return infoModalTemplatePrefab; } }
        public SquareItem SquareItemPrefab { get { return squareItemPrefab; } }
        public Button NavButtonPrefab { get { return navButtonPrefab; } }
        public RectTransform DialogParent { get { return dialogParent; } }
        public AppData AppDataObject { get { return appDataObject; } }

        private List<IModalLayer> modalLayerList = new List<IModalLayer>();
        // Global Static Variable
        public static AppManager Instance { get; private set; }

        private ItemData itemData = default;
        public ItemData ItemDataList => itemData;
        // App Starts here. Ie. the "main" function
        void Start()
        {
            itemData = new ItemData();
            Instance = this;
            GetItemData();
            homeView.Setup();
        }

        public void GetItemData()
        {
            TextAsset file = AppDataObject.ItemJSONFile;
            if (file == null)
            {
                Debug.LogError($"AppManager.cs GetItemData() :: Unable to load the Item Data File.");
                return;
            }

            // Deserialize the JSON from ItemData.JSON and store it in ItemData
            Debug.Log($"File text: {file.text}");
            JsonUtility.FromJsonOverwrite(file.text, itemData);
            Debug.Log($"ItemData: {itemData.data.Count}");
        }

        public void AddToModalLayerList(IModalLayer layer)
        {
            if (layer == null)
            {
                //ERROR
                Debug.Log("AppManager.cs AddToModalLayerList():: Layer passed in is null");
                return;
            }

            // If a modal is already in the list, hide that one before showing the new one.
            if (modalLayerList.Count >= 1)
            {
                modalLayerList[modalLayerList.Count - 1].HideLayer();
            }

            layer.ShowLayer();
            if (!modalLayerList.Contains(layer))
            {
                modalLayerList.Add(layer);
                int layerIndex = modalLayerList.IndexOf(layer);
                layer.AssignId(layerIndex);
            }

            PrintModalLayerList();
        }

        public void RemoveFromModalLayerList(IModalLayer layer)
        {
            if (layer == null)
            {
                //ERROR
                Debug.Log("AppManager.cs AddToModalLayerList():: Layer passed in is null");
                return;
            }

            layer.HideLayer();
            if (modalLayerList.Contains(layer))
            {
                modalLayerList.Remove(layer);
            }

            // Show the next modal in the list (ie. the last element) if there are anymore in the list
            if (modalLayerList.Count > 1)
            {
                modalLayerList[modalLayerList.Count - 1].ShowLayer();
            }

            PrintModalLayerList("cyan");
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