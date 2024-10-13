using UnityEngine;

namespace Core.Character.Events
{
    public struct PlayerDeselectInteractEvent
    {
        public PlayerView playerView { get; set; }
        public GameObject interactObject { get; set; }

        public PlayerDeselectInteractEvent(PlayerView playerView, GameObject interactObject)
        {
            this.playerView = playerView;
            this.interactObject = interactObject;
        }
    }
}