namespace Core.Game.CameraManager.Events
{
    public struct CameraZoomInEvent
    {
        public float deltaZoomIn { get; set; }
        public float targetZoom { get; set; }

        public CameraZoomInEvent(float deltaZoomIn, float targetZoom)
        {
            this.deltaZoomIn = deltaZoomIn;
            this.targetZoom = targetZoom;
        }
    }
}