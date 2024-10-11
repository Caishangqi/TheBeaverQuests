using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CubeInteractHandler
{
    public CubeView CubeView;

    public CubeInteractHandler(CubeView CubeView)
    {
        this.CubeView = CubeView;
        //
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
