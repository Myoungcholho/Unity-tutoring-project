using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    //public bool isPlayerAbove = false;

    public LayerMask GroundLayer;
    public LayerMask PlayerLayer;

    public RaycastHit2D headHit;
    public RaycastHit2D isGround;

    private Vector3 lastPosition;

    private float headRayDistance = 0.7f;
    private float groundRayDistance = 0.65f;

    public bool hasKey = false;
    void Start()
    {
        lastPosition = transform.position;
    }

    private void FixedUpdate()
    {
        CarryAbovePlayer();  
    }

    public bool isGroundRayDetect()
    {
        Vector3 GroundstartPosition = transform.position + new Vector3(-0.325f, -0.5f, 0);

        Debug.DrawRay(GroundstartPosition, Vector2.right * groundRayDistance, Color.red);
        isGround = Physics2D.Raycast(GroundstartPosition, Vector2.right, groundRayDistance, GroundLayer);
        return isGround;
    }

    public bool isHeadRayDetect()
    {
        Vector3 startPosition = transform.position + new Vector3(-0.35f, 0.5f, 0);

        Debug.DrawRay(startPosition, Vector2.right * headRayDistance, Color.red);
        headHit = Physics2D.Raycast(startPosition, Vector2.right, headRayDistance, PlayerLayer);
        return headHit;
    }

    private void CarryAbovePlayer()
    {
        if (isHeadRayDetect())
        {
            Vector3 movedPosition = transform.position - lastPosition;
            headHit.transform.position += movedPosition;
        }
        lastPosition = transform.position;
    }
    
}