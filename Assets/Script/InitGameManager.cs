using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class InitGameManager : MonoBehaviour
{
    public GameObject FirstCanvas;
    public GameObject MenuCanvas;
    public GameObject PlayerCanvas;
    public GameObject YesornoCanvas;
    public GameObject Option2Canvas;
    public GameObject KeyBoardConfigCanvas;

    public TextMeshProUGUI menuText;
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
            case 0:
                EnableCanvas(MenuCanvas);
                currentPageIndex = 1;
                break;
            case 1:
                if (menuText.text.Equals(textOptions[0]))
                {
                    EnableCanvas(PlayerCanvas);
                    currentPageIndex = 2;
                }
                else if (menuText.text.Equals(textOptions[1]))
                {
                    EnableCanvas(Option2Canvas);
                    currentPageIndex = 3;
                }
                else if (menuText.text.Equals(textOptions[2]))
                {
                    DisableCanvas(MenuCanvas);
                    currentPageIndex = 0;
                }
                break;
            case 2:
                EnableCanvas(YesornoCanvas);
                currentPageIndex = 4;
                break;
            case 3:
                EnableCanvas(KeyBoardConfigCanvas);
                currentPageIndex = 4;
                break;
            default:
                break;
        }
    }
    void EscCanvas()
    {
        switch(currentPageIndex)
        {
            case 0:
                break;
            case 1:
                if (MenuCanvas.activeSelf)
                {
                    DisableCanvas(MenuCanvas);
                    currentPageIndex = 0;
                }
                break;
            case 2:
                if (PlayerCanvas.activeSelf)
                {
                    DisableCanvas(PlayerCanvas);
                    currentPageIndex = 1;
                }
                break;
            case 3:
                if (Option2Canvas.activeSelf)
                {
                    DisableCanvas(Option2Canvas);
                    currentPageIndex = 1;
                }
                break;
            case 4:
                if (YesornoCanvas.activeSelf)
                {
                    DisableCanvas(YesornoCanvas);
                    currentPageIndex = 2;
                }
                else if (KeyBoardConfigCanvas.activeSelf)
                {
                    DisableCanvas(KeyBoardConfigCanvas);
                    currentPageIndex = 3;
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
