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
        if(!isJumping && anim.GetBool("isJumping") == false)
        {
            if (playerStatus.headRayDetect && playerStatus.headRayDetect.collider.gameObject != this.gameObject)
            {
                if (playerStatus.headRayDetect.collider.gameObject.layer == LayerMask.NameToLayer("MovableWall"))
                {
                    playerStatus.headRayDetect.rigidbody.AddForce(Vector2.up * 35f, ForceMode2D.Impulse);
                }
                else if (playerStatus.headRayDetect.collider.gameObject.layer == LayerMask.NameToLayer("Player")
                    && playerStatus.headRayDetect.collider.gameObject != this.gameObject)
                {
                    AddForceWall(playerStatus.headRayDetect.collider.gameObject);
                }
                //anim.SetBool("isJumping", true);

            }
            else if (playerStatus.footRayDetect && playerStatus.footRayDetect.collider.gameObject != this.gameObject)
            {
                rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);

                //anim.SetBool("isJumping", true);
                jumpTimeCount = jumpTime;
                isJumping = true;
            }
            
            
            anim.SetBool("isJumping", true);
        }
        
    }
    private void JumpKeyPress()
    {
        
        if (jumpTimeCount > 0 && isJumping)
        {
            //Debug.Log("JumpKeyPress");
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
        isJumping = false;
    }

    private void StopJumpAnimation()
    {
        if (!isJumping)
        {
            if (playerStatus.footRayDetect && playerStatus.footRayDetect.collider.gameObject != this.gameObject/*playerStatus.footRayDetect.collider.CompareTag("Ground")*/)
                anim.SetBool("isJumping", false);
        }

    }
    
    private void FixedUpdate()
    {
        StopJumpAnimation();
        /*if(playerStatus.footRayDetect *//*&& playerStatus.footRayDetect.collider.gameObject.layer == LayerMask.NameToLayer("Player")*//*)
        {
            if(playerStatus.footRayDetect.collider.gameObject != this.gameObject && this.gameObject.name == "Player2")
                Debug.Log(playerStatus.footRayDetect.collider.gameObject.name);
        }*/
    }
    

    public void AddForceWall(GameObject abovePlayer)
    {
        Debug.Log(abovePlayer.name);
        if(playerStatus.headRayDetect && abovePlayer.layer == LayerMask.NameToLayer("Player"))
        {
            PlayerJump abovePlayersPlayerJump = abovePlayer.GetComponent<PlayerJump>();
            if (abovePlayersPlayerJump != null)
            {
                if(abovePlayersPlayerJump.playerStatus.headRayDetect)
                {
                    abovePlayer = abovePlayersPlayerJump.playerStatus.headRayDetect.collider.gameObject;
                    abovePlayersPlayerJump.AddForceWall(abovePlayer);
                }
                    
            }
        }
        else if(abovePlayer.layer == LayerMask.NameToLayer("MovableWall"))
        {
            playerStatus.headRayDetect.rigidbody.AddForce(Vector2.up * 35f, ForceMode2D.Impulse);
        }
    }
/*
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Player"))
            Debug.Log("ONCOllisionEnter2D");
    }*/
}
