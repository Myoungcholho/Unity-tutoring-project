using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GrowingCatcus : MonoBehaviour
{
    public GameObject Catcus;
    public GameObject TargetPosition;

    private float moveSpeed;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
            StartCoroutine(MoveCatcusHold());
    }

    IEnumerator MoveCatcusHold()
    {   
        
        while (true)
        {   
            if (Catcus.transform.position.x < TargetPosition.transform.position.x)
                break;
            moveSpeed = 1f * Time.deltaTime;
            Catcus.transform.position = Vector3.MoveTowards(Catcus.transform.position, TargetPosition.transform.position, moveSpeed);
            yield return null;
        }
            
        yield return null;
    }
}
