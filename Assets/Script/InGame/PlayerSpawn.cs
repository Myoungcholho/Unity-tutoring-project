using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    public GameObject playerPrefabObj;
    private Vector3[] spawnPositions;

    private void Awake()
    {
        Spawn(2);
    }
    
    private void Spawn(int countOfPlayers)
    {
        spawnPositions = new Vector3[countOfPlayers];
        for (int i = 0; i < spawnPositions.Length; i++)
        {
            spawnPositions[i] = transform.GetChild(i).position;
            if(playerPrefabObj != null)
            {
                Instantiate(playerPrefabObj, spawnPositions[i], Quaternion.identity);
            }
        }
    }
}
