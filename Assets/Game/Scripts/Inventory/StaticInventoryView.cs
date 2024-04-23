using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Inventory
{
    public class StaticInventoryView : InventoryView
    {
        [SerializeField] private GameObject itemSlotPrefab;
        [SerializeField] private Transform viewTransform;
        [SerializeField] private InventoryHolder inventoryHolder;
        [SerializeField] private List<ItemSlotView> itemSlotViewList;
        public override void Start()
        {
            base.Start();
            if (inventoryHolder != null)
            {
                inventorySystem = inventoryHolder.InventorySystem;
                inventorySystem.OnInventorySlotChanged += UpdateSlot;
            }
            else
            {
                Debug.LogWarning($"No inventory assigned to {this.gameObject}");
            }
            CreateInventorySlotView();
            AssignSlot(inventorySystem);
        }

        private void CreateInventorySlotView()
        {
            itemSlotViewList = new();
            for (int i = 0; i < inventorySystem.InventorySize; i++)
            {
                GameObject itemSlot = Instantiate(itemSlotPrefab, viewTransform);
                ItemSlotView itemSlotView = itemSlot.GetComponent<ItemSlotView>();
                itemSlotViewList.Add(itemSlotView);
            }
        }

        public override void AssignSlot(InventorySystem inventorySystem)
        {
            slotDictionary = new();
            if (itemSlotViewList.Count != inventorySystem.InventorySize)
            {
                Debug.LogWarning($"Inventory slot out of size on {this.gameObject}");
            }
            for (int i = 0; i < inventorySystem.InventorySize; i++)
            {
                slotDictionary.Add(itemSlotViewList[i], inventorySystem.InventorySlotList[i]);
                itemSlotViewList[i].Init(inventorySystem.InventorySlotList[i]);
            }
        }


    }
}
