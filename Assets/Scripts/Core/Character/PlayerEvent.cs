using Core.Character.Events;
using UnityEngine.Events;

namespace Core.Character
{
    public static class PlayerEvent
    {
        public static UnityAction<PlayerMoveEvent> PlayerMoveEvent;
    }
}