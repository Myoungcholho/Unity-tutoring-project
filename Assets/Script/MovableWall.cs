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
        leftRay();
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
            else if(leftPower < rightPower)
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
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("MovableWall"))
        {
            Debug.Log("OnCOllisionStay");
            MovableWall otherMovableWall = collision.gameObject.GetComponent<MovableWall>();
            
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        MovableWall otherMovableWall = collision.gameObject.GetComponent<MovableWall>();
        
        if (collision.gameObject.layer == LayerMask.NameToLayer("MovableWall"))
        {
           
        }

    }
    private bool isDetect = false;
    public void LeftMovableWallPlus()
    {
        if(leftRayDetect && !isDetect)
        {
            isDetect = true;
        }
    }

    public void LeftLeftMovableWallPlus()
    {

    }

    private float sideRaycastDistance = 0.2f;

    private RaycastHit2D leftRayDetect;
    private void leftRay()
    {

        leftRayDetect = Physics2D.Raycast(transform.position + new Vector3(-0.55f, 0, 0), Vector2.down, sideRaycastDistance, movableWallLayer);
        Debug.DrawRay(transform.position + new Vector3(-0.55f, 0, 0), Vector2.down * 0.2f, Color.red);
        if(leftRayDetect)
        {
            Debug.Log("MovableWall LeftRayDetect");
        }
    }
}
