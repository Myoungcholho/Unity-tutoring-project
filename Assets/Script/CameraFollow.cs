using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Vector3 offset = new Vector3(0f, 0f, -50f);
    private Vector3 velocity;

    [SerializeField]
    private float smoothTime = 0.15f;

    [SerializeField]
    private Transform target1;

    [SerializeField]
    private Transform target2;

    [SerializeField]
    private Transform[] targets;

    private GameObject[] Players;
    private void Awake()
    {
        Players = GameObject.FindGameObjectsWithTag("Player");
        int i = 0;
        foreach(var Player in Players)
        {
            targets[i] = Player.transform;
            i++;
        }
        target1 = targets[0];
        target2 = targets[1];
    }
    void FixedUpdate()
    {
        /*float X = (target1.position.x + target2.position.x) / 2;
        Vector3 targetPosition = new Vector3(X, 0, 0) + offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);*/
        for (int i = 0; i < targets.Length; i++)
        {
            if (targets[i].position.x < target2.position.x)
            {
                target2 = targets[i];
            }
            else if (targets[i].position.x > target1.position.x)
            {
                target1 = targets[i];
            }
        }
        float X = (target1.position.x + target2.position.x) / 2;
        Vector3 targetPosition = new Vector3(X, 0, 0) + offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }
}
