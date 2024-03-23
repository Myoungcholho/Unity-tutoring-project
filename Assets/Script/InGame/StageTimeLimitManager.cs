using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageTimeLimitManager : MonoBehaviour
{
    public float limitTime;

    public int score = 0;

    public int targetScore;

    public GameObject keyObject;
    void Start()
    {
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

    private void PlusScore()
    {
        score++;
        if (score == targetScore)
        {
            keyObject.SetActive(true);
        }
    }
}
