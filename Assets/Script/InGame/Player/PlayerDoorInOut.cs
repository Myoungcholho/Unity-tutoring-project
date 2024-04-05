using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Player : MonoBehaviour
{

    private bool canEnter = true;
    private bool hasEnteredDoor = false;

    public Action enterDoor;
    public Action exitDoor;
   
    private void InOutDoor()
    {
        if (leftRayDetect)
        {
            TryEnterDoor(leftRayDetect);
        }
        else if (rightRayDetect)
        {
            TryEnterDoor(rightRayDetect);
        }
    }
    private void TryEnterDoor(RaycastHit2D rayDetect)
    {
        if (rayDetect.collider.gameObject.layer == LayerMask.NameToLayer("Door"))
        {
            Door door = rayDetect.collider.gameObject.GetComponent<Door>();
            if (canEnter && door.isOpened)
            {
                spriteRenderer.enabled = !spriteRenderer.enabled;
                gameObject.layer = spriteRenderer.enabled ? LayerMask.NameToLayer("Player") : LayerMask.NameToLayer("Ghost");
                
                if (!hasEnteredDoor)
                    enterDoor?.Invoke();
                else
                    exitDoor?.Invoke();
                hasEnteredDoor = !hasEnteredDoor;
                canEnter = false;
                Invoke("EnableEnterExit", 1f); // 1ÃÊ ÄðÅ¸ÀÓ
            }
        }
    }

    private void EnableEnterExit()
    {
        canEnter = true;
    }
}
