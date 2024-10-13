using System;
using Core.Character.Events;

namespace Core.Character
{
    public static class PlayerEvent
    {
        public static Action<PlayerMoveEvent> PlayerMoveEvent;
        public static Action<PlayerCarryCubeEvent> PlayerCarryEvent;

        public static Action<PlayerSelectInteractEvent> PlayerSelectInteractEvent;
        public static Action<PlayerDeselectInteractEvent> PlayerDeselectInteractEvent;
    }
}