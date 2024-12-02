using System;
using Core.Game.SettingManager.Events;

namespace Core.Game.SettingManager
{
    public static class SettingManagerEvent
    {
        public static Action<RequestSettingWidgetEvent> RequestSettingWidgetEvent;
    }
}