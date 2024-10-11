using Core.Character.Events;
using UnityEngine;

namespace Core.Character.Handler
{
    public class PlayerInputHandler
    {
        public PlayerView PlayerView;

        public PlayerInputHandler(PlayerView playerView)
        {
            this.PlayerView = playerView;
            PlayerEvent.PlayerMoveEvent += OnPlayerMoveEvent;
        }

        public void OnPlayerMoveEvent(PlayerMoveEvent playerMoveEvent)
        {
            Debug.Log(playerMoveEvent.position.x + " " + playerMoveEvent.position.y);
        }

        ~PlayerInputHandler()
        {
            PlayerEvent.PlayerMoveEvent -= OnPlayerMoveEvent;
        }
    }
}