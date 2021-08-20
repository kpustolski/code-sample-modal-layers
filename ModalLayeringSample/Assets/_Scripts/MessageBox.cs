using UnityEngine;
using UnityEngine.Events;

namespace CodeSampleModalLayer
{
    public class MessageBox : MonoBehaviour
    {
        private static AppManager appMan = AppManager.Instance;
        public static void CreateItemInfoModal(Item item, SquareItem.LocationCreated locationCreated)
        {
            ItemInfoModal m = Instantiate(appMan.UIMan.ItemInfoModalPrefab, appMan.UIMan.DialogParent);
            m.Setup(item: item, locationCreated: locationCreated);
        }

        public static void CreateBackpackModal()
        {
            var appMan = AppManager.Instance;
            BackpackModal m = Instantiate(appMan.UIMan.BackpackModalPrefab, appMan.UIMan.DialogParent);
            m.Setup();
        }

        public static void CreateInfoModal(string title, string description, UnityAction cbOnActionButtonClick)
        {
            var appMan = AppManager.Instance;
            InfoModal m = Instantiate(appMan.UIMan.InfoModalPrefab, appMan.UIMan.DialogParent);
            m.Setup(title: title, description: description, cbOnActionButtonClick: cbOnActionButtonClick);
        }
    }
}