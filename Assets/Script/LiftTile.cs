using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LiftTile : MonoBehaviour
{
    public int playerNumber;
    //public GameObject[] Players;
    public float moveDistance = 3.0f;

    public LayerMask PlayerLayer;

    [SerializeField]
    private int total = 0;

    public int Total
    { 
        get { return total; }
        set 
        {
            total = value;
            int visibleText = playerNumber - total;
            text.text = visibleText.ToString();
        }
    }

    private Vector3 OriginPosition;
    private PlayerStatus playerstatus;
    private RaycastHit2D isPlayerBelowRay;

    public float addRayXposition = -1.91f;
    public float addRayYposition = -0.32f;
    public float Distance = 3.84f;

    //test
    public bool isUp = true;

    private TextMeshPro text;
    private LiftTile lifttile;
    private Rigidbody2D rigid;

    private float move;
    private void Start()
    {
        //Players = GameObject.FindGameObjectsWithTag("Player");
        lifttile = GetComponent<LiftTile>();
        text = GetComponentInChildren<TextMeshPro>();
        rigid = GetComponent<Rigidbody2D>();
        OriginPosition = transform.position;
        text.text = playerNumber.ToString();
    }
    
    private void FixedUpdate()
    {
        Vector3 startPosition = transform.position + new Vector3(addRayXposition, addRayYposition, 0);
        Debug.DrawRay(startPosition, Vector2.right * Distance, Color.red);
        if (total >= playerNumber /*isUp*/)
        {

            if (transform.position.y < OriginPosition.y + moveDistance)
            {
                //transform.position = Vector3.MoveTowards(transform.position, transform.position * Vector2.right, 0.05f);
                move = 1;
                //transform.position += Vector3.up * 0.05f/*0.1f * Time.deltaTime*/;
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
                if(!isPlayerBelow())
                {
                    //transform.position = Vector3.MoveTowards(transform.position, OriginPosition, 0.05f);
                    move = -1;
                    //transform.position += Vector3.down * 0.05f;
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



        isPlayerBelowRay = Physics2D.Raycast(startPosition, Vector2.right, Distance, PlayerLayer);
        return isPlayerBelowRay;
    }
}
