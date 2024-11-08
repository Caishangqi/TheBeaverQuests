using System;

namespace Core.Game.SceneManager.Events
{
    /// <summary>
    /// A Complete Scene Transfer event, including unload scene, load scene,
    /// and trigger LoadingWidget. User are expected to invoke this event if
    /// they want to execute any scene to scene operation
    /// </summary>
    public struct SceneRequestTransferEvent
    {
        public String targetScene { get; set; }
        public String unLoadedScene { get; set; }
        public bool automaticallyUnloadInstigatorScene { get; set; }
        public bool isTriggerLoadWidget { get; set; }

        public SceneRequestTransferEvent(String targetScene, String unLoadedScene,
            bool automaticallyUnloadInstigatorScene, bool isTriggerLoadWidget)
        {
            this.targetScene = targetScene;
            this.unLoadedScene = unLoadedScene;
            this.automaticallyUnloadInstigatorScene = automaticallyUnloadInstigatorScene;
            this.isTriggerLoadWidget = isTriggerLoadWidget;
        }
    }
}