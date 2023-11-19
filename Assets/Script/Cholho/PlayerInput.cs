using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public PlayerType playerType;

    public KeyCode leftKey;
    public KeyCode rightKey;
    public KeyCode jumpKey;

    public float horizontal { get; private set; }
    public delegate void PlayerInputJump();
    public PlayerInputJump delegateJump;

    private void Start()
    {
        horizontal = 0;
    }

    void Update()
    {
        horizontal = GetHorizontalAxis();
        JumpAction();
    }

    private int GetHorizontalAxis()
    {
        if (Input.GetKey(leftKey) && Input.GetKey(rightKey))
        {
            return 0;
        }

        if (Input.GetKey(leftKey))
        {
            return -1;
        }

        if (Input.GetKey(rightKey))
        {
            return 1;
        }

        return 0;
    }
    private void JumpAction()
    {
        if (Input.GetKey(jumpKey))
        {
            if (delegateJump != null)
            {
                delegateJump.Invoke();
            }
        }
    }
}
