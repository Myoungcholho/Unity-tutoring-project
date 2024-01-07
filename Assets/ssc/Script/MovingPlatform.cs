using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MovingPlatform : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float moveDistance = 3f;

    public enum MoveDirection
    {
        
        diagonal,
        vertical,
        horizon
    }
    public MoveDirection movedirection;

    private Vector3 Position;
    private bool isDirection;

    void Start()
    {
        Position = this.transform.position;
    }

    private void FixedUpdate()
    {
        switch(movedirection)
        {
            
            case MoveDirection.diagonal:
                MoveDiagonal();
                break;

            case MoveDirection.vertical:
                MoveVertical();
                break;
            case MoveDirection.horizon:
                MoveHorizion();
                break;

        }

    }

    private void MoveDiagonal()
    {
        if (Position.x + moveDistance < transform.position.x)
            isDirection = false;
        if (Position.x - moveDistance > transform.position.x)
            isDirection = true;

        if (isDirection)
        {
            transform.position += Vector3.right * moveSpeed * Time.deltaTime;
            transform.position += Vector3.up * moveSpeed * Time.deltaTime;
        }
        else
        {
            transform.position += Vector3.left * moveSpeed * Time.deltaTime;
            transform.position += Vector3.down * moveSpeed * Time.deltaTime;
        }
    }
    private void MoveVertical()
    {
        if (Position.y + moveDistance < transform.position.y)
            isDirection = false;
        if (Position.y - moveDistance > transform.position.y)
            isDirection = true;
        if (isDirection)
            transform.position += Vector3.up * moveSpeed * Time.deltaTime;
        else
            transform.position += Vector3.down * moveSpeed * Time.deltaTime;
    }
    private void MoveHorizion()
    {
        if (Position.x + moveDistance < transform.position.x)
            isDirection = false;
        if (Position.x - moveDistance > transform.position.x)
            isDirection = true;

        if (isDirection)
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
