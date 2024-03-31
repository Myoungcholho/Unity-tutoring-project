using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class OptionManager : MonoBehaviour
{
    public Button PRDbutton;
    public Button KeyboardButton;
    public Button WindowButton;
    public Button BGButton;
    public Button BGMButton;
    public Button SEButton;
    public Button DISPButton;
    public Button OKButton;
    public Button CancelButton;

    public UnityEvent onSpacePressed = new UnityEvent();
    public GameObject KeyboardConfigCanvas;

    private InitInputManager initInputManager;
    private int currentButtonIndex = 0;

    private string[] window = { "WINDOW", "FULL" };
    private string[] bg = { "WHITE", "BLUE", "GREEN" };
    private string[] disp = { "OFF", "ON" };
    private int idx = 0;

    private void Start()
    {
        initInputManager = InitInputManager.instance;

        initInputManager.APress += APRESS;
        initInputManager.DPress += DPRESS;

        // PRD 버튼을 선택 상태로 만듭니다.
        SelectButton(CONSTDEFINE.PRDCONFIG);
    }

    private void Update()
    {
        // W 키와 S 키를 처리합니다.
        HandleInput();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            onSpacePressed.Invoke();
        }
    }

    public void OnSpacePressed()
    {
        if(KeyboardConfigCanvas!= null)
        {
            KeyboardConfigCanvas.SetActive(true);
        }
    }

    private void APRESS()
    {
        if (currentButtonIndex == 0 || currentButtonIndex == 1)
            return;

        switch (currentButtonIndex)
        {
            case CONSTDEFINE.WINDOWMODE:
                idx = (idx - 1 + window.Length) % window.Length;
                OptionText.instance.UpdateWindowText(window[idx]);
                if (idx == 0)
                {
                    //기능:창모드로
                }
                else if (idx == 1)
                {
                    // 기능:전체화면모드로
                }
                break;
            case CONSTDEFINE.BGCOLOR:
                idx = (idx - 1 + bg.Length) % bg.Length;
                OptionText.instance.UpdateBgText(bg[idx]);
                if (idx == 0)
                {
                    //기능:배경 하얀색으로
                }
                else if (idx == 1)
                {
                    //기능:배경 파란색으로
                }
                else if (idx == 2)
                {
                    //기능:배경 초록색으로
                }
                break;
            case CONSTDEFINE.DISPNUMBER:
                idx = (idx - 1 + disp.Length) % disp.Length;
                OptionText.instance.UpdateDispText(disp[idx]);
                if (idx == 0)
                {
                    //기능:기본
                }
                else if (idx == 1)
                {
                    //기능:머리에 모자쓰고 숫자 쓰기
                }
                break;
            default:
                break;
        }
    }

    private void DPRESS()
    {
        if (currentButtonIndex == 0 || currentButtonIndex == 1)
            return;

        switch (currentButtonIndex)
        {
            case CONSTDEFINE.WINDOWMODE:
                idx = (idx + 1) % window.Length;
                OptionText.instance.UpdateWindowText(window[idx]);
                if (idx == 0)
                {
                    //기능:창모드로
                }
                else if (idx == 1)
                {
                    // 기능:전체화면모드로
                }
                break;
            case CONSTDEFINE.BGCOLOR:
                idx = (idx + 1) % bg.Length;
                OptionText.instance.UpdateBgText(bg[idx]);
                if (idx == 0)
                {
                    //기능:배경 하얀색으로
                }
                else if (idx == 1)
                {
                    //기능:배경 파란색으로
                }
                else if (idx == 2)
                {
                    //기능:배경 초록색으로
                }
                break;
            case CONSTDEFINE.DISPNUMBER:
                idx = (idx + 1) % disp.Length;
                OptionText.instance.UpdateDispText(disp[idx]);
                if (idx == 0)
                {
                    //기능:기본
                }
                else if (idx == 1)
                {
                    //기능:머리에 모자쓰고 숫자 쓰기
                }
                break;
            default:
                break;
        }
    }


    private void HandleInput()
    {
        // W 키를 누르면 이전 버튼으로 이동합니다.
        if (Input.GetKeyDown(KeyCode.W))
        {
            SelectButton(currentButtonIndex - 1);
        }
        // S 키를 누르면 다음 버튼으로 이동합니다.
        else if (Input.GetKeyDown(KeyCode.S))
        {
            SelectButton(currentButtonIndex + 1);
        }
    }


    private void SelectButton(int index)
    {
        // 현재 버튼 인덱스를 갱신합니다.
        currentButtonIndex = index;

        // 인덱스를 범위 내에 유지합니다.
        /*if (currentButtonIndex < CONSTDEFINE.PRDCONFIG)
        {
            currentButtonIndex = CONSTDEFINE.OK;
        }
        else if (currentButtonIndex > CONSTDEFINE.OK)
        {
            currentButtonIndex = CONSTDEFINE.PRDCONFIG;
        }*/

        // 해당하는 버튼을 선택 상태로 만듭니다.
        switch (currentButtonIndex)
        {
            case CONSTDEFINE.PRDCONFIG:
                PRDbutton.Select();
                break;
            case CONSTDEFINE.KEYBOARDCONFIG:
                KeyboardButton.Select();
                break;
            case CONSTDEFINE.WINDOWMODE:
                WindowButton.Select();
                break;
            case CONSTDEFINE.BGCOLOR:
                BGButton.Select();
                break;
            case CONSTDEFINE.BGMVOLUME:
                BGMButton.Select();
                break;
            case CONSTDEFINE.SEVOLUME:
                SEButton.Select();
                break;
            case CONSTDEFINE.DISPNUMBER:
                DISPButton.Select();
                break;
            case CONSTDEFINE.OK:
                OKButton.Select();
                if (Input.GetKeyDown(KeyCode.A))
                {
                    CancelButton.Select();
                }
                else if (Input.GetKeyDown(KeyCode.D))
                {
                    CancelButton.Select();
                }
                break;
            case CONSTDEFINE.CANCLE:
                OKButton.Select();
                if (Input.GetKeyDown(KeyCode.A))
                {
                    OKButton.Select();
                }
                else if (Input.GetKeyDown(KeyCode.D))
                {
                    OKButton.Select();
                }
                break;
            default:
                break;
        }
    }
}
