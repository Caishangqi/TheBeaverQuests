using System;
using Core.AStarManager.Events;

namespace Core.AStarManager
{
    public static class AStarEvents
    {
        public static Action<NodesMapCompletedEvent> NodesMapCompletedEvent;
        public static Action<PathRequestEvent> PathRequestEvent;
    }
}