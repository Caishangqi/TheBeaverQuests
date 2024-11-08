using System;
using System.Collections.Generic;
using UnityEngine;

namespace Core.AStarManager.Events
{
    
    public struct PathRequestEvent
    {
        public Vector2 StartPosition;
        public Vector2 TargetPosition;
        public Action<List<Node>> OnPathFound;

        public PathRequestEvent(Vector2 startPosition, Vector2 targetPosition, Action<List<Node>> onPathFound)
        {
            StartPosition = startPosition;
            TargetPosition = targetPosition;
            OnPathFound = onPathFound;
        }
    }
}