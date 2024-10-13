using UnityEngine;

namespace Core.Character.Events
{
    public struct PlayerSelectInteractEvent
    {
        public PlayerView playerView { get; set; }
        public GameObject interactObject { get; set; }

        public PlayerSelectInteractEvent(PlayerView playerView, GameObject interactObject)
        {
            this.playerView = playerView;
            this.interactObject = interactObject;
        }
    }
}