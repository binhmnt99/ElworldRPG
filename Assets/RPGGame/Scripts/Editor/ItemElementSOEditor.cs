namespace RPG.Inventory
{
    using UnityEditor;

    [CustomEditor(typeof(ItemElementSO))]
    public class ItemElementSOEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            ItemElementSO itemDetailSO = (ItemElementSO)target;
            itemDetailSO.ItemId = itemDetailSO.Id.ToGuid().ToString();
            itemDetailSO.ItemName = itemDetailSO.name;
        }
    }
}
