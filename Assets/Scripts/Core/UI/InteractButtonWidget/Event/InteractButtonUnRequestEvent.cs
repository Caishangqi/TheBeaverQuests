using UnityEngine;

namespace Core.UI.InteractButtonWidget.Event
{
    public struct InteractButtonUnRequestEvent
    {
        public GameObject instigator { get; set; }

        public InteractButtonUnRequestEvent(GameObject instigator)
        {
            this.instigator = instigator;
        }
    }
}