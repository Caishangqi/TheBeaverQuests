using UnityEngine;

namespace Core.Game.SettingManager.Events
{
    public struct RequestSettingWidgetEvent
    {
        public GameObject instigator { get; set; }
        public bool requestState { get; set; }

        public RequestSettingWidgetEvent(GameObject instigator, bool state)
        {
            this.instigator = instigator;
            requestState = state;
        }
    }
}