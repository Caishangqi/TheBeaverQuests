using Core.Character.Events;

namespace Core.Character.Handler
{
    public class PlayerActionHandler
    {
        private PlayerView playerView { get; set; }

        public PlayerActionHandler(PlayerView playerView)
        {
            this.playerView = playerView;
            PlayerEvent.PlayerCarryEvent += OnPlayerCarryEvent;
        }

        public void OnPlayerCarryEvent(PlayerCarryCubeEvent playerCarryEvent)
        {
            CubeView carriedCube = playerCarryEvent.cube;
            playerView.playerData.carriedObj = carriedCube;
            // 播放动画
            
            // Attach
            carriedCube.transform.SetParent(playerView.transform,true);
            // Disable Cube Collision
            
        }
    }
}