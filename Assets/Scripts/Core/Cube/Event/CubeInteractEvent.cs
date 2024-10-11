using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//打包interact中用到的参数
public struct CubeInteractEvent
{
    public GameObject cube;
    private bool hasCovered;
    private BoxCollider2D m_cubeCollider;
    private BoxCollider2D m_plateCollider;

    //private float m_PositionThreshold;
    //private float m_SizeThreshold;
}
