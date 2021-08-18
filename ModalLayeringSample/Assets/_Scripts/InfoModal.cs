using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

namespace CodeSampleModalLayer
{
    public class InfoModal : ModalBase, IModalLayer
    {
        [Header("InfoModal Variables")]
		[Space(5)]
        [SerializeField]
		private Button closeButton = default;
		[SerializeField]
		private Button actionButton = default;
		private string modalId = default;
        [SerializeField]
		private TextMeshProUGUI titleText = default;
        [SerializeField]
		private TextMeshProUGUI descriptionText = default;

        private const string title ="Are you sure?";
        private const string description = "Your backpack will be cleared";

        public override void Initialize()
        {
            base.Initialize();
        }

        public void Setup(UnityAction cbOnActionButtonClick)
        {
            Initialize();
            
            titleText.text = title;
            descriptionText.text = description;
            
            appMan.UIMan.AddToModalLayerList(this as IModalLayer);
            closeButton.onClick.AddListener(Shutdown);
            actionButton.onClick.AddListener(() =>
            {
                if (cbOnActionButtonClick != null)
                {
                    cbOnActionButtonClick();
                }
                Shutdown();
            });

            ShowAnimated(cbBeforeAnimationStart: null);
        }
		public void Shutdown()
		{
            // HideAnimated is located in the base class
            HideAnimated(cbOnAnimationComplete: ShutdownOnAnimationCompleteCallback);
		}

		private void ShutdownOnAnimationCompleteCallback()
		{
			// Turn off button interactables to avoid double clicks
			closeButton.interactable = false;
			actionButton.interactable = false;

			closeButton.onClick.RemoveAllListeners();
			actionButton.onClick.RemoveAllListeners();

			// Remove from modal layer list
			appMan.UIMan.RemoveFromModalLayerList(this as IModalLayer);
			Destroy(gameObject);
		}

#region ModalLayer Functions

		public void ShowLayer()
		{
			ShowAnimated(cbBeforeAnimationStart: null);
		}
		public void HideLayer()
		{
			HideAnimated(cbOnAnimationComplete: null);
		}
		public string GetId()
		{
			return modalId;
		}

		public void AssignId(int layerIndex)
		{
			modalId = $"InfoModal_{layerIndex}";
		}

#endregion
    }
}