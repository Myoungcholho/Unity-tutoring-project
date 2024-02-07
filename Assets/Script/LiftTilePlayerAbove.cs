using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiftTilePlayerAbove : MonoBehaviour
{
    public LiftTile lifttile;

    private bool wasOnTile = false;
    public bool isOnTile = false;
    public PlayerStatus playerstatus;
    public LiftTilePlayerAbove liftTilePlayerAbove;


    

    void Start()
    {
        playerstatus = GetComponent<PlayerStatus>();
    }

    private void Update()
    {
        MoveUpTilePlayerAboved();
        //CountAbovePlayerNumber();
        //Debug.Log(CountAbovePlayerNumber2());
        //count = CountAbovePlayerNumber2();
        //CountAbovePlayerNumber2();
    }
    private void MoveUpTilePlayerAboved()
    {

        isOnTile = false;

        if (playerstatus.footRayDetect)
        {
            if (playerstatus.footRayDetect.collider.gameObject.layer == LayerMask.NameToLayer("MoveUpTile"))
            {
                isOnTile = true;
                lifttile = playerstatus.footRayDetect.collider.GetComponent<LiftTile>();
            }
            else if (playerstatus.footRayDetect.collider.gameObject.layer == LayerMask.NameToLayer("Player"))
            {

                LiftTilePlayerAbove lifttileplayerAbove = playerstatus.footRayDetect.collider.GetComponent<LiftTilePlayerAbove>();
                lifttile = lifttileplayerAbove.lifttile;

                if (lifttileplayerAbove.isOnTile)
                    isOnTile = true;
            }
        }

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
/*
    private int CountAbovePlayerNumber2()
    {
        
        if (playerstatus.footRayDetect)
        {
            if (playerstatus.footRayDetect.collider.gameObject.layer == LayerMask.NameToLayer("MoveUpTile"))
            {
                if(playerstatus.headRayDetect)
                {
                    if (playerstatus.headRayDetect.collider.gameObject.layer == LayerMask.NameToLayer("Player"))
                    {
                        return 1 + CountAbovePlayer(playerstatus.headRayDetect.collider.gameObject);
                    }
                    
                }
                return 1;
                
            }
            
        }

        return 0;

    }

    private int CountAbovePlayer(GameObject abovePlayer)
    {

        liftTilePlayerAbove = abovePlayer.GetComponent<LiftTilePlayerAbove>();
        
        if (liftTilePlayerAbove != null && liftTilePlayerAbove.playerstatus.headRayDetect)
        {
            
            if (liftTilePlayerAbove.playerstatus.headRayDetect.collider != null && liftTilePlayerAbove.playerstatus.headRayDetect.collider.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                return 1 + CountAbovePlayer(liftTilePlayerAbove.playerstatus.headRayDetect.collider.gameObject); 
            }
        }
        
        //위에 플레이어가 없으면 1을 반환
        return 1;
    }

    */

    
}
