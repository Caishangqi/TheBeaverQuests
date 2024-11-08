namespace Core.Game.CameraManager.Events
{
    public struct CameraZoomOutEvent
    {
        public float deltaZoomOut { get; set; }
        public float targetZoom { get; set; }
        public CameraZoomOutEvent(float deltaZoomOut, float targetZoom)
        {
            this.deltaZoomOut = deltaZoomOut;
            this.targetZoom = targetZoom;
        }
    }
}