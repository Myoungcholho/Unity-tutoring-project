using System;
using System.Collections;
using System.Collections.Generic;
//using System.Drawing;
//using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

public partial class Player : MonoBehaviour
{
    [Header("Jump")]
    public float jumpPower = 1f;
    public float addJumpPower = 5f;
    public bool isJumping = false;

    
    private float jumpTime = 1f;
    private float jumpTimeCount = 0;


    private void JumpKeyPress()
    {
        if (jumpTimeCount <= 0 && footRayDetect)
        {
            if (headRayDetect && headRayDetect.collider.gameObject != this.gameObject)
            {
                if (headRayDetect.collider.gameObject.layer == LayerMask.NameToLayer("MovableWall"))
                {
                    headRayDetect.rigidbody.AddForce(Vector2.up * 3f, ForceMode2D.Impulse);
                }
                else if (headRayDetect.collider.gameObject.layer == LayerMask.NameToLayer("Player")
                    && headRayDetect.collider.gameObject != this.gameObject)
                {
                    AddForceWallUp(headRayDetect.collider.gameObject);
                }
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
        }
        if (isJumping && jumpTimeCount > 0)
        {
            rigid.AddForce(Vector2.up * addJumpPower, ForceMode2D.Impulse);
            jumpTimeCount -= Time.fixedDeltaTime;
        }
    }

    private void JumpKeyUp()
    {
        /*isJumping = false;
        jumpTimeCount = 0;*/
    }

    private void StopJumpAnimation()
    {
        if (rigid.velocity.y <= 0)
        {
            if (footRayDetect && footRayDetect.collider.gameObject != this.gameObject/*playerStatus.footRayDetect.collider.CompareTag("Ground")*/)
            {
                anim.SetBool("isJumping", false);
                isJumping = false;
                jumpTimeCount = 0;
            }
                
        }
    }


    public void AddForceWallUp(GameObject abovePlayer) //움직이는 벽 위에 있을 시 통통 튀기는 함수
    {
        if (headRayDetect && abovePlayer.layer == LayerMask.NameToLayer("Player"))
        {
            Player abovePlayersPlayerJump = abovePlayer.GetComponent<Player>();

            if (abovePlayersPlayerJump != null)
            {
                if (abovePlayersPlayerJump.headRayDetect)
                {
                    abovePlayer = abovePlayersPlayerJump.headRayDetect.collider.gameObject;
                    //맨 위의 오브젝트까지 재귀
                    abovePlayersPlayerJump.AddForceWallUp(abovePlayer);
                }
            }
        }
        else if (abovePlayer.layer == LayerMask.NameToLayer("MovableWall"))
        {
            headRayDetect.rigidbody.AddForce(Vector2.up * 3f, ForceMode2D.Impulse);
        }
    }
}
