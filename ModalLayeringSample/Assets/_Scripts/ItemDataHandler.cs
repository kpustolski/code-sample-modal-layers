using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CodeSampleModalLayer
{
    [CreateAssetMenu(fileName = "Item_Data_Handler", menuName = "ScriptableObjects/ItemDataEditor")]
    public class ItemDataHandler : ScriptableObject
    {
        public List<Item> data = new List<Item>();

        public string CreateJsonStringFromData(bool makeJSONPretty)
        {
            return JsonUtility.ToJson(this, makeJSONPretty);
        }

        public int ValidateData()
        {
            int errors = 0;
            foreach (Item i in data)
            {
                if (!IsItemValid(i))
                {
                    errors++;
                }
            }

            return errors;
        }

        public bool IsItemValid(Item item)
        {
            // TODO: Fill with validation cases
            return true;
        }
    }
}