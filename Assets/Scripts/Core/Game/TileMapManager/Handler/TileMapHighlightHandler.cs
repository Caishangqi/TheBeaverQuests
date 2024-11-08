using System.Collections.Generic;
using UnityEngine;

namespace Core.Game.TileMapManager.Handler
{
    public class TileMapHighlightHandler
    {
        public TileMapManagerView view;
        public List<Vector3Int> highlightedCells = new List<Vector3Int>();
        public List<Vector3Int> unhighlightedCells = new List<Vector3Int>();
        public Vector3Int previousPlayerCellPosition;
        
        public TileMapHighlightHandler(TileMapManagerView view)
        {
            this.view = view;
        }
        
        
    }
}