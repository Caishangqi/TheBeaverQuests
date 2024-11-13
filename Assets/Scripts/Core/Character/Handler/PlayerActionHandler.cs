using Core.Character.Events;
using Core.Game;
using Core.Game.AudioManager;
using Core.Game.AudioManager.Data;
using Core.Game.AudioManager.Events;
using Core.Game.Events;
using Core.Game.PostProcessManager;
using Core.Game.PostProcessManager.Events;
using Core.Game.TileMapManager.CubePlaceSelection;
using Core.Game.TileMapManager.CubePlaceSelection.Events;
using Core.Game.TileMapManager.PositionMarker;
using Core.Game.TileMapManager.PositionMarker.Events;
using Core.UI.Event;
using Core.UI.InteractButtonWidget;
using Core.UI.InteractButtonWidget.Event;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core.Character.Handler
{
    public class PlayerActionHandler
    {
        private PlayerView playerView { get; set; }

        public PlayerActionHandler(PlayerView playerView)
        {
            this.playerView = playerView;
            PlayerEvent.PlayerCarryEvent += OnPlayerCarryEvent;
            PlayerEvent.PlayerLayDownEvent += OnPlayerLayDownEvent;
            PositionMarkerEvent.PositionMarkerClickEvent += OnPositionMarkerClickEvent;
            InteractButtonEvent.InteractButtonClickEvent += OnInteractButtonClickEvent;
        }

        public void RequestCubePlaceSelectionUI()
        {
            CubePlaceSelectionEvent.RequestCubePlaceSelectionUIEvent.Invoke(
                new RequestCubePlaceSelectionUIEvent(playerView));
        }

        private void OnInteractButtonClickEvent(InteractButtonClickEvent interactButtonClickEvent)
        {
            if (playerView.playerData.carriedObj == null) // 如果玩家携带Cube
            {
                PlayerEvent.PlayerCarryCubeAnimeEvent?.Invoke(new PlayerCarryCubeAnimeEvent());
                Debug.Log("Invoke PlayerCarryCubeEvent ->");
                PlayerEvent.PlayerCarryEvent?.Invoke(new PlayerCarryCubeEvent(playerView,
                    interactButtonClickEvent.view.targetGameObject));
            }
        }
        
        // 当玩家触碰可选方块位置后触发的事件
        private void OnPositionMarkerClickEvent(PositionMarkerClickEvent positionMarkerClickEvent)
        {
            if (playerView.playerData.carriedObj) // 如果玩家携带Cube
            {
                PlayerEvent.PlayerLayDownEvent?.Invoke(new PlayerLayDownCubeEvent(playerView,
                    playerView.playerData.carriedObj, positionMarkerClickEvent.targetPosition));
            }
        }

        public void OnPlayerLayDownEvent(PlayerLayDownCubeEvent playerLayDownEvent)
        {
            CubeView carriedCube = playerLayDownEvent.cube;
            playerView.transform.DetachChildren();
            carriedCube.cubeCollider.enabled = true;
            carriedCube.rigidbody2D.isKinematic = true;
            SceneManager.MoveGameObjectToScene(carriedCube.gameObject, carriedCube.originalScene);
            carriedCube.IsHeld = false;
            carriedCube.gameObject.transform.position = playerLayDownEvent.layDownPosition;
            // UnHighlight
            PostProcessEvent.GameObjectUnHighlightEvent?.Invoke(new GameObjectUnHighlightEvent(carriedCube.gameObject));
            // broadcast the cube collider enable event (Update the NavMesh)
            GameGenericEvent.ColliderEnableEvent.Invoke(new ColliderEnableEvent(carriedCube.gameObject));
            AudioEvent.PlaySoundEvent?.Invoke(new PlaySoundEvent(playerView.gameObject,ESound.ENTITY_PLAYER_DROP_CUBE));
            playerView.playerData.carriedObj = null;
        }

        public void OnPlayerCarryEvent(PlayerCarryCubeEvent playerCarryEvent)
        {
            CubeView carriedCube = playerCarryEvent.gameObject.GetComponent<CubeView>();
            carriedCube.rigidbody2D.isKinematic = true;
            carriedCube.cubeCollider.enabled = false;

            carriedCube.IsHeld = true;
            playerView.playerData.carriedObj = carriedCube;
            // broadcast the cube collider disable event (Update the NavMesh)
            GameGenericEvent.ColliderDisableEvent.Invoke(new ColliderDisableEvent(carriedCube.gameObject));
            // set the offset that make cube above player
            carriedCube.transform.position = playerView.transform.position + new Vector3(0, 1, 0);

            // Attach
            carriedCube.transform.SetParent(playerView.transform, true);
            // Disable Cube Collision

            // Add post highlight effect
            PostProcessEvent.GameObjectHighlightEvent?.Invoke(new GameObjectHighlightEvent(carriedCube.gameObject));
        }


        public void OnDestroy()
        {
            PlayerEvent.PlayerCarryEvent -= OnPlayerCarryEvent;
            PlayerEvent.PlayerLayDownEvent -= OnPlayerLayDownEvent;
            PositionMarkerEvent.PositionMarkerClickEvent -= OnPositionMarkerClickEvent;
        }
    }
}