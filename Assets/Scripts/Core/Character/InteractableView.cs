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
        
        private CubeInteractUI interactUI; // 引用Manager

        private void Awake()
        {
            if (spriteRenderer == null)
            {
                spriteRenderer = GetComponent<SpriteRenderer>();
            }

            originalColor = spriteRenderer.color;
            
            // 找到场景中的InteractableManager
            interactUI = FindObjectOfType<CubeInteractUI>();
        }

        // 高亮物品
        public void Highlight()
        {
            spriteRenderer.color = highlightColor;
            
            // 通知Manager该物体被高亮
            interactUI.SetCurrentInteractable(this);
        }

        // 移除高亮
        public void RemoveHighlight()
        {
            spriteRenderer.color = originalColor;
            // 通知Manager移除按钮
            interactUI.RemoveButtonForInteractable(this);
        }
    }
}