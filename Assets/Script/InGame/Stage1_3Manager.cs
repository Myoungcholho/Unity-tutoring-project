using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage1_3Manager : MonoBehaviour
{
    public GameObject[] walls;
 
    public int wallDownCount = 0; //몇 번째 벽을 컨트롤할지 정하는 변수

    private WallUpDown wallScript;

    void Start()
    {
        walls = GameObject.FindGameObjectsWithTag("BlockingWall");
        
    }

    public void ControlWallDown()
    {
        wallDownCount++; 

        wallScript = walls[wallDownCount - 1].GetComponent<WallUpDown>();
        wallScript.StartWallDown();
    }

    public void ControlWallUp()
    {  
        wallScript = walls[wallDownCount - 1].GetComponent<WallUpDown>();
        wallScript.StartWallUp();
 
        wallDownCount--;
    }

}
