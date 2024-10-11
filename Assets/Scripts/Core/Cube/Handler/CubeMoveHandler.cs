using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CubeMoveHandler
{
    //public CubeMoveEvent CubeMoveEvent;
    public CubeView CubeView;

    public CubeMoveHandler(CubeView CubeView)
    {
        this.CubeView = CubeView;
        CubeEvent.CubeMoveEvent += OnCubeMoveEvent;
        
    }

    public void OnCubeMoveEvent(CubeMoveEvent cubeMoveEvent)
    {
        // 计算新的位置：m_position += m_velocity * Time.deltaTime
        cubeMoveEvent.position += cubeMoveEvent.velocity * Time.deltaTime;

        // 将计算出的新位置应用到Cube的Transform组件中
        CubeView.transform.position = cubeMoveEvent.position;
        Debug.Log(cubeMoveEvent.position.x + " " + cubeMoveEvent.position.y);
    }

    ~CubeMoveHandler()
    {
        CubeEvent.CubeMoveEvent -= OnCubeMoveEvent;
    }
}