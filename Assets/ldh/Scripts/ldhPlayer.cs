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
    private void OnTriggerStay2D(Collider2D collision) //is Trigger를 하면 exit, enter,stay  충돌 검사 이건 땅에 붙어있을때
    {
        if (collision.CompareTag("Ground"))
        {
            isGround = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision) //점프하면서 발이 땅에서 떨어질때!
    {
        if(collision.CompareTag("Ground"))
        {
            isGround = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        dir.x = Input.GetAxisRaw("Horizontal");    //GetAxis => unity에서 지원하는 방향키? 축을 가져온다. 그걸로 이동.
        dir.y = Input.GetAxisRaw("Vertical");
        dir = new Vector3(dir.x, dir.y, 0);

        if (Input.GetKeyDown(KeyCode.Space) && isGround)
            rb.AddForce(jump * power, ForceMode2D.Impulse);


    }


    private void FixedUpdate()
    {
        gameObject.transform.position += dir * speed * Time.fixedDeltaTime;
        animator.SetFloat("speed", dir.magnitude);  //magnitude => 벡터 길이반환
        if(dir.x != 0)
        {
            spriteRenderer.flipX = dir.x < 0;
        }
    }
}
