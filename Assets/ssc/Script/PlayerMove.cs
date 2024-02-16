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
    private int moveDirection = 0;

    private float move = 0;

    private PlayerStatus playerStatus;

    private Vector3 lastPosition;

    private bool canFreeze = true;

    private bool isOn = false;

    private GameObject currentUnderPlayer = null;

    private float groundRayDistance = 0.9f;
    private float groundX = -0.44f;
    private float groundY = -0.475f;

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
        DecideFreeze();
    }
    private void FixedUpdate()
    {
        Move();
        CarryUnderPlayer();
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

            if (playerStatus.movingLeftRayDetect.collider != null)
            {
                //if(playerStatus.movingLeftRayDetect.collider.gameObject.layer == LayerMask.NameToLayer("Player"))
                    //move = 0;
            }

        }
        if (moveDirection == 1)
        {
            spriteRenderer.flipX = false;
            playerDirection = false;
            playerStatus.movingRightRayDetect = Physics2D.Raycast(rightRaycastPosition.position, Vector2.down, raycastDistance, playerLayer);
            Debug.DrawRay(rightRaycastPosition.position, Vector2.down * 0.2f, Color.red);
            if (playerStatus.movingRightRayDetect.collider != null)
            {
                //if (playerStatus.movingRightRayDetect.collider.gameObject.layer == LayerMask.NameToLayer("Player"))
                    //move = 0;
            }
            
        }
   
        rigid.velocity = new Vector2(move, rigid.velocity.y);
        
        anim.SetBool("isWalking", move != 0);
    }

    
    private void CarryUnderPlayer()
    {
        Vector3 GroundstartPosition = transform.position + new Vector3(groundX, groundY, 0);

        //Debug.DrawRay(GroundstartPosition, Vector2.right * groundRayDistance, Color.red);
        RaycastHit2D footRayDetect = Physics2D.Raycast(GroundstartPosition, Vector2.right, groundRayDistance, playerLayer);
        if (footRayDetect)
        {
            GameObject detectedPlayer = footRayDetect.collider.gameObject;
            if (detectedPlayer.layer == LayerMask.NameToLayer("Player"))
            {

                if (currentUnderPlayer != detectedPlayer)
                {
                    isOn = true;
                    lastPosition = footRayDetect.transform.position;
                    currentUnderPlayer = detectedPlayer;
                }
                else if (isOn && lastPosition != footRayDetect.transform.position)
                {
                    Vector3 movedPosition = footRayDetect.transform.position - lastPosition;
                    transform.position += movedPosition;
                    lastPosition = footRayDetect.transform.position;
                }
            }
        }
        else
        {
            isOn = false;
            currentUnderPlayer = null;
        }
    }

    private void DecideFreeze()
    {
        if (playerStatus.leftRayDetect)
        {
            if (playerStatus.leftRayDetect.collider.CompareTag("Ground"))
            {
                canFreeze = false;
            }
        }
        else if(playerStatus.rightRayDetect)
        {
            if (playerStatus.rightRayDetect.collider.CompareTag("Ground"))
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
