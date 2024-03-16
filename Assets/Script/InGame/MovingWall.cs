using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MovingWall : MonoBehaviour
{
    public float distanceToMove = 0;

    private Vector3 beginPosition;
    private Vector3 endPosition;
    private GameObject wallObject;

    private float moveSpeed = 0.005f;

    private bool isMoving = false;
    void Start()
    {
        wallObject = transform.GetChild(0).gameObject;
        beginPosition = wallObject.transform.position;
        endPosition = beginPosition + new Vector3(distanceToMove, 0);
    }

    void Update()
    {
        
    }

    private IEnumerator MoveWallLeft()
    {
        //왼쪽으로 벽 이동 후 2초 기다린 다음 MoveWallRight() 함수 실행
        while(wallObject.transform.position.x != endPosition.x)
        {
            wallObject.transform.position = Vector3.MoveTowards(wallObject.transform.position, endPosition, moveSpeed);
            yield return null;
        }
        yield return new WaitForSeconds(2.0f);
        StartCoroutine(MoveWallRight());
    }

    private IEnumerator MoveWallRight()
    {
        ////오른쪽으로 벽 이동 후 2초 기다린 다음 MoveWallLeft() 함수 실행
        while (wallObject.transform.position.x != beginPosition.x)
        {
            wallObject.transform.position = Vector3.MoveTowards(wallObject.transform.position, beginPosition, moveSpeed);
            yield return null;
        }
        yield return new WaitForSeconds(2.0f);
        StartCoroutine(MoveWallLeft());
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //오브젝트가 감지되면 코드 실행
        if(!isMoving)
        {
            StartCoroutine(MoveWallLeft());
            isMoving = true;
        }
        
    }
}
