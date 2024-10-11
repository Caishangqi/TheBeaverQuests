using System;
using Core.Character.Events;
using Core.Character.Handler;
using UnityEngine;

namespace Core.Character
{
    public class PlayerView : MonoBehaviour
    {
        #region PlayerView Data

        [SerializeField] private float speed = 5;

        [SerializeField] PlayerInputHandler playerInputHandler;
        [SerializeField] PlayerSo playerSO;

        [SerializeField] SpriteRenderer spriteRenderer;

        [SerializeField] Rigidbody2D rigidbody2D;

        [SerializeField] BoxCollider2D playerCollider;

        private Vector2 movement;


        // Start is called before the first frame update
        public void Start()
        {
            playerInputHandler = new PlayerInputHandler(this);
            playerSO = new PlayerSo(this);
        }

        // Update is called once per frame
        private void Update()
        {
            movement.x = Input.GetAxis("Horizontal") * speed;
            movement.y = Input.GetAxis("Vertical") * speed;
            GetMousePosition();
        }

        private void GetMousePosition()
        {
            // Handle input for both mouse and touch
            if (Input.GetMouseButtonDown(0) || Input.touchCount > 0)
            {
                Vector2 screenPosition;

                // If there is a touch, get the position of the first touch
                if (Input.touchCount > 0)
                {
                    screenPosition = Input.GetTouch(0).position;
                }
                else
                {
                    screenPosition = Input.mousePosition;
                }

                // Convert the screen position of the click or touch to world position
                if (Camera.main != null) playerSO._mouseClickPosition = Camera.main.ScreenToWorldPoint(screenPosition);
                else
                {
                    throw new Exception("No main camera found");
                }
            }
        }


        private void FixedUpdate()
        {
            Debug.Log(playerSO._mouseClickPosition.normalized);
            rigidbody2D.position = Vector2.MoveTowards(rigidbody2D.position, playerSO._mouseClickPosition, speed * Time.fixedDeltaTime);
            if (rigidbody2D.velocity.magnitude > 0.1f)
            {
                PlayerEvent.PlayerMoveEvent?.Invoke(new PlayerMoveEvent(gameObject.transform.position, this));
            }
        }

        #endregion
    }
}