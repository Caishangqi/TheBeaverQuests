using System;
using Core.UI.Event;
using UnityEngine;

namespace Core.UI
{
    public class UIView : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer spriteRenderer;  //绘制图标

        public void Pause()
        {
            Time.timeScale = 0;
        }
        
        //检测到触发这个事件 跳出按钮的sprite
        //UIEvent.PressCubeInteractWidgetEvent?.Invoke(new PressCubeInteractWidgetEvent(other.gameObject));
    }
}