using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct PlateTriggeredEvent
{
    public Vector3 platePosition;   // 压力板的位置
    public GameObject instigator;   // 触发事件的对象

    public PlateTriggeredEvent(Vector3 platePosition, GameObject instigator)
    {
        this.platePosition = platePosition;
        this.instigator = instigator;
    }
}

