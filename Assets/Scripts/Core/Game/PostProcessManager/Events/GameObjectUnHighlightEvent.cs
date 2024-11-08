using UnityEngine;

namespace Core.Game.PostProcessManager.Events
{
    public struct GameObjectUnHighlightEvent
    {
        public GameObject gameObject { get; set; }

        public GameObjectUnHighlightEvent(GameObject gameObject)
        {
            this.gameObject = gameObject;
        }
    }
}