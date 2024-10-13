using System.Collections;
using System.Collections.Generic;
using Core.Cube.Event;
using Core.Cube.Handler;
using UnityEngine;

public class CubeView : MonoBehaviour
{
    public Rigidbody2D rigidbody2D;
    
    public BoxCollider2D cubeCollider;
    // 新的 BoxCollider2D 检测器
    private BoxCollider2D detectionCollider;
    
    [SerializeField] SpriteRenderer spriteRenderer;
    
    // Start is called before the first frame update
    public CubeMoveHandler CubeMoveHandler;
    public CubeInteractHandler CubeInteractHandler;
    public PlayerNearbyHandler PlayerNearbyHandler;
    
    private Vector3 cubePosition;
    private GameObject player;  // 用来存储Player的引用
    public void Start()
    {
        CubeMoveHandler = new CubeMoveHandler(this);
        CubeInteractHandler = new CubeInteractHandler(this);
        PlayerNearbyHandler = new PlayerNearbyHandler(this);
        
        // 创建一个比 Cube 边缘大 2.0f 的 BoxCollider2D
        detectionCollider = gameObject.AddComponent<BoxCollider2D>();
        detectionCollider.isTrigger = true;  // 设置为触发器
        detectionCollider.size = cubeCollider.size + new Vector2(2.0f, 2.0f);  // 比 Cube 大 2.0f

        // 使用GameObject.Find找到Player对象
        player = GameObject.Find("Player");  // 假设Player的名字是 "Player"
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == player)
        {
            CubeEvent.PlayerNearbyEvent?.Invoke(new PlayerNearbyEvent(player));
            Debug.Log("Player has entered the detection area!");
            // 你可以在这里执行触发时的逻辑
        }
    }
    // Update is called once per frame
    void Update()
    {

    }
}
