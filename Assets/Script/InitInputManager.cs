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
    public Action rightArrow;
    public Action leftArrow;

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
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (rightArrow != null)
            {
                rightArrow.Invoke();
            }
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            if (leftArrow != null)
            {
                leftArrow.Invoke();
            }
        }
    }
}

