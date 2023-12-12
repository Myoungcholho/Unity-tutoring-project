using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public enum commendKeyEnum
    {
        KeyLeftArrow,
        KeyRightArrow,
        KeyUpArrow,
        KeyA,
        KeyD,
        KeyW,
    }


    Dictionary<int, KeyCode> commendKey;

    public PlayerType playerType;

    public commendKeyEnum leftKey;
    public commendKeyEnum rightKey;
    public commendKeyEnum jumpKey;

    public float horizontal { get; private set; }
    public delegate void PlayerInputJump();
    public PlayerInputJump delegateJump;

    public event Action onJump;

    private void Start()
    {
        commendKey = new Dictionary<int, KeyCode>
        {
            {0,KeyCode.LeftArrow },
            {1,KeyCode.RightArrow},
            {2,KeyCode.UpArrow},
            {3,KeyCode.A},
            {4,KeyCode.D },
            {5,KeyCode.W }
        };

        horizontal = 0;

    }

    void Update()
    {
        horizontal = GetHorizontalAxis();
        JumpAction();
    }

    private int GetHorizontalAxis()
    {
        if (Input.GetKey(commendKey[(int)leftKey]) && Input.GetKey(commendKey[(int)rightKey]))
        {
            return 0;
        }

        if (Input.GetKey(commendKey[(int)leftKey]))
        {
            return -1;
        }

        if (Input.GetKey(commendKey[(int)rightKey]))
        {
            return 1;
        }

        return 0;
    }
    private void JumpAction()
    {
        if (Input.GetKey(commendKey[(int)jumpKey]))
        {
            if (onJump != null)
            {
                onJump.Invoke();
            }
        }
    }
}
