using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class MovableWall : MonoBehaviour
{

    public LayerMask movableWallLayer;

    public int numberOfPlayers = 2;

    private Rigidbody2D rigid;

    private int leftPower = 0;
    public int LeftPower
    {
        get { return leftPower; }
        set
        {
            leftPower = value;

        }
    }

    private int rightPower = 0;
    public int RightPower
    {
        get { return rightPower; }
        set { rightPower = value; }
    }

    private int dumpRightPower = 0;
    private int dumpLeftPower = 0;

    private TextMeshPro text;

    private float sideRaycastDistance = 0.2f;
    private RaycastHit2D leftRayDetect;
    private RaycastHit2D rightRayDetect;


    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        text = GetComponentInChildren<TextMeshPro>();
        text.text = numberOfPlayers.ToString();
    }

    private void FixedUpdate()
    {
        BesidePlusPower();
        leftRay();
        RightRay();
        MoveWall();

    }
    

    private void leftRay()
    {
        leftRayDetect = Physics2D.Raycast(transform.position + new Vector3(-0.51f, 0, 0), Vector2.down, sideRaycastDistance, movableWallLayer);
        Debug.DrawRay(transform.position + new Vector3(-0.55f, 0, 0), Vector2.down * 0.2f, Color.red);
    }

    private void RightRay()
    {
        rightRayDetect = Physics2D.Raycast(transform.position + new Vector3(0.51f, 0, 0), Vector2.down, sideRaycastDistance, movableWallLayer);
        Debug.DrawRay(transform.position + new Vector3(0.51f, 0, 0), Vector2.down * 0.2f, Color.red);
    }
    
    private void BesidePlusPower()
    {
        if (leftRayDetect)
        {
            rightPower -= dumpRightPower;
            
            MovableWall besideMovableWall = leftRayDetect.collider.gameObject.GetComponent<MovableWall>();
            dumpRightPower = besideMovableWall.rightPower;
            rightPower += besideMovableWall.rightPower;
        }
  
        if (rightRayDetect)
        {
            leftPower -= dumpLeftPower;

            MovableWall besideMovableWall = rightRayDetect.collider.gameObject.GetComponent<MovableWall>();
            dumpLeftPower = besideMovableWall.leftPower;
            leftPower += besideMovableWall.leftPower;
            
        }
    }

    private void MoveWall()
    {
        float move = 0;
        int count = numberOfPlayers;
        count -= (leftPower + rightPower);

        if (count <= 0)
        {
            if (leftPower > rightPower)
            {
                move = -0.7f;
                rigid.constraints &= ~RigidbodyConstraints2D.FreezePositionX;
            }
            else if (leftPower < rightPower)
            {
                move = 0.7f;
                rigid.constraints &= ~RigidbodyConstraints2D.FreezePositionX;
            }
            else
            {
                rigid.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
                move = 0;
            }
        }
        else
        {
            rigid.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        }

        if (count > -1)
        {
            text.text = count.ToString();
        }
        else
        {
            text.text = "0";
        }
            
        rigid.velocity = new Vector2(move, rigid.velocity.y);
    }
}