using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

namespace CodeSampleModalLayer
{
    public class ModalBase : MonoBehaviour
    {
        [Header("ModalBase Variables")]
        [Space(5)]
        [SerializeField]
        private CanvasGroup contentPanelCanvasGroup = default;
        [SerializeField]
        private RectTransform contentPanelRectTransform = default;

        protected AppManager appMan = default;
        protected string modalId = default;

        // Animation variables
        private Vector3 contentScalePunch = new Vector3(0.1f, 0.1f, 0.1f);
        private Sequence hideSequence = default;
        private Sequence showSequence = default;
        
        private const float contentFadeDuration = 0.2f;
        private const float contentScaleDuration = 0.2f;
        
        // Helps determine when we should show/hide the modal in the modal layer list.
        // Example: For the BackpackModal, we want to make sure it's still seen in the background.
        protected bool bDoHideModal = true;

        public virtual void Initialize()
        {
            appMan = AppManager.Instance;
        }

        protected void ShowAnimated()
        {
            contentPanelCanvasGroup.alpha = 0;

            ResetSequence(showSequence);
            showSequence = DOTween.Sequence();
            
            showSequence.AppendCallback(() =>
            {
                gameObject.SetActive(true);
            })
            .Append(contentPanelCanvasGroup.DOFade(1f, contentFadeDuration))
            .Join(contentPanelRectTransform.DOPunchScale(contentScalePunch, contentScaleDuration));
        }

        protected void HideAnimated(UnityAction cbOnAnimationComplete)
        {
            ResetSequence(hideSequence);
            hideSequence = DOTween.Sequence();

            hideSequence.Append(contentPanelCanvasGroup.DOFade(0f, contentFadeDuration))
                .Join(contentPanelRectTransform.DOPunchScale(contentScalePunch, contentScaleDuration))
                .OnComplete(() =>
                {
                    gameObject.SetActive(false);
                    if (cbOnAnimationComplete != null)
                    {
                        cbOnAnimationComplete();
                    }
                });
        }

        private void ResetSequence(Sequence seq)
        {
            if(seq != null)
            {
                seq.Kill(false);
                seq = null;
            }
        }

#region Modal Layer Functions

        public virtual void ShowLayer() 
        { 
            if(bDoHideModal)
            {
                ShowAnimated();
            }
        }

        public virtual void HideLayer(UnityAction cbOnHideLayer) 
        {
            // Hide the modal with animation
            if(bDoHideModal)
            {
                HideAnimated(cbOnAnimationComplete: cbOnHideLayer);
            }
            else
            {
                // We aren't hiding the modal, but the callback still needs to be called.
                if(cbOnHideLayer != null)
                {
                    cbOnHideLayer();
                }
            }
         }

        public virtual void OnRemovalFromLayerList()
        {
            Destroy(gameObject);
        }

        public virtual void  AssignId(int layerIndex) { }
        public string GetId() { return modalId; }
        
#endregion

    }
}