using CholHo;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushWall : MonoBehaviour
{
    [SerializeField]
    private bool isPossible = false;
    private bool isPossiblePlus = true;
    private PlayerInput playerInput;

    public MovableWall movableWall;

    private int lastMoveDirection;

    private PlayerMove playerMove;

    private int PossibleDirection = 0;

    void Start()
    {
        playerMove = GetComponent<PlayerMove>();
        playerInput = GetComponent<PlayerInput>();
        playerInput.OnMove += ApplyForce;
        playerInput.OnMoveKeyUp += StopMove;
    }
    private void StopMove()
    {
        if(isPossible && !isPossiblePlus)
        {
            movableWall.power -= lastMoveDirection;
            isPossiblePlus = true;
        }
        
    }
    private void ApplyForce(int MoveDirection)
    {
        /*if(movableWall != null && isPossible)
            movableWall.power += MoveDirection;*/
        if(MoveDirection != 0 && isPossible && isPossiblePlus && MoveDirection == PossibleDirection)
        {
            lastMoveDirection = MoveDirection;
            movableWall.power += MoveDirection;
            isPossiblePlus = false;

        }


    }

    void Update()
    {
        
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision != null)
        {
            //플레이어들의 isTrue 세기 
            if (collision.gameObject.layer == LayerMask.NameToLayer("MovableWall"))
            {
                isPossible = true;
                isPossiblePlus = true;
                movableWall = collision.collider.GetComponent<MovableWall>();
                if (collision.transform.position.x - transform.position.x < 0)
                {
                    PossibleDirection = -1;
                }
                else
                    PossibleDirection = 1;
            }
            else if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                PushWall pushWall = collision.gameObject.GetComponent<PushWall>();
                if (pushWall.isPossible)
                {
                    PossibleDirection = pushWall.PossibleDirection;
                    isPossible = true;
                    isPossiblePlus = true;
                    movableWall = pushWall.movableWall;
                }
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
