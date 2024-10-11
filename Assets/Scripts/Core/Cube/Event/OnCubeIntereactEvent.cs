using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class OnCubeIntereactEvent : MonoBehaviour
{
    public GameObject Cube;

    private bool hasCovered = false; //是否cover了plate
    private BoxCollider2D m_cubeCollider;
    private BoxCollider2D m_plateCollider;
    
    // 设置一个几乎重合的阈值
    [FormerlySerializedAs("m_positionThreshold")] [SerializeField]
    private float m_PositionThreshold = 0.2f; // 位置差异阈值

    [FormerlySerializedAs("m_sizeThreshold")] [SerializeField]
    private float m_SizeThreshold = 0.2f; // 尺寸差异阈值
    
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("PuzzleCube"))
        {
            m_cubeCollider = other.GetComponent<BoxCollider2D>();
            if (CheckOverlap())
            {
                hasCovered = true;
                Debug.Log(gameObject.name + " has covered ");
                
                // if (objectInteract)
                // {
                //     objectInteract.SetActive(false);
                // }
            }
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("PuzzleCube"))
        {
            hasCovered = false;
            m_cubeCollider = null;
            // if (objectInteract)
            // {
            //     objectInteract.SetActive(true);    
            // }
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        m_plateCollider = gameObject.GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private bool CheckOverlap()
    {
        // 比较中心点和尺寸
        var distance = Vector2.Distance(m_plateCollider.gameObject.transform.position,
            m_cubeCollider.gameObject.transform.position);
        if (distance < m_PositionThreshold)
        {
            Debug.Log("两个BoxCollider2D几乎重合 " + distance);
            return true;
        }
        return false;
    }
}
