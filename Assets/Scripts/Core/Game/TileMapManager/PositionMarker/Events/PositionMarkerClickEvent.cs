using UnityEngine;

namespace Core.Game.TileMapManager.PositionMarker.Events
{
    public struct PositionMarkerClickEvent
    {
        public Vector3 targetPosition;
        public PositionMarkerView positionMarkerView;

        public PositionMarkerClickEvent(Vector3 targetPosition, PositionMarkerView positionMarkerView)
        {
            this.targetPosition = targetPosition;
            this.positionMarkerView = positionMarkerView;
        }
    }
}