using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;

    private Rigidbody2D rigid;
    private Vector2 inputVec;
    private Animator animator;

    private float v;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();       
    }

    private void FixedUpdate()
    {
        Vector2 nextVec = inputVec.normalized * speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVec);
        animator.SetInteger("hRaw", (int)v);
        
    }

    private void Move()
    {
        if(Input.GetKey(KeyCode.A))
        {
            inputVec.x = -1;
            v = -1;
            animator.SetBool("Moving", true);
        }
        else if(Input.GetKey(KeyCode.D))
        {
            inputVec.x = 1;
            v = 1;
            animator.SetBool("Moving", true);
        }
        else
        {
            inputVec.x = 0;
            v = 0;
            animator.SetBool("Moving", false);
        }
    }


}
