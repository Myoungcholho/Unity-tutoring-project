using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InitGameManager : MonoBehaviour
{
    public GameObject FirstCanvas;
    public GameObject MenuCanvas;
    public GameObject PlayerCanvas;
    public GameObject YesornoCanvas;
    public GameObject Option2Canvas;
    public GameObject KeyBoardConfigCanvas;

    private int currentIndex = 0;
    private string[] textOptions = { "LOCAL PLAY MODE", "OPTION", "EXIT" };

    private void OnEnable()
    {
        InitInputManager.instance.enterPress += EnterCanvas;
        InitInputManager.instance.escPress += EscCanvas;
        InitInputManager.instance.DPress += ChangeTextForward;
        InitInputManager.instance.APress += ChangeTextBackward;
    }
    void Start()
    {
        EnableCanvas(FirstCanvas);
        UpdateText();
    }

    private void UpdateText()
    {
        if(MenuCanvas != null)
        {
            ButtonTextUpdater buttonTextUpdater = MenuCanvas.GetComponentInChildren<ButtonTextUpdater>();

            if (buttonTextUpdater != null)
            {
                buttonTextUpdater.UpdateButtonText(textOptions[currentIndex]);
            }
        }
    }

    private void ChangeTextForward()
    {
        currentIndex = (currentIndex + 1) % textOptions.Length;
        UpdateText();
    }
    private void ChangeTextBackward()
    {
        currentIndex = (currentIndex - 1 + textOptions.Length) % textOptions.Length;
        UpdateText();
    }

    void EnterCanvas()
    {
        //switch 문 int 형 변수로 입력해보기
        if (FirstCanvas.activeSelf)
        {
            EnableCanvas(MenuCanvas);
        }
        else if(MenuCanvas.activeSelf)
        {
            if (MenuCanvas.GetComponentInChildren<TextMeshProUGUI>().text.Equals(textOptions[0]))
            {
                EnableCanvas(PlayerCanvas);
            }
            else if (MenuCanvas.GetComponentInChildren<TextMeshProUGUI>().text == "OPTION")
            {
                EnableCanvas(Option2Canvas);
            }
            else if (MenuCanvas.GetComponentInChildren<TextMeshProUGUI>().text == "EXIT")
            {
                DisableCanvas(MenuCanvas);
            }
        }
        else if (PlayerCanvas.activeSelf)
        {
            EnableCanvas(YesornoCanvas);
        }
        else if (Option2Canvas.activeSelf)
        {
            EnableCanvas(KeyBoardConfigCanvas);
        }
    }
    void EscCanvas()
    {
        DisableCanvas(KeyBoardConfigCanvas);
        DisableCanvas(Option2Canvas);
        DisableCanvas(YesornoCanvas);
        DisableCanvas(PlayerCanvas);
        DisableCanvas(MenuCanvas);
    }
    private void EnableCanvas(GameObject canvas)
    {
        if (canvas != null)
        {
            canvas.SetActive(true);
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
