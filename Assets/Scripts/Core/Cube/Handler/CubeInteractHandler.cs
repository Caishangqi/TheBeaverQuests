using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CubeInteractHandler
{
    public CubeView CubeView;

    public CubeInteractHandler(CubeView CubeView)
    {
        this.CubeView = CubeView;
        //还需要订阅player的interact事件
        CubeEvent.CubeInteractEvent += OnCubeInteractEvent;
    }

    public void OnCubeInteractEvent(CubeInteractEvent CubeInteractEvent)
    {

        //Debug.Log(CubeInteractEvent.hasCovered + " " + CubeInteractEvent.cube);
        Debug.Log( CubeInteractEvent.cube);
    }

    ~CubeInteractHandler()
    {
        CubeEvent.CubeInteractEvent -= OnCubeInteractEvent;
    }
}
