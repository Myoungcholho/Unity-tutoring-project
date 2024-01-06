using System.Collections;
using System.Collections.Generic;
//using System.Drawing;
//using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

public class PlayerJump : MonoBehaviour
{
    

    public float jumpPower = 1f;
    public float addJumpPower = 5f;

    public LayerMask GroundLayer;
    public LayerMask PlayerLayer;

    public bool isJumping = false;

    private Animator anim;
    private Rigidbody2D rigid;
    private PlayerInput playerInput;

    private RaycastHit2D headHit;
    private RaycastHit2D isGround;
    //
    public float jumpTime = 0.5f;
    private float jumpTimeCount = 0;

    private Vector3 lastPosition;

    private void Start()
    {
        rigid = gameObject.GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        playerInput = GetComponent<PlayerInput>();

        playerInput.OnJumpKeyDown += JumpKeyDown;
        playerInput.OnJumpKeyUp += JumpKeyUp;
        playerInput.OnJumpKeyPress += JumpKeyPress;

        lastPosition = transform.position;
    }

    private void JumpKeyDown()
    {
        if(headHit)
            anim.SetBool("isJumping", true);
        else if (isGround)
        {
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            isJumping = true;
            anim.SetBool("isJumping", true);
            jumpTimeCount = jumpTime;
        }
        
            
        
    }
    private void JumpKeyPress()
    {
        if(jumpTimeCount > 0 && isJumping)
        {
            rigid.AddForce(Vector2.up * addJumpPower, ForceMode2D.Impulse);
            jumpTimeCount -= Time.deltaTime;
        }
        else
        {
            isJumping = false;
        }
    }

    private void JumpKeyUp()
    {
        isJumping= false;
    }

    private void StopJumpAnimation()
    {
        if (!isJumping)
        {
            if (isGround)
                anim.SetBool("isJumping", false);
        }

    }

    private void FixedUpdate()
    {
        Vector3 GroundstartPosition = transform.position + new Vector3(-0.3f, -0.6f, 0);
        isGround = Physics2D.Raycast(GroundstartPosition, Vector2.right, 0.6f, GroundLayer);
        Debug.DrawRay(GroundstartPosition, Vector2.right * 0.6f, Color.red);
        if (isGround)
            Debug.Log("Ground Detected");

        


        HeadRayDetect();
        StopJumpAnimation();

    }
    private void HeadRayDetect()
    {
        Vector3 movedPosition = transform.position - lastPosition;
        Debug.Log(movedPosition);
        Vector3 startPosition = transform.position + new Vector3(-0.3f, 0.5f, 0);
        headHit = Physics2D.Raycast(startPosition, Vector2.right, 0.6f, PlayerLayer);
        if (headHit)
        {
            headHit.transform.position += movedPosition;
            Debug.Log("Head Detected");
        }
        lastPosition = transform.position;
        
        Debug.DrawRay(startPosition, Vector2.right * 0.6f, Color.red);
    }

    private void JumpKey()
    {
        if (isGround /*&& (rigid.velocity.y > -0.001 && rigid.velocity.y < 0.001)*/)
        {
            isJumping = true;
        }
    }

}
