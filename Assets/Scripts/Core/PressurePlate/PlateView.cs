using System;
using System.Collections;
using System.Collections.Generic;
using Core.Cube;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class PlateView : MonoBehaviour
{
    #region PlateView

    #region Components

    [SerializeField] private SpriteRenderer spriteRenderer; // 压力板的可视化
    [SerializeField] private BoxCollider2D plateCollider; // 压力板的碰撞器

    #endregion

    #region Editor

    [SerializeField] private float m_PositionThreshold = 0.2f; // 位置差异阈值

    [SerializeField] private float m_SizeThreshold = 0.2f; // 尺寸差异阈值

    // ID 用来匹配墙
    [SerializeField] public int plateId = 1; // 每个Plate的唯一ID

    #endregion

    // 可接受箱子的颜色
    [SerializeField] public CubeColor acceptCubeColor = CubeColor.RED;

    // 覆盖在板子上的游戏对象
    public GameObject coveredGameObject;

    public BoxCollider2D m_plateCollider; // 压力板的碰撞器
    public BoxCollider2D m_cubeCollider; // 检测Cube的碰撞器
    public bool isCovered = false; // 记录是否被覆盖

    #region Handlers

    public PlateTriggeredHandler plateTriggeredHandler;

    #endregion


    public void Start()
    {
        m_plateCollider = GetComponent<BoxCollider2D>();
        plateTriggeredHandler = new PlateTriggeredHandler(this);
    }

    // 当有物体进入压力板的碰撞区域时触发
    private void OnTriggerEnter2D(Collider2D other)
    {
        plateTriggeredHandler.HandlePlateTriggered(other);
    }

    // 当物体离开压力板时触发
    private void OnTriggerExit2D(Collider2D other)
    {
        plateTriggeredHandler.HandlePlateUnTriggered(other);
    }

    //检测重叠
    private bool CheckOverlap()
    {
        if (m_cubeCollider == null || m_plateCollider == null)
        {
            return false;
        }

        // 比较中心点之间的距离
        var distance = Vector2.Distance(m_plateCollider.gameObject.transform.position,
            m_cubeCollider.gameObject.transform.position);

        // 判断是否在位置阈值范围内
        if (distance < m_PositionThreshold)
        {
            Debug.Log("两个BoxCollider2D几乎重合 " + distance);
            return true;
        }

        return false;
    }

    // 更新压力板的可视化状态
    public void UpdatePlateVisual()
    {
        if (isCovered)
        {
            spriteRenderer.color = Color.green; // 压力板被覆盖时变为绿色
        }
        else
        {
            spriteRenderer.color = Color.white; // 压力板未被覆盖时变为红色
        }
    }

    private void OnDestroy()
    {
        
        plateTriggeredHandler.OnDestroy();
    }

    #endregion
}