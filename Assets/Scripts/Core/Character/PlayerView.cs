using System;
using System.Collections.Generic;
using System.Linq;
using Core.Character.Events;
using Core.Character.Handler;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

namespace Core.Character
{
    public class PlayerView : MonoBehaviour
    {
        #region PlayerView

        [SerializeField] public float speed = 5;
        [SerializeField] public float interactionRadius = 3f; // 交互范围半径
        [SerializeField] public LayerMask interactableLayerMask; // 可交互物品的图层

        #region Handler

        [SerializeField] public PlayerInputHandler playerInputHandler;
        [SerializeField] public PlayerActionHandler playerActionHandler;
        [SerializeField] public PlayerInteractHandler PlayerInteractHandler;

        #endregion


        [SerializeField] public SpriteRenderer spriteRenderer;

        [SerializeField] public Rigidbody2D rigidbody2D;

        [SerializeField] public BoxCollider2D playerCollider;

        [SerializeField] public PlayerSo playerData { get; set; }

        private Vector2 targetPosition;
        private bool isMoving;

        [SerializeField] public InteractableView currentHighlightedInteractableView;

        // Start is called before the first frame update
        public void Start()
        {
            playerInputHandler = new PlayerInputHandler(this);
            playerActionHandler = new PlayerActionHandler(this);
            PlayerInteractHandler = new PlayerInteractHandler(this);

            playerData = new PlayerSo();
            targetPosition = transform.position;
            isMoving = false;
        }

        // Update is called once per frame
        private void Update()
        {
            if (Input.GetMouseButtonDown(0)) // 0表示鼠标左键
            {
                //IsPointerOverUIElement();

                Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                targetPosition = new Vector2(mouseWorldPosition.x, mouseWorldPosition.y);
                isMoving = true;
            }

            PlayerInteractHandler.DetectInteractable();
        }

        private bool IsPointerOverUIElement()
        {
            PointerEventData eventData = new PointerEventData(EventSystem.current)
            {
                position = Input.mousePosition
            };
            List<RaycastResult> raycastResults = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventData, raycastResults);

            Debug.Log(raycastResults.Count);
            Debug.Log(raycastResults);

            return raycastResults.Count > 0;
        }

        private void FixedUpdate()
        {
            if (isMoving)
            {
                Vector2 currentPosition = rigidbody2D.position;
                Vector2 direction = (targetPosition - currentPosition).normalized;
                Vector2 newPosition = currentPosition + direction * speed * Time.fixedDeltaTime;

                // 当距离足够近时停止移动
                if (Vector2.Distance(newPosition, targetPosition) < 0.1f)
                {
                    newPosition = targetPosition;
                    isMoving = false;
                }

                rigidbody2D.MovePosition(newPosition);

                if (rigidbody2D.velocity.magnitude > 0.1f || isMoving)
                {
                    PlayerEvent.PlayerMoveEvent?.Invoke(new PlayerMoveEvent(newPosition, this));
                }
            }
        }


        // 可视化交互半径
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, interactionRadius);
        }
    }

    #endregion
}