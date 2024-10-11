using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct PlateCoveredEvent
{
    //public Vector3 platePosition;   // 压力板的位置
    public GameObject coveredObject;  // 覆盖压力板的物体

    // public PlateCoveredEvent( GameObject coveredObject, Vector3 platePosition)
    // {
    //     this.platePosition = platePosition;
    //     this.coveredObject = coveredObject;
    // }
    public PlateCoveredEvent( GameObject coveredObject)
    {
        this.coveredObject = coveredObject;
    }
}
