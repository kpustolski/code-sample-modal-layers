using UnityEngine;
using System;

namespace CodeSampleModalLayer
{
    [Serializable]
    public class ItemIcon
    {
        public string itemId;
        public Sprite iconSprite;
    }

    [Serializable]
    public class CategoryGradient
    {
        public Utilities.InventoryCategories category;
        public Gradient gradient;
    }

    [CreateAssetMenu(fileName = "AppData", menuName = "ScriptableObjects/AppData", order = 1)]
    public class AppData : ScriptableObject
    {
        [Header("ItemIcons")]
        [Space(5)]
        [SerializeField]
        private ItemIcon[] itemIcons = default;

        [Header("Category Gradients")]
        [Space(5)]
        [SerializeField]
        private CategoryGradient[] categoryGradients = default;

        [Header("JSON Files")]
        [SerializeField]
        private TextAsset itemJSONFile = default;
        [SerializeField]
        private TextAsset copyJSONFile = default;
        public TextAsset ItemJSONFile { get { return itemJSONFile; } }
        public TextAsset CopyJSONFile { get { return copyJSONFile; } }

        public Sprite GetItemIcon(string id)
        {
            foreach (ItemIcon i in itemIcons)
            {
                if (i.itemId.Equals(id))
                {
                    return i.iconSprite;
                }
            }
            // Item not found in the list
            Debug.LogError($"AppData.cs GetItemIconByItemName() :: Item with id {id} not found in ItemIcons list.");
            return null;
        }

        public Gradient GetCategoryGradient(Utilities.InventoryCategories category)
        {
            foreach (CategoryGradient cg in categoryGradients)
            {
                if (cg.category.Equals(category))
                {
                    return cg.gradient;
                }
            }

            Debug.LogError($"AppData.cs GetCategoryGradient() :: CategoryGradient with category {category} not found in categoryGradients list.");
            return null;
        }
    }
}