using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FollowKey : MonoBehaviour
{
    private GameObject TargetPlayer;
    private GameObject LastPlayer;
    private Vector3 velocity;
    private Vector3 TargetPosition;
    
    private float smoothTime = 0.15f;

    private PlayerMove playermove;
    private PlayerStatus playerStatus;
    private Vector3 AddKeyPosition;
    void Start()
    {
        TargetPlayer = null;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(TargetPlayer != null)
        {
            if(playermove.playerDirection)
            {
                TargetPosition = TargetPlayer.transform.position + Vector3.right * 0.2f;
            }
            else
            {
                TargetPosition = TargetPlayer.transform.position + Vector3.left * 0.2f;
            }
            transform.position = Vector3.SmoothDamp(transform.position, TargetPosition, ref velocity, smoothTime);
            
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                if (playerStatus != null)
                    playerStatus.hasKey = false;

                playermove = collision.gameObject.GetComponent<PlayerMove>();
                TargetPlayer = collision.gameObject;
                LastPlayer = collision.gameObject;

                playerStatus = LastPlayer.GetComponent<PlayerStatus>();
                playerStatus.hasKey = true;
            }
        }
    }
}
