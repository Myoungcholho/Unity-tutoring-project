using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableWall : MonoBehaviour
{
    public int playerNumber = 2;
    
    private Rigidbody2D rigid;

    public int leftPower = 0;
    public int LeftPower
    {
        get { return leftPower; }
        set { value = leftPower; }
    }
    public int rightPower = 0;
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log( "PlayerNumber = " + playerNumber + " Power = " + power);
    }

    private void FixedUpdate()
    {
        float move = 0;
        int count = playerNumber;
        count -= (leftPower + rightPower);
        if(count <= 0)
        {
            if (leftPower > rightPower)
                move = -0.7f;
            else if(leftPower < rightPower)
                move = 0.7f;
            else
                move = 0;
            
        }
        rigid.velocity = new Vector2(move, rigid.velocity.y);
        
    }
}
