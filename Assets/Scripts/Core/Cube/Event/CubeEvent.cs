using System;
using System.Collections;
using System.Collections.Generic;
using Core.Cube.Event;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;


public static class CubeEvent 
{
    public static Action<CubeMoveEvent> CubeMoveEvent;
    
    public static Action<CubeInteractEvent> CubeInteractEvent;
    
    public static Action<PlayerNearbyEvent> PlayerNearbyEvent;
    
}
