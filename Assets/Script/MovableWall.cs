using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class MovableWall : MonoBehaviour
{
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
    private TextMeshPro text;

    public LayerMask movableWallLayer;

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
            text.text = count.ToString();
        else
            text.text = "0";
        rigid.velocity = new Vector2(move, rigid.velocity.y);

    }
    

    private bool isDetect = false;
    public void leftMovableWallPlus()
    {
        if (!isDetect)
        {
            if (leftRayDetect)
            {
                isDetect = true;
                MovableWall besideMovableWall = leftRayDetect.collider.gameObject.GetComponent<MovableWall>();
                besideMovableWall.leftPower += leftPower;
            }

        }


    }

    public void leftMovableWallMinus()
    {
        if (isDetect)
        {
            isDetect = false;
            MovableWall besideMovableWall = leftRayDetect.collider.gameObject.GetComponent<MovableWall>();
            besideMovableWall.leftPower -= leftPower;
        }

    }

    private float sideRaycastDistance = 0.2f;

    private RaycastHit2D leftRayDetect;
    private void leftRay()
    {

        leftRayDetect = Physics2D.Raycast(transform.position + new Vector3(-0.51f, 0, 0), Vector2.down, sideRaycastDistance, movableWallLayer);
        Debug.DrawRay(transform.position + new Vector3(-0.55f, 0, 0), Vector2.down * 0.2f, Color.red);
        if (leftRayDetect)
        {
            Debug.Log("MovableWall LeftRayDetect");
        }
    }

    private RaycastHit2D rightRayDetect;
    private void RightRay()
    {
        rightRayDetect = Physics2D.Raycast(transform.position + new Vector3(0.51f, 0, 0), Vector2.down, sideRaycastDistance, movableWallLayer);
        Debug.DrawRay(transform.position + new Vector3(0.51f, 0, 0), Vector2.down * 0.2f, Color.red);
        Debug.Log("MovableWall RightRayDetect");
    }
    private int saveRightPower = 0;
    private int saveLeftPower = 0;
    private bool isLeftReal = false;
    private bool isRightReal = false;
    private void BesidePlusPower()
    {
        if (isLeftReal)
        {
            //Debug.Log("dsfsf");
            
            isLeftReal = false;
        }
        if (isRightReal)
        {
            //Debug.Log("dsfsf");
            
            isRightReal = false;
        }

        if (leftRayDetect)
        {
            rightPower -= saveRightPower;
            
            MovableWall besideMovableWall = leftRayDetect.collider.gameObject.GetComponent<MovableWall>();
            saveRightPower = besideMovableWall.rightPower;
            rightPower += besideMovableWall.rightPower;
            isLeftReal = true;
        }
        


        if (rightRayDetect)
        {
            leftPower -= saveLeftPower;

            MovableWall besideMovableWall = rightRayDetect.collider.gameObject.GetComponent<MovableWall>();
            saveLeftPower = besideMovableWall.leftPower;
            leftPower += besideMovableWall.leftPower;
            isRightReal = true;
        }
        
    }
    
}