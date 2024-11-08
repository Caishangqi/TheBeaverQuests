using System;
using Core.Game.ControllerModule.Manager.Events;
using Unity.VisualScripting;

namespace Core.Game.ControllerModule.Manager
{
    public static class ControllerEvent
    {
        public static Action<RequestPlayerUnpossessControllerEvent> RequestPlayerUnpossessControllerEvent;
        public static Action<RequestPlayerPossessControllerEvent> RequestPlayerPossessControllerEvent;
    }
}