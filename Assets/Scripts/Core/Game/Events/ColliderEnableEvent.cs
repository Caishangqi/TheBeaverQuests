using UnityEngine;

namespace Core.Game.Events
{
    public struct ColliderEnableEvent
    {
        public GameObject gameObject;

        public ColliderEnableEvent(GameObject gameObject)
        {
            this.gameObject = gameObject;
        }
    }
}