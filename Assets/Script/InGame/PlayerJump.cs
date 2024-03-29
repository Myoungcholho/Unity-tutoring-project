using System;
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

    private float jumpTime = 1f;
    private float jumpTimeCount = 0;


    private void Start()
    {
        rigid = gameObject.GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        playerInput = GetComponent<PlayerInput>();
        playerInput.OnJumpKeyUp += JumpKeyUp;
        playerInput.OnJumpKeyPress += JumpKeyPress;

        playerStatus = GetComponent<PlayerStatus>();

    }

    private void JumpKeyPress()
    {
        if (jumpTimeCount <= 0 && playerStatus.footRayDetect)
        {
            if (playerStatus.headRayDetect && playerStatus.headRayDetect.collider.gameObject != this.gameObject)
            {
                if (playerStatus.headRayDetect.collider.gameObject.layer == LayerMask.NameToLayer("MovableWall"))
                {
                    playerStatus.headRayDetect.rigidbody.AddForce(Vector2.up * 3f, ForceMode2D.Impulse);
                }
                else if (playerStatus.headRayDetect.collider.gameObject.layer == LayerMask.NameToLayer("Player")
                    && playerStatus.headRayDetect.collider.gameObject != this.gameObject)
                {
                    AddForceWallUp(playerStatus.headRayDetect.collider.gameObject);
                }
                //anim.SetBool("isJumping", true);
                return;
            }
            if (!isJumping && anim.GetBool("isJumping") == false)
            {
                rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
                isJumping = true;
                jumpTimeCount = jumpTime;
                anim.SetBool("isJumping", true);
                return;
            }
            else if (isJumping)
            {
                anim.SetBool("isJumping", false);
                return;
            }

        }
        if (isJumping && jumpTimeCount > 0)
        {
            rigid.AddForce(Vector2.up * addJumpPower, ForceMode2D.Impulse);
            jumpTimeCount -= Time.fixedDeltaTime;
        }
    }

    private void JumpKeyUp()
    {
        isJumping = false;
        jumpTimeCount = 0;
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

    }


    public void AddForceWallUp(GameObject abovePlayer) //움직이는 벽 위에 있을 시 통통 튀기는 함수
    {
        if (playerStatus.headRayDetect && abovePlayer.layer == LayerMask.NameToLayer("Player"))
        {
            PlayerJump abovePlayersPlayerJump = abovePlayer.GetComponent<PlayerJump>();

            if (abovePlayersPlayerJump != null)
            {
                if (abovePlayersPlayerJump.playerStatus.headRayDetect)
                {
                    abovePlayer = abovePlayersPlayerJump.playerStatus.headRayDetect.collider.gameObject;
                    abovePlayersPlayerJump.AddForceWallUp(abovePlayer);
                }
            }
        }
        else if (abovePlayer.layer == LayerMask.NameToLayer("MovableWall"))
        {
            playerStatus.headRayDetect.rigidbody.AddForce(Vector2.up * 3f, ForceMode2D.Impulse);
        }
    }
}
