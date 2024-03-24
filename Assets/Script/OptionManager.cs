using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class OptionManager : MonoBehaviour
{
    public Button PRDbutton;
    public Button KeyboardButton;
    public Button WindowButton;
    public Button FullButton;
    public Button WhiteButton;
    public Button BlueButton;
    public Button GreenButton;
    public Button BGMButton;
    public Button SEButton;
    public Button OffButton;
    public Button OnButton;
    public Button OKButton;
    public Button CancelButton;


    private InitInputManager initInputManager;
    private int currentButtonIndex = 0;

    private string[] str = { "test1", " test2" };
    private int idx = 0;

    private void Start()
    {
        initInputManager = InitInputManager.instance;

        initInputManager.APress += APRESS;

        // PRD 버튼을 선택 상태로 만듭니다.
        SelectButton(CONSTDEFINE.PRDCONFIG);
    }

    private void Update()
    {
        // W 키와 S 키를 처리합니다.
        HandleInput();
    }

    private void APRESS()
    {
        if (currentButtonIndex == 0 || currentButtonIndex == 1)
            return;

        switch (currentButtonIndex)
        {
            case 2:
                idx = idx + 1 > 1 ? 0 : 1;
                OptionText.instance.UpdateWindowText(str[idx]);

                if(idx == 0)
                {
                    // 기능
                }
                else if(idx == 1)
                {
                    // 기능
                }

                break;
            case 3:

                break;
        }

    }
    private void DPRESS()
    {

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
        if (currentButtonIndex < CONSTDEFINE.PRDCONFIG)
        {
            currentButtonIndex = CONSTDEFINE.CANCEL;
        }
        else if (currentButtonIndex > CONSTDEFINE.CANCEL)
        {
            currentButtonIndex = CONSTDEFINE.PRDCONFIG;
        }

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
                WhiteButton.Select();
                break;
            case CONSTDEFINE.BGMVOLUME:
                BGMButton.Select();
                break;
            case CONSTDEFINE.SEVOLUME:
                SEButton.Select();
                break;
            case CONSTDEFINE.DISPNUMBER:
                OffButton.Select();
                break;
            case CONSTDEFINE.OK:
                OKButton.Select();
                break;
            case CONSTDEFINE.CANCEL:
                CancelButton.Select();
                break;
            default:
                break;
        }
    }
}
