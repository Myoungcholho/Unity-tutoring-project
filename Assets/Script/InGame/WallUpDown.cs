using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallUpDown : MonoBehaviour
{
    private Vector3 wallStartPosition;
    private Vector3 endPosition;

    void Start()
    {
        wallStartPosition = transform.position;
        endPosition = wallStartPosition + new Vector3(0, 1f, 0);
        StartCoroutine("WallUp");
    }

    public void StartWallUp() 
    {
        StartCoroutine("WallUp");
        StopCoroutine("WallDown");
    }

    public void StartWallDown()
    {
        StartCoroutine("WallDown");
        StopCoroutine("WallUp");
    }
    private IEnumerator WallUp()
    {

        while (transform.position.y <= endPosition.y)
        {
            transform.position += new Vector3(0, 0.5f * Time.deltaTime, 0);
            transform.localScale += new Vector3(0, 1f * Time.deltaTime, 0);

            yield return null;
        }
        StopCoroutine("WallUp");

    }
    private IEnumerator WallDown()
    {


        while (transform.position.y >= wallStartPosition.y)
        {
            transform.position += new Vector3(0, -0.5f * Time.deltaTime, 0);
            transform.localScale += new Vector3(0, -1f * Time.deltaTime, 0);

            yield return null;
        }
        StopCoroutine("WallDown");
    }
}
