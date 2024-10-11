using System;
using Core.Character.Events;
using Core.Character.Handler;
using UnityEngine;

namespace Core.Character
{
    public class PlayerView : MonoBehaviour
    {
        [SerializeField] private float speed = 5;

        [SerializeField] PlayerInputHandler playerInputHandler;

        [SerializeField] SpriteRenderer spriteRenderer;

        [SerializeField] Rigidbody2D rigidbody2D;

        [SerializeField] BoxCollider2D playerCollider;

        private Vector2 movement;

        // Start is called before the first frame update
        public void Start()
        {
            playerInputHandler = new PlayerInputHandler(this);
        }

        // Update is called once per frame
        private void Update()
        {
            movement.x = Input.GetAxis("Horizontal") * speed;
            movement.y = Input.GetAxis("Vertical") * speed;
        }

        private void FixedUpdate()
        {
            
            rigidbody2D.velocity = movement * speed;
            if (rigidbody2D.velocity.magnitude > 0.1f)
            {
                PlayerEvent.PlayerMoveEvent?.Invoke(new PlayerMoveEvent(gameObject.transform.position, this));
            }
        }
    }
}