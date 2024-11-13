using Core.Character;

namespace Core.Game.ControllerModule.Controller
{
    public abstract class ControllerBase
    {
        protected PlayerView playerView { get; set; }

        public ControllerBase(PlayerView playerView)
        {
            this.playerView = playerView;
        }

        // 当控制器获得控制权时调用
        public virtual void OnPossess()
        {
        }

        // 当控制器失去控制权时调用
        public virtual void OnUnpossess()
        {
        }

        // 子类需要实现的逻辑更新方法
        public abstract void ProcessInput();
    }
}