using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Button : MonoBehaviour
{
    public GameObject blockingDoor;
    private Vector3 startPosition;
    

    private Vector3 wallStartPosition;
    private Vector3 endPosition;

    public bool isPushed = false;
    private GameObject pushingObject = null;
    private void Start()
    {
        startPosition = transform.position;
        wallStartPosition = blockingDoor.transform.position;
        endPosition = wallStartPosition + new Vector3(0, 1f, 0);
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!isPushed)
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("MovableWall")
            || collision.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                transform.position = startPosition + new Vector3(0, -0.2f, 0);
                isPushed = true;
                pushingObject = collision.gameObject;
                StartCoroutine("WallUp");
                StopCoroutine("WallDown");
            }
        }
        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("MovableWall")
            || collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            if (pushingObject == collision.gameObject)
            {
                isPushed = false;
                Debug.Log("isPushed = false");
            }
            
                transform.position = startPosition;
                StartCoroutine("WallDown");
                StopCoroutine("WallUp");
            
            
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

    }
    private IEnumerator WallDown()
    {
        

        while (blockingDoor.transform.position.y >= wallStartPosition.y)
        {
            blockingDoor.transform.position += new Vector3(0, -0.5f * Time.deltaTime, 0);
            blockingDoor.transform.localScale += new Vector3(0, -1f * Time.deltaTime, 0);

            yield return null;
        }

    }
}
