using System;
using UnityEngine;

namespace Core.Game.SceneManager.Events
{
    public struct SceneLoadingEvent
    {
        public AsyncOperation loadingScene { get; set; }
        public String sceneName { get; set; }

        public SceneLoadingEvent(AsyncOperation loadingScene, String sceneName)
        {
            this.loadingScene = loadingScene;
            this.sceneName = sceneName;
        }
    }
}