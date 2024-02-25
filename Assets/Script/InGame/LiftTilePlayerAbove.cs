using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiftTilePlayerAbove : MonoBehaviour
{
    public LiftTile liftTile;
    public PlayerStatus playerStatus;
    public LiftTilePlayerAbove liftTilePlayerAbove;

    private bool wasOnTile = false;
    private bool isOnTile = false;
    
    void Start()
    {
        playerStatus = GetComponent<PlayerStatus>();
        
    }

    private void Update()
    {
        MoveUpTilePlayerAboved();
    }

    private void MoveUpTilePlayerAboved()
    {
        isOnTile = false;

        if (playerStatus.footRayDetect)
        {
            if (playerStatus.footRayDetect.collider.gameObject.layer == LayerMask.NameToLayer("MoveUpTile"))
            {
                isOnTile = true;
                liftTile = playerStatus.footRayDetect.collider.GetComponent<LiftTile>();
            }
            else if (playerStatus.footRayDetect.collider.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                LiftTilePlayerAbove liftTileUnderPlayerAbove = playerStatus.footRayDetect.collider.GetComponent<LiftTilePlayerAbove>();
                if (liftTileUnderPlayerAbove.isOnTile)
                    isOnTile = true;

                liftTile = liftTileUnderPlayerAbove.liftTile;
            }
        }
        if (isOnTile && !wasOnTile)
        {
            if (liftTile != null)
                liftTile.Total++;
            wasOnTile = true;
            
        }
        else if (!isOnTile && wasOnTile)
        {
            if (liftTile != null)
                liftTile.Total--;
            wasOnTile = false;
        }

    }
}
