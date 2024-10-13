using UnityEngine;

namespace Core.UI.Handler
{
    public class CubePickUpHandler
    {
        public UIView uiView;
        
        public CubePickUpHandler(UIView uiView)
        {
            this.UIView =  uiView;
            PlateEvent.PlateTriggeredEvent += OnPlateTriggeredEvent;
        }

        public UIView UIView { get; set; }

        //处理trigger事件
        private void OnPlateTriggeredEvent(PlateTriggeredEvent PlateTriggeredEvent)
        {
            Debug.Log($"{PlateTriggeredEvent:instigator.name} has triggered the pressure plate.");
            // 在这里实现业务逻辑，例如打开门或激活机关
        }

        ~CubePickUpHandler()
        {
            PlateEvent.PlateTriggeredEvent -= OnPlateTriggeredEvent;
        }
        //订阅player对箱子的交互事件
    }
}