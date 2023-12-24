using System.Drawing;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public float jumpForce;

    [SerializeField]
    private bool isGround;              //private

    public int jumpCount;
    private PlayerInput playerInput;
    private Rigidbody2D rigid;
    private Animator playerAnimator;
    private SpriteRenderer playerRenderer;

    private Vector2 size;
    public LayerMask layerMask;
    public float angle;

    public Transform legTransform;
    public BoxCollider2D legCollider;

    public float circleRadius = 0.1f;

    public float lowGravity = 2f;
    public float highGravity = 5f;

    public bool IsMine { get; private set; }
    public bool IsLongJump { get; set; } = false;
    public bool IsGoingDown { get; set; } = false;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        rigid = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        playerRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        playerInput.onJumpKeyDown += JumpKeyDown;
        playerInput.onJumpKey += JumpKey;
        playerInput.onJumpKeyUp += JumpKeyUp;

        size = new Vector2(legCollider.size.x, legCollider.size.y);

        IsMine = true;
    }

    private void Update()
    {
        
    }

    private void FixedUpdate()
    {
        // velocity.y <= 0 으로는 처리가 안됨.
        // Collider가 작은 만큼 미리 rigidbody의 미리 처리 기능을 켜놓았는데
        // 땅에 닿지 않아도 본인의 상체 Collider를 인식해 땅이라고 판단하는 이상한 현상이 일어남.
        //Collider2D isGround = Physics2D.OverlapBox(legTransform.position, size, angle,layerMask);
        if (rigid.velocity.y <= -0.01f)
            IsGoingDown = true;

        if (IsGoingDown)
        {
            isGround = (bool)Physics2D.OverlapCircle(legTransform.position, circleRadius, layerMask);
            if (isGround)
            {
                Debug.Log("땅을 밟고 있음!");
                IsGoingDown = false;
                jumpCount = 0;
                playerAnimator.SetBool("isGround", true);
            }
        }

        Move(playerInput.horizontal);
    }

    // Gizmos 전용 메서드 함수
    void OnDrawGizmos()
    {
        Gizmos.color = UnityEngine.Color.red;
        Gizmos.DrawSphere(legTransform.position, circleRadius);
    }

    private void Move(float horizontal)
    {
        rigid.velocity = new Vector2(horizontal * speed, rigid.velocity.y);
        FlipX();
        WalkAnimPlay();
    }
    private void JumpKeyDown()
    {
        if(jumpCount == 0)
        {
            jumpCount++;
            rigid.velocity = Vector2.zero;
            rigid.AddForce(new Vector2(0, jumpForce));
            playerAnimator.SetBool("isGround", false);
        }
    }

    // 다른 플레이어에게 영향을 주는 메서드
    public void ApplyInfluence(Vector3 influence)
    {
        transform.position += influence;
    }

    // 중력
    private void JumpKey()
    {
        rigid.gravityScale = lowGravity;
    }
    // 중력
    private void JumpKeyUp()
    {
        rigid.gravityScale = highGravity;
    }

    // 플레이어 애니메이션 Walk
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
    // 플레이어 Sprite Flip
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
}
