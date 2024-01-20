using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveUpTile : MonoBehaviour
{
    public int playerNumber;
    //public GameObject[] Players;
    public float moveDistance = 3.0f;

    [SerializeField]
    private int total = 0;

    public int Total
    {
        get { return total; }
        set { total = value; }
    }
    private Vector3 OriginPosition;

    
    private void Start()
    {
        //Players = GameObject.FindGameObjectsWithTag("Player");
        
        OriginPosition = transform.position;
    }
    
    private void FixedUpdate()
    {

        if (total >= playerNumber)
        {
            
            if (transform.position.y < OriginPosition.y + moveDistance)
                transform.position += Vector3.up * Time.deltaTime;
        }
        else
        {
            if (transform.position.y > OriginPosition.y)
                transform.position += Vector3.down * Time.deltaTime;
        }
    }
    
}
