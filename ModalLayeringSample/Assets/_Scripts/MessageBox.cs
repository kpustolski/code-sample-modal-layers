using UnityEngine;
using UnityEngine.Events;

namespace CodeSampleModalLayer
{
    public class MessageBox : MonoBehaviour
    {
        private static ItemInfoModal itemInfoModal = default;
        private static InfoModal infoModal = default;
        private static BackpackModal backpackModal = default;
        
        private static AppManager appMan = AppManager.Instance;
        public static void CreateItemInfoModal(Item item, SquareItem.LocationCreated locationCreated)
        {
            // First set up
            if(itemInfoModal == null)
            {
                ItemInfoModal m = Instantiate(appMan.UIMan.ItemInfoModalPrefab, appMan.UIMan.DialogParent);
                m.Setup(item: item, locationCreated: locationCreated);
                itemInfoModal = m;
                return;
            }
            itemInfoModal.Setup(item: item, locationCreated: locationCreated);
        }

        public static void CreateBackpackModal()
        {
            // First setup
            if(backpackModal == null)
            {
                BackpackModal m = Instantiate(appMan.UIMan.BackpackModalPrefab, appMan.UIMan.DialogParent);
                m.Setup();
                backpackModal = m;
                return;
            }
            backpackModal.Setup();
        }

        public static void CreateInfoModal(string title, string description, UnityAction cbOnActionButtonClick)
        {
            // First setup
            if(infoModal == null)
            {
                InfoModal m = Instantiate(appMan.UIMan.InfoModalPrefab, appMan.UIMan.DialogParent);
                m.Setup(title: title, description: description, cbOnActionButtonClick: cbOnActionButtonClick);
                infoModal = m;
                return;
            }
            infoModal.Setup(title: title, description: description, cbOnActionButtonClick: cbOnActionButtonClick);
        }
    }
}