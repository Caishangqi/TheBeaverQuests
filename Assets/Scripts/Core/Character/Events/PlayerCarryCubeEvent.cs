using UnityEngine;

namespace Core.Character.Events
{
    public struct PlayerCarryCubeEvent
    {
        public PlayerView instigator { get; set; }
        public GameObject gameObject { get; set; }

        public PlayerCarryCubeEvent(PlayerView instigator, GameObject gameObject)
        {
            this.instigator = instigator;
            this.gameObject = gameObject;
        }
    }
}