using System;
using Core.Game.TileMapManager.CubePlaceSelection.Events;
using Core.Game.TileMapManager.PositionMarker.Events;

namespace Core.Game.TileMapManager.PositionMarker
{
    public static class PositionMarkerEvent
    {
        public static Action<PositionMarkerClickEvent> PositionMarkerClickEvent;
    }
}