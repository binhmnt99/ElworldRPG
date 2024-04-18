using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace Scripts.Inventory
{
    [Serializable]
    public class InventorySystem
    {
        [SerializeField] private List<InventorySlot> inventorySlotList;

        public List<InventorySlot> GetInventorySlotList => inventorySlotList;
        public int GetInventorySize => GetInventorySlotList.Count;

        public UnityAction<InventorySlot> OnInventorySlotChanged;

        public InventorySystem(int size)
        {
            inventorySlotList = new(size);
            for (int i = 0; i < size; i++)
            {
                inventorySlotList.Add(new InventorySlot());
            }
        }

        public bool AddToInventory(ItemDetailSO itemToAdd, int amountToAdd)
        {
            if (ContainsItem(itemToAdd, out List<InventorySlot> inventorySlotList)) //Check Item exist in inventory
            {
                foreach (InventorySlot slot in inventorySlotList)
                {
                    if (slot.RoomLeftInStack(amountToAdd))
                    {
                        slot.AddToStack(amountToAdd);
                        OnInventorySlotChanged?.Invoke(slot);
                        return true;
                    }
                }

            }
            if (HasFreeSlot(out InventorySlot freeSlot)) //Get the first available slot
            {
                freeSlot.UpdateInventorySlot(itemToAdd, amountToAdd);
                OnInventorySlotChanged?.Invoke(freeSlot);
                return true;
            }
            return false;
        }

        public bool ContainsItem(ItemDetailSO itemToAdd, out List<InventorySlot> inventorySlot)
        {
            inventorySlot = inventorySlotList.Where(i => i.GetItemData == itemToAdd).ToList();
            Debug.Log(inventorySlot.Count);
            return inventorySlot != null;
        }

        public bool HasFreeSlot(out InventorySlot freeSlot)
        {
            freeSlot = inventorySlotList.FirstOrDefault(i => i.GetItemData == null);
            return freeSlot != null;
        }
    }
}
