using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.Events;

public class InitGameManager : MonoBehaviour
{
    public GameObject FirstCanvas;
    public GameObject MenuCanvas;
    public GameObject PlayerCanvas;
    public GameObject YesornoCanvas;
    public GameObject Option2Canvas;
    public GameObject KeyBoardConfigCanvas;

    public TextMeshProUGUI menuText;
    public OptionManager optionManager;
    private ButtonTextUpdater_MenuCanvas buttonTextUpdater;
    ButtonTextUpdater_PlayerCanvas playerCanvas;
    const int maxIndex = 3;

    private int currentIndex = 0;
    private int currentPageIndex { 
        get; 
        set;
    }
    private int currentPageText = 0;
    private string[] textOptions = { "LOCAL PLAY MODE", "OPTION", "EXIT" };
    private string[] textPlayers = { "2PLAYERS GAME", "3PLAYERS GAME", "4PLAYERS GAME" };

    private void OnEnable()
    {
        InitInputManager.instance.enterPress += EnterCanvas;
        InitInputManager.instance.escPress += EscCanvas;
        InitInputManager.instance.DPress += ChangeTextForward;
        InitInputManager.instance.APress += ChangeTextBackward;
    }
    private void Awake()
    {
        buttonTextUpdater = MenuCanvas.GetComponentInChildren<ButtonTextUpdater_MenuCanvas>();
        playerCanvas = PlayerCanvas.GetComponentInChildren<ButtonTextUpdater_PlayerCanvas>();
    }
    void Start()
    {
        EnableCanvas(FirstCanvas);
        //UpdateText();
        optionManager = FindObjectOfType<OptionManager>(); // OptionManager 찾아서 할당
        if (optionManager.KeyboardConfigCanvas != null)
        {
            optionManager.OnSpacePressed();
        }
    }

    // A와 D키 눌렸을 때 호출됨
    private void UpdateText()
    {
        switch (currentPageIndex)
        {
            case CONSTDEFINE.BASICWINDOW:
                break;
            case CONSTDEFINE.SELECTWINDOW:
                if (PlayerCanvas != null)
                {
                    buttonTextUpdater.UpdateButtonText(textOptions[currentIndex]);
                }
                break;
            case CONSTDEFINE.PLAYERSELECT:
                {   
                    playerCanvas.UpdateButtonText(textPlayers[currentIndex]);
                }
                break;
            default:
                break;
        }
    }

    // D키가 눌렸을 경우 
    private void ChangeTextForward()
    {
        currentIndex = (currentIndex + 1) % textOptions.Length;
        UpdateText();
    }
    // A키가 눌렸을 경우
    private void ChangeTextBackward()
    {
        currentIndex = (currentIndex + (maxIndex-1) + textOptions.Length) % textOptions.Length;
        UpdateText();
    }

    void EnterCanvas()
    {
        //switch 문 int 형 변수로 입력해보기
        switch (currentPageIndex)
        {
            case CONSTDEFINE.BASICWINDOW:
                EnableCanvas(MenuCanvas);
                currentPageIndex = CONSTDEFINE.SELECTWINDOW;
                break;
            case CONSTDEFINE.SELECTWINDOW:
                if (menuText.text.Equals(textOptions[0]))
                {
                    EnableCanvas(PlayerCanvas);
                    currentPageIndex = CONSTDEFINE.PLAYERSELECT;
                }
                else if (menuText.text.Equals(textOptions[1]))
                {
                    EnableCanvas(Option2Canvas);
                    currentPageIndex = CONSTDEFINE.OPTIONWINDOW;
                }
                else if (menuText.text.Equals(textOptions[2]))
                {
                    DisableCanvas(MenuCanvas);
                    currentPageIndex = CONSTDEFINE.BASICWINDOW;
                }
                break;
            case CONSTDEFINE.PLAYERSELECT:
                EnableCanvas(YesornoCanvas);
                currentPageIndex = CONSTDEFINE.LASTWINDOW;
                break;
            case CONSTDEFINE.OPTIONWINDOW:
                EnableCanvas(KeyBoardConfigCanvas);
                currentPageIndex = CONSTDEFINE.LASTWINDOW;
                break;
            default:
                break;
        }
    }

    private void onSpacePressed()
    {
        if (currentPageIndex == CONSTDEFINE.OPTIONWINDOW)
        {
            if (optionManager.KeyboardConfigCanvas != null)
            {
                optionManager.OnSpacePressed();
                currentPageIndex = CONSTDEFINE.LASTWINDOW;
            }
        }
    }
    void EscCanvas()
    {
        switch(currentPageIndex)
        {
            case CONSTDEFINE.BASICWINDOW:
                break;
            case CONSTDEFINE.SELECTWINDOW:
                if (MenuCanvas.activeSelf)
                {
                    DisableCanvas(MenuCanvas);
                    currentPageIndex = CONSTDEFINE.BASICWINDOW;
                }
                break;
            case CONSTDEFINE.PLAYERSELECT:
                if (PlayerCanvas.activeSelf)
                {
                    DisableCanvas(PlayerCanvas);
                    currentPageIndex = CONSTDEFINE.SELECTWINDOW;
                }
                break;
            case CONSTDEFINE.OPTIONWINDOW:
                if (Option2Canvas.activeSelf)
                {
                    DisableCanvas(Option2Canvas);
                    currentPageIndex = CONSTDEFINE.SELECTWINDOW;
                }
                break;
            case CONSTDEFINE.LASTWINDOW: 
                if (YesornoCanvas.activeSelf)
                {
                    DisableCanvas(YesornoCanvas);
                    currentPageIndex = CONSTDEFINE.PLAYERSELECT;
                }
                else if (KeyBoardConfigCanvas.activeSelf)
                {
                    DisableCanvas(KeyBoardConfigCanvas);
                    currentPageIndex = CONSTDEFINE.OPTIONWINDOW;
                }
                break;
            default:
                break;
        }
    }
    private void EnableCanvas(GameObject canvas)
    {
        if (canvas != null)
        {
            canvas.SetActive(true);
            UpdateText(); // Canvas를 활성화할 때마다 텍스트 업데이트
        }
    }
    private void DisableCanvas(GameObject canvas)
    {
        if (canvas != null)
        {
            canvas.SetActive(false);
        }
    }

    private void OnDisable()
    {
        if (InitInputManager.instance != null)
        {
            InitInputManager.instance.enterPress -= EnterCanvas;
            InitInputManager.instance.escPress -= EscCanvas;
            InitInputManager.instance.DPress -= ChangeTextForward;
            InitInputManager.instance.APress -= ChangeTextBackward;
        }
    }
}
