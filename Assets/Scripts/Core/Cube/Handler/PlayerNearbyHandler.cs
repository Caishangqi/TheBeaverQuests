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
            Vector3 cubePosition = playerNearbyEvent.cube.transform.position;
            Vector3 playerPosition = playerNearbyEvent.player.transform.position;
            
            float distance = Vector3.Distance(cubePosition, playerPosition);
            
             // 打印距离（或根据距离执行其他操作
             Debug.Log("Distance between Cube and Player: " + distance);
        }

        ~PlayerNearbyHandler()
        {
            CubeEvent.PlayerNearbyEvent -= OnPlayerNearbyEvent;
        }
    }
}