using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private PlayerJump jump;
    private PlayerMove move;
    private PlayerInput input;

    void Start()
    {
        move = GetComponent<PlayerMove>();
        jump = GetComponent<PlayerJump>();
        input = GetComponent<PlayerInput>();

        input.OnJump += jump.JumpKey;
        input.OnMove += move.MoveKey;
    }

}