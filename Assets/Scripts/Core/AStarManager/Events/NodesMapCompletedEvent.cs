using UnityEngine;

namespace Core.AStarManager.Events
{
    public struct NodesMapCompletedEvent
    {
        public BoundsInt maxBounds;

        public NodesMapCompletedEvent(BoundsInt maxBounds)
        {
            this.maxBounds = maxBounds;
        }
    }
}