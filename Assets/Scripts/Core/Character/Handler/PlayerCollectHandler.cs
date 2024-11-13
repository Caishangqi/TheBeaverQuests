using Core.Game.SceneManager;
using Core.Game.SceneManager.Events;
using UnityEngine;

namespace Core.Character.Handler
{
    public class PlayerCollectHandler
    {
        public PlayerView PlayerView;

        public GameObject newDetectArea;

        public PlayerCollectHandler(PlayerView playerView)
        {
            this.PlayerView = playerView;

            //SceneEvent.SceneLoadCompleteEvent += OnSceneLoadCompleteEvent;
        }

        // private void OnSceneLoadCompleteEvent(SceneLoadCompleteEvent obj)
        // {
        //     newDetectArea = GameObject.FindGameObjectWithTag("DetectArea");
        //     PlayerView.detectArea = newDetectArea;
        //     Debug.Log(PlayerView.detectArea);
        // }
    }
}