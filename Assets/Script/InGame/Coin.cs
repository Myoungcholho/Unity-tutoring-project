using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public Action GetCoin;
   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        this.gameObject.SetActive(false);
        GetCoin?.Invoke();
    }
}
