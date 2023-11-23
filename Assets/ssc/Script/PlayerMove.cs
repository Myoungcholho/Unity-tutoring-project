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

    public Transform leftRaycastPosition; // ���� ����ĳ��Ʈ ���� ��ġ
    public Transform rightRaycastPosition; // ������ ����ĳ��Ʈ ���� ��ġ
    public float raycastDistance = 0.2f; // ����ĳ��Ʈ�� ����
    

    private Animator anim;
    private Rigidbody2D rigid;
    private SpriteRenderer spriteRenderer;

    private bool isJumping = false;
    private bool isGround;

    private RaycastHit2D hitdown;
    void Start()
    {
        rigid = gameObject.GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        JumpKey();
    }

    void FixedUpdate()
    {
        isGround = Physics2D.OverlapCircle(pos.position, checkRadius, wallLayer);
        hitdown = Physics2D.Raycast(pos.position, Vector2.down, raycastDistance, wallLayer);
        
        Move();
        Jump();

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

    void JumpKey()
    {
        if (isGround && Input.GetKeyDown(jumpKey))
        {
            isJumping = true;
            anim.SetBool("isJumping", true);
        }
    }
    void Jump()
    {
        if (!isJumping)
        {
            if(isGround)
                anim.SetBool("isJumping", false);
            return;
        }

        rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        isJumping = false;
    }
}