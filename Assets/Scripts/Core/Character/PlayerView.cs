using System;
using Core.Character.Events;
using Core.Character.Handler;
using UnityEngine;

namespace Core.Character
{
    public class PlayerView : MonoBehaviour
    {
        #region PlayerView

        [SerializeField] public float speed = 5;


        #region Handler

        [SerializeField] public PlayerInputHandler playerInputHandler;
        [SerializeField] public PlayerActionHandler playerActionHandler;

        #endregion


        [SerializeField] public SpriteRenderer spriteRenderer;

        [SerializeField] public Rigidbody2D rigidbody2D;

        [SerializeField] public BoxCollider2D playerCollider;

        [SerializeField] public PlayerSo playerData { get; set; }

        private Vector2 targetPosition;
        private bool isMoving;

        // Start is called before the first frame update
        public void Start()
        {
            playerInputHandler = new PlayerInputHandler(this);
            playerActionHandler = new PlayerActionHandler(this);
            playerData = new PlayerSo();
            targetPosition = transform.position;
            isMoving = false;
        }

        // Update is called once per frame
        private void Update()
        {
            if (Input.GetMouseButtonDown(0)) // 0表示鼠标左键
            {
                Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                targetPosition = new Vector2(mouseWorldPosition.x, mouseWorldPosition.y);
                isMoving = true;
            }
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
    }

    #endregion
}