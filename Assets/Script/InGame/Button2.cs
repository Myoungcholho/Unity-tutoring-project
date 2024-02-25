using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Button2 : MonoBehaviour
{
    public GameObject blockingDoor;
    public GameObject Floor;
    private Vector3 startPosition;
    

    private Vector3 wallStartPosition;
    private Vector3 wallEndPosition;

    private Vector3 floorEndPosition;
    private void Start()
    {
        startPosition = transform.position;
        wallStartPosition = blockingDoor.transform.position;
        wallEndPosition = wallStartPosition + new Vector3(0, 1f, 0);

        floorEndPosition = Floor.transform.position + new Vector3(-6f, 0, 0);
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("MovableWall")
            || collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            transform.position = startPosition + new Vector3(0, -0.2f, 0);
           
            StartCoroutine("WallUp");
            StartCoroutine("FloorActivate");
        }
    }
    
    private IEnumerator WallUp()
    {
        while (blockingDoor.transform.position.y <= wallEndPosition.y)
        {
            blockingDoor.transform.position += new Vector3(0, 0.5f * Time.deltaTime, 0);
            blockingDoor.transform.localScale += new Vector3(0, 1f * Time.deltaTime, 0);

            yield return null;
        }

    }

    private IEnumerator FloorActivate()
    {
        while (Floor.transform.position.x >= floorEndPosition.x)
        {
            Floor.transform.position += new Vector3(-3f * Time.deltaTime, 0, 0);
            
            yield return null;  
        }
    }

}
