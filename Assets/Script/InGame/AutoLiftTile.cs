using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;
using static UnityEngine.Rendering.DebugUI;

public class AutoLiftTile : MonoBehaviour
{
    public float moveDuration = 2f;
    public float moveSpeed = 0.4f;

    private RaycastHit2D belowRay;

    private float addRayXposition = -1.93f;
    private float addRayYposition = -0.32f;
    private float rayDistance = 3.85f;
    public LayerMask playerLayer;

    private Vector3 beginPosition;
    private Vector3 endPosition;

    private Vector3 velocity;
    private float smoothTime = 0.8f;

    void Start()
    {
        beginPosition = transform.position;
        endPosition = transform.position + new Vector3(0, 2f);
        StartCoroutine("MoveUp");
    }
    private void Update()
    {
        BelowRay();
    }

    private IEnumerator MoveUp() //일정 시간 동안 위로 이동
    {
        float time = 0;
        while (time < moveDuration)
        {
            if(belowRay != true)
            {
                transform.position = Vector3.SmoothDamp(transform.position, endPosition, ref velocity, smoothTime);
            }
               
            time += Time.deltaTime;
            yield return null;
        }
        StartCoroutine(MoveDown());
        
    }
    private IEnumerator MoveDown() //일정 시간 동안 beginPosition으로 아래로 이동
    {
        float time = 0;
        while (time < moveDuration)
        {
            if (belowRay != true)
            {
                transform.position = Vector3.SmoothDamp(transform.position, beginPosition, ref velocity, smoothTime);
            }
                
            time += Time.deltaTime;
            yield return null;
        }   
        StartCoroutine(MoveUp());
    }

    private void BelowRay()
    {
        Vector3 startPosition = transform.position + new Vector3(addRayXposition, addRayYposition, 0);
        Debug.DrawRay(startPosition, Vector2.right * rayDistance, Color.red);
        belowRay = Physics2D.Raycast(startPosition, Vector2.right, rayDistance, playerLayer);
        
    }
}