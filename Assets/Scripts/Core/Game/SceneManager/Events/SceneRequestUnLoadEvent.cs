using System;

namespace Core.Game.SceneManager.Events
{
    public struct SceneRequestUnLoadEvent
    {
        public String sceneName { get; set; }

        public SceneRequestUnLoadEvent(String sceneName)
        {
            this.sceneName = sceneName;
        }
    }
}