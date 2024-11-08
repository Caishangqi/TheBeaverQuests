using System;
using UnityEngine;

namespace Core.Game.SceneManager.Events
{
    public struct SceneUnloadingEvent
    {
        public AsyncOperation unloadingScene { get; set; }
        public String sceneName { get; set; }

        public SceneUnloadingEvent(AsyncOperation unloadingScene, String sceneName)
        {
            this.unloadingScene = unloadingScene;
            this.sceneName = sceneName;
        }
    }
}