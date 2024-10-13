using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlateView : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;  // 压力板的可视化
    [SerializeField] private BoxCollider2D plateCollider;    // 压力板的碰撞器
    [SerializeField] private float m_PositionThreshold = 0.2f;  // 位置差异阈值
    [SerializeField] private float m_SizeThreshold = 0.2f;      // 尺寸差异阈值
    [SerializeField] private int plateId = 1;  // 每个Plate的唯一ID
    
    private BoxCollider2D m_plateCollider;  // 压力板的碰撞器
    private BoxCollider2D m_cubeCollider;   // 检测Cube的碰撞器
    private BoxCollider2D m_playerCollider; //player重量
    private bool isCovered = false;         // 记录是否被覆盖
    
    public void Start()
    {
        m_plateCollider = GetComponent<BoxCollider2D>();
        // 初始化压力板状态
        UpdatePlateVisual();
    }

    // 当有物体进入压力板的碰撞区域时触发
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Cube"))  // 检测触发的对象
        {
            m_cubeCollider = other.GetComponent<BoxCollider2D>();
            m_playerCollider = other.GetComponent<BoxCollider2D>();
            isCovered = true;
            UpdatePlateVisual();  // 更新压力板状态
            if (CheckOverlap())
            {
                if (!isCovered)
                {
                    isCovered = true;
                    Debug.Log(gameObject.name + " has covered ");
                    // 根据特定条件触发不同的事件
                    PlateEvent.PlateTriggeredEvent?.Invoke(new PlateTriggeredEvent( gameObject.transform.position,other.gameObject));//触发trigger
                }
                else
                {
                    // 触发覆盖事件
                    PlateEvent.PlateCoveredEvent?.Invoke(new PlateCoveredEvent(other.gameObject));
                }
            }
        }
    }

    // 当物体离开压力板时触发
    private void OnTriggerExit2D(Collider2D other)
    {
        if ((other.CompareTag("Player") || other.CompareTag("Cube")) && isCovered)
        {
            isCovered = false;
            UpdatePlateVisual();  // 更新压力板状态
            m_cubeCollider = null;
        }
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
    private void UpdatePlateVisual()
    {
        if (isCovered)
        {
            spriteRenderer.color = Color.green;  // 压力板被覆盖时变为绿色
        }
        else
        {
            spriteRenderer.color = Color.red;  // 压力板未被覆盖时变为红色
        }
    }
}
