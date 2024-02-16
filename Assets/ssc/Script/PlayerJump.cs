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

    public bool isJumping = false;

    private Animator anim;
    private Rigidbody2D rigid;
    private PlayerInput playerInput;
    private PlayerStatus playerStatus;
    
    private float jumpTime = 0.1f;
    private float jumpTimeCount = 0;

    private bool canAddJump = false;
    private void Start()
    {
        rigid = gameObject.GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        playerInput = GetComponent<PlayerInput>();
        playerInput.OnJumpKeyDown += JumpKeyDown;
        playerInput.OnJumpKeyUp += JumpKeyUp;
        playerInput.OnJumpKeyPress += JumpKeyPress;

        playerStatus = GetComponent<PlayerStatus>();

    }

    private void JumpKeyDown()
    {
        if (playerStatus.isHeadRayDetect())
            anim.SetBool("isJumping", true);
        else if (playerStatus.isGroundRayDetect())
        {
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            isJumping = true;
            anim.SetBool("isJumping", true);
            jumpTimeCount = jumpTime;
            canAddJump = true;
        }

    }
    private void JumpKeyPress()
    {
        /*if (playerStatus.isHeadRayDetect())
            anim.SetBool("isJumping", true);
        else if (playerStatus.footRayDetect && !isJumping)
        {
            Debug.Log("JumpKeyFirst");
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            isJumping = true;
            anim.SetBool("isJumping", true);
            jumpTimeCount = jumpTime;
            canAddJump = true;
        }*/

        if (jumpTimeCount > 0 && isJumping)
        {
            //Debug.Log("JumpKeyPress");
            rigid.AddForce(Vector2.up * addJumpPower, ForceMode2D.Impulse);
            jumpTimeCount -= Time.deltaTime;
        }
        else
        {
            isJumping = false;
            canAddJump = false;
        }
    }

    private void JumpKeyUp()
    {
        
        isJumping = false;
        canAddJump = false;
    }

    private void StopJumpAnimation()
    {
        if (!isJumping)
        {
            if (playerStatus.isGroundRayDetect())
                anim.SetBool("isJumping", false);
        }

    }
    
    private void FixedUpdate()
    {
   
        StopJumpAnimation();

    }
    

    private void JumpKey()
    {
        if (playerStatus.isGroundRayDetect())
        {
            isJumping = true;
        }
    }

}
