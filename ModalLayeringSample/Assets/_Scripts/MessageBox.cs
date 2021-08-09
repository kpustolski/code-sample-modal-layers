using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CodeSampleModalLayer
{
    public class MessageBox : MonoBehaviour
    {
        public static void CreateInfoModal(string description)
        {
            var appMan = AppManager.Instance;
            InfoModalTemplate m = Instantiate(appMan.InfoModalTemplatePrefab, appMan.DialogParent);
            m.Setup(descText: description);
        }
    }
}