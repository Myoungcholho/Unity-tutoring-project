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

        // PRD ��ư�� ���� ���·� ����ϴ�.
        SelectButton(CONSTDEFINE.PRDCONFIG);
    }

    private void Update()
    {
        // W Ű�� S Ű�� ó���մϴ�.
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
                    // ���
                }
                else if(idx == 1)
                {
                    // ���
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
        // W Ű�� ������ ���� ��ư���� �̵��մϴ�.
        if (Input.GetKeyDown(KeyCode.W))
        {
            SelectButton(currentButtonIndex - 1);
        }
        // S Ű�� ������ ���� ��ư���� �̵��մϴ�.
        else if (Input.GetKeyDown(KeyCode.S))
        {
            SelectButton(currentButtonIndex + 1);
        }
    }



    private void SelectButton(int index)
    {
        // ���� ��ư �ε����� �����մϴ�.
        currentButtonIndex = index;

        // �ε����� ���� ���� �����մϴ�.
        if (currentButtonIndex < CONSTDEFINE.PRDCONFIG)
        {
            currentButtonIndex = CONSTDEFINE.CANCEL;
        }
        else if (currentButtonIndex > CONSTDEFINE.CANCEL)
        {
            currentButtonIndex = CONSTDEFINE.PRDCONFIG;
        }

        // �ش��ϴ� ��ư�� ���� ���·� ����ϴ�.
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
