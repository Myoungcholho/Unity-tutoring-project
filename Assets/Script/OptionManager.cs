using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionManager : MonoBehaviour
{
    private PRDscript PRDtext;
    private KEYBOARDscript KEYBOARDtext;
    private WINDOWscript WINDOWtext;
    private BGscript BGtext;
    private BGMscript BGMtext;
    private SEscript SEtext;
    private DISPscript DISPtext;
    const int maxIndex = 2;

    private string[] windowModeText = { "WINDOW", "FULL" };
    private string[] bgColorText = { "Nomal", "Bule", "Green" };
    private int bgmVolume;
    private int seVolume;
    private string[] dispNumberText = { "Off", "On" };

    private int currentIndex = 0;
    private int currentverticalIndex
    {
        get;
        set;
    }
    private int currentPageText = 0;

    private void OnEnable()
    {
        InitInputManager.instance.DPress += ChangeTextForward;
        InitInputManager.instance.APress += ChangeTextBackward;
    }

    private void Awake()
    {
        PRDtext = GetComponent<PRDscript>();
        KEYBOARDtext = GetComponent<KEYBOARDscript>();
        WINDOWtext = GetComponent<WINDOWscript>();
        BGtext = GetComponent<BGscript>();
        BGMtext = GetComponent<BGMscript>();
        SEtext = GetComponent<SEscript>();
        DISPtext = GetComponent<DISPscript>();
    }
    private void UpdateText()
    {
        switch (currentverticalIndex)
        {
            case CONSTDEFINE.PRDCONFIG:
                break;
            case CONSTDEFINE.KEYBOARDCONFIG:
                break;
            case CONSTDEFINE.WINDOWMODE:
                WINDOWtext.UpdateButtonText(windowModeText[currentIndex]);
                break;
            case CONSTDEFINE.BGCOLOR:
                BGtext.UpdateButtonText(bgColorText[currentIndex]);
                break;
            case CONSTDEFINE.BGMVOLUME:
                break;
            case CONSTDEFINE.SEVOLUME:
                break;
            case CONSTDEFINE.DISPNUMBER:
                DISPtext.UpdateButtonText(dispNumberText[currentIndex]);
                break;
            default:
                break;
        }
    }
    private void ChangeTextForward()
    {
        switch (currentverticalIndex)
        {
            case CONSTDEFINE.WINDOWMODE:
                currentIndex = (currentIndex + 1) % windowModeText.Length;
                break;
            case CONSTDEFINE.BGCOLOR:
                currentIndex = (currentIndex + 1) % bgColorText.Length;
                break;
            case CONSTDEFINE.DISPNUMBER:
                currentIndex = (currentIndex + 1) % dispNumberText.Length;
                break;
            default:
                break;
        }
        UpdateText();
    }
    // A키가 눌렸을 경우
    private void ChangeTextBackward()
    {
        switch (currentverticalIndex)
        {
            case CONSTDEFINE.WINDOWMODE:
                currentIndex = (currentIndex + (maxIndex - 1) + windowModeText.Length) % windowModeText.Length;
                break;
            case CONSTDEFINE.BGCOLOR:
                currentIndex = (currentIndex + (maxIndex) + bgColorText.Length) % bgColorText.Length;
                break;
            case CONSTDEFINE.DISPNUMBER:
                currentIndex = (currentIndex + (maxIndex - 1) + dispNumberText.Length) % dispNumberText.Length;
                break;
            default:
                break;
        }
        UpdateText();
    }

    private void OnDisable()
    {
        InitInputManager.instance.DPress -= ChangeTextForward;
        InitInputManager.instance.APress -= ChangeTextBackward;
    }
}
