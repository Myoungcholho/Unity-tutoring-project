using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public KeyCode jumpKey = KeyCode.Space;

    private PlayerJump jump;
    private PlayerMove move;

    private int moveDirection = 0;

    void Start()
    {
        move = GetComponent<PlayerMove>();
        jump = GetComponent<PlayerJump>();
    }
    void Update()
    {
        if(Input.GetKeyDown(jumpKey))  //점프 입력
        {
            jump?.JumpInvoke();
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
        move?.MoveInvoke(moveDirection);
    }
}
