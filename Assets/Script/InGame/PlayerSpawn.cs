using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    
    private GameObject player3;
    private GameObject player4;
    private void Awake()
    {
        
        player3 = GameObject.Find("player3");
        player4 = GameObject.Find("player4");
    }

    private void Spawn(int countOfPlayers)
    {
        if(countOfPlayers == 2)
        {
            player3?.SetActive(false);
            player4?.SetActive(false);
        }
        else if(countOfPlayers == 3)
        {
            player4?.SetActive(false);
        }

    }
}
