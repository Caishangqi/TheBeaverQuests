using System.Collections;
using System.Collections.Generic;
using Core.Cube;
using UnityEngine;

public struct PlateTriggeredEvent
{
    public Vector3 platePosition; // 压力板的位置
    public GameObject instigator; // 触发事件的对象
    public CubeColor plateColor;
    public int plateId;

    public PlateTriggeredEvent(Vector3 platePosition, GameObject instigator, CubeColor plateColor, int plateId)
    {
        this.platePosition = platePosition;
        this.instigator = instigator;
        this.plateColor = plateColor;
        this.plateId = plateId;
    }
}