namespace RPG.Inventory
{
    using UnityEngine;
    using RPG.CustomGuid;
    using UnityEngine.UI;

    [CreateAssetMenu(fileName = "ItemElementSO", menuName = "Inventory/ItemElementSO", order = 0)]
    public class ItemElementSO : ScriptableObject
    {
        public SerializableGuid Id = SerializableGuid.NewGuid();
        public string ItemId;
        public string ItemName;
        [TextArea(4, 4)]
        public string ItemDescription;
        public Image ItemImage;
        public int MaxStackSize;
    }
}

