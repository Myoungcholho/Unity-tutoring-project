using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiftTilePlayerAbove : MonoBehaviour
{
    public LiftTile liftTile;
    
    public Player player;
    public LiftTilePlayerAbove liftTilePlayerAbove;

    private bool wasOnTile = false;
    private bool isOnTile = false;
    
    void Start()
    {
        player = GetComponent<Player>();
        
    }

    private void Update()
    {
        MoveUpTilePlayerAboved();
    }

    private void MoveUpTilePlayerAboved()
    {
        isOnTile = false;

        if (player.footRayDetect)
        {
            LayerMask DetectedUnderObjectLayer = player.footRayDetect.collider.gameObject.layer;
            if (DetectedUnderObjectLayer == LayerMask.NameToLayer("MoveUpTile"))
            {
                //Ÿ�� ���� ������ ǥ��, �Ʒ� Ÿ���� LiftTile ������Ʈ�� ���� ���� ����
                isOnTile = true;
                liftTile = player.footRayDetect.collider.GetComponent<LiftTile>();
            }
            else if (DetectedUnderObjectLayer == LayerMask.NameToLayer("Player"))
            {
                //�÷��̾� ���� ���� ���, �Ʒ��� �ִ� �÷��̾ Ÿ�� ���� �ִ� ���� ��, isOnTIle�� True�� ��� �� �÷��̾ Ÿ�� ���� ������ ǥ��
                LiftTilePlayerAbove liftTileUnderPlayerAbove = player.footRayDetect.collider.GetComponent<LiftTilePlayerAbove>();

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
