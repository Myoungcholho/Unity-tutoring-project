using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{

    public LayerMask groundLayer;
    public LayerMask playerLayer;

    public RaycastHit2D headRayDetect;
    public RaycastHit2D footRayDetect;

    public RaycastHit2D movingLeftRayDetect;
    public RaycastHit2D movingRightRayDetect;


    public RaycastHit2D leftRayDetect;
    public RaycastHit2D rightRayDetect;
    

    public float headRayDistance = 0.7f;
    public float groundRayDistance = 0.66f;

    public bool hasKey = false;

    private float sideRaycastDistance = 0.2f;
    public Transform leftRaycastPosition; // 왼쪽 레이캐스트 시작 위치
    public Transform rightRaycastPosition; // 오른쪽 레이캐스트 시작 위치

    private float groundX = -0.325f; // -0.325f
    private float groundY = -0.5f; //-0.5f

    private void FixedUpdate()
    {
        
        leftRay();
        rightRay();

    }

    public bool isGroundRayDetect()
    {
        Vector3 GroundstartPosition = transform.position + new Vector3(groundX, groundY, 0);

        Debug.DrawRay(GroundstartPosition, Vector2.right * groundRayDistance, Color.red);
        footRayDetect = Physics2D.Raycast(GroundstartPosition, Vector2.right, groundRayDistance, groundLayer);
        return footRayDetect;
    }

    public bool isHeadRayDetect()
    {
        Vector3 startPosition = transform.position + new Vector3(-0.34f, 0.5f, 0);

        Debug.DrawRay(startPosition, Vector2.right * headRayDistance, Color.red);
        headRayDetect = Physics2D.Raycast(startPosition, Vector2.right, headRayDistance, playerLayer);
        return headRayDetect;
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
}