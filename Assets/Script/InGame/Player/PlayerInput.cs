using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public partial class Player : MonoBehaviour
{
    [Header("Input")]

    public KeyCode jumpKey = KeyCode.Space;

    public KeyCode leftKey = KeyCode.A;
    public KeyCode rightKey = KeyCode.D;

    public KeyCode ActionKey = KeyCode.W;

    public delegate void MoveFunction(int Direction);
    public MoveFunction OnMove;

    public delegate void JumpFunction();
    public JumpFunction OnJumpKeyPress;

    public Action OnActionKeyDown;

    private int playerMoveDirection = 0;

    //public delegate void StopMoveFunction();
    
    void JumpInput()
    {
       
        if (Input.GetKey(jumpKey))
            OnJumpKeyPress?.Invoke();
        
            
    }
    void MoveInput()
    {
        playerMoveDirection = 0;

        if (Input.GetKey(leftKey) && !hasEnteredDoor)
        {
            playerMoveDirection -= 1;
        }
        if (Input.GetKey(rightKey) && !hasEnteredDoor)
        {
            playerMoveDirection += 1;
        }
        
        OnMove?.Invoke(playerMoveDirection);
    }
    void ActionInput()
    {

        if(Input.GetKey(ActionKey))
        {
            OnActionKeyDown?.Invoke();
        }
    }
}
