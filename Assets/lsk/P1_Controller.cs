using System.Collections; //
using System.Collections.Generic; //
using System.Collections.Specialized;
using System.ComponentModel.Design;
using System.Net;
using System.Security.Cryptography;
using UnityEngine; //

public class P1_Controller : MonoBehaviour
{
    private Rigidbody2D rigid;
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    public float maxspeed;
    public bool jumping = false;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (Mathf.Abs(rigid.velocity.y) < 0.01f)
            {
                rigid.AddForce(new Vector2(0, 400));
                jumping = true;
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        Debug.Log(horizontal);

        Vector2 position = transform.position;

        if (horizontal > 0)
        {
            if (Input.GetKey(KeyCode.A))
            {
                horizontal = 0;
            }
        }
        else if(horizontal < 0)
        {
            if (Input.GetKey(KeyCode.D))
            {
                horizontal = 0;
            }
        }

        position.x = position.x + maxspeed * horizontal;
        transform.position = position;

        if (horizontal == -1.0)
        {
            spriteRenderer.flipX = true;

        }
        else
        {
            spriteRenderer.flipX = false;
        }

        animator.SetBool("Walking", Mathf.Abs(horizontal) > 0.01f);
        animator.SetBool("Jumping", jumping);
        if (rigid.velocity.y == 0)
        {
            jumping = false;
        }
    }
}
