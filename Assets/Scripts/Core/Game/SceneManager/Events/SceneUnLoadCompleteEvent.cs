using System;
using UnityEngine;

namespace Core.Game.SceneManager.Events
{
    public struct SceneUnLoadCompleteEvent
    {
        public AsyncOperation unloadScene { get; set; }
        public String sceneName { get; set; }

        public SceneUnLoadCompleteEvent(AsyncOperation unloadScene, String sceneName)
        {
            this.unloadScene = unloadScene;
            this.sceneName = sceneName;
        }
    }
}