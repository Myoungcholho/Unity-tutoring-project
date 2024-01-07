using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.U2D;
using UnityEngine;



public class ldhPlayer : MonoBehaviour
{
    Animator animator;

    SpriteRenderer spriteRenderer;

    [SerializeField]
    private float speed = 3f;

    [SerializeField]
    private float power = 3f;

    Rigidbody2D rb;

    [SerializeField]
    Collider2D col;

    bool isGround = false;

    Vector3 dir = Vector3.zero; //dir => direction , zero => (0,0,0)
    bool jumping = false;
    bool walking = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void OnTriggerStay2D(Collider2D collision) //is Trigger�� �ϸ� exit, enter,stay  �浹 �˻� �̰� ���� �پ�������
    {
        if (collision.CompareTag("Ground"))
        {
            isGround = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision) //�����ϸ鼭 ���� ������ ��������!
    {
        if(collision.CompareTag("Ground"))
        {
            isGround = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // transform.position += new Vector3(dir.x, 0, 0) * speed * Time.deltaTime; 

        //dir.x = Input.GetAxisRaw("Horizontal");    //GetAxis => unity���� �����ϴ� ����Ű? ���� �����´�. �װɷ� �̵�.
        //dir.y = Input.GetAxisRaw("Vertical");     ���� w�� sŰ

        dir = Vector3.zero;

        if (Input.GetKey(KeyCode.A))
        {
            dir = Vector3.left;
        }
        else if(Input.GetKey(KeyCode.D))
        {
            dir = Vector3.right;
        }


        if (Input.GetKeyDown(KeyCode.Space) && isGround)
        {
            rb.AddForce(Vector3.up * power, ForceMode2D.Impulse);
            animator.SetBool("jumping", true);
        }
        //isGround �� false�̸�  jumping �ִϸ��̼�
           

    }


    private void FixedUpdate()
    {
        if (dir != Vector3.zero)
        {
            transform.position += dir.normalized * speed * Time.fixedDeltaTime;
            animator.SetFloat("speed", speed);
            spriteRenderer.flipX = dir.x < 0;
            walking = true;
            animator.SetBool("walking", walking);
        }
        else
        {
            walking = false;
            animator.SetFloat("speed", 0f);

        }
        //Landing platform
        if (rb.velocity.y < 0) 
        {
            Debug.DrawRay(rb.position, Vector3.down, new Color(0, 1, 0));

            RaycastHit2D rayHit = Physics2D.Raycast(rb.position, Vector3.down, 1, LayerMask.GetMask("Platform"));

            if (rayHit.collider != null)
            {
                if (rayHit.distance < 0.5f)
                    animator.SetBool("jumping", false);
            }
        }
        
    }
}
