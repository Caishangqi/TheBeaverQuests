using System;

namespace Core.Game.SceneManager.Events
{
    public struct SceneRequestLoadEvent
    {
        public String sceneName { get; set; }
        public SceneRequestLoadEvent(String sceneName)
        {
            this.sceneName = sceneName;
        }
    }
}