using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Spring : MonoBehaviour
{
    public float jumpPower = 100;
    private Rigidbody2D rd;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
        {
            return;
        }
        else if(collision.transform.position.y < this.transform.position.y)
        {
            return;
        }

        //Debug.Log("Detected");
        rd = collision.gameObject.GetComponent<Rigidbody2D>();
        rd.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
    }
}
