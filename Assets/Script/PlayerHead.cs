using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class PlayerHead : MonoBehaviour
{
    [SerializeField]
    private GameObject[] objectToMove;

    public CholHo.PlayerInput input;
    public PlayerMovement playerMovement;

    private void Awake()
    {
        
    }

    private void Start()
    {
        objectToMove = new GameObject[2];
    }

    void Update()
    {
        for (int i = 0; i < 2; ++i)
        {
            if (objectToMove[i] != null)
            {
                ObjectToMove(i);
            }
        }
       
        
    }

    private void FixedUpdate()
    {
    }

    private void ObjectToMove(int idx)
    {
        objectToMove[idx].transform.position += new Vector3(input.horizontal, 0f, 0f) * playerMovement.speed * Time.deltaTime;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            for (int i = 0; i < 2; ++i)
            {
                if (objectToMove[i] == collision.gameObject)
                    return;
            }

            for (int i = 0; i < 2; ++i)
            {
                if (objectToMove[i] == null)
                {
                    objectToMove[i] = collision.gameObject;
                    return;
                }
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
       if(collision.collider.CompareTag("Player"))
        {
            for(int i=0; i<2; ++i)
            {
                if (objectToMove[i] == collision.gameObject)
                {
                    objectToMove[i] = null;
                }
            }
        }
    }
}
