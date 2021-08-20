using System.Collections.Generic;
using UnityEngine;

namespace CodeSampleModalLayer
{
    [CreateAssetMenu(fileName = "Copy_Data_Handler", menuName = "ScriptableObjects/CopyDataEditor")]
    public class CopyHandler : ScriptableObject
    {
        public List<Copy> data = new List<Copy>();

        public string CreateJsonStringFromData(bool makeJSONPretty)
        {
            return JsonUtility.ToJson(this, makeJSONPretty);
        }
    }
}