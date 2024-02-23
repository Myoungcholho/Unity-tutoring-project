using CholHo;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushWall : MonoBehaviour
{
    [SerializeField]
    private bool isAttached = false;
    
    public MovableWall movableWall;

    [SerializeField]
    private int powerDirection = 0;

    private PlayerStatus playerStatus;

    [SerializeField]
    private bool leftPowerIncreased = false;
    [SerializeField]
    private bool rightPowerIncreased = false;

    void Start()
    {
        playerStatus = GetComponent<PlayerStatus>();
    }

    void Update()
    {
/*
        if (isAttached && playerStatus.movingLeftRayDetect == true && powerDirection == -1 && !leftPowerIncreased)
        {
            movableWall.LeftPower++;
            
            leftPowerIncreased = true;
        }
        else if (!playerStatus.movingLeftRayDetect)
        {
            if (leftPowerIncreased == true)
            {
                movableWall.LeftPower--;
                leftPowerIncreased = false;
            }
        }

        if (isAttached && playerStatus.movingRightRayDetect == true && powerDirection == 1 && !rightPowerIncreased)
        {
            movableWall.RightPower++;
            rightPowerIncreased = true;
        }
        else if (!playerStatus.movingRightRayDetect)
        {
            if (rightPowerIncreased == true)
            {
                movableWall.RightPower--;
                rightPowerIncreased = false;
            }
        }*/
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        
       /* if (collision != null)
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("MovableWall"))
            {
                
                if((playerStatus.leftRayDetect && playerStatus.leftRayDetect.collider.gameObject.layer == LayerMask.NameToLayer("MovableWall") )
                    || playerStatus.rightRayDetect && (playerStatus.rightRayDetect.collider.gameObject.layer == LayerMask.NameToLayer("MovableWall")))
                {
                    isAttached = true;

                    movableWall = collision.collider.GetComponent<MovableWall>();
                    if (collision.transform.position.x - transform.position.x < 0)
                    {
                        powerDirection = -1;
                    }
                    else
                    {
                        powerDirection = 1;
                    }
                }
                
            }
            else if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                
                PushWall otherPlayerspushWall = collision.gameObject.GetComponent<PushWall>();
                if (otherPlayerspushWall.isAttached)
                {
                    powerDirection = otherPlayerspushWall.powerDirection;
                    isAttached = true;
                    Debug.Log("isAttacehd");
                    movableWall = otherPlayerspushWall.movableWall;
                }
                else
                {
                    isAttached = false;
                }
            }
        }*/
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        /*if (collision != null)
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("MovableWall"))
            {
                if(this.gameObject.name == "3P_Player")
                    Debug.Log(collision.gameObject.name);
                isAttached = false;
            }
            else if(collision.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                isAttached = false;
            }
        }*/
    }
}
