using Core.Cube.Event;
using UnityEngine;

namespace Core.UI.Handler
{
    public class CubePickUpHandler
    {
        public UIView   UIView;
        
        public CubePickUpHandler(UIView UIView)
        {
            this.UIView = UIView;
            CubeEvent.PlayerNearbyEvent += OnPlayerNearbyEvent;
        }

        private void OnPlayerNearbyEvent(PlayerNearbyEvent playerNearbyEvent)
        {
            
        }

        ~CubePickUpHandler()
        {
            CubeEvent.PlayerNearbyEvent -= OnPlayerNearbyEvent;
        }
        //订阅player对箱子的交互事件
    }
}