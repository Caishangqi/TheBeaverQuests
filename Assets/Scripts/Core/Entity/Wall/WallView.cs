using System;
using System.Collections;
using System.Collections.Generic;
using Core.Entity.Wall.Handler;
using Core.Game;
using Core.Game.Events;
using UnityEngine;

namespace Core.Entity.Wall
{
    public class WallView : MonoBehaviour
    {
        #region WallView

        [SerializeField] public int wallIndex = 1;
        [SerializeField] public List<int> requiredIndex;
        [SerializeField] public List<int> containedIndex;

        #region Handler

        public WallInteractionHandler wallInteractionHandler;

        #endregion


        #region Components

        [SerializeField] public SpriteRenderer spriteRenderer;

        [SerializeField] public Rigidbody2D rigidbody2D;

        [SerializeField] public BoxCollider2D wallCollider;

        public WallSo wallSo { get; set; }
        
        private Animator wallAnimator;

        #endregion

        // Start is called before the first frame update
        void Start()
        {
            wallInteractionHandler = new WallInteractionHandler(this);
            wallSo = new WallSo();
            wallAnimator = GetComponent<Animator>();
        }

        // Update is called once per frame
        void Update()
        {
        }

        public void DisableWallCollision()
        {
            GameGenericEvent.ColliderDisableEvent.Invoke(new ColliderDisableEvent(this.gameObject));
            wallAnimator.SetBool("hasBreaking", true);
            wallCollider.enabled = false;
            rigidbody2D.isKinematic = true;
            //spriteRenderer.enabled = false;
            StartCoroutine(EnableSpriteRendererAfterDelay(1.0f));
        }

        private IEnumerator EnableSpriteRendererAfterDelay(float delay)
        {
            // 等待指定的延迟时间
            yield return new WaitForSeconds(delay);

            // guanbi spriteRenderer
            spriteRenderer.enabled = false;
        }

        public void EnableWallCollision()
        {
            wallAnimator.SetBool("hasBreaking", false);
            if (wallCollider)
            {
                wallCollider.enabled = true;
                GameGenericEvent.ColliderEnableEvent.Invoke(new ColliderEnableEvent(this.gameObject));
            }
            rigidbody2D.isKinematic = true;
            spriteRenderer.enabled = true;
        }

        private void OnDestroy()
        {
            wallInteractionHandler.OnDestroy();
        }

        #endregion
    }
}