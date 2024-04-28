using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Inventory
{
    using UnityEngine;
    using UnityEditor;

    [CustomEditor(typeof(ItemDetailSO))]
    public class ItemEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            ItemDetailSO itemDetailSO = (ItemDetailSO)target;
            itemDetailSO.DisplayId = itemDetailSO.Id.ToGuid().ToString();
            itemDetailSO.DisplayName = itemDetailSO.name;
        }
    }
}
