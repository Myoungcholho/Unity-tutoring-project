using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonRe2 : MonoBehaviour
{
    public GameObject blockingDoor;
    public GameObject floor;

    private GameObject mushRoomObject;
    private Vector3 WallendPosition;

    private Vector3 startPosition;

    private Vector3 wallStartPosition;

    private Vector3 floorTargetPos;
    void Start()
    {
         
        mushRoomObject = transform.GetChild(0).gameObject;
        startPosition = mushRoomObject.transform.position;

        wallStartPosition = blockingDoor.transform.position;
        WallendPosition = wallStartPosition + new Vector3(0, 2f, 0);
        floorTargetPos = floor.transform.position + new Vector3(-6f, 0, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("MovableWall")
            || collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            
            mushRoomObject.transform.position = startPosition + new Vector3(0, -0.2f, 0);
            StartCoroutine("MoveWall");
            StartCoroutine("MoveFloor");
        }
    }

    private IEnumerator MoveWall()
    {

        while (blockingDoor.transform.position.y <= WallendPosition.y)
        {
            blockingDoor.transform.position += new Vector3(0, 1.3f * Time.deltaTime, 0);
            blockingDoor.transform.localScale += new Vector3(0, 1.7f * Time.deltaTime, 0);

            yield return null;
        }
        StopCoroutine("MoveWall");
    }
    private IEnumerator MoveFloor()
    {


        while (floor.transform.position.x >= floorTargetPos.x)
        {
            floor.transform.position += new Vector3(-2f * Time.deltaTime, 0, 0);
            yield return null;
        }

    }
}
