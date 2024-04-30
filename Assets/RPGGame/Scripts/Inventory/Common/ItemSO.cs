namespace RPG.Inventory
{
    using System;
    using UnityEngine;
    using Scripts.Inventory;
    using UnityEngine.UI;

    [CreateAssetMenu(menuName = "Inventory/ItemSO")]
    [Serializable]
    public class ItemSO : ScriptableObject
    {
        public SerializableGuid ItemID = SerializableGuid.NewGuid();

        public string ItemName;

        [TextArea(4,4)]
        public string ItemDescription;

        public Image ItemImage;

        public int MaxStackSize;
    }
}