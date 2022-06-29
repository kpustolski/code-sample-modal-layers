using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

namespace CodeSampleModalLayer
{
    public class InfoModal : ModalBase
    {
        [Header("InfoModal Variables")]
		[Space(5)]
        [SerializeField]
		private Button closeButton = default;
		[SerializeField]
		private Button actionButton = default;
        [SerializeField]
		private TextMeshProUGUI titleText = default;
        [SerializeField]
		private TextMeshProUGUI descriptionText = default;

        public override void Initialize()
        {
            base.Initialize();
			closeButton.interactable = true;
			actionButton.interactable = true;
        }

        public void Setup(string title, string description, UnityAction cbOnActionButtonClick)
        {
            Initialize();
            
            titleText.text = title;
            descriptionText.text = description;
            
            appMan.UIMan.AddToModalLayerList(layer: this);
            closeButton.onClick.AddListener(Shutdown);
            actionButton.onClick.AddListener(() =>
            {
                if (cbOnActionButtonClick != null)
                {
                    cbOnActionButtonClick();
                }
                Shutdown();
            });

            ShowAnimated();
        }

		public void Shutdown()
		{
            appMan.UIMan.RemoveFromModalLayerList(layer: this);
		}

#region Modal Layer Functions

		public override void ShowLayer()
		{
			ShowAnimated();
		}

		public override void HideLayer(UnityAction cbOnHideLayer) 
		{
			HideAnimated(cbOnAnimationComplete: cbOnHideLayer);
		}

		public override void OnRemovalFromLayerList()
		{
			// Turn off button interactables to avoid double clicks
			closeButton.interactable = false;
			actionButton.interactable = false;

			closeButton.onClick.RemoveAllListeners();
			actionButton.onClick.RemoveAllListeners();

			Destroy(gameObject);
		}

		public override void AssignId(int layerIndex)
		{
			modalId = $"InfoModal_{layerIndex}";
		}

#endregion
    }
}