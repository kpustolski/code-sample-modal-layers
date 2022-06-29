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

        // Animation variables
        private Vector3 contentScalePunch = new Vector3(0.1f, 0.1f, 0.1f);
        private Sequence hideSequence = default;
        private Sequence showSequence = default;
        
        private const float contentFadeDuration = 0.2f;
        private const float contentScaleDuration = 0.2f;

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
    }
}