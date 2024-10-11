using UnityEngine;

namespace Core.Character.Events
{
    public struct PlayerMoveEvent
    {
        public Vector2 position;
        public PlayerView targetPlayerView;

        public PlayerMoveEvent(Vector2 position, PlayerView targetPlayerView)
        {
            this.position = position;
            this.targetPlayerView = targetPlayerView;
        }
    }
}