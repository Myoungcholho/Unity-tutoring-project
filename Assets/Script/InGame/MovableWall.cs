using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;
using static UnityEditor.Experimental.GraphView.GraphView;

public class MovableWall : MonoBehaviour
{
    public bool customNumberOfPlayers = false;
    public LayerMask movableWallLayer;

    public int numberOfPlayers = 2;

    private Rigidbody2D rigid;
    [SerializeField]
    private int leftPower = 0;
    public int LeftPower
    {
        get { return leftPower; }
        set { leftPower = value; Debug.Log("LeftPower Change"); }
    }
    [SerializeField]
    private int rightPower = 0;
    public int RightPower
    {
        get { return rightPower; }
        set { rightPower = value; Debug.Log("RightPower Change"); }
    }

    private int dumpRightPower = 0;
    private int dumpLeftPower = 0;

    private TextMeshPro text;

    private float sideRaycastLength = 3.94f;
    public float sideRayDistance = 0.43f;
    private float sideRayHeight = 1.95f;
    private RaycastHit2D leftRayDetect;
    private RaycastHit2D rightRayDetect;

    private RaycastHit2D[] leftArrayRayDetect;
    private RaycastHit2D[] rightArrayRayDetect;
    private RaycastHit2D[] testRay;
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        text = GetComponentInChildren<TextMeshPro>();
        InitialSettings();
        text.text = numberOfPlayers.ToString();
        
    }
    private void Update()
    {
        BesidePlusPower();
        

    }
    private void FixedUpdate()
    {
        leftRay();
        RightRay();
        Powering();
        //MoveWall();
    }
    

    private void leftRay()
    {
        leftRayDetect = Physics2D.Raycast(transform.position + new Vector3(-sideRayDistance, sideRayHeight, 0), Vector2.down, sideRaycastLength, movableWallLayer);
        Debug.DrawRay(transform.position + new Vector3(-sideRayDistance, sideRayHeight, 0), Vector2.down * sideRaycastLength, Color.red);
        //0.43
        leftArrayRayDetect = Physics2D.RaycastAll(transform.position + new Vector3(-sideRayDistance, sideRayHeight, 0), Vector2.down, sideRaycastLength, movableWallLayer);

    }

    private void RightRay()
    {
        rightRayDetect = Physics2D.Raycast(transform.position + new Vector3(sideRayDistance, sideRayHeight, 0), Vector2.down, sideRaycastLength, movableWallLayer);
        Debug.DrawRay(transform.position + new Vector3(sideRayDistance, sideRayHeight, 0), Vector2.down * sideRaycastLength, Color.red);

        rightArrayRayDetect = Physics2D.RaycastAll(transform.position + new Vector3(sideRayDistance, sideRayHeight, 0), Vector2.down, sideRaycastLength, movableWallLayer);

    }
  
    private void Powering()
    {
        
       
        rightPower = 0;
        foreach(RaycastHit2D hit in leftArrayRayDetect)
        {
            if (hit && hit.collider.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                //Debug.Log(hit.collider.gameObject.name);
                //Debug.Log(leftArrayRayDetect.Length);
                rightPower += hit.collider.GetComponent<PlayerStatus>().curRightPower;
            }
            
        }
        leftPower = 0;
        foreach (RaycastHit2D hit in rightArrayRayDetect)
        {
            if (hit && hit.collider.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                //Debug.Log(hit.collider.gameObject.name);
                leftPower += hit.collider.GetComponent<PlayerStatus>().curLeftPower;
            }
        }
        //rightPower = rightPower / 2;
        //leftPower = leftPower / 2;
        MoveWall();
    }
    private void BesidePlusPower()
    {
        if (leftRayDetect && leftRayDetect.collider.gameObject.layer == LayerMask.NameToLayer("MovableWall"))
        {
            rightPower -= dumpRightPower;
            
            MovableWall besideMovableWall = leftRayDetect.collider.gameObject.GetComponent<MovableWall>();
            dumpRightPower = besideMovableWall.rightPower;
            rightPower += besideMovableWall.rightPower;
        }
  
        if (rightRayDetect && rightRayDetect.collider.gameObject.layer == LayerMask.NameToLayer("MovableWall"))
        {
            leftPower -= dumpLeftPower;

            MovableWall besideMovableWall = rightRayDetect.collider.gameObject.GetComponent<MovableWall>();
            dumpLeftPower = besideMovableWall.leftPower;
            leftPower += besideMovableWall.leftPower;
            
        }
    }
    private float move = 0;
    private int count = 0;
    private void MoveWall()
    {
        //move = 0;
        count = numberOfPlayers;
        count -= (leftPower + rightPower);
        if (this.name == "MovableWall2-1_2")
            Debug.Log(count);
        
        if (count <= 0)
        {
            if (leftPower > rightPower)
            {
                move = -0.4f;
                rigid.constraints &= ~RigidbodyConstraints2D.FreezePositionX;
                
            }
            else if (leftPower < rightPower)
            {
                move = 0.4f;
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
            
            //move = 0;
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

    private void InitialSettings()
    {
        GameObject[] Players;
        Players = GameObject.FindGameObjectsWithTag("Player");
        int i = 0;
        foreach (var Player in Players)
        {
            i++;
        }
        if (!customNumberOfPlayers)
        {
            if (this.gameObject.name == "MovableWall1-2_3")
                numberOfPlayers = i - 1;
            else
                numberOfPlayers = i;
        }
    }
}