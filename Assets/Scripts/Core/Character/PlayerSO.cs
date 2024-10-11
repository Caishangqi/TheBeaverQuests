using UnityEngine;

namespace Core.Character
{
    public class PlayerSo
    {
        PlayerView playerView;
        public Vector2 _mouseClickPosition = Vector2.zero;
        public bool _isMoving = false;
        public PlayerSo(PlayerView playerView)
        {
            this.playerView = playerView;
        }
    }
}