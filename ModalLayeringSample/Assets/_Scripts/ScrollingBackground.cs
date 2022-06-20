using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace CodeSampleModalLayer
{
    public class ScrollingBackground : MonoBehaviour
    {
        public enum ScrollDirection
        {
            None = 0,
            Left = 1,
            Right = 2,
            Up = 3,
            Down = 4,
            UpperDiagonalLeft = 5,
            UpperDiagonalRight = 6,
            LowerDiagonalLeft = 7,
            LowerDiagonalRight = 8
        }

        [SerializeField]
        private Image backgroundImage = default;

        // Control what direction the scrolling background should take
        [SerializeField]
        private ScrollDirection scrollDirection;
        private RectTransform backgroundContainer = default;

        [Tooltip("In milliseconds")]
        [SerializeField]
        private float duration = default; // in milliseconds

        public void Initialize()
        {
            Vector2 offset;
            backgroundContainer = backgroundImage.GetComponent<RectTransform>();

            if(scrollDirection == ScrollDirection.None)
            {
                // We don't want the background to scroll
                return;
            }

            // Offset is determined by the width/height of the background container
            var offsetValues = GetOffsetBasedOnDirection();
            offset = new Vector2(offsetValues.X, offsetValues.Y);
            
            // We need to create a copy of the material so we don't modify the 
            // actual material in the assets folder.
            backgroundImage.material = new Material(backgroundImage.material);
            backgroundImage.material.DOOffset(offset, duration)
                .SetLoops(-1, LoopType.Restart)
                .SetEase(Ease.Linear)
                .SetId("ScrollingBackground.cs :: DOOffset()"); // For debugging
        }

        public (float X, float Y) GetOffsetBasedOnDirection()
        {
            float backgroundWidth = backgroundContainer.rect.width;
            float backgroundHeight = backgroundContainer.rect.height;

            switch(scrollDirection)
            {
                case ScrollDirection.Left:
                    return (backgroundWidth, 0);
                case ScrollDirection.Right:
                     return (-backgroundWidth, 0);
                case ScrollDirection.Up:
                    return (0f, -backgroundHeight );
                case ScrollDirection.Down:
                    return (0f,backgroundHeight );
                case ScrollDirection.UpperDiagonalLeft:
                    return (backgroundWidth, -backgroundHeight); 
                case ScrollDirection.UpperDiagonalRight:
                    return (-backgroundWidth, -backgroundHeight);
                case ScrollDirection.LowerDiagonalLeft:
                    return (backgroundWidth, backgroundHeight);
                case ScrollDirection.LowerDiagonalRight:
                    return (-backgroundWidth, backgroundHeight);
                default:
                    Debug.Log("ScrollingBackground GetOffsetBasedOnDirection():: No direction selected. Background will not scroll.");
                    return (0,0);
            }
        }

        
        public void Shutdown()
        {
            // Stops the animation
            backgroundImage.material.DOKill();
        }
    }
}