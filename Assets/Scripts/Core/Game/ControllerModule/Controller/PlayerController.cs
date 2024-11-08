using Core.Character;
using UnityEngine;

namespace Core.Game.ControllerModule.Controller
{
    public class PlayerController : ControllerBase
    {
        public PlayerController(PlayerView playerView) : base(playerView)
        {
        }

        public override void OnPossess()
        {
            Debug.Log("PlayerController Possessed");
        }

        public override void OnUnpossess()
        {
            Debug.Log("PlayerController Unpossessed");
        }

        public override void ProcessInput()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector3 targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                targetPosition.z = 0;

                //playerView.MoveTo(targetPosition);
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                //playerView.Jump();
            }
        }
    }
}