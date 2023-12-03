using System.Collections; //
using System.Collections.Generic; //
using System.Collections.Specialized;
using System.ComponentModel.Design;
using System.Security.Cryptography;
using UnityEngine; //

public class P1_Controller : MonoBehaviour
{
    private Rigidbody2D rigid;
    private SpriteRenderer spriteRenderer;

    public float maxspeed;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");

        Vector2 position = transform.position;
        position.x = position.x + maxspeed * horizontal;
        transform.position = position;

        /*
         if (Input.GetKey(KeyCode.RightArrow))
        {
            if(Input.GetKeyUp(KeyCode.LeftArrow))
            {
                horizontal = 0;
                position.x = position.x - maxspeed * horizontal;
                transform.position = position;
            }
            else
            {
                position.x = position.x - maxspeed * horizontal;
                transform.position = position;
            }
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (Input.GetKeyUp(KeyCode.RightArrow))
            {
                horizontal = 0;
                position.x = position.x - maxspeed * horizontal;
                transform.position = position;
            }
            else
            {
                position.x = position.x - maxspeed * horizontal;
                transform.position = position;
            }
        }
        */

        if (Input.GetButtonDown("Jump"))
        {
            position.y = position.y + 1; //서서히 올라가지 않고 한칸 위로 순간이동 함..
            transform.position = position;
        }

        if (horizontal == -1.0)
        {
            spriteRenderer.flipX = true;

        }
        else
        {
            spriteRenderer.flipX = false;
        }
    }
}
