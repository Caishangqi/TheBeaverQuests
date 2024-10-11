using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeView : MonoBehaviour
{
    [SerializeField] Rigidbody2D rigidbody2D;
    [SerializeField] BoxCollider2D cubeCollider;
    [SerializeField] SpriteRenderer spriteRenderer;
    
    // Start is called before the first frame update
    public CubeMoveHandler CubeMoveHandler;
    public CubeInteractHandler CubeInteractHandler;
    
    private Vector2 cubePosition;
    public void Start()
    {
        CubeMoveHandler = new CubeMoveHandler(this);
        CubeInteractHandler = new CubeInteractHandler(this);
    }

    // Update is called once per frame
    void Update()
    {
        //检查交互（？）
    }
}
