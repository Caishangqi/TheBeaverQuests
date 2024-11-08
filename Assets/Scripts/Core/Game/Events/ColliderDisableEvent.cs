using UnityEngine;

namespace Core.Game.Events
{
    public struct ColliderDisableEvent
    {
        public GameObject gameObject;

        public ColliderDisableEvent(GameObject gameObject)
        {
            this.gameObject = gameObject;
        }
    }
}