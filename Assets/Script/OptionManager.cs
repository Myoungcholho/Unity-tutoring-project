using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionManager : MonoBehaviour
{
    public RectTransform[] panelTrasnform;

    private int colIndex;   // ���� ��
    private int rowIndex; // ���� ��

    private string[] windowModeText = { "WINDOW", "FULL" };
    private string[] bgColorText = { "Nomal","Bule", "Green" };
    private int bgmVolume;
    private int seVolume;
    private string[] dispNumberText = { "Off", "On" };

    private void OnEnable()
    {
        //�������ְ�
    }

    private void Start()
    {
        colIndex = 0;
        bgmVolume = 10;
    }

    private void Update()
    {
        
    }

    //���� Ű �Է� �� ȣ��
    private void EnterPress()
    {
        switch (colIndex)
        {
            case CONSTDEFINE.KEYBOARDCONFIG:
                // ĳ���� â Ȱ��ȭ
                break;
        }
    }

    // w

    // s

    // a

    // d

    private void OnDisable()
    {
        //�����ְ�
    }
}
