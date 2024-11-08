namespace Core.Game.TileMapManager.CubePlaceSelection.Events
{
    public struct DisplayCubePlaceSelectionUIEvent
    {
        public CubePlaceSelectionView cubePlaceSelectionView;

        public DisplayCubePlaceSelectionUIEvent(CubePlaceSelectionView cubePlaceSelectionView)
        {
            this.cubePlaceSelectionView = cubePlaceSelectionView;
        }
    }
}