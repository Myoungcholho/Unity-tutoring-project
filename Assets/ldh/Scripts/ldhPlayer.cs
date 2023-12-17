using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;



public class ldhPlayer : MonoBehaviour
{
    Animator animator;

    SpriteRenderer spriteRenderer;

    [SerializeField]
    private float speed = 3;

    [SerializeField]
    private float power = 3;

    Rigidbody2D rb;

    [SerializeField]
    Collider2D col;

    bool isGround = false;

    Vector3 dir = Vector3.zero; //dir => direction , zero => (0,0,0)
    Vector3 jump = Vector3.up;

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
        dir.x = Input.GetAxisRaw("Horizontal");    //GetAxis => unity���� �����ϴ� ����Ű? ���� �����´�. �װɷ� �̵�.
        dir.y = Input.GetAxisRaw("Vertical");
        dir = new Vector3(dir.x, dir.y, 0);

        if (Input.GetKeyDown(KeyCode.Space) && isGround)
            rb.AddForce(jump * power, ForceMode2D.Impulse);


    }


    private void FixedUpdate()
    {
        gameObject.transform.position += dir * speed * Time.fixedDeltaTime;
        animator.SetFloat("speed", dir.magnitude);  //magnitude => ���� ���̹�ȯ
        if(dir.x != 0)
        {
            spriteRenderer.flipX = dir.x < 0;
        }
    }
}
