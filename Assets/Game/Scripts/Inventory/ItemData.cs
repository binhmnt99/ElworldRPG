namespace Scripts.Inventory
{
    using UnityEngine;

    [CreateAssetMenu(fileName = "ItemData", menuName = "Inventory/ItemData", order = 0)]
    public class ItemData : ScriptableObject
    {
        public int Id;
        public string DisplayName;
        [TextArea(4,4)]
        public string Description;
        public Sprite Icon;
        public int MaxStackSize;
    }
}
