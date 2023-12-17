using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public delegate void JumpFunction();
    public delegate void MoveFunction(int Direction);
    public event JumpFunction OnJump;
    public event MoveFunction OnMove;

    public KeyCode jumpKey = KeyCode.Space;

    private PlayerJump jump;
    private PlayerMove move;

    private int moveDirection = 0;

    void Start()
    {
        move = GetComponent<PlayerMove>();
        jump = GetComponent<PlayerJump>();
        OnJump += jump.JumpKey;
        OnMove += move.MoveKey;
    }
    void Update()
    {
        if(Input.GetKeyDown(jumpKey))  //점프 입력
        {
            OnJump?.Invoke();
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
