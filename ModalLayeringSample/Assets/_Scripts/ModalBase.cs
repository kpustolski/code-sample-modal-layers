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
		[SerializeField]
        private RectTransform contentPanelRectTransform = default;

        protected AppManager appMan = default;

        // Animation variables
        private const float overlayFadeDuration = 0.1f;
        private const float contentFadeDuration = 0.2f;
        private const float buttonFadeDuration = 0.2f;
        private Vector3 contentScalePunch = new Vector3(0.1f, 0.1f, 0.1f);
        private const float contentScaleDuration = 0.2f;
        private Sequence showSequence = default;


        public virtual void Initialize()
        {
            appMan = AppManager.Instance;
        }

        protected void ShowAnimated(UnityAction cbBeforeAnimationStart)
        {
            overlayCanvasGroup.alpha = 0;
            contentPanelCanvasGroup.alpha = 0;

            Sequence showSequence = DOTween.Sequence();
			showSequence.stringId = "ModalBase.cs :: ShowSequence"; // for debugging
            showSequence.AppendCallback(() =>
            {
				gameObject.SetActive(true);
                if (cbBeforeAnimationStart != null)
                {
                    cbBeforeAnimationStart();
                }
            })
            .Append(overlayCanvasGroup.DOFade(1f, overlayFadeDuration).SetId(overlayCanvasGroup))
            .Append(contentPanelCanvasGroup.DOFade(1f, contentFadeDuration).SetId(contentPanelCanvasGroup))
			.Join(contentPanelRectTransform.DOPunchScale(contentScalePunch, contentScaleDuration).SetId(contentPanelRectTransform));
        }

        protected void HideAnimated(UnityAction cbOnAnimationComplete)
        {
            Sequence hideSequence = DOTween.Sequence();
			hideSequence.stringId = "ModalBase.cs :: hideSequence"; // For debugging
            hideSequence.Append(contentPanelCanvasGroup.DOFade(0f, contentFadeDuration).SetId(contentPanelCanvasGroup))
				.Join(contentPanelRectTransform.DOPunchScale(contentScalePunch, contentScaleDuration).SetId(contentPanelRectTransform))
                .Append(overlayCanvasGroup.DOFade(0f, overlayFadeDuration).SetId(overlayCanvasGroup))
                .OnComplete(() =>
                {
					gameObject.SetActive(false);
					if (cbOnAnimationComplete != null)
					{
						cbOnAnimationComplete();
					}
                });
        }
    }
}