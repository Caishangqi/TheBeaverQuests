using Core.Game;
using Core.Game.Events;
using UnityEngine;

namespace Miscellaneous.Visual
{
    [ExecuteInEditMode]
    public class DynamicSortingOrder : MonoBehaviour
    {
        private SpriteRenderer spriteRenderer;
        private bool needUpdate = false;

        void Start()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();

            if (Application.isPlaying)
            {
                GameGenericEvent.ColliderDisableEvent += OnColliderDisableEvent;
                GameGenericEvent.ColliderEnableEvent += OnColliderEnableEvent;
            }
        }

        private void OnDestroy()
        {
            if (Application.isPlaying)
            {
                GameGenericEvent.ColliderDisableEvent -= OnColliderDisableEvent;
                GameGenericEvent.ColliderEnableEvent -= OnColliderEnableEvent;
            }
        }

        private void OnColliderEnableEvent(ColliderEnableEvent colliderEnableEvent)
        {
            if (colliderEnableEvent.gameObject == gameObject)
            {
                needUpdate = false;
                UpdateSortingOrder();
            }
        }

        private void OnColliderDisableEvent(ColliderDisableEvent colliderDisableEvent)
        {
            if (colliderDisableEvent.gameObject == gameObject)
                needUpdate = true;
        }

        void Update()
        {
            if (!Application.isPlaying)
            {
                UpdateSortingOrder();
            }
            else if (needUpdate)
            {
                UpdateSortingOrder();
            }
        }

        void UpdateSortingOrder()
        {
            spriteRenderer.sortingOrder = Mathf.RoundToInt(-transform.position.y * 100);
        }
    }
}