namespace Core.Character.Events
{
    public struct PlayerCarryCubeEvent
    {
        public PlayerView instigator { get; set; }
        public CubeView cube { get; set; }
    }
}