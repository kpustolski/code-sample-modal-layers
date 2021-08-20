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
            if (string.IsNullOrEmpty(item.id))
            {
                Debug.LogError($"ItemDataHandler.cs IsItemValid() :: Item with name: {item.name} has a missing or null unique id.");
                return false;
            }

            if (DoesItemIdAlreadyExist(item.id))
            {
                Debug.LogError($"ItemDataHandler.cs IsItemValid() :: Item object of id {item.id} already exists. Check object with the name: {item.name}. Make sure the id is unique for each object.");
                return false;
            }

            return true;
        }
        
        // Helper to see if there are duplicate item objects in the list based on its id.
        // The id for each item should be unique.
        private bool DoesItemIdAlreadyExist(string id)
        {
            int copies = 0;
            for (int i = 0; i < data.Count; i++)
            {
                if (data[i].id.Equals(id))
                {
                    copies++;
                }
            }
            return copies > 1;
        }
    }
}