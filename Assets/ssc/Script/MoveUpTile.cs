using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveUpTile : MonoBehaviour
{
    public int playerNumber;
    public GameObject[] Players;
    public int total;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        Debug.Log(PlayerCount());
        if(PlayerCount() >= playerNumber )
        {
            Debug.Log("Move UP");
        }
    }
    private int PlayerCount()
    {
        total = 0;
        foreach (var player in Players)
        {
            PlayerJump playerjump = player.GetComponent<PlayerJump>();
            total += playerjump.PlayerCount;
        }
        return total;
    }
}
