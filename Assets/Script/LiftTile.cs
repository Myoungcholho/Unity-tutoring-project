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

    private float addRayXposition = -1.1f;
    private float addRayYposition = -0.32f;
    private float Distance = 2.2f;


    public bool isUp = true;

    private TextMeshPro text;
    private LiftTile lifttile;

    private void Start()
    {
        //Players = GameObject.FindGameObjectsWithTag("Player");
        lifttile = GetComponent<LiftTile>();
        text = GetComponentInChildren<TextMeshPro>();
        OriginPosition = transform.position;
    }
    
    private void FixedUpdate()
    {
        Vector3 startPosition = transform.position + new Vector3(addRayXposition, addRayYposition, 0);
        Debug.DrawRay(startPosition, Vector2.right * Distance, Color.red);
        if (/*total >= playerNumber*/ isUp)
        {

            if (transform.position.y < OriginPosition.y + moveDistance)
                transform.position += Vector3.up * 0.05f/*0.1f * Time.deltaTime*/;
        }
        else
        {
            if (transform.position.y > OriginPosition.y)
            {
                if(!isPlayerBelow())
                    transform.position += Vector3.down * Time.deltaTime;
            }

        }
    }
    bool isPlayerBelow()
    {
        Vector3 startPosition = transform.position + new Vector3(addRayXposition, addRayYposition, 0);



        isPlayerBelowRay = Physics2D.Raycast(startPosition, Vector2.right, Distance, PlayerLayer);
        return isPlayerBelowRay;
    }
}
