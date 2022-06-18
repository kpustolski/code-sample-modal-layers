using System.Collections.Generic;
using UnityEngine;

namespace CodeSampleModalLayer
{
    public class Backpack
    {
        private const int maxTotalItems = 10;
        private List<Item> itemList = new List<Item>();
        public int MaxTotalItems { get { return maxTotalItems; } }
        public List<Item> ItemList { get { return itemList; } }

        public void AddItem(Item item)
        {
            // No need to add the item to the itemList if it's already represented in there.
            if (item.AmountInBackpack >= 1)
            {
                return;
            }

            itemList.Add(item);
        }

        public void RemoveItem(Item item)
        {
            // Don't remove the item entirely from the list if there is more than one in the backpack.
            if (item.AmountInBackpack >= 1)
            {
                return;
            }

            foreach (Item i in itemList)
            {
                if (i.id.Equals(item.id))
                {
                    itemList.Remove(i);
                    return;
                }
            }
        }

        public void RemoveAllItems()
        {
            itemList.Clear();
        }

        public int GetTotalItemsInBackpack()
        {
            if (ItemList.Count == 0)
            {
                return 0;
            }

            int amount = 0;
            foreach (Item i in ItemList)
            {
                amount += i.AmountInBackpack;
            }
            return amount;
        }

        public bool IsBackpackEmpty()
        {
            return (GetTotalItemsInBackpack() == 0);
        }

        public bool IsBackpackFull()
        {
            return (GetTotalItemsInBackpack() >= maxTotalItems);
        }
    }
}