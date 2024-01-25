using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiftTilePlayerAbove : MonoBehaviour
{
    public LiftTile lifttile;

    private bool wasOnTile = false;
    public bool isOnTile = false;
    private PlayerStatus playerstatus;

    void Start()
    {
        playerstatus = GetComponent<PlayerStatus>();
    }

    private void FixedUpdate()
    {
        MoveUpTilePlayerAboved();
    }

    private void MoveUpTilePlayerAboved()
    {

        isOnTile = false;

        if (playerstatus.isGround)
        {
            if (playerstatus.isGround.collider.gameObject.layer == LayerMask.NameToLayer("MoveUpTile"))
            {
                isOnTile = true;
                lifttile = playerstatus.isGround.collider.GetComponent<LiftTile>();
            }
            else if (playerstatus.isGround.collider.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                
                
                LiftTilePlayerAbove lifttileplayerAbove = playerstatus.isGround.collider.GetComponent<LiftTilePlayerAbove>();
                lifttile = lifttileplayerAbove.lifttile;
                if (lifttileplayerAbove.isOnTile)
                    isOnTile = true;
                
                
            }
        }
        /*if (isOnTile)
        {
            transform.SetParent(lifttile.transform);
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            rb.gravityScale = 0;
        }
        else
        {
            transform.parent = null;
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            rb.gravityScale = 3;
        }*/
        if (isOnTile && !wasOnTile)
        {
            
            
            
            if (lifttile != null)
                lifttile.Total++;

           
            wasOnTile = true;


        }
        else if (!isOnTile && wasOnTile)
        {
            
            
            if (lifttile != null)
                lifttile.Total--;
            
            wasOnTile = false;

        }

    }
}
