using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlatformMovement : MonoBehaviour
{
    public float speed;

    public Transform startPos;
    public Transform endPos;
    private Transform destPos;
    private Rigidbody2D rigid;

    void Start()
    {
        //rigid = GetComponent<Rigidbody2D>();

        transform.position = startPos.position;
        destPos = endPos;
    }

    private void FixedUpdate()
    {
        transform.position = Vector2.MoveTowards(transform.position, destPos.position, Time.deltaTime * speed);
        float distance = Vector2.Distance(transform.position, destPos.position);
        //Debug.Log("transform.position : " + transform.position + "/ destPos.position : " + destPos.position + "/ distance : " + distance);
        if (distance <= 0.05f)
        {
            if(destPos == endPos)
            {
                destPos = startPos;
            }
            else
            {
                destPos = endPos;
            }
        }


    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.transform.CompareTag("Player"))
        {
            collision.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.collider.transform.CompareTag("Player"))
        {
            collision.transform.SetParent(null);
        }
    }
}
