namespace RPG.Inventory
{
    using UnityEngine;
    using UnityEngine.UI;
    using TMPro;

    public class ItemSlotView : MonoBehaviour
    {
        [SerializeField] private Image itemImage;
        [SerializeField] private TextMeshProUGUI itemCountText;
        [SerializeField] private ItemSlot assignedItemSlot;

        private Button button;

        public ItemSlot AssignedItemSlot => assignedItemSlot;


        private void Awake()
        {
            ClearSlot();
            button = GetComponent<Button>();
            button?.onClick.AddListener(OnItemSlotClick);
        }

        private void ClearSlot()
        {
            assignedItemSlot?.ClearItemSlot();
            itemImage.sprite = null;
            itemImage.color = Color.clear;
            itemCountText.text = "";
        }

        private void OnItemSlotClick()
        {
            Debug.Log("Clicked"); ;
        }
    }
}

