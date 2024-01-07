using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public delegate void MoveFunction(int Direction);
    public MoveFunction OnMove;
    public delegate void JumpFunction();
    public JumpFunction OnJumpKeyDown;
    public JumpFunction OnJumpKeyUp;
    public JumpFunction OnJumpKeyPress;
    public KeyCode jumpKey = KeyCode.Space;

    public KeyCode leftKey = KeyCode.A;
    public KeyCode rightKey = KeyCode.D;

    private int moveDirection = 0;

    void Start()
    {
       
    }
    void Update()
    {
        if(Input.GetKeyDown(jumpKey))
            OnJumpKeyDown?.Invoke();
        if(Input.GetKey(jumpKey))
            OnJumpKeyPress?.Invoke();
        if(Input.GetKeyUp(jumpKey))
            OnJumpKeyUp?.Invoke();

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
    }
}
