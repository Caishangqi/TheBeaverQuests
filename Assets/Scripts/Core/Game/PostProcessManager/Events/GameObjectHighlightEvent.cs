using UnityEngine;

namespace Core.Game.PostProcessManager.Events
{
    public struct GameObjectHighlightEvent
    {
        public GameObject gameObject { get; set; }

        public GameObjectHighlightEvent(GameObject gameObject)
        {
            this.gameObject = gameObject;
        }
    }
}