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

    void FixedUpdate()
    {
        float X = (target1.position.x + target2.position.x) / 2;
        Vector3 targetPosition = new Vector3(X, 0, 0) + offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }
}
