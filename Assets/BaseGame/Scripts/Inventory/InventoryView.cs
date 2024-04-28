using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Inventory
{
    public abstract class InventoryView : MonoBehaviour
    {
        [SerializeField] private MouseItemData mouseItemData;

        protected InventorySystem inventorySystem;
        protected Dictionary<ItemSlotView, InventorySlot> slotDictionary;

        public InventorySystem InventorySystem => inventorySystem;
        public Dictionary<ItemSlotView, InventorySlot> SlotDictionary => slotDictionary;

        public virtual void Start() { }

        public abstract void AssignSlot(InventorySystem inventorySystem);
        protected virtual void UpdateSlot(InventorySlot updateSlot)
        {
            foreach (var slot in SlotDictionary)
            {
                if (slot.Value == updateSlot)
                {
                    slot.Key.UpdateSlot(updateSlot);
                }
            }
        }
        public void SlotClicked(ItemSlotView clickedSlot)
        {
            Debug.Log($"Slot clicked at {clickedSlot.AssignedInventorySlot.ItemData.DisplayName}");
        }
    }
}
