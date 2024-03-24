using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using System.Linq;
public class Stage1_3Manager : MonoBehaviour
{
    public GameObject[] walls;
    public GameObject additionalBoxObject;
    public GameObject[] floatingObjects;
    public GameObject buttonObject;
    public GameObject wallObject;
    private int wallDownCount = 0; //몇 번째 벽을 컨트롤할지 정하는 변수

    private WallUpDown wallScript;
    private ButtonRe buttonRe;
    private GameObject[] Players;
    private int NumberOfPlayers = 0;

    private void Awake()
    {
        walls = GameObject.FindGameObjectsWithTag("BlockingWall");
        walls = walls.OrderByDescending(wall => wall.transform.position.x).ToArray();
        CountNumberOfPlayers();
        FloatinglandHeightAdjustment();
        NumberOfBoxsInitialSettings();
        NumberOfButtonsInitialSettings();

        ButtonRe[] buttonReArray = FindObjectsOfType<ButtonRe>();

        foreach (var buttonRe in buttonReArray)
        {
            buttonRe.buttonPressed += ControlWallDown;
            buttonRe.buttonReleased += ControlWallUp;
        }
    }
   
    public void ControlWallDown() 
    {
        wallDownCount++; // 버튼이 한 개씩 눌릴 때마다 카운트를 1씩 더함

        //wallDownCount 값에 따라 순서대로 WallUpDown스크립트 컴포넌트에 접근
        wallScript = walls[wallDownCount - 1].GetComponent<WallUpDown>();
        wallScript.StartWallDown();
    }

    public void ControlWallUp()
    {  
        wallScript = walls[wallDownCount - 1].GetComponent<WallUpDown>();
        wallScript.StartWallUp();
 
        wallDownCount--;
    }

    private void CountNumberOfPlayers()
    {
        Players = GameObject.FindGameObjectsWithTag("Player");
        
        foreach (var Player in Players)
        {
            NumberOfPlayers++;
        }
    }

    private void NumberOfBoxsInitialSettings()
    {
        if(NumberOfPlayers == 4)
        {
            additionalBoxObject.SetActive(false);
        }
            
    }
    private void FloatinglandHeightAdjustment()
    {
        if(NumberOfPlayers > 2)
        {
            foreach(GameObject floatingObject in floatingObjects)
            {
                floatingObject.transform.position += new Vector3(0, 1f, 0);
            }
        }
    }

    private void NumberOfButtonsInitialSettings()
    {
        if(NumberOfPlayers == 2)
        {
            buttonObject.SetActive(false);
            wallObject.SetActive(false);
        }
    }
}
