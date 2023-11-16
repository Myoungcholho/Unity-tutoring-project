using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float jumpForce;

    private Rigidbody2D rigid;
    private Vector2 inputVec;
    private Animator animator;

    private float horizontal;
    private bool isGround;
    private int jumpCount = 0;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveInput();
        OnPlayAnim();

        //Debug.Log("JumpCount : " + jumpCount);
    }

    private void FixedUpdate()
    {
        MoveVelocity();
    }

    private void MoveInput()
    {
        if (Input.GetKey(Key.dictionary[KeyAction.P1_LEFT]))
        {
            horizontal = -1;
        }
        else if (Input.GetKey(Key.dictionary[KeyAction.P1_RIGHT]))
        {
            horizontal = 1;
        }
        else
        {
            horizontal = 0;
        }

        if (Input.GetKey(Key.dictionary[KeyAction.P1_JUMP]) && jumpCount < 1)
        {
            jumpCount++;
            rigid.velocity = Vector2.zero;
            rigid.AddForce(new Vector2(0, jumpForce));
        }
    }

    private void MoveVelocity()
    {
        rigid.velocity = new Vector2(horizontal * speed , rigid.velocity.y);
    }

    private void OnPlayAnim()
    {
        if (animator.GetInteger("hRaw") != horizontal)
        {
            animator.SetInteger("hRaw", (int)horizontal);
        }
        else if (animator.GetBool("isGround") != isGround)
        {
            animator.SetBool("isGround", isGround);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Ground"))
        {
            if (collision.contacts[0].normal.y > 0.7f)
            {
                isGround = true;
                jumpCount = 0;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Ground"))
        {
            isGround = false;
        }
    }
}