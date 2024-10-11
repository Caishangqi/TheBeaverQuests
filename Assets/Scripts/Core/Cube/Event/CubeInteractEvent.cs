using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//打包interact中用到的参数
public struct CubeInteractEvent
{
    public GameObject cube;
    //public bool hasCovered;
    //private BoxCollider2D m_cubeCollider;
    //private BoxCollider2D m_plateCollider;

    // public CubeInteractEvent(GameObject cube, bool hasCovered)
    // {
    //     this.cube = cube;
    //     this.hasCovered = hasCovered;
    //     m_cubeCollider = cube.GetComponent<BoxCollider2D>();
    //     //m_plateCollider = plate.GetComponent<BoxCollider2D>();
    // }
    public CubeInteractEvent(GameObject cube)
    {
        this.cube = cube;
    }
}
