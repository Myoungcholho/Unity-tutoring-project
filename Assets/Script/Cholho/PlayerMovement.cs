using System.Drawing;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public float jumpForce;

    [SerializeField]
    private bool isGround;              //private

    private int jumpCount;
    private PlayerInput playerInput;
    private Rigidbody2D playerRigidbody;
    private Animator playerAnimator;
    private SpriteRenderer playerRenderer;

    private Vector2 size;
    public LayerMask layerMask;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        playerRigidbody = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        playerRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        playerInput.onJump += Jump;
        size = new Vector2(GetComponent<BoxCollider2D>().size.x, GetComponent<BoxCollider2D>().size.y);
    }

    private void FixedUpdate()
    {
        
        Collider2D[] hits = Physics2D.OverlapBoxAll(transform.position, size, 0, layerMask);
        if (hits.Length >1)
        {
            Debug.Log("hit name : " + hits[1].name);
            isGround = true;
            jumpCount = 0;
            JumpAnimPlay();
        }
        else
        {
            isGround = false;
            JumpAnimPlay();
        }

        Move();
    }

    // Gizmos 전용 메서드 함수
    void OnDrawGizmos()
    {
        Gizmos.color = UnityEngine.Color.red;
        Gizmos.DrawWireCube(transform.position, size);
    }

    private void Move()
    {
        playerRigidbody.velocity = new Vector2(playerInput.horizontal * speed, playerRigidbody.velocity.y);
        FlipX();
        WalkAnimPlay();
    }
    private void Jump()
    {
        if(jumpCount < 1)
        {
            jumpCount++;
            playerRigidbody.velocity = Vector2.zero;
            playerRigidbody.AddForce(new Vector2(0, jumpForce));
            JumpAnimPlay(); 
        }
    }
    private void WalkAnimPlay()
    {
        if (playerInput.horizontal != 0 )
        {
            playerAnimator.SetBool("isWalking", true);
        }
        else
        {
            playerAnimator.SetBool("isWalking", false);
        }
    }

    private void JumpAnimPlay()
    {
        if (playerAnimator.GetBool("isGround") != isGround)
        {
            playerAnimator.SetBool("isGround", isGround);
        }
    }

    private void FlipX()
    {
        if (playerInput.horizontal == -1)
        {
            playerRenderer.flipX = true;
        }
        else if (playerInput.horizontal == 1)
        {
            playerRenderer.flipX = false;
        }
    }


    private void OnDestroy()
    {
        playerInput.delegateJump -= Jump;
    }
}
