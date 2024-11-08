using System;
using Core.Character;
using Core.Character.Events;
using UnityEngine;

namespace Core.Entity.Spawn
{
    public class SpawnPointView : MonoBehaviour
    {
        #region SpawnPointView

        #region Components

        [SerializeField] public SpriteRenderer spriteRenderer;

        [SerializeField] public Rigidbody2D rigidbody2D;

        [SerializeField] public BoxCollider2D spawnPointCollider;

        #endregion

        #region Handler

        #endregion

        private void Start()
        {
            PlayerEvent.PlayerSetLocationEvent?.Invoke(new PlayerSetLocationEvent(gameObject.transform.position));
            rigidbody2D.isKinematic = true;
            spawnPointCollider.enabled = false;
            spriteRenderer.enabled = false;
        }

        #endregion
    }
}