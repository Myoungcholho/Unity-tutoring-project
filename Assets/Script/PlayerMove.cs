using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed = 1f;
    public float jumpPower = 1f;
    public KeyCode jumpKey = KeyCode.Space;

    public Transform pos;
    public float checkRadius;
    public LayerMask wallLayer;

    public Transform leftRaycastPosition; // 왼쪽 레이캐스트 시작 위치
    public Transform rightRaycastPosition; // 오른쪽 레이캐스트 시작 위치
    public float raycastDistance = 0.2f; // 레이캐스트의 길이
    

    private Animator anim;
    private Rigidbody2D rigid;
    private SpriteRenderer spriteRenderer;

    private bool isJumping = false;
    private bool isGround;

    void Start()
    {
        rigid = gameObject.GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        MoveKey();
    }

    void FixedUpdate()
    {
        isGround = Physics2D.OverlapCircle(pos.position, checkRadius, wallLayer);
        Move();
        Jump();
    }

    void MoveKey()
    {
        if (isGround && Input.GetKeyDown(jumpKey))
        {
            isJumping = true;
            anim.SetBool("isJumping", true);
        }
        else if (isGround && !isJumping)
        {
            anim.SetBool("isJumping", false);
        }
    }
    void Move()
    {
        float move = 0;

        if (Input.GetKey(KeyCode.A))
        {
            move = -moveSpeed;
            spriteRenderer.flipX = true;

            RaycastHit2D hitLeft = Physics2D.Raycast(leftRaycastPosition.position, Vector2.left, raycastDistance, wallLayer);
            if (hitLeft.collider != null)
                move = 0;
        }
        else if (Input.GetKey(KeyCode.D))
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

    void Jump()
    {
        if (!isJumping)
        {
            return;
        }

        rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        isJumping = false;
    }
}