using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlateEvent
{
    public static Action<PlateCoveredEvent> PlateCoveredEvent;
    public static Action<PlateTriggeredEvent> PlateTriggeredEvent;
    //public static Action<PlateCollidedEvent> PlateCollidedEvent;
    
    //[SerializeField] private SpriteRenderer spriteRenderer; // 用于可视化压力板状态

}
