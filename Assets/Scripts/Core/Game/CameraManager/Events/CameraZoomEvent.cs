namespace Core.Game.CameraManager.Events
{
    public struct CameraZoomEvent
    {
        public CameraView CameraView { get; set; }
        public float currentZoom { get; set; }

        public CameraZoomEvent(float currentZoom, CameraView cameraView)
        {
            this.currentZoom = currentZoom;
            this.CameraView = cameraView;
        }
    }
}