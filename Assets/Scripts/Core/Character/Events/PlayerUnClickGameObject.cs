namespace Core.Character.Events
{
    public struct PlayerUnClickGameObject
    {
        public PlayerView instigator;

        public PlayerUnClickGameObject(PlayerView instigator)
        {
            this.instigator = instigator;
        }
    }
}