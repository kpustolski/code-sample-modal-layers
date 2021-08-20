using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace CodeSampleModalLayer
{
    public class BackpackButton : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI countText = default;

        private Button button = default;
        private AppManager appMan = default;

        public void Setup()
        {
            appMan = AppManager.Instance;
            button = GetComponent<Button>();
            button.onClick.AddListener(OpenBackpackModal);
        }

        public void OpenBackpackModal()
        {
            MessageBox.CreateBackpackModal();
        }

        public void UpdateCountText(int amount, int maxAmount)
        {
            countText.text = string.Format(appMan.DataMan.GetCopyText("itemcount"), amount, maxAmount);
            if(amount.Equals(maxAmount))
            {
                countText.text = appMan.DataMan.GetCopyText("backpack.full");
            }
        }

        public void Shutdown()
        {
            button.onClick.RemoveAllListeners();
        }
    }
}