using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LiftTile : MonoBehaviour
{
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
    private PlayerStatus playerstatus;
    private RaycastHit2D isPlayerBelowRay;

    private float addRayXposition = -1.93f;
    private float addRayYposition = -0.32f;
    private float Distance = 3.85f;

    

    private TextMeshPro text;
    private LiftTile liftTile;
    private Rigidbody2D rigid;

    private float move;

    

    private void Start()
    {
        
        liftTile = GetComponent<LiftTile>();
        text = GetComponentInChildren<TextMeshPro>();
        rigid = GetComponent<Rigidbody2D>();
        OriginPosition = transform.position;
        text.text = numberOfPlayers.ToString();
    }

    
    private void FixedUpdate()
    {
        Vector3 startPosition = transform.position + new Vector3(addRayXposition, addRayYposition, 0);
        Debug.DrawRay(startPosition, Vector2.right * Distance, Color.red);
        canUp = (total >= numberOfPlayers);
        
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
            if (transform.position.y > OriginPosition.y)
            {
                if (!isPlayerBelow())
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
                move = 0;
            }
        }

        rigid.velocity = new Vector2(rigid.velocity.x, move);
    }
    bool isPlayerBelow()
    {
        Vector3 startPosition = transform.position + new Vector3(addRayXposition, addRayYposition, 0);

        isPlayerBelowRay = Physics2D.Raycast(startPosition, Vector2.right, Distance, playerLayer);
        return isPlayerBelowRay;
    }
}
