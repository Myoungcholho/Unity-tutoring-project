using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public GameObject blockingDoor;
    private Vector3 startPosition;
    private bool isActive = false;

    private void Start()
    {
        startPosition = transform.position;
    }
    private void FixedUpdate()
    {
        DeActivateDoor();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("MovableWall")
            || collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            transform.position = startPosition + new Vector3(0, -0.2f, 0);
            isActive = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("MovableWall")
            || collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            transform.position = startPosition;
        }
    }
    private void DeActivateDoor()
    {
        if(isActive)
        blockingDoor.transform.position = new Vector3(0, -1f, 0);
    }
    private void ActivateDoor()
    {

    }
}
