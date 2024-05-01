namespace RPG.UI
{
    using System.Collections;
    using TMPro;
    using UnityEngine;

    public class InitLoadingTextController : MonoBehaviour
    {
        public float delayTime = 1f;
        public string Text = "Loading";
        [SerializeField] private TextMeshProUGUI textMeshProUGUI;
        private int count;
        private float time;

        private void Start()
        {
            count = 0;
            time = 0;
        }

        private void FixedUpdate()
        {
            time += Time.fixedDeltaTime;
            if (time >= delayTime)
            {
                time = 0f;
            }
            if (time <= 0f)
            {
                if (count <= 3)
                {
                    switch (count)
                    {
                        case 0:
                            textMeshProUGUI.text = Text;
                            break;
                        case 1:
                            textMeshProUGUI.text = Text + ".";
                            break;
                        case 2:
                            textMeshProUGUI.text = Text + "..";
                            break;
                        case 3:
                            textMeshProUGUI.text = Text + "...";
                            count = -1;
                            break;
                    }
                }
                count++;
            }
        }
    }
}