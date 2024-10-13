using System.Linq;
using Core.Character.Events;
using UnityEngine;

namespace Core.Character.Handler
{
    public class PlayerInteractHandler
    {
        public PlayerView PlayerView;

        public PlayerInteractHandler(PlayerView playerView)
        {
            this.PlayerView = playerView;
        }

        public void DetectInteractable()
        {
            Collider2D[] hitColliders =
                Physics2D.OverlapCircleAll(PlayerView.transform.position, PlayerView.interactionRadius,
                    PlayerView.interactableLayerMask);
            if (hitColliders.Length == 0)
            {
                RemoveCurrentHighlight();
                return;
            }

            // 找到距离最近的可交互物品
            Collider2D closestCollider = hitColliders
                .OrderBy(collider => Vector2.Distance(PlayerView.transform.position, collider.transform.position))
                .FirstOrDefault();

            if (closestCollider != null)
            {
                // 如果有这个交互高亮组件
                InteractableView interactable = closestCollider.GetComponent<InteractableView>();
                if (interactable != PlayerView.currentHighlightedInteractableView) // 如果最近的哪个高亮组件不是现在这个
                {
                    RemoveCurrentHighlight(); // 把现在这个取消高亮
                    PlayerView.currentHighlightedInteractableView = interactable; // 设置PlayerView中的需要高亮的组件为新的最近的
                    PlayerView.currentHighlightedInteractableView.Highlight(); // 把新的高亮了

                    PlayerEvent.PlayerSelectInteractEvent?.Invoke(new PlayerSelectInteractEvent(PlayerView,
                        PlayerView.currentHighlightedInteractableView.gameObject));
                }
            }
        }

        public void RemoveCurrentHighlight()
        {
            if (PlayerView.currentHighlightedInteractableView != null)
            {
                PlayerEvent.PlayerDeselectInteractEvent?.Invoke(new PlayerDeselectInteractEvent(PlayerView,
                    PlayerView.currentHighlightedInteractableView.gameObject));

                PlayerView.currentHighlightedInteractableView.RemoveHighlight();
                PlayerView.currentHighlightedInteractableView = null;
            }
        }
    }
}