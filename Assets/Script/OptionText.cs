using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionText : MonoBehaviour
{
    //�̱��� ���ٿ� ������Ƽ
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
    private static OptionText m_instance; //�̱����� �Ҵ�� ����

    //�� ����� Ȱ��ȭ�ϱ� ���� ����
    public bool isPRDEnabled = true;
    public bool isKEYBOARDEnabled = true;
    public bool isWINDOWEnabled = true;
    public bool isBGEnabled = true;
    public bool isBGMEnabled = true;
    public bool isSEEnabled = true;
    public bool isDISPEnabled = true;

    //�� ����� ���� ����
    public TextMeshProUGUI prdText;
    public TextMeshProUGUI keyboardText;
    public TextMeshProUGUI windowText;
    public TextMeshProUGUI bgText;
    public TextMeshProUGUI bgmText;
    public TextMeshProUGUI seText;
    public TextMeshProUGUI dispText;

    //�� ��ɿ� ���� �ؽ�Ʈ ������Ʈ
    public void UpdateButtonText(string newText, TextMeshProUGUI text)
    {
        if (text != null)
        {
            text.text = newText;
        }
    }

    //���� ��ɺ� ������Ʈ �ż���
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