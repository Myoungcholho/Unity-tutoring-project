using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Player : MonoBehaviour
{
    
    public Transform leftRaycastPosition;
    public Transform rightRaycastPosition;

    private Animator anim;
    private Rigidbody2D rigid;
    private SpriteRenderer spriteRenderer;
    private CapsuleCollider2D playerCollider;
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerCollider = GetComponent<CapsuleCollider2D>();
        //Move
        OnMove += GetMoveDirection;

        //Jump
        OnJumpKeyPress += JumpKeyPress;

        //Status
        OnActionKeyDown += InOutDoor;

        
    }

    void Update()
    {
        //Input
        MoveInput();
        JumpInput();
        
        ActionInput();

        //Move
        CheckFreezeCondition();

        //Status
        BesidePlusPower();
        Power();
        isGroundRayDetect();
        isHeadRayDetect();
        leftRay();
        rightRay();
    }
    void FixedUpdate()
    {
        //Move
        Move();

        //Jump
        StopJumpAnimation();
    }
}
