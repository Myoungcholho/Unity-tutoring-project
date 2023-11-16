using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public float movePower = 1f;
    public float jumpPower = 1f;
    Animator anim;
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    Vector3 movement;

    bool isJumping = false;

    bool isGround;
    public Transform pos;
    public float checkRadius;
    public LayerMask islayer;

    void Start()
    {
        rigid = gameObject.GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        isGround = Physics2D.OverlapCircle(pos.position, checkRadius, islayer);
        if (isGround == true && Input.GetButtonDown("Jump"))
        {
            isJumping = true;
            anim.SetBool("isJumping", true);
        }
        else if (isGround && !isJumping)
        {
            anim.SetBool("isJumping", false);
        }
    }

    void FixedUpdate()
    {
        Move();
        Jump();
    }

    void Move()
    {
        Vector3 moveVelocity = Vector3.zero;
        
        if (Input.GetAxisRaw("Horizontal") < 0)
        {
            moveVelocity = Vector3.left;
            spriteRenderer.flipX = true;
        }

        else if (Input.GetAxisRaw("Horizontal") > 0)
        {
            moveVelocity = Vector3.right;
            spriteRenderer.flipX = false;
        }

        transform.position += moveVelocity * movePower * Time.deltaTime;
        if (moveVelocity.normalized.x == 0)
        {
            anim.SetBool("isWalking", false);
        }
        else
        {
            anim.SetBool("isWalking", true);
        }
       
    }

    void Jump()
    {
        if (!isJumping)
        {
            return;
        }
           

        rigid.velocity = Vector2.zero;

        Vector2 jumpVelocity = new Vector2(0, jumpPower);
        rigid.AddForce(jumpVelocity, ForceMode2D.Impulse);
        
        isJumping = false;
    }
}
