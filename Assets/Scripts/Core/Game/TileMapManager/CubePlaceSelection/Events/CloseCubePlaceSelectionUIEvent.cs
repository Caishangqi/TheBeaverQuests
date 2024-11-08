namespace Core.Game.TileMapManager.CubePlaceSelection.Events
{
    public struct CloseCubePlaceSelectionUIEvent
    {
        public CubePlaceSelectionView view;

        public CloseCubePlaceSelectionUIEvent(CubePlaceSelectionView view)
        {
            this.view = view;
        }
    }
}