using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage2_1Manager : MonoBehaviour
{
    public float limitTime;

    public int score = 0;

    public int targetScore;

    public GameObject keyObject;
    void Start()
    {
        SetTargetScore();
        Coin[] coinArray = FindObjectsOfType<Coin>();

        foreach (var coin in coinArray)
        {
            coin.GetCoin += PlusScore;
        }
    }

    // Update is called once per frame
    void Update()
    {
        limitTime -= Time.deltaTime;
    }

    private void SetTargetScore()
    {
        int countOfCoins = GameObject.FindObjectsOfType<Coin>().Length;
        targetScore= countOfCoins;

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
}
