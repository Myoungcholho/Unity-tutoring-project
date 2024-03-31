using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public partial class Player : MonoBehaviour
{
    [Header("Status")]
    public LayerMask groundLayer;
    public LayerMask playerLayer;

    public RaycastHit2D headRayDetect;
    public RaycastHit2D footRayDetect;

    public RaycastHit2D movingLeftRayDetect;
    public RaycastHit2D movingRightRayDetect;


    public RaycastHit2D leftRayDetect;
    public RaycastHit2D rightRayDetect;
    

    private float headRayDistance = 0.65f;
    public float groundRayDistance = 0.66f;

    public bool hasKey = false;

    private float sideRaycastDistance = 0.2f;
    

    public float groundX = -0.325f;
    private float groundY = -0.5f;

    //
    public int curLeftPower = 0;
    public int curRightPower = 0;

    private int dumpRightPower = 0;
    private int dumpLeftPower = 0;
    private bool isLeftPowering = false;
    private bool isRightPowering = false;

    private bool canEnter = true;
    private bool hasEnteredDoor = false;
    public void isGroundRayDetect()
    {
        Vector3 GroundstartPosition = transform.position + new Vector3(groundX, groundY, 0);

        Debug.DrawRay(GroundstartPosition + new Vector3(0, -0.1f, 0), Vector2.right * groundRayDistance, Color.yellow);
        footRayDetect = Physics2D.Raycast(GroundstartPosition + new Vector3(0, -0.1f, 0), Vector2.right, groundRayDistance, groundLayer);
    }

    public void isHeadRayDetect()
    {
        Vector3 startPosition = transform.position + new Vector3(-0.32f, 0.55f, 0);

        Debug.DrawRay(startPosition, Vector2.right * headRayDistance, Color.red);
        headRayDetect = Physics2D.Raycast(startPosition, Vector2.right, headRayDistance, playerLayer);
    }
    
    private void leftRay()
    {
        leftRayDetect = Physics2D.Raycast(leftRaycastPosition.position, Vector2.down, sideRaycastDistance, playerLayer);
        Debug.DrawRay(leftRaycastPosition.position, Vector2.down * 0.2f, Color.red);
        
    }
    private void rightRay()
    {
        rightRayDetect = Physics2D.Raycast(rightRaycastPosition.position, Vector2.down, sideRaycastDistance, playerLayer);
        Debug.DrawRay(rightRaycastPosition.position, Vector2.down * 0.2f, Color.red);
    }
    
    
    private void Power()
    {
        if(!isLeftPowering && movingLeftRayDetect)
        {
            curLeftPower++;
            isLeftPowering = true;
        }
        else if(isLeftPowering && !movingLeftRayDetect)
        {
            curLeftPower--;
            isLeftPowering = false;
        }

        if (!isRightPowering && movingRightRayDetect)
        {
            curRightPower++;
            isRightPowering = true;
        }
        else if (isRightPowering && !movingRightRayDetect)
        {
            curRightPower--;
            isRightPowering = false;
        }
    }
    private void BesidePlusPower()
    {
        curRightPower -= dumpRightPower;
        if (leftRayDetect)
        {
            if(leftRayDetect.collider.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                Player besidePlayerStatus = leftRayDetect.collider.gameObject.GetComponent<Player>();
                dumpRightPower = besidePlayerStatus.curRightPower;
                curRightPower += besidePlayerStatus.curRightPower;
            }
            else if(leftRayDetect.collider.gameObject.layer == LayerMask.NameToLayer("MovableWall"))
            {

            }
           
        }
        else
            dumpRightPower = 0;

        curLeftPower -= dumpLeftPower;
        if (rightRayDetect && rightRayDetect.collider.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Player besidePlayerStatus = rightRayDetect.collider.gameObject.GetComponent<Player>();
            dumpLeftPower = besidePlayerStatus.curLeftPower;
            curLeftPower += besidePlayerStatus.curLeftPower;
        }
        else
            dumpLeftPower = 0;
    }
    
    private void EnterDoor()
    {
        if(leftRayDetect)
        {
            if(leftRayDetect.collider.gameObject.layer == LayerMask.NameToLayer("Door"))
            {
                Door door = leftRayDetect.collider.gameObject.GetComponent<Door>();
                if(canEnter && door.isOpened)
                {
                    spriteRenderer.enabled = !spriteRenderer.enabled;
                    gameObject.layer = spriteRenderer.enabled ? LayerMask.NameToLayer("Player") : LayerMask.NameToLayer("Ghost");
                    canEnter = false;
                    hasEnteredDoor = !hasEnteredDoor;
                    Invoke("EnableEnter", 1f);
                }
                
            }
        }
        else if(rightRayDetect )
        {
            if(rightRayDetect.collider.gameObject.layer == LayerMask.NameToLayer("Door"))
            {
                Door door = rightRayDetect.collider.gameObject.GetComponent<Door>();
                if (canEnter && door.isOpened)
                {
                    spriteRenderer.enabled = !spriteRenderer.enabled;
                    gameObject.layer = spriteRenderer.enabled ? LayerMask.NameToLayer("Player") : LayerMask.NameToLayer("Ghost");
                    canEnter = false;
                    hasEnteredDoor = !hasEnteredDoor;

                    Invoke("EnableEnter", 1f);
                }
            }
        }
    }
    private void EnableEnter()
    {
        canEnter = true;
    }
}