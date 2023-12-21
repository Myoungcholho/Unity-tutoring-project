using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    public delegate void JumpFunction();
    public event JumpFunction OnJump;

    public float jumpPower = 1f;

    public Transform pos;
    public float checkRadius;
    public LayerMask wallLayer;

    private Animator anim;
    private Rigidbody2D rigid;

    private bool isJumping = false;
    private bool isGround;
    
    void Start()
    {
        rigid = gameObject.GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        OnJump += JumpKey;
    }

    private void FixedUpdate()
    {
        //isGround = Physics2D.OverlapCircle(pos.position, checkRadius, wallLayer);
        isGround = Physics2D.OverlapBox(pos.position, pos.localScale, wallLayer);
        Jump();
        if(rigid.velocity.y > -0.000001 && rigid.velocity.y < 0.000001)
            Debug.Log(rigid.velocity.y);
    }

    public void JumpInvoke()
    {
        OnJump?.Invoke();
    }

    public void JumpKey()
    {
        if (isGround && (rigid.velocity.y > -0.001 && rigid.velocity.y < 0.001))
        {
            isJumping = true;
        }
    }
    public void Jump()
    {
        if (!isJumping)
        {
            if (isGround && (rigid.velocity.y > -0.001 && rigid.velocity.y < 0.001))
                anim.SetBool("isJumping", false);
            return;
        }

        rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        anim.SetBool("isJumping", true);
        isJumping = false;
    }
}
