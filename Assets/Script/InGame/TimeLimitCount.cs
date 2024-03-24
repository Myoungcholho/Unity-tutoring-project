using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeLimitCount : MonoBehaviour
{
    public Action GameOver;

    public float limitTime;

    private TextMeshPro text;

    private int min;
    private int sec;
    void Start()
    {
        text = GetComponent<TextMeshPro>();

        ButtonRe[] buttonReArray = FindObjectsOfType<ButtonRe>();

        foreach (var buttonRe in buttonReArray)
        {
            buttonRe.buttonPressed += AddTimeWhenPressedButton;
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        CountTime();


    }

    private void CountTime()
    {
        if (limitTime < 0)
        {
            GameOver?.Invoke();
        }
        limitTime -= Time.deltaTime;

        min = (int)(limitTime / 60); // 분 계산
        sec = (int)(limitTime % 60); // 초 계산

        //Debug.Log(string.Format("{0:D2}:{1:D2}", min, sec));

        text.text = string.Format("{0:D2}:{1:D2}", min, sec);
    }
    
    private void AddTimeWhenPressedButton()
    {
        limitTime++;
    }
}

