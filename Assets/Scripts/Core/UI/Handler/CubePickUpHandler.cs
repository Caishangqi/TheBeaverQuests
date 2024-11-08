using Core.Character;
using Core.Cube.Event;
using Core.UI.Event;
using UnityEngine;
using UnityEngine.UI;

namespace Core.UI.Handler
{
    public class CubePickUpHandler
    {
        //public UIView   UIView;
        public CubeInteractUIView CubeInteractUIView { get; set; }
        
        public CubePickUpHandler(CubeInteractUIView cubeInteractUIView)
        {
            this.CubeInteractUIView = cubeInteractUIView;
            //CubeEvent.PlayerNearbyEvent += OnPlayerNearbyEvent;
            UIEvent.PressCubeInteractWidgetEvent += OnPressCubeInteractWidgetEvent;
        }

        public void OnPressCubeInteractWidgetEvent(PressCubeInteractWidgetEvent pressCubeInteractWidgetEvent)
        {
            // 在物体旁边显示按钮
            PositionButtonNearInteractable(pressCubeInteractWidgetEvent);
        }

        // 将按钮的位置移动到Interactable物体旁边
        private void PositionButtonNearInteractable(PressCubeInteractWidgetEvent pressCubeInteractWidgetEvent)
        {
            InteractableView interactable = pressCubeInteractWidgetEvent.CurrentInteractable;
            // 获取Interactable物体的世界坐标
            Vector3 objectWorldPosition = interactable.transform.position;
            //Debug.Log("Object World Position: " + objectWorldPosition); // 调试信息

            Canvas canvas = pressCubeInteractWidgetEvent.Canvas;
            Vector3 offset = pressCubeInteractWidgetEvent.Offset;
            
            // 计算加上偏移的世界坐标
            Vector3 offsetPosition = objectWorldPosition + canvas.transform.TransformVector((Vector3)offset);

            Button pickUpButton = pressCubeInteractWidgetEvent.PickUpButton;
            
            // 将按钮的位置设置为Interactable的世界位置加上偏移
            pickUpButton.GetComponent<RectTransform>().position = offsetPosition;
            
            // 输出按钮的最终位置
            //Debug.Log("PickUpButton Position: " + pickUpButton.GetComponent<RectTransform>().position); // 调试信息
            
            pickUpButton.gameObject.SetActive(true); // 使按钮可见
            pickUpButton.interactable = true; // 启用按钮交互
            //Debug.Log("offsetPosition" );
        }
        
        ~CubePickUpHandler()
        {
            //CubeEvent.PlayerNearbyEvent -= OnPlayerNearbyEvent;
            UIEvent.PressCubeInteractWidgetEvent -= OnPressCubeInteractWidgetEvent;
        }
        //订阅player对箱子的交互事件
    }
}