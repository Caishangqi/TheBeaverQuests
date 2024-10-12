using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeView : MonoBehaviour
{
    public Rigidbody2D rigidbody2D;
    public BoxCollider2D cubeCollider;
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
