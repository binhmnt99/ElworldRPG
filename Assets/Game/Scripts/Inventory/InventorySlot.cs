using System;
using UnityEngine;

namespace Scripts.Inventory
{
    [Serializable]
    public class InventorySlot
    {
        [SerializeField] private ItemData itemData;
        [SerializeField] private int stackSize;

        public ItemData GetItemData => itemData;
        public int GetStackSize => stackSize;

        public InventorySlot(ItemData item, int amount)
        {
            itemData = item;
            stackSize = amount;
        }

        public InventorySlot()
        {
            ClearInventorySlot();
        }

        public void ClearInventorySlot()
        {
            itemData = null;
            stackSize = -1;
        }

        public void AddToStack(int amount)
        {
            stackSize += amount;
        }

        public void RemoveFromStack(int amount)
        {
            stackSize -= amount;
        }

        public void UpdateInventorySlot(ItemData itemData, int amount)
        {
            this.itemData = itemData;
            stackSize = amount;
        }

        public bool RoomLeftInStack(int amountToAdd)
        {
            if (stackSize + amountToAdd <= itemData.MaxStackSize)
            {
                return true;
            }
            return false;
        }

        public bool RoomLeftInStack(int amountToAdd, out int amountRemaining)
        {
            amountRemaining = itemData.MaxStackSize - stackSize;
            return RoomLeftInStack(amountToAdd);
        }
    }
}
