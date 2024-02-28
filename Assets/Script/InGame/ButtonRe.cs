using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonRe : MonoBehaviour
{
    public GameObject blockingDoor;

    private GameObject mushRoomObject;
    private Vector3 endPosition;

    private Vector3 startPosition;

    private Vector3 wallStartPosition;

    private GameObject pressingObject;

    public bool isDown = false;

    public bool isPressed = false;
    [SerializeField]
    private GameObject[] walls;

    private GameObject camera;
    private Stage1_3Manager stageManager;
    void Start()
    {
         
        mushRoomObject = transform.GetChild(0).gameObject;
        startPosition = mushRoomObject.transform.position;

        wallStartPosition = blockingDoor.transform.position;
        endPosition = wallStartPosition + new Vector3(0, 1f, 0);
        StartCoroutine("WallUp");

        walls = GameObject.FindGameObjectsWithTag("BlockingWall");
        if(this.gameObject.name == "Button1_3_6")
            stageManager = GetComponent<Stage1_3Manager>(); 
        camera = GameObject.FindGameObjectWithTag("MainCamera");
        stageManager = camera.GetComponent<Stage1_3Manager>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("MovableWall")
            || collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            if(pressingObject != collision.gameObject)
            {
                if(!isPressed)
                {
                    Debug.Log("Enter" + collision.name);
                    pressingObject = collision.gameObject;
                    isDown = true;
                    mushRoomObject.transform.position = startPosition + new Vector3(0, -0.2f, 0);
                    /*StartCoroutine("WallDown");*/
                    StopCoroutine("WallUp");
                    isPressed = true;
                    //�Լ� ȣ��
                    stageManager.ControlWallDown();
                }
                
            }
            
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("MovableWall")
            || collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            if(collision.gameObject == pressingObject)
            {
                Debug.Log("Exit");
                mushRoomObject.transform.position = startPosition;
                isDown = false;
                /*StartCoroutine("WallUp");
                StopCoroutine("WallDown");*/
                pressingObject = null;
                isPressed = false;
                stageManager.ControlWallUp();
            }
            
        }
    }


    private IEnumerator WallUp()
    {

        while (blockingDoor.transform.position.y <= endPosition.y)
        {
            blockingDoor.transform.position += new Vector3(0, 0.5f * Time.deltaTime, 0);
            blockingDoor.transform.localScale += new Vector3(0, 1f * Time.deltaTime, 0);

            yield return null;
        }
        StopCoroutine("WallUp");

    }
    private IEnumerator WallDown()
    {


        while (blockingDoor.transform.position.y >= wallStartPosition.y)
        {
            blockingDoor.transform.position += new Vector3(0, -0.5f * Time.deltaTime, 0);
            blockingDoor.transform.localScale += new Vector3(0, -1f * Time.deltaTime, 0);

            yield return null;
        }
        StopCoroutine("WallDown");
    }

}