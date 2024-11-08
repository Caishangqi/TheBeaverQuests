using System.Collections.Generic;
using Core.AStarManager;
using Core.AStarManager.Events;
using Core.Character.Events;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Core.Character.Handler
{
    public class PlayerInputHandler
    {
        public PlayerView PlayerView;

        private List<Node> path = new List<Node>();

        public PlayerInputHandler(PlayerView playerView)
        {
            this.PlayerView = playerView;
            PlayerEvent.PlayerMoveEvent += OnPlayerMoveEvent;
        }

        public void OnPlayerMoveEvent(PlayerMoveEvent playerMoveEvent)
        {
            //Debug.Log(playerMoveEvent.position.x + " " + playerMoveEvent.position.y);
        }
        

        public void OnDestroy()
        {
            PlayerEvent.PlayerMoveEvent -= OnPlayerMoveEvent;
        }
    }
}