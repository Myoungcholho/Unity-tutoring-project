using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{

    
    public float moveSpeed = 1f;

    public LayerMask wallLayer;
    public LayerMask playerLayer;
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

    private PlayerStatus playerStatus;

    private Vector3 lastPosition;

    private bool canFreeze = true;

    private bool isOn = false;
    private GameObject currentUnderPlayer = null;
    void Start()
    {
        playerStatus = GetComponent<PlayerStatus>();
        rigid = gameObject.GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerInput = GetComponent<PlayerInput>();
        playerInput.OnMove += MoveKey;
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
        playerStatus.movingLeftRayDetect = Physics2D.Raycast(leftRaycastPosition.position, Vector2.down, raycastDistance, wallLayer);
        playerStatus.movingRightRayDetect = Physics2D.Raycast(rightRaycastPosition.position, Vector2.down, raycastDistance, wallLayer);
        anim.SetBool("isWalking", false);
    }
    private void MoveKey(int Direction)
    {
        MoveDirection = Direction;
        

    }

    public void Move()
    {
        move = 0;
        move = moveSpeed * MoveDirection;
        if (MoveDirection == 0 && canFreeze)
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
            playerStatus.movingLeftRayDetect = Physics2D.Raycast(leftRaycastPosition.position, Vector2.down, raycastDistance, playerLayer);
            Debug.DrawRay(leftRaycastPosition.position, Vector2.down * 0.2f, Color.red);

            if (playerStatus.movingLeftRayDetect.collider != null)
            {
                //if(playerStatus.movingLeftRayDetect.collider.gameObject.layer == LayerMask.NameToLayer("Player"))
                    //move = 0;
            }

        }
        if (MoveDirection == 1)
        {
            spriteRenderer.flipX = false;
            PlayerDirection = false;
            //0.1 0.1 0
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

    private float groundRayDistance = 0.9f;
    private float groundX = -0.44f; // -0.325f
    private float groundY = -0.475f; //-0.5f
    private void CarryUnderPlayer()
    {
        Vector3 GroundstartPosition = transform.position + new Vector3(groundX, groundY, 0);

        //Debug.DrawRay(GroundstartPosition, Vector2.right * groundRayDistance, Color.red);
        RaycastHit2D footRayDetect = Physics2D.Raycast(GroundstartPosition, Vector2.right, groundRayDistance, playerLayer);
        if (footRayDetect/*playerStatus.isGroundRayDetect()*/)
        {
            GameObject detectedPlayer = footRayDetect/*playerStatus.footRayDetect*/.collider.gameObject;
            if (detectedPlayer.layer == LayerMask.NameToLayer("Player"))
            {

                if (currentUnderPlayer != detectedPlayer)
                {
                    isOn = true;
                    lastPosition = /*playerStatus.*/footRayDetect.transform.position;
                    currentUnderPlayer = detectedPlayer;
                }
                else if (isOn && lastPosition != /*playerStatus.*/footRayDetect.transform.position)
                {
                    Vector3 movedPosition = /*playerStatus.*/footRayDetect.transform.position - lastPosition;
                    transform.position += movedPosition;
                    lastPosition = /*playerStatus.*/footRayDetect.transform.position;
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
   /* private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            if (collision.transform.position.y < transform.position.y)
            {
                lastPosition = collision.transform.position;
            }
        }
            
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            if(collision.transform.position.y < transform.position.y)
            {
                Vector3 movedPosition = collision.transform.position - lastPosition;
                transform.position += movedPosition;
                lastPosition = collision.transform.position;
            }

        }

        if(collision != null)
        {

        }
        else
        {
            Debug.Log("sdfsdf");
        }
    }*/
}
