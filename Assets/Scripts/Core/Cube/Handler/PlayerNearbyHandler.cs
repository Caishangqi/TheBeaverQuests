using Core.Cube.Event;
using UnityEngine;

namespace Core.Cube.Handler
{
    public class PlayerNearbyHandler
    {
        public CubeView cubeView;

        public PlayerNearbyHandler(CubeView cubeView)
        {
            this.cubeView = cubeView;
            CubeEvent.PlayerNearbyEvent += OnPlayerNearbyEvent;
        }
        
        public void OnPlayerNearbyEvent(PlayerNearbyEvent playerNearbyEvent)
        {
            //暂空
        }

        ~PlayerNearbyHandler()
        {
            CubeEvent.PlayerNearbyEvent -= OnPlayerNearbyEvent;
        }
    }
}