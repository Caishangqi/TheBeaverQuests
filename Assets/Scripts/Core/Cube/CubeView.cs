using System;
using System.Collections;
using System.Collections.Generic;
using Core.Cube;
using Core.Cube.Event;
using Core.Cube.Handler;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CubeView : MonoBehaviour
{
    public Rigidbody2D rigidbody2D;

    public BoxCollider2D cubeCollider;

    public Scene originalScene;

    [SerializeField] public CubeColor cubeColor = CubeColor.RED;

    [SerializeField] public SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    public CubeMoveHandler CubeMoveHandler;
    public CubeInteractHandler CubeInteractHandler;
    public PlayerNearbyHandler PlayerNearbyHandler;

    // 这个变量非常重要，解决了 99 %踏板逆天行为，玩家在HeldCube时候不应该和踏板交互
    public bool IsHeld = false;

    private Vector3 cubePosition;
    private GameObject player; // 用来存储Player的引用

    public void Start()
    {
        CubeMoveHandler = new CubeMoveHandler(this);
        CubeInteractHandler = new CubeInteractHandler(this);
        PlayerNearbyHandler = new PlayerNearbyHandler(this);

        originalScene = gameObject.scene;
        // 使用GameObject.Find找到Player对象
        player = GameObject.Find("Player"); // 假设Player的名字是 "Player"
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

    private void OnDestroy()
    {
        CubeMoveHandler.OnDestroy();
        CubeInteractHandler.OnDestroy();
        PlayerNearbyHandler.OnDestroy();
    }
}