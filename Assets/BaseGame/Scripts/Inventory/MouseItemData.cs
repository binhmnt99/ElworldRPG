using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.Inventory
{
    public class MouseItemData : MonoBehaviour
    {
        public Image ItemImage;
        public TextMeshProUGUI ItemCount;

        private void Awake() {
            ItemImage.color = Color.clear;
            ItemCount.text = "";
        }
    }
}
