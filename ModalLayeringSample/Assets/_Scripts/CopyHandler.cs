using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

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

    // Made a class for copy related Key Value pairs to easily serialize to a JSON file.
    [Serializable]
    public class Copy
    {
        public string copyKey;
        public string copyValue;

        public override string ToString()
        {
            return $"copyKey: {copyKey} copyValue: {copyValue}";
        }
    }
}