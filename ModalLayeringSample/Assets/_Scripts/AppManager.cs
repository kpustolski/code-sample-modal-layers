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
        // Global Static Variable
        public static AppManager Instance { get; private set; }

        // App Starts here. Ie. the "main" function
        void Start()
        {
            Instance = this;
            homeView.Setup();
        }
    }
}