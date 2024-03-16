using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LiftTile : MonoBehaviour
{
    public bool customNumberOfPlayers = false;
    public bool moveUp = true;
    public int numberOfPlayers;

    public float moveDistance = 3.0f;

    public LayerMask playerLayer;

    public bool canUp = false;

    [SerializeField]
    private int total = 0;
    public int Total
    {
        get { return total; }
        set
        {
            total = value;
            int visibleText = numberOfPlayers - total;
            if (visibleText > -1)
                text.text = visibleText.ToString();
            else
                text.text = "0";
        }
    }

    private Vector3 OriginPosition;

    private RaycastHit2D isPlayerBelowRay;

    private float addRayXposition = -1.93f;
    private float addRayYposition = -0.32f;
    private float Distance = 3.85f;

    private TextMeshPro text;
    private Rigidbody2D rigid;

    private float move;

    private void Start()
    {
        text = GetComponentInChildren<TextMeshPro>();
        rigid = GetComponent<Rigidbody2D>();

        InitialSettings();
        OriginPosition = transform.position;
        text.text = numberOfPlayers.ToString();
    }

    
    private void FixedUpdate()
    {
        canUp = (total >= numberOfPlayers);

        if (moveUp == true)
            MoveUp();
        else
            MoveDown();

        rigid.velocity = new Vector2(rigid.velocity.x, move);
    }
    private void MoveUp()
    {
        if (canUp)
        {
            if (transform.position.y < OriginPosition.y + moveDistance)
            {
                move = 1;
            }
            else
            {
                move = 0;
            }
        }
        else
        {
            if (transform.position.y > OriginPosition.y && !isPlayerBelow())
            {
               
                    move = -1;
         
            }
            else
            {
                move = 0;
            }
        }
    }

    private void MoveDown()
    {
        if (canUp)
        {
            if (transform.position.y > OriginPosition.y - moveDistance && !isPlayerBelow())
            {
                move = -1;
            }
            else
            {
                move = 0;
            }
        }
        else
        {
            if (transform.position.y < OriginPosition.y)
            {
                move = 1;
            }
            else
            {
                move = 0;
            }
        }
    }

    bool isPlayerBelow()
    {
        Vector3 startPosition = transform.position + new Vector3(addRayXposition, addRayYposition, 0);
        Debug.DrawRay(startPosition, Vector2.right * Distance, Color.red);
        isPlayerBelowRay = Physics2D.Raycast(startPosition, Vector2.right, Distance, playerLayer);
        return isPlayerBelowRay;
    }

    private void InitialSettings()
    {
        GameObject[] Players;
        Players = GameObject.FindGameObjectsWithTag("Player");
        int TotalNumberofPlayers = 0;
        foreach (var Player in Players)
        {
            TotalNumberofPlayers++;
        }

        if(!customNumberOfPlayers)
        {
            numberOfPlayers = TotalNumberofPlayers;
        }
        
    }
}
