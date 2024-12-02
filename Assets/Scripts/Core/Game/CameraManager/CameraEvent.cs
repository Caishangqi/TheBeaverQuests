using System;
using Core.Game.CameraManager.Events;
using Unity.VisualScripting;

namespace Core.Game.CameraManager
{
    public static class CameraEvent
    {
        public static Action<CameraZoomInEvent> CameraZoomInEvent { get; set; }
        public static Action<CameraZoomOutEvent> CameraZoomOutEvent { get; set; }
        public static Action<CameraZoomEvent> CameraZoomEvent { get; set; }
    }
}