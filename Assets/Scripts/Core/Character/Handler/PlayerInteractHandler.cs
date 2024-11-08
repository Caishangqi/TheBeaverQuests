using System.Linq;
using Core.Character.Events;
using Core.Game.PostProcessManager;
using Core.Game.PostProcessManager.Events;
using Core.UI.InteractButtonWidget;
using Core.UI.InteractButtonWidget.Event;
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
                if (interactable != PlayerView.currentHighlightedInteractableView)
                {
                    RemoveCurrentHighlight(); // 把现在这个取消高亮
                    PlayerView.currentHighlightedInteractableView = interactable;
                    PostProcessEvent.GameObjectHighlightEvent.Invoke(
                        new GameObjectHighlightEvent(PlayerView.currentHighlightedInteractableView.gameObject));

                    PlayerEvent.PlayerSelectInteractEvent?.Invoke(new PlayerSelectInteractEvent(PlayerView,
                        PlayerView.currentHighlightedInteractableView.gameObject));
                    // We only want the InteractButtonAppear when player do not have carrier object
                    if (PlayerView.playerData.carriedObj == null)
                    {
                        InteractButtonEvent.InteractButtonRequestEvent?.Invoke(new InteractButtonRequestEvent(
                            interactable.gameObject
                        ));
                    }
                }
            }
        }

        public void RemoveCurrentHighlight()
        {
            if (PlayerView.currentHighlightedInteractableView != null)
            {
                PlayerEvent.PlayerDeselectInteractEvent?.Invoke(new PlayerDeselectInteractEvent(PlayerView,
                    PlayerView.currentHighlightedInteractableView.gameObject));

                PostProcessEvent.GameObjectUnHighlightEvent.Invoke(
                    new GameObjectUnHighlightEvent(PlayerView.currentHighlightedInteractableView.gameObject));

                InteractButtonEvent.InteractButtonUnRequestEvent?.Invoke(
                    new InteractButtonUnRequestEvent(PlayerView.gameObject));
                PlayerView.currentHighlightedInteractableView = null;
            }
        }

        public void OnDestroy()
        {
            return;
        }
    }
}