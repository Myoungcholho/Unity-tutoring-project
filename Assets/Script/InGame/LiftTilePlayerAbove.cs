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
                //Ÿ�� ���� ������ ǥ��, �Ʒ� Ÿ���� LiftTile ������Ʈ�� ���� ���� ����
                isOnTile = true;
                liftTile = playerStatus.footRayDetect.collider.GetComponent<LiftTile>();
            }
            else if (DetectedUnderObjectLayer == LayerMask.NameToLayer("Player"))
            {
                //�÷��̾� ���� ���� ���, �Ʒ��� �ִ� �÷��̾ Ÿ�� ���� �ִ� ���� ��, isOnTIle�� True�� ��� �� �÷��̾ Ÿ�� ���� ������ ǥ��
                LiftTilePlayerAbove liftTileUnderPlayerAbove = playerStatus.footRayDetect.collider.GetComponent<LiftTilePlayerAbove>();

                if (liftTileUnderPlayerAbove.isOnTile)
                    isOnTile = true;

                liftTile = liftTileUnderPlayerAbove.liftTile;
            }
        }

        if (isOnTile && !wasOnTile)
        {
            //Ÿ�� ���� ���� �� 1�� ����
            if (liftTile != null)
                liftTile.Total++;
            wasOnTile = true;

        }
        else if (!isOnTile && wasOnTile)
        {
            //Ÿ�Ͽ��� ���� �� 1�� ����
            if (liftTile != null)
                liftTile.Total--;
            wasOnTile = false;
        }

    }
}
