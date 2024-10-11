using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnCubeMoveEvent : MonoBehaviour
{
    public Vector2 position;
    public Vector2 velocity;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        // 计算新的位置：m_position += m_velocity * Time.deltaTime
        position += velocity * Time.deltaTime;

        // 将计算出的新位置应用到Cube的Transform组件中
        transform.position = position;
    }
}
