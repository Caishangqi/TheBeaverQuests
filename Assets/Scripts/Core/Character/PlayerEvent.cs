using System;
using Core.Character.Events;

namespace Core.Character
{
    public static class PlayerEvent
    {
        public static Action<PlayerMoveEvent> PlayerMoveEvent;
        public static Action<PlayerCarryCubeEvent> PlayerCarryEvent;
        public static Action<PlayerLayDownCubeEvent> PlayerLayDownEvent;

        public static Action<PlayerSelectInteractEvent> PlayerSelectInteractEvent;
        public static Action<PlayerDeselectInteractEvent> PlayerDeselectInteractEvent;
        public static Action<PlayerSetLocationEvent> PlayerSetLocationEvent;
        public static Action<PlayerUnClickGameObject> PlayerUnClickGameObject;
        public static Action<PlayerCarryCubeAnimeEvent> PlayerCarryCubeAnimeEvent;
    }
}