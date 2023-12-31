using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MovingPlatform : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float moveDistance = 3f;

    private Vector3 Position;
    private bool isRight;
    BoxCollider2D other;
    void Start()
    {
        other = GetComponent<BoxCollider2D>();
        Position = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Position.x + moveDistance < transform.position.x)
        {
            isRight = false;
        }

        if (Position.x - moveDistance > transform.position.x)
        {
            isRight = true;
        }

        if (isRight)
            transform.position += Vector3.right * moveSpeed * Time.deltaTime;
        else
            transform.position += Vector3.left * moveSpeed * Time.deltaTime;

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
           
            collision.transform.parent = this.transform;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            
            collision.transform.parent = null;
        }
    }
}
