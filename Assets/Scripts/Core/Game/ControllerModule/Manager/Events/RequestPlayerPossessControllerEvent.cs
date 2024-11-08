using Core.Game.ControllerModule.Controller;
using UnityEngine;

namespace Core.Game.ControllerModule.Manager.Events
{
    public struct RequestPlayerPossessControllerEvent
    {
        public ControllerBase newController { get; set; }
        public GameObject instigator { get; set; }

        public RequestPlayerPossessControllerEvent(ControllerBase newController, GameObject instigator)
        {
            this.newController = newController;
            this.instigator = instigator;
        }
    }
}