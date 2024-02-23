using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingGroundPosition : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject[] Players;
        Players = GameObject.FindGameObjectsWithTag("Player");
        int i = 0;
        foreach (var Player in Players)
        {
            i++;
        }
        switch (i)
        {
            case 3:
                transform.position += new Vector3(0, 1, 0);
                break;
            case 4:
                transform.position += new Vector3(0, 2, 0);
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
