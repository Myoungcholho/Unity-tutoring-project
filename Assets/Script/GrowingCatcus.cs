using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GrowingCatcus : MonoBehaviour
{
    public GameObject Catcus;
    public GameObject TargetPosition;
    
    private bool isTrigger = false;
    
    void FixedUpdate()
    {
        if(isTrigger)
        {
            if (Catcus.transform.position.x > TargetPosition.transform.position.x)
                Catcus.transform.position += Vector3.left * Time.deltaTime;
            
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
                isTrigger = true;
    }
}
