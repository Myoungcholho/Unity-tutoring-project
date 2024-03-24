using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Stage2_3Manager : MonoBehaviour
{

    public int score = 0;

    public int targetScore;

    public GameObject keyObject;

    public GameObject blinkTile;
    void Start()
    {
        SetTargetScore();
        Coin[] coinArray = FindObjectsOfType<Coin>();

        foreach (var coin in coinArray)
        {
            coin.GetCoin += PlusScore;
        }

        StartCoroutine(Hide());
    }

    private void SetTargetScore()
    {
        int countOfCoins = GameObject.FindObjectsOfType<Coin>().Length;
        targetScore = countOfCoins;
    }

    private void PlusScore()
    {
        score++;
        if (score == targetScore)
        {
            ActiveKey();
        }
    }

    private void ActiveKey()
    {
        keyObject.SetActive(true);
    }


    private IEnumerator Hide()
    {
        blinkTile.SetActive(false);
        
        yield return new WaitForSeconds(2.0f);
        StartCoroutine(Show());
    }

    private IEnumerator Show()
    {
        blinkTile.SetActive(true);
        yield return new WaitForSeconds(2.0f);
        StartCoroutine(Hide());
    }
}
