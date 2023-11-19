using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public float jumpForce;

    private bool isGround;
    private int jumpCount;
    private PlayerInput playerInput;
    private Rigidbody2D playerRigidbody;
    private Animator playerAnimator;

    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        playerRigidbody = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();

        playerInput.delegateJump += Jump;
    }
    private void FixedUpdate()
    {
        Move();
        PlayAnim();
    }

    private void Move()
    {
        playerRigidbody.velocity = new Vector2(playerInput.horizontal * speed, playerRigidbody.velocity.y);
    }
    private void Jump()
    {
        if(jumpCount < 1)
        {
            jumpCount++;
            playerRigidbody.velocity = Vector2.zero;
            playerRigidbody.AddForce(new Vector2(0, jumpForce));
        }
    }
    private void PlayAnim()
    {
        if (playerAnimator.GetInteger("hRaw") != playerInput.horizontal)
        {
            playerAnimator.SetInteger("hRaw", (int)playerInput.horizontal);
        }
        else if (playerAnimator.GetBool("isGround") != isGround)
        {
            playerAnimator.SetBool("isGround", isGround);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Ground"))
        {
            if (collision.contacts[0].normal.y > 0.7f)
            {
                isGround = true;
                jumpCount = 0;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Ground"))
        {
            isGround = false;
        }
    }

    private void OnDestroy()
    {
        playerInput.delegateJump -= Jump;
    }
}
