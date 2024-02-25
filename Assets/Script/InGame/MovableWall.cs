using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;
using static UnityEditor.Experimental.GraphView.GraphView;

public class MovableWall : MonoBehaviour
{
    public bool customNumberOfPlayers = false;
    public LayerMask Layer;

    public int numberOfPlayers = 2;

    private Rigidbody2D rigid;
    [SerializeField]
    private int leftPower = 0;
    
    [SerializeField]
    private int rightPower = 0;
    
    private TextMeshPro text;

    public float sideRaycastLength = 3.94f;
    public float sideRayDistance = 0.43f; //0.43f
    public float sideRayHeight = 1.95f;

    private RaycastHit2D leftRayDetect;
    private RaycastHit2D rightRayDetect;

    private RaycastHit2D[] leftArrayRayDetect;
    private RaycastHit2D[] rightArrayRayDetect;

    private float move = 0;
    private int count = 0;

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        text = GetComponentInChildren<TextMeshPro>();
        InitialSettings();
        text.text = numberOfPlayers.ToString();
    }
    private void Update()
    {
        leftRay();
        RightRay();
        Powering();

    }
    private void FixedUpdate()
    {
        MoveWall();
    }
    

    private void leftRay()
    {
        leftRayDetect = Physics2D.Raycast(transform.position + new Vector3(-sideRayDistance, sideRayHeight, 0), Vector2.down, sideRaycastLength, Layer);
        Debug.DrawRay(transform.position + new Vector3(-sideRayDistance, sideRayHeight, 0), Vector2.down * sideRaycastLength, Color.red);
        /*leftArrayRayDetect*/ = Physics2D.RaycastAll(transform.position + new Vector3(-sideRayDistance, sideRayHeight, 0), Vector2.down, sideRaycastLength, Layer);
        
    }

    private void RightRay()
    {
        rightRayDetect = Physics2D.Raycast(transform.position + new Vector3(sideRayDistance, sideRayHeight, 0), Vector2.down, sideRaycastLength, Layer);
        Debug.DrawRay(transform.position + new Vector3(sideRayDistance, sideRayHeight, 0), Vector2.down * sideRaycastLength, Color.red);

        rightArrayRayDetect = Physics2D.RaycastAll(transform.position + new Vector3(sideRayDistance, sideRayHeight, 0), Vector2.down, sideRaycastLength, Layer);

    }
  
    private void Powering()
    {
        rightPower = 0;
        if(leftArrayRayDetect != null)
        {
            foreach (RaycastHit2D hit in leftArrayRayDetect)
            {
                if (hit && hit.collider.gameObject.layer == LayerMask.NameToLayer("Player"))
                {
                    rightPower += hit.collider.GetComponent<PlayerStatus>().curRightPower;
                }
            }
        }
        if (leftRayDetect && leftRayDetect.collider.gameObject.layer == LayerMask.NameToLayer("MovableWall"))
        {
            MovableWall besideMovableWall = leftRayDetect.collider.gameObject.GetComponent<MovableWall>();
            rightPower += besideMovableWall.rightPower;
            
        }

        leftPower = 0;
        if(rightArrayRayDetect != null)
        {
            foreach (RaycastHit2D hit in rightArrayRayDetect)
            {
                if (hit && hit.collider.gameObject.layer == LayerMask.NameToLayer("Player"))
                {
                    leftPower += hit.collider.GetComponent<PlayerStatus>().curLeftPower;
                }
            }
        }
        if (rightRayDetect && rightRayDetect.collider.gameObject.layer == LayerMask.NameToLayer("MovableWall"))
        {
            MovableWall besideMovableWall = rightRayDetect.collider.gameObject.GetComponent<MovableWall>();
            leftPower += besideMovableWall.leftPower;
        }
    }
    
    private void MoveWall()
    {
        
        count = numberOfPlayers;
        count -= (leftPower + rightPower);
        
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