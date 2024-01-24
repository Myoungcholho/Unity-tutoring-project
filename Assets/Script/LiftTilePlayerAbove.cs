using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiftTilePlayerAbove : MonoBehaviour
{
    public LiftTile moveuptile;

    private bool wasOnTile = false;

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

        bool isOnTile = false;

        if (playerstatus.isGround)
        {
            if (playerstatus.isGround.collider.gameObject.layer == LayerMask.NameToLayer("MoveUpTile"))
            {
                isOnTile = true;
                moveuptile = playerstatus.isGround.collider.GetComponent<LiftTile>();
            }
            else if (playerstatus.isGround.collider.gameObject.layer == LayerMask.NameToLayer("Player"))
            {

                isOnTile = true;
                LiftTilePlayerAbove moveuptileplayerAbove = playerstatus.isGround.collider.GetComponent<LiftTilePlayerAbove>();
                moveuptile = moveuptileplayerAbove.moveuptile;
            }
        }

        if (isOnTile && !wasOnTile)
        {

            if (moveuptile != null)
                moveuptile.Total++;


            wasOnTile = true;


        }
        else if (!isOnTile && wasOnTile)
        {

            if (moveuptile != null)
                moveuptile.Total--;

            wasOnTile = false;

        }

    }
}
