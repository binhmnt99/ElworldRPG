namespace RPG.Inventory
{
    using UnityEngine;

    public class ItemSlot
    {
        [SerializeField] private ItemElementSO itemElement;
        [SerializeField] private int stackSize;

        public ItemElementSO ItemElementSO => itemElement;
        public int StackSize => stackSize;

        public ItemSlot()
        {
            ClearItemSlot();
        }

        public ItemSlot(ItemElementSO item, int amount)
        {
            itemElement = item;
            stackSize = amount;
        }

        public void ClearItemSlot()
        {
            itemElement = null;
            stackSize = -1;
        }
    }
}

