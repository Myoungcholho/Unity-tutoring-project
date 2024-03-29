using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{

    public float moveSpeed = 1f;
    
    public LayerMask playerLayer;

    public Transform leftRaycastPosition;
    public Transform rightRaycastPosition;
    
    private float raycastDistance = 0.2f; //3f

    public bool playerDirection;

    private Animator anim;
    private Rigidbody2D rigid;
    private SpriteRenderer spriteRenderer;
    private PlayerInput playerInput;

    public int moveDirection = 0;
    public float move = 0;

    private PlayerStatus playerStatus;

    private bool canFreeze = true;

    private LayerMask nothingLayer;

    void Start()
    {
        playerStatus = GetComponent<PlayerStatus>();
        rigid = gameObject.GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerInput = GetComponent<PlayerInput>();
        playerInput.OnMove += GetMoveDirection;
        playerInput.OnMoveKeyUp += StopMove;
    }
    private void Update()
    {
        CheckFreezeCondition();
    }
    private void FixedUpdate()
    {
        Move();
       
    }
    private void StopMove()
    {
        playerStatus.movingLeftRayDetect = Physics2D.Raycast(leftRaycastPosition.position, Vector2.down, raycastDistance, nothingLayer);
        playerStatus.movingRightRayDetect = Physics2D.Raycast(rightRaycastPosition.position, Vector2.down, raycastDistance, nothingLayer);
        anim.SetBool("isWalking", false);
    }
    private void GetMoveDirection(int Direction)
    {
        moveDirection = Direction;
    }

    public void Move()
    {
        move = 0;
        move = moveSpeed * moveDirection;
        if (moveDirection == 0 && canFreeze)
        {
            rigid.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        }
        else
        {
            rigid.constraints &= ~RigidbodyConstraints2D.FreezePositionX;
        }

        if (moveDirection == -1)
        {
            spriteRenderer.flipX = true;
            playerDirection = true;
            playerStatus.movingLeftRayDetect = Physics2D.Raycast(leftRaycastPosition.position, Vector2.down, raycastDistance, playerLayer);
            Debug.DrawRay(leftRaycastPosition.position, Vector2.down * 0.2f, Color.red);

        }
        if (moveDirection == 1)
        {
            spriteRenderer.flipX = false;
            playerDirection = false;
            playerStatus.movingRightRayDetect = Physics2D.Raycast(rightRaycastPosition.position, Vector2.down, raycastDistance, playerLayer);
            Debug.DrawRay(rightRaycastPosition.position, Vector2.down * 0.2f, Color.red);
        }
    
        rigid.velocity = new Vector2(move, rigid.velocity.y);
        
        anim.SetBool("isWalking", move != 0);
    }

    private void CheckFreezeCondition()
    {
        if (playerStatus.leftRayDetect)
        {
            if (playerStatus.leftRayDetect.collider.CompareTag("Ground")
                && !(playerStatus.leftRayDetect.collider.gameObject.layer == LayerMask.NameToLayer("MovableWall")))
            {
                canFreeze = false;
            }
        }
        else if(playerStatus.rightRayDetect)
        {
            if (playerStatus.rightRayDetect.collider.CompareTag("Ground")
                && !(playerStatus.rightRayDetect.collider.gameObject.layer == LayerMask.NameToLayer("MovableWall")))
            {
                canFreeze = false;
            }
        }
        else
        {
            canFreeze = true;
        }
    }
}
