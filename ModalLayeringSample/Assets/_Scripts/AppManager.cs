using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CodeSampleModalLayer
{
    public class AppManager : MonoBehaviour
    {
        [Header("Views")]
        [Space(5)]
        [SerializeField]
        private HomeView homeView = default;

        // [Header("Prefabs")]
        // [Space(5)]
        // [SerializeField]
        // private 

        private List<IModalLayer> modalLayerList = new List<IModalLayer>();
        // Global Static Variable
        public static AppManager Instance { get; private set; }

        // App Starts here. Ie. the "main" function
        void Start()
        {
            Instance = this;
            homeView.Setup();
        }

        public void AddToModalLayerList(IModalLayer layer)
        {
            if (layer == null)
            {
                //ERROR
                Debug.Log("AppManager.cs AddToModalLayerList():: Layer passed in is null");
                return;
            }

            if (modalLayerList.Count > 1)
            {

                // Current modal layer on the UI must hide
                // The new layer must show and be added to the list
            }

            if (!modalLayerList.Contains(layer))
            {
                modalLayerList.Add(layer);
            }
        }

        public void RemoveFromModalLayerList(IModalLayer layer)
        {
            if (layer == null)
            {
                //ERROR
                Debug.Log("AppManager.cs AddToModalLayerList():: Layer passed in is null");
                return;
            }

            // Need to check if the layer is the current one on the screen
            // If so, hide that modal and remove it from the list.
            // If it's not on the screen, "quietly" remove it from the list

            if (modalLayerList.Contains(layer))
            {
                modalLayerList.Remove(layer);
            }
        }

        //DEBUG
        private void PrintModalLayerList()
        {
            for (int i = 0; i < modalLayerList.Count; i++)
            {
                Debug.Log($"<color=white>{modalLayerList[i].GetId()}_{i}</color>");
            }
        }
    }
}