using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Door : MonoBehaviour
{
    public Sprite[] opendDoor;

    public bool isOpened = false;

    SpriteRenderer spriteRenderer;
    SpriteRenderer spriteRenderer2;

    private void Start()
    {
        GameObject[] Players = GameObject.FindGameObjectsWithTag("Player");
        int i = 0;
        foreach (var Player in Players)
        {
            i++;
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
                collision.gameObject.SetActive(false);
                spriteRenderer.sprite = opendDoor[0];
                spriteRenderer2.sprite = opendDoor[1];
                isOpened = true;
            }
        }
    }
}
