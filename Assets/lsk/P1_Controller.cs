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
    public bool moving = false;
    public LayerMask Tilemap;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            Jump();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        Debug.Log(horizontal);

        Move(horizontal); //걷기 애니메이션 제어
        UpdateAnimations(horizontal); //걷기 및 점프 애니메이션 제어
    }

    void Move(float horizontal)
    {
        Vector2 position = transform.position;

        if (horizontal > 0)
        {
            if (Input.GetKey(KeyCode.A))
            {
                horizontal = 0;
            }
        }
        else if (horizontal < 0)
        {
            if (Input.GetKey(KeyCode.D))
            {
                horizontal = 0;
            }
        }

        position.x = position.x + maxspeed * horizontal;
        transform.position = position;

        if (horizontal == -1.0f)
        {
            // horizontal이 -1일 때 (좌측으로 이동)
            spriteRenderer.flipX = true; // 스프라이트를 좌우로 뒤집어 좌측 방향으로 향하도록 설정
            moving = true; // 이동 중임을 나타내는 플래그를 true로 설정
        }
        else if (horizontal == 1.0f)
        {
            //우측이동
            spriteRenderer.flipX = false; //스프라이트 원래대로 돌람
            moving = true; //이동 중임을 나타냄
        }
        else
        {
            //이동하지 않을 때 멈춰있는 모션
            moving = false; //이동 중이 아님
        }
    }

    void Jump()
    {
        //y 속도가 0일때만 점프 가능하도록 설정
        rigid.AddForce(new Vector2(0, 800));
        jumping = true;
    }

    bool IsGrounded()
    {
        float circleRadius = 0.2f;
        Vector2 circleCenter = new Vector2(transform.position.x, transform.position.y - 0.1f);

        Collider2D[] colliders = Physics2D.OverlapCircleAll(circleCenter, circleRadius, Tilemap);

        foreach (var collider in colliders)
        {
            if (collider.gameObject != gameObject)
            {
                jumping = false;
                return true;
            }
        }

        return false;
    }

    void UpdateAnimations(float horizontal)
    {
        animator.SetBool("Walking", moving);
        animator.SetBool("Jumping", jumping);
        animator.SetFloat("VerticalSpeed", jumping ? 1f : 0f);
    }
}
