using CholHo;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushWall : MonoBehaviour
{
    [SerializeField]
    private bool isPossible = false;
    
    private PlayerInput playerInput;

    public MovableWall movableWall;


    private PlayerMove playerMove;

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
        playerMove = GetComponent<PlayerMove>();
        playerInput = GetComponent<PlayerInput>();
        
    }
    
    
    void Update()
    {
        
        
        /*if (isPossible)
        {*/
            if (isPossible && playerStatus.movingLeftRayDetect == true && powerDirection == -1 && !leftPowerIncreased)
            {
                movableWall.leftPower += 1;
                leftPowerIncreased = true;
            }
            else if(!playerStatus.movingLeftRayDetect)
            {
                if(leftPowerIncreased == true)
                {
                    movableWall.leftPower -= 1;
                    leftPowerIncreased = false;
                }
                
            }

            if (playerStatus.movingRightRayDetect == true && powerDirection == 1 && !rightPowerIncreased)
            {
                movableWall.rightPower += 1;
                rightPowerIncreased = true;
            }
            else if (!playerStatus.movingRightRayDetect)
            {
                if (rightPowerIncreased == true)
                {
                    movableWall.rightPower -= 1;
                    rightPowerIncreased = false;
                }

            }
        //}

    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        
        if (collision != null)
        {
             
            if (collision.gameObject.layer == LayerMask.NameToLayer("MovableWall"))
            {
                isPossible = true;
                
                movableWall = collision.collider.GetComponent<MovableWall>();
                if (collision.transform.position.x - transform.position.x < 0)
                {
                    powerDirection = -1;
                }
                else
                    powerDirection = 1;
            }
            else if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                PushWall pushWall = collision.gameObject.GetComponent<PushWall>();
                if (pushWall.isPossible)
                {
                    powerDirection = pushWall.powerDirection;
                    isPossible = true;
                    
                    movableWall = pushWall.movableWall;
                }
                /*else
                {
                    isPossible = false;
                }*/
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision != null)
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("MovableWall") || collision.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                isPossible = false;
            }
        }
    }
}
