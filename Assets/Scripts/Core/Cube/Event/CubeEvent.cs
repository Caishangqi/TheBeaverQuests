using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;


public static class CubeEvent 
{
    public static Action<CubeMoveEvent> CubeMoveEvent;
    
    public static Action<CubeInteractEvent> CubeInteractEvent;
    
}
