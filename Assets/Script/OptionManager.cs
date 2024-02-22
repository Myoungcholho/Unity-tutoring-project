using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionManager : MonoBehaviour
{
    public RectTransform[] panelTrasnform;

    private int colIndex;   // 세로 값
    private int rowIndex; // 가로 값

    private string[] windowModeText = { "WINDOW", "FULL" };
    private string[] bgColorText = { "Nomal","Bule", "Green" };
    private int bgmVolume;
    private int seVolume;
    private string[] dispNumberText = { "Off", "On" };

    private void OnEnable()
    {
        //연결해주고
    }

    private void Start()
    {
        colIndex = 0;
        bgmVolume = 10;
    }

    private void Update()
    {
        
    }

    //엔터 키 입력 시 호출
    private void EnterPress()
    {
        switch (colIndex)
        {
            case CONSTDEFINE.KEYBOARDCONFIG:
                // 캐릭터 창 활성화
                break;
        }
    }

    // w

    // s

    // a

    // d

    private void OnDisable()
    {
        //끊어주고
    }
}
