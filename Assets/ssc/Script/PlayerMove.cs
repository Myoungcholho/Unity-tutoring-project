using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{

    public RaycastHit2D hitLeft;
    public RaycastHit2D hitRight;
    public float moveSpeed = 1f;

    public LayerMask wallLayer;

    public Transform leftRaycastPosition; // 왼쪽 레이캐스트 시작 위치
    public Transform rightRaycastPosition; // 오른쪽 레이캐스트 시작 위치
    public float raycastDistance = 3f; // 레이캐스트의 길이

    public bool PlayerDirection;
    private Animator anim;
    private Rigidbody2D rigid;
    private SpriteRenderer spriteRenderer;
    private PlayerInput playerInput;
    private int MoveDirection = 0;

    private float move = 0;


    void Start()
    {
        rigid = gameObject.GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerInput = GetComponent<PlayerInput>();
        playerInput.OnMove += MoveKey;
        
    }

    private void FixedUpdate()
    {
        Move();
        Debug.DrawRay(rightRaycastPosition.position, Vector2.down * 0.2f, Color.red);
        Debug.DrawRay(leftRaycastPosition.position, Vector2.down * 0.2f, Color.red);

    }

    private void MoveKey(int Direction)
    {
        MoveDirection = Direction;
    }

    public void Move()
    {
        move = 0;
        move = moveSpeed * MoveDirection;
        if (MoveDirection == 0)
        {
            rigid.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;

        }
        else
        {
            rigid.constraints &= ~RigidbodyConstraints2D.FreezePositionX;
        }

        if (MoveDirection == -1)
        {
            spriteRenderer.flipX = true;
            PlayerDirection = true;
            //-0.09 0.1 0
            /*hitLeft = Physics2D.Raycast(leftRaycastPosition.position, Vector2.down, raycastDistance, wallLayer);
            if (hitLeft.collider != null)
            {
                move = 0;
            }*/

        }
        if (MoveDirection == 1)
        {
            spriteRenderer.flipX = false;
            PlayerDirection = false;
            //0.1 0.1 0
           /* hitRight = Physics2D.Raycast(rightRaycastPosition.position, Vector2.down, raycastDistance, wallLayer);
            if (hitRight.collider != null)
            {
                move = 0;
            }*/
        }
   
        rigid.velocity = new Vector2(move, rigid.velocity.y);
        anim.SetBool("isWalking", move != 0);
    }

}
