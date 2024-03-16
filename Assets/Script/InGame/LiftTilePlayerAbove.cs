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
            LayerMask DetectedUnderObjectLayer = playerStatus.footRayDetect.collider.gameObject.layer;
            if (DetectedUnderObjectLayer == LayerMask.NameToLayer("MoveUpTile"))
            {
                //타일 위에 있음을 표시, 아래 타일의 LiftTile 컴포넌트에 대한 참조 저장
                isOnTile = true;
                liftTile = playerStatus.footRayDetect.collider.GetComponent<LiftTile>();
            }
            else if (DetectedUnderObjectLayer == LayerMask.NameToLayer("Player"))
            {
                //플레이어 위에 있을 경우, 아래에 있는 플레이어가 타일 위에 있는 상태 즉, isOnTIle이 True일 경우 현 플레이어도 타일 위에 있음을 표시
                LiftTilePlayerAbove liftTileUnderPlayerAbove = playerStatus.footRayDetect.collider.GetComponent<LiftTilePlayerAbove>();

                if (liftTileUnderPlayerAbove.isOnTile)
                    isOnTile = true;

                liftTile = liftTileUnderPlayerAbove.liftTile;
            }
        }

        if (isOnTile && !wasOnTile)
        {
            //타일 위에 있을 시 1번 실행
            if (liftTile != null)
                liftTile.Total++;
            wasOnTile = true;

        }
        else if (!isOnTile && wasOnTile)
        {
            //타일에서 나갈 시 1번 실행
            if (liftTile != null)
                liftTile.Total--;
            wasOnTile = false;
        }

    }
}
