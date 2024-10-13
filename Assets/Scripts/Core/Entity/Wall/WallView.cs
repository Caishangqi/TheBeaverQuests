using Core.Entity.Wall.Handler;
using UnityEngine;

namespace Core.Entity.Wall
{
    public class WallView : MonoBehaviour
    {
        #region WallView

        #region Handler

        public WallInteractionHandler wallInteractionHandler;

        #endregion

        #region Components

        [SerializeField] public SpriteRenderer spriteRenderer;

        [SerializeField] public Rigidbody2D rigidbody2D;

        [SerializeField] public BoxCollider2D wallCollider;
        public WallSo wallSo { get; set; }

        #endregion

        // Start is called before the first frame update
        void Start()
        {
            wallInteractionHandler = new WallInteractionHandler(this);
            wallSo = new WallSo();
        }

        // Update is called once per frame
        void Update()
        {
        }

        #endregion
    }
}