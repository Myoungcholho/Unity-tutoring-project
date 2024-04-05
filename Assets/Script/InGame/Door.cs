using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Door : MonoBehaviour
{
    public Sprite[] opendDoor;

    public bool isOpened = false;

    private int count;

    private int countOfPlayers;
    SpriteRenderer spriteRenderer;
    SpriteRenderer spriteRenderer2;

    Player player;
    private void Start()
    {
        GameObject[] playersObj = GameObject.FindGameObjectsWithTag("Player");
        int i = 0;
        foreach (var playerGameobj in playersObj)
        {
            countOfPlayers++;
            player = playerGameobj.GetComponent<Player>();
            player.enterDoor += CountUp;
            player.exitDoor += CountDown;
        }

        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer2 = transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>();

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("Key"))
            {
                //키가 닿았으면 실행
                collision.gameObject.SetActive(false);
                spriteRenderer.sprite = opendDoor[0];
                spriteRenderer2.sprite = opendDoor[1];
                isOpened = true;
            }
        }
    }

    private void CountUp()
    {
        count++;
        TrystageClear();
    }
    private void CountDown()
    {
        count--;
    }

    private void TrystageClear()
    {
        if(count == countOfPlayers)
        {
            Debug.Log("스테이지 클리어");
            //StageClear
        }
    }
}
