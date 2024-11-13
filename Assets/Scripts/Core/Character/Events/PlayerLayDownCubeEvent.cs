using UnityEngine;

namespace Core.Character.Events
{
    public struct PlayerLayDownCubeEvent
    {
        public PlayerView instigator { get; set; }
        public CubeView cube { get; set; }
        public Vector3 layDownPosition { get; set; }

        public PlayerLayDownCubeEvent(PlayerView instigator, CubeView cube, Vector3 layDownPosition)
        {
            this.instigator = instigator;
            this.cube = cube;
            this.layDownPosition = layDownPosition;
        }
    }
}