using System.Collections;
using System.Collections.Generic;
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
        private CanvasGroup overlayCanvasGroup = default;
        [SerializeField]
        private CanvasGroup contentPanelCanvasGroup = default;

        protected AppManager appMan = default;

        // Animation variables
        private const float overlayFadeDuration = 0.2f;
        private const float contentFadeDuration = 0.2f;
        private const float buttonFadeDuration = 0.2f;
        private Vector3 contentScalePunch = new Vector3(0.1f, 0.1f, 0.1f);
        private const float contentScaleDuration = 0.2f;

        public virtual void Initialize()
        {
            appMan = AppManager.Instance;
        }

        protected void ShowAnimated(UnityAction cbBeforeAnimationStart)
        {
            overlayCanvasGroup.alpha = 0;
            contentPanelCanvasGroup.alpha = 0;

            Sequence showSequence = DOTween.Sequence();
            showSequence.AppendCallback(() =>
            {
                if (cbBeforeAnimationStart != null)
                {
                    cbBeforeAnimationStart();
                }
            })
            .Append(overlayCanvasGroup.DOFade(1f, overlayFadeDuration))
            .Append(contentPanelCanvasGroup.DOFade(1f, contentFadeDuration));
        }

        protected void HideAnimated(UnityAction cbOnAnimationComplete)
        {
            Sequence hideSequence = DOTween.Sequence();
            hideSequence.Append(contentPanelCanvasGroup.DOFade(0f, contentFadeDuration))
                .Append(overlayCanvasGroup.DOFade(0f, overlayFadeDuration))
                .OnComplete(() =>
                {
                    if (cbOnAnimationComplete != null)
                    {
                        cbOnAnimationComplete();
                    }
                });
        }
    }
}