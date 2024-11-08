using UnityEngine;

namespace Core.Character.Events
{
    public struct PlayerSetLocationEvent
    {
        
        public Vector2 position { get; set; }

        public PlayerSetLocationEvent(Vector2 position)
        {
            
            this.position = position;
        }
    }
}