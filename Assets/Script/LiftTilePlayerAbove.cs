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
    
    private Rigidbody2D rigid;
    private Animator anim;
    private PlayerJump playerJump;

    void Start()
    {
        playerStatus = GetComponent<PlayerStatus>();
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        playerJump = GetComponent<PlayerJump>();
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
        if(!playerJump.isJumping)
        {
            if (!playerJump.isJumping && isOnTile && liftTile.canUp)
            {
                //rigid.velocity = new Vector2(rigid.velocity.x, 0.1f);
            }
            else if (!playerJump.isJumping && isOnTile && !liftTile.canUp)
            {

                //rigid.velocity = new Vector2(rigid.velocity.x, -1f);


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
