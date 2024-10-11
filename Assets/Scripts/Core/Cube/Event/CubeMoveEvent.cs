using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//
public struct CubeMoveEvent
{
    public Vector2 position;
    
    public Vector2 velocity;

    public CubeMoveEvent(Vector2 position, Vector2 velocity)
    {
        this.position = position;
        this.velocity = velocity;
    }
}
