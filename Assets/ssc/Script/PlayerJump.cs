using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    public float jumpPower = 1f;
    public KeyCode jumpKey = KeyCode.Space;

    public Transform pos;
    public float checkRadius;
    public LayerMask wallLayer;

    private Animator anim;
    private Rigidbody2D rigid;

    private bool isJumping = false;
    private bool isGround;
    // Start is called before the first frame update
    void Start()
    {
        rigid = gameObject.GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        isGround = Physics2D.OverlapCircle(pos.position, checkRadius, wallLayer);
    }

    public void JumpKey()
    {
        if (isGround && Input.GetKeyDown(jumpKey))
        {
            isJumping = true;

        }
    }
    public void Jump()
    {
        if (!isJumping)
        {
            if (isGround)
                anim.SetBool("isJumping", false);
            return;
        }

        rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        anim.SetBool("isJumping", true);
        isJumping = false;
    }
}
