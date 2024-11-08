using UnityEngine;

namespace Core.Game.ControllerModule.Manager.Events
{
    public struct RequestPlayerUnpossessControllerEvent
    {
        public GameObject instigator { get; set; }

        public RequestPlayerUnpossessControllerEvent(GameObject instigator)
        {
            this.instigator = instigator;
        }
    }
}