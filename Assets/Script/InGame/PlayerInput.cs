using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public delegate void MoveFunction(int Direction);
    public MoveFunction OnMove;

    public delegate void JumpFunction();
    //public JumpFunction OnJumpKeyDown;
    //public JumpFunction OnJumpKeyUp;
    public JumpFunction OnJumpKeyPress;

    public Action OnActionKeyDown;
    public KeyCode jumpKey = KeyCode.Space;

    public KeyCode leftKey = KeyCode.A;
    public KeyCode rightKey = KeyCode.D;

    public KeyCode ActionKey = KeyCode.W;

    private int moveDirection = 0;

    public delegate void StopMoveFunction();
    //public JumpFunction OnMoveKeyUp;
    
    void Update()
    {

        
        Move();
        Jump();
        
    }
    
    void Jump()
    {
       
        if (Input.GetKey(jumpKey))
            OnJumpKeyPress?.Invoke();
        
            
    }
    void Move()
    {
        moveDirection = 0;

        if (Input.GetKey(leftKey))
        {
            moveDirection -= 1;
        }
        if (Input.GetKey(rightKey))
        {
            moveDirection += 1;
        }
        
        OnMove?.Invoke(moveDirection);
        /*if (Input.GetKeyUp(leftKey) || Input.GetKeyUp(rightKey))
        {
            OnMoveKeyUp?.Invoke();
        }*/
    }
    void Action()
    {
        if(Input.GetKey(ActionKey))
        {
            OnActionKeyDown?.Invoke();
        }
    }
}
