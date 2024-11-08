using System.Collections.Generic;
using Core.Character;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Core.Game.TileMapManager.CubePlaceSelection.Events
{
    public struct RequestCubePlaceSelectionUIEvent
    {
        public PlayerView instigator;

        public RequestCubePlaceSelectionUIEvent(PlayerView instigator)
        {
            this.instigator = instigator;
        }
    }
}