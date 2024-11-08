using UnityEngine;

namespace Core.UI.InteractButtonWidget.Event
{
    public struct InteractButtonRequestEvent
    {
        public GameObject gameObject;

        public InteractButtonRequestEvent(GameObject gameObject)
        {
            this.gameObject = gameObject;
        }
    }
}