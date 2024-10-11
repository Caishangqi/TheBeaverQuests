using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;


public static class CubeEvent 
{
    public static UnityAction<CubeMoveEvent> CubeMoveEvent;
    
    public static UnityAction<CubeInteractEvent> CubeInteractEvent;
    
}
