using System;

namespace Core.Game.CameraManager.Handler
{
    public class CameraMovementHandler
    {
        public CameraView cameraView { get; set; }

        public CameraMovementHandler(CameraView cameraView)
        {
            this.cameraView = cameraView;
        }

        public float GetCameraPercentZoom()
        {
            return Math.Clamp(cameraView.mainCamera.orthographicSize / (cameraView.maxZoom - cameraView.minZoom), 0, 1);
        }

        public void SetCameraPercentZoom(float cameraPercentZoom)
        {
            cameraView.mainCamera.orthographicSize = (cameraView.maxZoom - cameraView.minZoom) * cameraPercentZoom;
        }
    }
}