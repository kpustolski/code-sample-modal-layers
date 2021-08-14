using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CodeSampleModalLayer
{
    public class MessageBox : MonoBehaviour
    {
        private static AppManager appMan = AppManager.Instance;
        public static void CreateInfoModal(Item item, SquareItem.LocationCreated locationCreated)
        {
            InfoModalTemplate m = Instantiate(appMan.UIMan.InfoModalTemplatePrefab, appMan.UIMan.DialogParent);
            m.Setup(item: item, locationCreated: locationCreated);
        }

        public static void CreateBackpackModal()
        {
            var appMan = AppManager.Instance;
            BackpackModal m = Instantiate(appMan.UIMan.BackpackModalPrefab, appMan.UIMan.DialogParent);
            m.Setup();
        }
    }
}