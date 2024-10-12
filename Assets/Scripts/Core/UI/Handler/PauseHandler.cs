using Core.UI.Event;
using UnityEngine;

namespace Core.UI.Handler
{
    public class PauseHandler
    {
        public UIView   UIView;

        public PauseHandler(UIView UIView)
        {
            this.UIView = UIView;
            UIEvent.PauseEvent += OnPauseEvent;
        }

        private void OnPauseEvent(PauseEvent pauseEvent)
        {
            //函数实现
        }

        ~PauseHandler()
        {
            UIEvent.PauseEvent -= OnPauseEvent;
        }
    }
}