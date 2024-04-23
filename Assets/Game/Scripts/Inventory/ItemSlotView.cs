using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

namespace Scripts.Inventory
{
    public class ItemSlotView : MonoBehaviour
    {
        [SerializeField] private Image itemImage;
        [SerializeField] private TextMeshProUGUI itemCount;
        [SerializeField] private InventorySlot inventorySlot;

        private Button button;

        public InventorySlot AssignedInventorySlot => inventorySlot;
        public InventoryView InventoryView { get; private set; }

        private void Awake()
        {
            ClearSlot();
            button = GetComponent<Button>();
            button?.onClick.AddListener(OnItemSlotClick);
            InventoryView = transform.parent.GetComponent<InventoryView>();
        }

        public void Init(InventorySlot slot)
        {
            inventorySlot = slot;
            UpdateSlot(slot);
        }

        public void UpdateSlot(InventorySlot slot)
        {
            if (slot.ItemData != null)
            {
                itemImage.sprite = slot.ItemData.Icon;
                itemImage.color = Color.white;
                UpdateItemStackText(slot);
            }
            else
            {
                ClearSlot();
            }
        }

        private void UpdateItemStackText(InventorySlot slot)
        {
            if (slot.StackSize > 1)
            {
                itemCount.text = slot.StackSize.ToString();
            }
            else
            {
                itemCount.text = "";
            }
        }

        public void UpdateSlot()
        {
            if (inventorySlot != null)
            {
                UpdateSlot(inventorySlot);
            }
        }

        private void ClearSlot()
        {
            inventorySlot?.ClearInventorySlot();
            itemImage.sprite = null;
            itemImage.color = Color.clear;
            itemCount.text = "";
        }

        private void OnItemSlotClick()
        {
            try
            {
                InventoryView?.SlotClicked(this);
            }
            catch (System.Exception)
            {
                Debug.Log("No item in this slot");
            }
        }
    }
}
