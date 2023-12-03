using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public delegate void JumpFunction();
    public delegate void MoveFunction(int Direction);
    public event JumpFunction OnJump;
    public event MoveFunction OnMove;

    public KeyCode jumpKey = KeyCode.Space;
    
    void Update()
    {
        if(Input.GetKeyDown(jumpKey))  //점프 입력
        {
            OnJump?.Invoke();
        }

        if(Input.GetKey(KeyCode.A)) //이동 입력
        {
            OnMove?.Invoke(-1);
        }
        else if(Input.GetKey(KeyCode.D))
        {
            OnMove?.Invoke(1);
        }
        else
        { 
            OnMove?.Invoke(0); 
        } 
        if(Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D))
        {
            OnMove?.Invoke(0);
        }
    }
}
