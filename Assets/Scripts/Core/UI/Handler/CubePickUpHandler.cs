using Core.Cube.Event;
using Core.UI.Event;
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
            UIEvent.PressCubeInteractWidgetEvent += OnPressCubeInteractWidgetEvent;
        }

        private void OnPlayerNearbyEvent(PlayerNearbyEvent playerNearbyEvent)
        {
            
        }

        private void OnPressCubeInteractWidgetEvent(PressCubeInteractWidgetEvent pressCubeInteractWidgetEvent)
        {
            
        }

        ~CubePickUpHandler()
        {
            CubeEvent.PlayerNearbyEvent -= OnPlayerNearbyEvent;
        }
        //订阅player对箱子的交互事件
    }
}