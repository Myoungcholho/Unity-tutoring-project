using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public partial class Player : MonoBehaviour
{
    [Header("Move")]
    public float moveSpeed = 1f;

    public LayerMask canReceiveForceLayer;

    [HideInInspector]
    public bool playerDirection;
    private float raycastDistance = 0.2f; //3f

    private int moveDirection = 0;
    private float move = 0;

    private bool canFreeze = true;

    private void GetMoveDirection(int Direction)
    {
        moveDirection = Direction;
    }

    private void Move()
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
            movingLeftRayDetect = Physics2D.Raycast(leftRaycastPosition.position, Vector2.down, raycastDistance, canReceiveForceLayer);
            Debug.DrawRay(leftRaycastPosition.position, Vector2.down * 0.2f, Color.red);

        }
        if (moveDirection == 1)
        {
            spriteRenderer.flipX = false;
            playerDirection = false;
            movingRightRayDetect = Physics2D.Raycast(rightRaycastPosition.position, Vector2.down, raycastDistance, canReceiveForceLayer);
            Debug.DrawRay(rightRaycastPosition.position, Vector2.down * 0.2f, Color.red);
        }

        rigid.velocity = new Vector2(move, rigid.velocity.y);

        anim.SetBool("isWalking", move != 0);
    }

    private void CheckFreezeCondition()
    {
        if (leftRayDetect)
        {
            if (leftRayDetect.collider.CompareTag("Ground")
                && !(leftRayDetect.collider.gameObject.layer == LayerMask.NameToLayer("MovableWall")))
            {
                canFreeze = false;
            }
        }
        else if (rightRayDetect)
        {
            if (rightRayDetect.collider.CompareTag("Ground")
                && !(rightRayDetect.collider.gameObject.layer == LayerMask.NameToLayer("MovableWall")))
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
