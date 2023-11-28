using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public delegate void JumpFunction();
    public delegate void MoveFunction(float Direction);
    public event JumpFunction OnJump;
    public event MoveFunction OnMove;

    public KeyCode jumpKey = KeyCode.Space;
    
    void Update()
    {
        if(Input.GetKeyDown(jumpKey))  //���� �Է�
        {
            OnJump?.Invoke();
        }

        if(Input.GetKey(KeyCode.A)) //�̵� �Է�
        {
            OnMove?.Invoke(-1f);
        }
        else if(Input.GetKey(KeyCode.D))
        {
            OnMove?.Invoke(1f);
        }
        else { OnMove?.Invoke(0f);}
        


    }
}
