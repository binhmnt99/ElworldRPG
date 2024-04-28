using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Inventory
{
    [RequireComponent(typeof(SphereCollider))]
    public class ItemPickup : MonoBehaviour
    {
        public float PickupRadius;
        public ItemDetailSO ItemData;
        private SphereCollider sphereCollider;

        private void Awake() {
            sphereCollider = GetComponent<SphereCollider>();
            sphereCollider.isTrigger = true;
            sphereCollider.radius = PickupRadius;
        }

        private void OnTriggerEnter(Collider other)
        {
            var inventory = other.transform.GetComponent<InventoryHolder>();
            if (inventory == null) return;
            if (inventory.InventorySystem.AddToInventory(ItemData,1))
            {
                Destroy(this.gameObject);
            }
        }
    }
}
