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

        // PRD ��ư�� ���� ���·� ����ϴ�.
        SelectButton(CONSTDEFINE.PRDCONFIG);
    }

    private void Update()
    {
        // W Ű�� S Ű�� ó���մϴ�.
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
                    //���:â����
                }
                else if (idx == 1)
                {
                    // ���:��üȭ�����
                }
                break;
            case CONSTDEFINE.BGCOLOR:
                idx = (idx - 1 + bg.Length) % bg.Length;
                OptionText.instance.UpdateBgText(bg[idx]);
                if (idx == 0)
                {
                    //���:��� �Ͼ������
                }
                else if (idx == 1)
                {
                    //���:��� �Ķ�������
                }
                else if (idx == 2)
                {
                    //���:��� �ʷϻ�����
                }
                break;
            case CONSTDEFINE.DISPNUMBER:
                idx = (idx - 1 + disp.Length) % disp.Length;
                OptionText.instance.UpdateDispText(disp[idx]);
                if (idx == 0)
                {
                    //���:�⺻
                }
                else if (idx == 1)
                {
                    //���:�Ӹ��� ���ھ��� ���� ����
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
                    //���:â����
                }
                else if (idx == 1)
                {
                    // ���:��üȭ�����
                }
                break;
            case CONSTDEFINE.BGCOLOR:
                idx = (idx + 1) % bg.Length;
                OptionText.instance.UpdateBgText(bg[idx]);
                if (idx == 0)
                {
                    //���:��� �Ͼ������
                }
                else if (idx == 1)
                {
                    //���:��� �Ķ�������
                }
                else if (idx == 2)
                {
                    //���:��� �ʷϻ�����
                }
                break;
            case CONSTDEFINE.DISPNUMBER:
                idx = (idx + 1) % disp.Length;
                OptionText.instance.UpdateDispText(disp[idx]);
                if (idx == 0)
                {
                    //���:�⺻
                }
                else if (idx == 1)
                {
                    //���:�Ӹ��� ���ھ��� ���� ����
                }
                break;
            default:
                break;
        }
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
        /*if (currentButtonIndex < CONSTDEFINE.PRDCONFIG)
        {
            currentButtonIndex = CONSTDEFINE.OK;
        }
        else if (currentButtonIndex > CONSTDEFINE.OK)
        {
            currentButtonIndex = CONSTDEFINE.PRDCONFIG;
        }*/

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
