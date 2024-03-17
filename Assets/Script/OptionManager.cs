using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionManager : MonoBehaviour
{

    public Button PRDbutton;
    private OptionText optionText;
    const int maxIndex = 2;

    private string[] windowModeText = { "WINDOW", "FULL" };
    private string[] bgColorText = { "Nomal", "Bule", "Green" };
    private int bgmVolume;
    private int seVolume;
    private string[] dispNumberText = { "Off", "On" };

    private int currentIndex = 0;
    private int currentverticalIndex = 0;
    private bool isButtonPressed = false;
    private int previousVerticalIndex;

    private void OnEnable()
    {
        InitInputManager.instance.DPress += ChangeTextForward;
        InitInputManager.instance.APress += ChangeTextBackward;
        InitInputManager.instance.AKeyDown += SetButtonPressed;
        InitInputManager.instance.DKeyDown += SetButtonPressed;
        InitInputManager.instance.WKeyDown += DecreaseIndex;
        InitInputManager.instance.SKeyDown += IncreaseIndex;
    }

    private void DecreaseIndex()
    {
        if (!isButtonPressed)
        {
            currentverticalIndex = (currentverticalIndex - 1 + 7) % 7;
            UpdateText();
        }
    }
    private void IncreaseIndex()
    {
        if (!isButtonPressed)
        {
            currentverticalIndex = (currentverticalIndex + 1) % 7;
            UpdateText();
        }
    }
    private void Awake()
    {
        optionText = OptionText.instance;
    }
    private void Start()
    {
        PRDbutton.Select();
    }
    private void UpdateText()
    {
        Debug.Log("UpdateText() called");
        switch (currentverticalIndex)
        {
            case CONSTDEFINE.PRDCONFIG:
                Debug.Log("prdconfig");
                break;
            case CONSTDEFINE.KEYBOARDCONFIG:
                Debug.Log("keyboardconfig");
                break;
            case CONSTDEFINE.WINDOWMODE:
                Debug.Log("windowmode");
                optionText.UpdateWindowText(windowModeText[currentIndex]);
                break;
            case CONSTDEFINE.BGCOLOR:
                Debug.Log("bgcolor");
                optionText.UpdateBgText(bgColorText[currentIndex]);
                break;
            case CONSTDEFINE.BGMVOLUME:
                Debug.Log("bgmvolume");
                break;
            case CONSTDEFINE.SEVOLUME:
                Debug.Log("sevolume");
                break;
            case CONSTDEFINE.DISPNUMBER:
                Debug.Log("dispnumber");
                optionText.UpdateDispText(dispNumberText[currentIndex]);
                break;
            default:
                break;
        }
    }
    private void ChangeTextForward()
    {
        if (!isButtonPressed)
        {
            previousVerticalIndex = currentverticalIndex;
            currentIndex = (currentIndex + 1) % GetTextArrayLength();
            UpdateText();
        }
    }
    // A키가 눌렸을 경우
    private void ChangeTextBackward()
    {
        if (!isButtonPressed)
        {
            previousVerticalIndex = currentverticalIndex;
            currentIndex = (currentIndex + (maxIndex - 1) + GetTextArrayLength()) % GetTextArrayLength();
            UpdateText();
        }
    }
    private void SetButtonPressed()
    {
        isButtonPressed = true;
    }
    private void ResetButtonPressed()
    {
        isButtonPressed = false;
    }
    private int GetTextArrayLength()
    {
        int length = 0;
        switch (currentverticalIndex)
        {
            case CONSTDEFINE.WINDOWMODE:
                length = windowModeText.Length;
                break;
            case CONSTDEFINE.BGCOLOR:
                length = bgColorText.Length;
                break;
            case CONSTDEFINE.DISPNUMBER:
                length = dispNumberText.Length;
                break;
            default:
                break;
        }
        return Mathf.Max(length, 1); //0대신에 1을 반환하도록
    }

    private void OnDisable()
    {
        InitInputManager.instance.DPress -= ChangeTextForward;
        InitInputManager.instance.APress -= ChangeTextBackward;
        InitInputManager.instance.AKeyDown -= SetButtonPressed;
        InitInputManager.instance.DKeyDown -= SetButtonPressed;
        InitInputManager.instance.WKeyDown -= DecreaseIndex;
        InitInputManager.instance.SKeyDown -= IncreaseIndex;
    }
}
