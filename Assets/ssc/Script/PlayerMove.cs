using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed = 1f;

    public LayerMask wallLayer;

    public Transform leftRaycastPosition; // 왼쪽 레이캐스트 시작 위치
    public Transform rightRaycastPosition; // 오른쪽 레이캐스트 시작 위치
    public float raycastDistance = 0.2f; // 레이캐스트의 길이


    private Animator anim;
    private Rigidbody2D rigid;
    private SpriteRenderer spriteRenderer;

    private float MoveDirection;
    void Start()
    {
        rigid = gameObject.GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MoveKey()
    {
        MoveDirection = 0;
        if (Input.GetKey(KeyCode.A))
            MoveDirection = -1;
        if (Input.GetKey(KeyCode.D))
            MoveDirection = 1;
        if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D))
        {
            MoveDirection = 0;
        }
    }
    public void Move()
    {
        float move = 0;

        if (MoveDirection == -1)
        {
            move = -moveSpeed;
            spriteRenderer.flipX = true;

            RaycastHit2D hitLeft = Physics2D.Raycast(leftRaycastPosition.position, Vector2.left, raycastDistance, wallLayer);
            if (hitLeft.collider != null)
                move = 0;
        }
        else if (MoveDirection == 1)
        {
            move = moveSpeed;
            spriteRenderer.flipX = false;
            RaycastHit2D hitRight = Physics2D.Raycast(rightRaycastPosition.position, Vector2.right, raycastDistance, wallLayer);
            if (hitRight.collider != null)
                move = 0;
        }

        rigid.velocity = new Vector2(move, rigid.velocity.y);
        anim.SetBool("isWalking", move != 0);

    }
}
