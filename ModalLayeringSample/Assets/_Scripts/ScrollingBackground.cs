using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace CodeSampleModalLayer
{
    public class ScrollingBackground : MonoBehaviour
    {

        [SerializeField]
        private Image backgroundImage = default;
        // [SerializeField]
        private RectTransform backgroundContainer = default;

        [Tooltip("In milliseconds")]
        [SerializeField]
        private float duration = default; // in milliseconds

        public void Initialize()
        {
            Vector2 offset;
            backgroundContainer = backgroundImage.GetComponent<RectTransform>();

            // Offset is determined by the width of the background container
            offset = new Vector2((backgroundContainer.rect.width*2), -(backgroundContainer.rect.height*2));
            
            // We need to create a copy of the material so we don't modify the 
            // actual material in the assets folder.
            backgroundImage.material = new Material(backgroundImage.material);
            backgroundImage.material.DOOffset(offset, duration)
                .SetLoops(-1, LoopType.Restart)
                .SetEase(Ease.Linear);
        }
    }
}