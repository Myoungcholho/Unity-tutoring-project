using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public delegate void MoveFunction(int Direction);
    public MoveFunction OnMove;
    public delegate void JumpFunction();
    public JumpFunction OnJump;
    public KeyCode jumpKey = KeyCode.Space;

    private int moveDirection = 0;

    void Start()
    {
       
    }
    void Update()
    {
        if(Input.GetKeyDown(jumpKey))  //점프 입력
        {
            OnJump.Invoke();
        }

        moveDirection = 0;

        if (Input.GetKey(KeyCode.A))
        {
            moveDirection -= 1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            moveDirection += 1;
        }
        OnMove?.Invoke(moveDirection);
    }
}
