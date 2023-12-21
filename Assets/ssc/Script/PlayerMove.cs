using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public delegate void MoveFunction(int Direction);
    public event MoveFunction OnMove;

    public float moveSpeed = 1f;

    public LayerMask wallLayer;

    public Transform leftRaycastPosition; // ���� ����ĳ��Ʈ ���� ��ġ
    public Transform rightRaycastPosition; // ������ ����ĳ��Ʈ ���� ��ġ
    public float raycastDistance = 0.2f; // ����ĳ��Ʈ�� ����

    private Animator anim;
    private Rigidbody2D rigid;
    private SpriteRenderer spriteRenderer;

    private int MoveDirection = 0;

    void Start()
    {
        rigid = gameObject.GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        OnMove += MoveKey;
    }

    private void FixedUpdate()
    {
        Move();
    }

    public void MoveInvoke(int MoveDirection)
    {
        OnMove?.Invoke(MoveDirection);
    }

    public void MoveKey(int Direction)
    {
        MoveDirection = Direction;
    }

    public void Move()
    {
        float move = 0;
        move = moveSpeed * MoveDirection;
        if (MoveDirection == -1)
        {
            spriteRenderer.flipX = true;

            RaycastHit2D hitLeft = Physics2D.Raycast(leftRaycastPosition.position, Vector2.left, raycastDistance, wallLayer);
            /*if (hitLeft.collider != null)
                move = 0;*/
        }
        if (MoveDirection == 1)
        {
            spriteRenderer.flipX = false;
            RaycastHit2D hitRight = Physics2D.Raycast(rightRaycastPosition.position, Vector2.right, raycastDistance, wallLayer);
            /*if (hitRight.collider != null)
                move = 0;*/
        }
   
        rigid.velocity = new Vector2(move, rigid.velocity.y);
        anim.SetBool("isWalking", move != 0);
    }
}
