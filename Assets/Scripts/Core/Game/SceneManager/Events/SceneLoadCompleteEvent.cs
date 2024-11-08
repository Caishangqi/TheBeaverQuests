using System;
using UnityEngine;

namespace Core.Game.SceneManager.Events
{
    public struct SceneLoadCompleteEvent
    {
        public AsyncOperation loadScene { get; set; }
        public String sceneName { get; set; }

        public SceneLoadCompleteEvent(AsyncOperation loadScene, String sceneName)
        {
            this.loadScene = loadScene;
            this.sceneName = sceneName;
        }
    }
}