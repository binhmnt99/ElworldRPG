using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Scripts.Inventory
{
    [Serializable]
    public class InventoryHolder : MonoBehaviour
    {
        [SerializeField] private int inventorySize;
        [SerializeField] protected InventorySystem inventorySystem;
        public static UnityAction<InventorySystem> OnDynamicInventoryDisplayRequested;

        public InventorySystem InventorySystem => inventorySystem;

        private void Awake()
        {
            inventorySystem = new(inventorySize);
        }
        
    }
}
