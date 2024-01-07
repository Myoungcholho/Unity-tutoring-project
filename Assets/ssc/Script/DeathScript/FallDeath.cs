using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class FallDeath : MonoBehaviour
{
    public Vector3 respawnPoint; 
     
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.transform.position = respawnPoint;
    }
}
