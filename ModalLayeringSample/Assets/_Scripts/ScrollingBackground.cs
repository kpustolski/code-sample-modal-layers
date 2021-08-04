using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CodeSampleModalLayer
{
    public class ScrollingBackground : MonoBehaviour
    {
        [SerializeField]
        private Image backgroundImage = default;
        [SerializeField]
        private RectTransform backgroundContainer = default;
        [SerializeField]
        private float duration = default;

        public void Initialize()
        {

        }
    }
}