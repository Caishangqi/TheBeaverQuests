using UnityEngine;

namespace Core.Cube.Event
{
    public class PlayerNearbyEvent
    {
        public GameObject player;
        //public GameObject cube;

        public PlayerNearbyEvent(GameObject player)
        {
            this.player = player;
        }
    }
}