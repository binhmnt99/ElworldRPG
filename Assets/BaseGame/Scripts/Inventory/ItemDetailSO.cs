using System;
using UnityEngine;
namespace Scripts.Inventory
{
    [CreateAssetMenu(fileName = "Item", menuName = "Inventory/Item", order = 0)]
    [Serializable]
    public class ItemDetailSO : ScriptableObject
    {
        public SerializableGuid Id {get;private set;}
        public string DisplayId;
        public string DisplayName;
        [TextArea(4, 4)]
        public string Description;
        public Sprite Icon;
        public int MaxStackSize;

        private void Awake() {
            Id = SerializableGuid.NewGuid();
        }

    }
}
