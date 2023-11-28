using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
   

    private PlayerJump jump;
    private PlayerMove move;

    public delegate void PlayesrMove();
    public delegate void PlayersJump();
    void Start()
    {
        
        move = GetComponent<PlayerMove>();
        jump = GetComponent<PlayerJump>();
    }

    void Update()
    {
        jump?.JumpKey();
        move?.MoveKey();
    }

    void FixedUpdate()
    {
        
        
        move?.Move();
        jump?.Jump();

    }
    

    
}