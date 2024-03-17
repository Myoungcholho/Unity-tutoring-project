using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitInputManager : MonoBehaviour
{
    //싱글턴 접근용 프로퍼티
    public static InitInputManager instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = FindObjectOfType<InitInputManager>();
            }
            return m_instance;
        }
    }
    private static InitInputManager m_instance;

    public Action enterPress;
    public Action escPress;
    public Action DPress;
    public Action APress;
    public Action WPress;
    public Action SPress;
    public Action SpacePress;
    public Action AKeyDown;
    public Action DKeyDown;
    public Action WKeyDown;
    public Action SKeyDown;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (enterPress != null)
            {
                enterPress.Invoke();
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(escPress != null)
            {
                escPress.Invoke();
            }
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (SpacePress != null)
            {
                SpacePress.Invoke();
            }
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (APress != null)
            {
                APress.Invoke();
            }
            if (AKeyDown != null)
            {
                AKeyDown.Invoke();
            }
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            if (DPress != null)
            {
                DPress.Invoke();
            }
            if (DKeyDown != null)
            {
                DKeyDown.Invoke();
            }
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (WPress != null)
            {
                WPress.Invoke();
            }
            if (WKeyDown != null)
            {
                WKeyDown.Invoke();
            }
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (SPress != null)
            {
                SPress.Invoke();
            }
            if (SKeyDown != null)
            {
                SKeyDown.Invoke();
            }
        }
    }
}

