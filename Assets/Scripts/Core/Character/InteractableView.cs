using UnityEngine;

namespace Core.Character
{
    /*
     * 交互 & 高亮View组件
     */
    public class InteractableView : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer spriteRenderer;

        private Color originalColor;
        private Color highlightColor = Color.yellow;

        private void Awake()
        {
            if (spriteRenderer == null)
            {
                spriteRenderer = GetComponent<SpriteRenderer>();
            }

            originalColor = spriteRenderer.color;
        }

        // 高亮物品
        public void Highlight()
        {
            spriteRenderer.color = highlightColor;
        }

        // 移除高亮
        public void RemoveHighlight()
        {
            spriteRenderer.color = originalColor;
        }
    }
}