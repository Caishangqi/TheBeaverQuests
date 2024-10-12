using System;
using UnityEngine;

namespace Core.UI.Event
{
    public class UIEvent
    {
        public static Action<CubePickUpEvent> CubePickUpEvent;
        public static Action<CubePutDownEvent> CubePutDownEvent;
        public static Action<PauseUIDisplayEvent> PauseEvent;
    }
}