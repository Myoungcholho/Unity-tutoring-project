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

        Move(horizontal); //�ȱ� �ִϸ��̼� ����
        UpdateAnimations(horizontal); //�ȱ� �� ���� �ִϸ��̼� ����
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
            // horizontal�� -1�� �� (�������� �̵�)
            spriteRenderer.flipX = true; // ��������Ʈ�� �¿�� ������ ���� �������� ���ϵ��� ����
            moving = true; // �̵� ������ ��Ÿ���� �÷��׸� true�� ����
        }
        else if (horizontal == 1.0f)
        {
            //�����̵�
            spriteRenderer.flipX = false; //��������Ʈ ������� ����
            moving = true; //�̵� ������ ��Ÿ��
        }
        else
        {
            //�̵����� ���� �� �����ִ� ���
            moving = false; //�̵� ���� �ƴ�
        }
    }

    void Jump()
    {
        //y �ӵ��� 0�϶��� ���� �����ϵ��� ����
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
