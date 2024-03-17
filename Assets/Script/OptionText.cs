using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionText : MonoBehaviour
{
    //싱글톤 접근용 프로퍼티
    public static OptionText instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = FindObjectOfType<OptionText>();
            }
            return m_instance;
        }
    }
    private static OptionText m_instance; //싱글톤이 할당될 변수

    //각 기능을 활성화하기 위한 변수
    public bool isPRDEnabled = true;
    public bool isKEYBOARDEnabled = true;
    public bool isWINDOWEnabled = true;
    public bool isBGEnabled = true;
    public bool isBGMEnabled = true;
    public bool isSEEnabled = true;
    public bool isDISPEnabled = true;

    //각 기능을 위한 변수
    public TextMeshProUGUI prdText;
    public TextMeshProUGUI keyboardText;
    public TextMeshProUGUI windowText;
    public TextMeshProUGUI bgText;
    public TextMeshProUGUI bgmText;
    public TextMeshProUGUI seText;
    public TextMeshProUGUI dispText;

    //각 기능에 대한 텍스트 업데이트
    public void UpdateButtonText(string newText, TextMeshProUGUI text)
    {
        if (text != null)
        {
            text.text = newText;
        }
    }

    //이하 기능별 업데이트 매서드
    public void UpdatePrdText(string newText)
    {
        if (isPRDEnabled)
        {
            UpdateButtonText(newText, prdText);
        }
    }

    public void UpdateKeyboardText(string newText)
    {
        if (isKEYBOARDEnabled)
        {
            UpdateButtonText(newText, keyboardText);
        }
    }

    public void UpdateWindowText(string newText)
    {
        if (isWINDOWEnabled)
        {
            UpdateButtonText(newText, windowText);
        }
    }

    public void UpdateBgText(string newText)
    {
        if (isBGEnabled)
        {
            UpdateButtonText(newText, bgText);
        }
    }

    public void UpdateBgmText(string newText)
    {
        if (isBGMEnabled)
        {
            UpdateButtonText(newText, bgmText);
        }
    }

    public void UpdateSeText(string newText)
    {
        if (isSEEnabled)
        {
            UpdateButtonText(newText, seText);
        }
    }

    public void UpdateDispText(string newText)
    {
        if (isDISPEnabled)
        {
            UpdateButtonText(newText, dispText);
        }
    }
}