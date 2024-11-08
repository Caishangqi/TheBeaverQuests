using System;
using Core.Game.Events;

namespace Core.Game
{
    public static class GameGenericEvent
    {
        public static Action<ColliderEnableEvent> ColliderEnableEvent;
        public static Action<ColliderDisableEvent> ColliderDisableEvent;
    }
}