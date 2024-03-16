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
    private int wallDownCount = 0; //�� ��° ���� ��Ʈ������ ���ϴ� ����

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
        wallDownCount++; // ��ư�� �� ���� ���� ������ ī��Ʈ�� 1�� ����

        //wallDownCount ���� ���� ������� WallUpDown��ũ��Ʈ ������Ʈ�� ����
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
